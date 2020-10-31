using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Self_HostedWebAPI
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }

    class CSVMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public CSVMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv"));
        }
        public override bool CanReadType(Type type)
        {
            return type == typeof(Product);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(Product);
        }
        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                var singleProduct = value as Product;
                if (singleProduct == null) throw new InvalidOperationException("Cannot serialize type");
                writer.WriteLine($"{singleProduct.Name}, {singleProduct.Category}");
            }
        }
        public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            using (var reader = new StreamReader(readStream))
            {
                var input = reader.ReadToEnd();
                return new Product
                {
                    Name = input.Substring(0, input.IndexOf(',')),
                    Category = input.Substring(input.IndexOf(',') + 2, input.IndexOf("\r\n") - input.IndexOf(',') - 2)
                };
            }
        }
    }
}
