using System;
using System.Collections.Generic;
using Codestellation.Quarks.IO;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.IO
{
    [TestFixture]
    public class SlidingStreamTests
    {
        private byte _counter;
        private int _freeCalled;
        private List<byte[]> _writeBuffers;

        [SetUp]
        public void SetUp()
        {
            _counter = 0;
            _freeCalled = 0;
            _writeBuffers = new List<byte[]>();
        }

        [Test]
        public void Can_read_byte_from_multiple_buffers()
        {
            //given
            var stream = new SlidingStream(CreateReadBuffer, DoNothing);

            //when
            var first = (byte)stream.ReadByte();
            var second = (byte)stream.ReadByte();

            //then
            Assert.That(first, Is.EqualTo(1));
            Assert.That(second, Is.EqualTo(2));
            Assert.That(_freeCalled, Is.EqualTo(2));
        }

        [Test]
        public void Can_read_to_buffer_from_multiple_buffers()
        {
            //given
            var stream = new SlidingStream(CreateReadBuffer, DoNothing);
            var buffer = new byte[2];

            //when
            stream.Read(buffer, 0, 2);
            stream.Read(buffer, 1, 2);

            //then
            Assert.That(buffer, Is.EquivalentTo(new byte[] { 1, 2 }));
            Assert.That(_freeCalled, Is.EqualTo(2));
        }

        [Test]
        public void Can_write_byte_to_claimed_buffer()
        {
            //given
            var stream = new SlidingStream(CreateWriteBuffer, DoNothing);

            //when
            stream.WriteByte(123);

            //then
            Assert.That(_writeBuffers[0], Is.EqualTo(new Byte[] { 123 }));
            Assert.That(_freeCalled, Is.EqualTo(1));
        }

        [Test]
        public void Can_write_from_buffer_to_multiple_buffers()
        {
            //given
            var stream = new SlidingStream(CreateWriteBuffer, DoNothing);
            var buffer = new byte[] { 123, 124 };

            //when
            stream.Write(buffer, 0, 2);

            //then
            Assert.That(_writeBuffers[0], Is.EquivalentTo(new byte[] { 123 }));
            Assert.That(_writeBuffers[1], Is.EquivalentTo(new byte[] { 124 }));
            Assert.That(_freeCalled, Is.EqualTo(2));
        }

        private byte[] CreateWriteBuffer()
        {
            var writeBuffer = new byte[1];
            _writeBuffers.Add(writeBuffer);
            return writeBuffer;
        }

        private void DoNothing(byte[] obj)
        {
            Assert.That(obj, Is.Not.Null);
            _freeCalled++;
        }

        private byte[] CreateReadBuffer()
        {
            _counter++;
            return new[] { _counter };
        }
    }
}