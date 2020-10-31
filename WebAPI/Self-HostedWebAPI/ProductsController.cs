using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Self_HostedWebAPI
{
    public class ProductsController : ApiController
    {
        public Product Get()
        {
            return new Product { Category = "Juice", Name = "Pepsi" };
        }
        public void Post(Product p)
        {
        }
    }
}
