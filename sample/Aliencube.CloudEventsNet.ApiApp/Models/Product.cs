using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aliencube.CloudEventsNet.ApiApp.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
