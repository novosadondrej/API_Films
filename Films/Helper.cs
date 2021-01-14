using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Films
{
    public static class Helper
    {
        public static HttpClient ApiClient { get; set; }

        public static void Initialize()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("http://www.omdbapi.com/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    
    }
}
