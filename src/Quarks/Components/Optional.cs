using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Codestellation.Quarks.Components
{
    public struct Optional<TValue>
    {
        private TValue _option;

        public bool HasValue { get; private set; }

        public TValue Value
        {
            get
            {
                if (HasValue == false)
                {
                    throw new InvalidOperationException("Value is empty.");
                }

                return _option;
            }
            set
            {
                HasValue = true;
                _option = value;
            }
        }

        public static explicit operator Optional<TValue>(TValue value) => new Optional<TValue> { Value = value };
    }

    public static class Optional
    {
        private static readonly ConcurrentDictionary<Tuple<Type, string>, Func<object, bool>> ReadersCache =
            new ConcurrentDictionary<Tuple<Type, string>, Func<object, bool>>();

        private static readonly ConcurrentDictionary<Type, Tuple<Func<object, object>, Func<object, object>>> ValueReadersCache =
            new ConcurrentDictionary<Type, Tuple<Func<object, object>, Func<object, object>>>();

        public static Optional<TValue> ToOptional<TValue>(this TValue self) => new Optional<TValue> { Value = self };

        public static bool ReadHasValue(object value, string propertyName)
        {
            Tuple<Type, string> key = Tuple.Create(value.GetType(), propertyName);
            Func<object, bool> reader = ReadersCache.GetOrAdd(key, BuildReader);
            return reader(value);
        }

        public static object GetValue(object value)
        {
            Tuple<Func<object, object>, Func<object, object>> reader = ValueReadersCache.GetOrAdd(value.GetType(), BuildReaderWriter);
            return reader.Item1(value);
        }

        public static object From(Type closedOptionalType, object value)
        {
            Func<object, object> writer = ValueReadersCache.GetOrAdd(closedOptionalType, BuildReaderWriter).Item2;
            return writer(value);
        }

        private static Tuple<Func<object, object>, Func<object, object>> BuildReaderWriter(Type arg)
        {
            Func<object, object> reader = BuildValueReader(arg);
            Func<object, object> writer = BuildValueWriter(arg);
            return Tuple.Create(reader, writer);
        }

        private static Func<object, object> BuildValueWriter(Type closedOptionalType)
        {
            Type valueType = closedOptionalType.GetGenericArguments()[0];
            ParameterExpression valueParameter = Expression.Parameter(typeof(object));
            UnaryExpression castedValue = Expression.Convert(valueParameter, valueType);

            MethodInfo method = closedOptionalType.GetMethod("op_Explicit", new[] { valueType });

            MethodCallExpression optional = Expression.Call(method, castedValue);
            UnaryExpression optionalAsObject = Expression.Convert(optional, typeof(object));

            return Expression.Lambda<Func<object, object>>(optionalAsObject, valueParameter).Compile();
        }

        private static Func<object, object> BuildValueReader(Type arg)
        {
            ParameterExpression valueParameter = Expression.Parameter(typeof(object));
            UnaryExpression castedValue = Expression.Convert(valueParameter, arg);
            MemberExpression valueProperty = Expression.PropertyOrField(castedValue, "Value");
            UnaryExpression valueAsObject = Expression.Convert(valueProperty, typeof(object));

            Func<object, object> buildValueReader = Expression.Lambda<Func<object, object>>(valueAsObject, valueParameter).Compile();
            return buildValueReader;
        }

        private static Func<object, bool> BuildReader(Tuple<Type, string> arg)
        {
            ParameterExpression valueParameter = Expression.Parameter(typeof(object));
            UnaryExpression castedValue = Expression.Convert(valueParameter, arg.Item1);

            MemberExpression optionalPropertyValue = Expression.PropertyOrField(castedValue, arg.Item2);

            MemberExpression hasValue = Expression.PropertyOrField(optionalPropertyValue, "HasValue");

            return Expression.Lambda<Func<object, bool>>(hasValue, valueParameter).Compile();
        }
    }
}