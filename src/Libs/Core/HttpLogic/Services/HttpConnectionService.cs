using Core.HttpLogic.Services.Interfaces;

namespace Core.HttpLogic.Services;

public record struct HttpConnectionData()
{
    public TimeSpan? Timeout { get; set; } = null;
    
    public CancellationToken CancellationToken { get; set; } = default;
    
    public string ClientName { get; set; }
}


internal class HttpConnectionService(IHttpClientFactory httpClientFactory) : IHttpConnectionService
{
    public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
    {
        var httpClient = string.IsNullOrWhiteSpace(httpConnectionData.ClientName)
            ? httpClientFactory.CreateClient()
            : httpClientFactory.CreateClient(httpConnectionData.ClientName);
            
        if (httpConnectionData.Timeout != null)
        {
            httpClient.Timeout = httpConnectionData.Timeout.Value;
        }
        
        return httpClient;
    }
    
    public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient, CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        var response = await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
        return response;
    }
}