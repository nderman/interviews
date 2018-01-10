using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Realmdigital_Interview.Models.Product;

namespace Realmdigital_Interview.Controllers
{
    public class ProductController
    {

        [Route("product")]
        public object GetProductById(string productId)
        {
            var product = new Product();

            var result = Product.GetById(productId);

            return result.Count > 0 ? result[0] : null;
        }

        [Route("product/search")]
        public List<object> GetProductsByName(string productName)
        {
            var product = new Product();

            var result = Product.GetByName(productName);

            return result.Count > 0 ? result[0] : null;
            return result;
        }
    }



    class ApiResponseProduct
    {
        public string BarCode { get; set; }
        public string ItemName { get; set; }
        public List<ApiResponsePrice> PriceRecords { get; set; }
    }

    class ApiResponsePrice
    {
        public string SellingPrice { get; set; }
        public string CurrencyCode { get; set; }
    }
}