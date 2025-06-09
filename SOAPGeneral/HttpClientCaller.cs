using System.Text;
using System.Xml.Linq;

namespace SOAPGeneral;

public static class HttpClientCaller
{
    private static HttpClient client = new HttpClient();
    public static async Task<string> CallSum(StringContent content, string requestUrl)
    {
        var response = await client.PostAsync(requestUrl, content);
        return await response.Content.ReadAsStringAsync();
    }
}
