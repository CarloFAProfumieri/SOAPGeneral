using System.Text;
using System.Xml.Linq;
using static SOAPGeneral.HttpClientCaller;

namespace SOAPGeneral;


public class SumarWs
{
    public int A { get; set; }
    public int B { get; set; }
    public const string RequestUrl = "http://www.dneonline.com/calculator.asmx";

    private XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
    private XNamespace tem = "http://tempuri.org/";
    private string mediaType = "text/xml";
    private Encoding encoding = Encoding.UTF8;
    private string soapAction = "\"http://tempuri.org/Add\"";

    public StringContent GetContent()
    {
       var envelope = Envelope();
       string soapRequestString = envelope.Declaration + envelope.ToString();
       var httpRequestContent = new StringContent(soapRequestString, encoding, mediaType);
       httpRequestContent.Headers.Add("SOAPAction", soapAction);
       return httpRequestContent;
    }
    private XDocument Envelope()
    {
       var soapEnvelope = new XDocument(
          new XElement(soap + "Envelope",
             new XAttribute(XNamespace.Xmlns + "soap", soap),
             new XAttribute(XNamespace.Xmlns + "tem", tem),
             new XElement(soap + "Header"),
             new XElement(soap + "Body",
                new XElement(tem + "Add",
                   new XElement(tem + "intA", A),
                   new XElement(tem + "intB", B)
                )
             )
          )
       );
       return soapEnvelope;
    }
    public int Parsear(string resultado)
    {
       //aca habria que agregar toda la logica de checkeo de errores
       var documentoX = XDocument.Parse(resultado);
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