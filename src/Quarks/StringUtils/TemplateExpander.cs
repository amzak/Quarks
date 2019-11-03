using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace Codestellation.Quarks.StringUtils
{
    public class TemplateExpander
    {
        private delegate string Renderer(object arg);

        public readonly string Body;
        private Dictionary<Type, Renderer> _renderers;
        private readonly Tuple<bool, string>[] _bodyTemplate;
        private readonly bool _noProperties;

        public TemplateExpander(string body)
        {
            Body = body;

            _renderers = new Dictionary<Type, Renderer>();

            string buffer = string.Empty;
            var property = false;

            var bodyTemplate = new List<Tuple<bool, string>>();


            for (var charIndex = 0; charIndex < body.Length; charIndex++)
            {
                char symbol = body[charIndex];

                if (symbol == '{')
                {
                    if (buffer != string.Empty)
                    {
                        bodyTemplate.Add(Tuple.Create(property, buffer));
                    }

                    buffer = string.Empty;
                    property = true;
                    continue;
                }

                if (symbol == '}')
                {
                    if (buffer != string.Empty)
                    {
                        bodyTemplate.Add(Tuple.Create(property, buffer));
                    }

                    buffer = string.Empty;
                    property = false;
                    continue;
                }

                buffer += symbol;

                if (charIndex == body.Length - 1)
                {
                    bodyTemplate.Add(Tuple.Create(property, buffer));
                }
            }

            _bodyTemplate = bodyTemplate.ToArray();
            _noProperties = !bodyTemplate.Any(x => x.Item1);
        }

        public string Render(object model)
        {
            if (_noProperties)
            {
                return Body;
            }

            if (!_renderers.TryGetValue(model.GetType(), out Renderer renderer))
            {
                renderer = BuildAndCacheRenderer(model.GetType());
            }

            return renderer(model);
        }

        private Renderer BuildAndCacheRenderer(Type getType)
        {
            Dictionary<Type, Renderer> afterCas;
            Dictionary<Type, Renderer> beforeCas;
            Renderer value = null;
            do
            {
                beforeCas = _renderers;
                Thread.MemoryBarrier();

                if (beforeCas.TryGetValue(getType, out Renderer result))
                {
                    return result;
                }

                if (value == null)
                {
                    value = BuildRenderer(getType);
                }

                var newDictionary = new Dictionary<Type, Renderer>(beforeCas, beforeCas.Comparer) { { getType, value } };

                afterCas = Interlocked.CompareExchange(ref _renderers, newDictionary, beforeCas);
            } while (beforeCas != afterCas);

            return value;
        }

        private Renderer BuildRenderer(Type type)
        {
            var arguments = new Expression[_bodyTemplate.Length];

            ParameterExpression parameter = Expression.Parameter(typeof(object), "input");
            UnaryExpression castedParameter = Expression.Convert(parameter, type);

            for (var i = 0; i < _bodyTemplate.Length; i++)
            {
                Tuple<bool, string> template = _bodyTemplate[i];
                bool isPropertyOrField = template.Item1;

                if (isPropertyOrField)
                {
                    string propertyName = template.Item2;
                    MemberExpression property = Expression.PropertyOrField(castedParameter, propertyName);
                    MethodCallExpression toStringMethod = Expression.Call(property, "ToString", null, null);
                    arguments[i] = toStringMethod;
                }
                else
                {
                    string constantString = template.Item2;
                    arguments[i] = Expression.Constant(constantString, typeof(string));
                }
            }

            //No concat, just make string of property.
            if (arguments.Length == 1)
            {
                return Expression.Lambda<Renderer>(arguments[0], parameter).Compile();
            }

            MethodInfo concatInfo = SelectConcatMethod(arguments.Length);

            MethodCallExpression concat = Expression.Call(concatInfo, arguments);
            Expression<Renderer> lambda = Expression.Lambda<Renderer>(concat, parameter);
            return lambda.Compile();
        }

        private MethodInfo SelectConcatMethod(int parameterCount)
        {
            if (parameterCount <= 0)
            {
                throw new InvalidOperationException();
            }

            IEnumerable<MethodInfo> concatMethods = typeof(string).GetMethods().Where(x => x.Name == "Concat");

            if (parameterCount <= 4)
            {
                return concatMethods
                    .Where(x => x.GetParameters().All(y => y.ParameterType == typeof(string)))
                    .Single(x => x.GetParameters().Length == parameterCount);
            }

            return concatMethods.Single(x => x.GetParameters().Length == 1 && x.GetParameters()[0].ParameterType == typeof(IEnumerable<string>));
        }


        public override string ToString() => Body;
    }
}