using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;

namespace OmegaTronOnionIO.Helpers
{
    public static class JsonHelper
    {
        public static T SendToServer<T>(string uri, NameValueCollection webRequestHeaders, string data = "", string method = "POST")
        {
            HttpClient httpClient = new HttpClient();

            T obj;
            try
            {
                foreach (var key in webRequestHeaders.Keys)
                {
                    if (key.ToString().ToUpper().Trim() == "AUTHORIZATION")
                    {

                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", webRequestHeaders[key.ToString()]);
                    }
                    else
                    {
                        httpClient.DefaultRequestHeaders.Add(key.ToString(), webRequestHeaders[key.ToString()]);
                    }
                }

                HttpResponseMessage response = null;

                switch (method.ToUpper().Trim())
                {
                    case "POST":
                        StringContent requestBody = new StringContent(data);
                        response = httpClient.PostAsync(uri, requestBody).Result;
                        break;
                    case "GET":
                        response = httpClient.GetAsync(uri).Result;
                        break;
                }


                string returnString = response.Content.ReadAsStringAsync().Result;
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(returnString);
                return obj;
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = wex.Message + "\n" + reader.ReadToEnd();
                        }
                    }
                }
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>("{}");
                return obj;
            }

        }
    }
}
