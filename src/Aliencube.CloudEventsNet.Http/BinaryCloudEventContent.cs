using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aliencube.CloudEventsNet.Http
{
    public class BinaryCloudEventContent : ByteArrayContent
    {
        public BinaryCloudEventContent(byte[] content) : base(content)
        {
        }

        public BinaryCloudEventContent(byte[] content, int offset, int count) : base(content, offset, count)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override Task<Stream> CreateContentReadStreamAsync()
        {
            return base.CreateContentReadStreamAsync();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return base.SerializeToStreamAsync(stream, context);
        }

        protected override bool TryComputeLength(out long length)
        {
            return base.TryComputeLength(out length);
        }
    }
}