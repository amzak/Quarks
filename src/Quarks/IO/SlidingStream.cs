using System;
using System.IO;

namespace Codestellation.Quarks.IO
{
    public class SlidingStream : Stream
    {
        private readonly Func<byte[]> _claim;
        private readonly Action<byte[]> _free;
        private long _position;
        private byte[] _currentBuffer;
        private int _currentPosition;

        public SlidingStream(Func<byte[]> claim, Action<byte[]> free)
        {
            _claim = claim;
            _free = free;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => Position;

        public override long Position
        {
            get => _position + _currentPosition;
            set => throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public override int ReadByte()
        {
            EnsureBuffer();

            var readByte = _currentBuffer[_currentPosition++];
            TryFreeBuffer();
            return readByte;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ValidateParameters(buffer, offset, count);
            EnsureBuffer();

            var maxBytesToRead = _currentBuffer.Length - _currentPosition;

            var bytesToRead = maxBytesToRead < count ? maxBytesToRead : count;

            if (bytesToRead <= 0)
            {
                return 0;
            }

            Array.Copy(_currentBuffer, _currentPosition, buffer, offset, bytesToRead);

            _currentPosition += bytesToRead;
            TryFreeBuffer();
            return bytesToRead;
        }

        public override void WriteByte(byte value)
        {
            EnsureBuffer();
            if (_currentPosition >= _currentBuffer.Length)
            {
                throw new IOException("Could not write beyond buffer length");
            }

            _currentBuffer[_currentPosition++] = value;
            TryFreeBuffer();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ValidateParameters(buffer, offset, count);

            var bytesWritten = 0;
            do
            {
                EnsureBuffer();
                var freeBytes = _currentBuffer.Length - _currentPosition;
                var bytesToWrite = Math.Min(count, freeBytes);

                var sourceIndex = offset + bytesWritten;
                Array.Copy(buffer, sourceIndex, _currentBuffer, _currentPosition, freeBytes);
                _currentPosition += bytesToWrite;
                bytesWritten += bytesToWrite;
            } while (bytesWritten < count);

            TryFreeBuffer();
        }

        private void EnsureBuffer()
        {
            if (_currentBuffer == null || _currentPosition >= _currentBuffer.Length)
            {
                AllocateBuffer();
            }
        }

        private void AllocateBuffer()
        {
            _position += _currentPosition;
            _currentPosition = 0;
            if (_currentBuffer != null)
            {
                _free(_currentBuffer);
            }

            if ((_currentBuffer = _claim()) == null)
            {
                throw new IOException("Could not allocate buffer to read or write");
            }
        }

        private void TryFreeBuffer()
        {
            if (_currentBuffer != null && _currentPosition >= _currentBuffer.Length)
            {
                _free(_currentBuffer);
                _currentBuffer = null;
            }
        }

        private static void ValidateParameters(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", "Buffer cannot be null");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "Offset should be non negative number");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "Offset should be non negative number");
            }
        }

        public override void Close() => Flush();

        public override void Flush()
        {
            if (_currentBuffer != null)
            {
                _free(_currentBuffer);
                _currentBuffer = null;
                _position = 0;
                _currentPosition = 0;
            }
        }

        public override void SetLength(long value) => throw new NotSupportedException();
    }
}