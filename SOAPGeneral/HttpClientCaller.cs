using System.Text;
using System.Xml.Linq;

namespace SOAPGeneral;
/*
    public const string REQUEST_URL = "http://www.dneonline.com/calculator.asmx";
    private const string MEDIA_TYPE = "text/xml";
    private const string SOAP_ACTION = "\"http://tempuri.org/Add\"";
    private const string SOAP_HEADER_NAME = "SOAPAction";
    private readonly Encoding m_encoding = Encoding.UTF8;
    
    private readonly XNamespace m_soap = "http://schemas.xmlsoap.org/soap/envelope/";
    private readonly XNamespace m_tem = "http://tempuri.org/";

    public StringContent GetContent()
    {
       var envelope = GetEnvelope();
       string soapRequestString = envelope.Declaration + envelope.ToString();
       var httpRequestContent = new StringContent(soapRequestString, m_encoding, MEDIA_TYPE);
       httpRequestContent.Headers.Add(SOAP_HEADER_NAME, SOAP_ACTION);
       return httpRequestContent;
    }
 */
public static class HttpClientCaller
{
    private static HttpClient client = new HttpClient();
    public static async Task<string> CallSoap(StringContent content, string requestUrl)
    {
        var response = await client.PostAsync(requestUrl, content);
        return await response.Content.ReadAsStringAsync();
    }
}
