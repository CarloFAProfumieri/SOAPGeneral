using System.Text;
using System.Xml.Linq;

namespace SOAPGeneral;
public static class HttpClientCaller
{
    public const string REQUEST_URL = "http://www.dneonline.com/calculator.asmx";

    private static HttpClient client = new HttpClient(){ BaseAddress = new Uri(REQUEST_URL) };

    public static async Task<XDocument> CallSoap(IEnvelope envelope, string requestUrl)
    {
        var httpRequestContent = new StringContent(envelope.GetEnvelope(), Encoding.UTF8, "text/xml");
        httpRequestContent.Headers.Add("SOAPAction", "\"http://tempuri.org/Add\"");
        var response = await client.PostAsync("", httpRequestContent);
        return XDocument.Parse(await response.Content.ReadAsStringAsync());
    }
}
