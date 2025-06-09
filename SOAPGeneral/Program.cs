using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using SOAPGeneral;

namespace useSOAP;

class Program
{
    static async Task Mained(string[] args)
    {
        var medicoEnvelope = new MedicoLabCentralDto {
            claveEstd = "abc123",
            idLaboratorio = 5,
            apiKey = "secret",
            medicoId = 42,
            matricula = "M1234",
            nombre = "Dr. Juan Pérez"
        };
        var envelope = medicoEnvelope.GetEnvelope();
        Console.WriteLine(envelope.ToString());
        var respuesta = await CallSoap(envelope);
            
        Console.WriteLine("Response: " + respuesta);
    }
    static async Task<int> CallSoap(XDocument envelope)
    {
        //Declaration tiene el tag <?xml version="1.0"> que va al principio del "sobre"
        string soapRequestString = envelope.Declaration + envelope.ToString();
        
        var httpClient = new HttpClient();
        var content = new StringContent(soapRequestString, Encoding.UTF8, "text/xml");
        
        content.Headers.Add("SOAPAction", "\"http://tempuri.org/Add\"");

        var response = await httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);
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
    
    static async Task<int> CallSum(XDocument envelope)
    {
            
        string soapRequestString = envelope.Declaration + envelope.ToString();
        var httpClient = new HttpClient();
        var content = new StringContent(soapRequestString, Encoding.UTF8, "text/xml");

        content.Headers.Add("SOAPAction", "\"http://tempuri.org/Add\"");

        var response = await httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);
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

public class MedicoSoapClient
{
    private readonly string endpoint = "https://www.santafe.gob.ar/labcentral/ws/WSLabCentralApi/";
        
}