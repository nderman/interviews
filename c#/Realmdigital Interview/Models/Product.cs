using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Realmdigital_Interview.Models.Product;

namespace Realmdigital_Interview.Models
{
    public class ProductController
    {
 		private string eanList = "http://192.168.0.241/eanlist?type=Web";

 		public object GetById(string id){
 			var result = this.request(id,"id");
 			return result.Count > 0 ? result[0] : null;
 		}

 		public List<object> GetByName(string name){
 			return this.request(name, "name");
 		}

 		public List<object> request(string data, string type){
 			string response = "";

 			 using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = client.UploadString(eanlist, "POST", "{ \""+type+"\": \"" + data + "\" }");
            }
            var reponseObject = JsonConvert.DeserializeObject<List<ApiResponseProduct>>(response);

            var result = new List<object>();
            for (int i = 0; i < reponseObject.Count; i++)
            {
                var prices = new List<object>();
                for (int j = 0; j < reponseObject[i].PriceRecords.Count; j++)
                {
                    if (reponseObject[i].PriceRecords[j].CurrencyCode == "ZAR")
                    {
                        prices.Add(new
                        {
                            Price = reponseObject[i].PriceRecords[j].SellingPrice,
                            Currency = reponseObject[i].PriceRecords[j].CurrencyCode
                        });
                    }
                }
                result.Add(new
                {
                    Id = reponseObject[i].BarCode,
                    Name = reponseObject[i].ItemName,
                    Prices = prices
                });
            }
            return result;
 		}







    }

}