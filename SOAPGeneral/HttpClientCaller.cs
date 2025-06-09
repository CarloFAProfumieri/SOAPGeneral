using System.Text;
using System.Xml.Linq;

namespace SOAPGeneral;


public class HttpClientCaller
{
    public static async Task<int> CallSum(XDocument envelope)
    {
        var httpClient = new HttpClient();
         
        //Declaration tiene el tag <?xml version="1.0"> que va al principio del "sobre"
        string soapRequestString = envelope.Declaration + envelope.ToString();
         
        /*
           El primer parametro le setea el codificado de los campos, en este caso UTF8
           por lo general
           -> si usa SOAP 1.1: text/xml
           -> si usa SOAP 1.2: application/soap+xml

        */
        var content = new StringContent(soapRequestString, Encoding.UTF8, "text/xml");

        //este es un header html para seleccionar cual metodo quiero usar de la pag que estoy consumiendo
        content.Headers.Add("SOAPAction", "\"http://tempuri.org/Add\"");
         
        //aca lo estoy consumiendo
        var response = await httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);
         
        // y aca ya paso a leer el resultado (muy especifico de cada llamado)
        var result = await response.Content.ReadAsStringAsync();
        var documentoX = XDocument.Parse(result);
        var soapNs = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");
        var tempuriNs = XNamespace.Get("http://tempuri.org/");
        var addResult = documentoX
            .Element(soapNs + "Envelope")?
            .Element(soapNs + "Body")?
            .Element(tempuriNs + "AddResponse")?
            .Element(tempuriNs + "AddResult")?
            .Value;
        return int.Parse(addResult);
    }
}