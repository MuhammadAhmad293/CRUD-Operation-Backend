namespace Common.HttpClientHelpers
{
    public interface IHttpClientHelper
    {
        Task<HttpResponseMessage> PostAsync(string url, object requestBody, string contentType, string token);
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
