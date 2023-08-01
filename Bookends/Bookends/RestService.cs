using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bookends
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<Book> GetBookData(string query)
        {
            Book book=null;
            try 
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Book>(content);
                    JObject d = JObject.Parse(content); 
                    Debug.WriteLine(d.Property("items")); 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }
             
            return book;   
        }
    }
}
