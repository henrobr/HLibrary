using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace HLibrary
{
    public class RequestApi
    {
        public static string UrlApi { get; set; }
        public static T Request<T>(string url = null, Method method = Method.Get, DataFormat dataFormat = DataFormat.None, object data = null, string auth = null)
        {
            var client = new RestClient(UrlApi);
            var request = new RestRequest(url, method: method); //(url, method, dataFormat);

            if (auth != null)
                request.AddHeader("Authorization", auth);
            if (data != null)
            {
                if (dataFormat == DataFormat.Json)
                {
                    data = JsonConvert.SerializeObject(data);
                    request.AddJsonBody(data);
                }

            }
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            RestResponse<T> response = client.Execute<T>(request);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new Exception("Usuário sem autorização para acessar a página");
                else
                    throw new Exception(response.ErrorException.Message);
            }

            return response.Data;
        }
    }
    public class Results<T> where T : class
    {
        public T Dados { get; set; }
        public int Status { get; set; }
        public string Message{ get; set; }
        public string Token { get; set; }
        public int Tr { get; set; }
        public int Pgs { get; set; }
        public int Pga { get; set; }

    }
}
