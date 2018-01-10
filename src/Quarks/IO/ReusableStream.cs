using System;
using System.IO;

namespace Codestellation.Quarks.IO
{
    internal sealed class ReusableStream : Stream
    {
        private byte[] _internalBuffer;
        private long _position;
        private long _length;

        public ReusableStream(byte[] buffer, long? length = null)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            ChangeBuffer(buffer, length);
        }

        public void ChangeBuffer(byte[] buffer, long? length = null)
        {
            if ((_length = length ?? buffer.Length) < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Must be non-negative number or null");
            }

            _internalBuffer = buffer;
            _position = 0;
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => true;

        public override long Length => _length;

        public override long Position
        {
            get => _position;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Position must be non-negative value");
                }

                if (value > Length)
                {
                    var message = $"Value must be lower than length {_length}";
                    throw new ArgumentOutOfRangeException(nameof(value), message);
                }

                _position = value;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    SeekInternal(offset);
                    break;
                case SeekOrigin.Current:
                    SeekInternal(_position + offset);
                    break;

                case SeekOrigin.End:
                    SeekInternal(_length + offset);
                    break;
                default:
                    throw new ArgumentException("Invalid SeekOrigin", nameof(origin));
            }

            return _position;
        }

        private void SeekInternal(long position)
        {
            if (position < 0)
            {
                throw new IOException("Could not seek before origin");
            }

            if (position >= _length)
            {
                throw new IOException("Could not seek beyond stream length");
            }

            _position = position;
        }

        public override int ReadByte()
        {
            if (_position >= _length)
            {
                return -1;
            }

            return _internalBuffer[_position++];
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ValidateParameters(buffer, offset, count);

            var maxBytesToRead = (int)(_length - _position);

            var bytesToRead = maxBytesToRead < count ? maxBytesToRead : count;

            if (bytesToRead <= 0)
            {
                return 0;
            }

            Array.Copy(_internalBuffer, _position, buffer, offset, bytesToRead);

            _position += bytesToRead;

            return bytesToRead;
        }

        public override void WriteByte(byte value)
        {
            if (_position >= _length)
            {
                throw new IOException("Could not write beyond buffer length");
            }

            _internalBuffer[_position++] = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ValidateParameters(buffer, offset, count);

            int bytesToWrite = count;

            //TODO: Consider faster unsafe copying
            Array.Copy(buffer, offset, _internalBuffer, _position, count);

            _position += bytesToWrite;
        }

        private static void ValidateParameters(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer), "Buffer cannot be null");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset should be non negative number");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Offset should be non negative number");
            }

            //if (buffer.Length - offset < count)
            //{
            //    throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
            //}
        }

        public override void Close()
        {
            //Do nothing, really. 
        }

        public override void Flush()
        {
            //Do nothing really
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
    }
}