using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
namespace AuthorQuerier
{
    public class AuthorClient
    {
        /// <summary>
        /// Loads the author information from the external resource Api and Deserializes it into an author model object.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<AuthorModel>> AuthorProcessor()
        {
             PageModel authorObject= null;
            var authorList = new List<AuthorModel>();
            //try
            //{
                int numOfPages = 2;
                for (int i = 1; i <= numOfPages; i++)
                {
                    var url = $"https://jsonmock.hackerrank.com/api/article_users/search?page={i}";
                    var client = new HttpClient();
                    //var content = await client.GetStringAsync(url);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine(data);
                        authorObject = JsonSerializer.Deserialize<PageModel>(data);
                    }
                    foreach (var item in authorObject.data)
                    {
                        authorList.Add(item);
                    }
                }
            //}
            //catch ( e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            return authorList;
        }
    }
}
