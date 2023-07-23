using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpClientHelpers
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private HttpClient HttpClient { get; }
        private IHttpContextAccessor HttpContextAccessor { get; }
        private ILogger Logger { get; }
        public HttpClientHelper(IHttpContextAccessor httpContextAccessor, ILogger<IHttpClientHelper> logger)
        {
            HttpClient = new HttpClient();
            HttpContextAccessor = httpContextAccessor;
            Logger = logger;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            PrepareHeader();
            HttpResponseMessage httpResponse = await HttpClient.GetAsync(url);
            return httpResponse;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object requestBody, string contentType, string token)
        {
            AddToken(token);
            string? myContent = JsonConvert.SerializeObject(requestBody);
            byte[]? buffer = Encoding.UTF8.GetBytes(myContent);
            ByteArrayContent? byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            Logger.LogInformation($"calling API: {url} with request: {myContent}");
            HttpResponseMessage httpResponse = await HttpClient.PostAsync(url, byteContent);
            return httpResponse;
        }
        private void PrepareHeader()
        {
            string authToken = HttpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrWhiteSpace(authToken))
            {
                // to remove "Bearer" keyword form token
                string[] tokendata = authToken.Split(' ');
                if (tokendata.Length >= 1)
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokendata[1]);

                //Add Language
                HttpClient.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentCulture.Name);

            }
        }
        private void AddToken(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                //AddToken
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Add Language
                HttpClient.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentCulture.Name);

            }
        }
    }
}
