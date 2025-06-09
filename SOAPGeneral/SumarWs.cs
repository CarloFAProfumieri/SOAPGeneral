using System.Text;
using System.Xml.Linq;
using static SOAPGeneral.HttpClientCaller;

namespace SOAPGeneral;


public class SumarWs
{
    public int A { get; set; }
    public int B { get; set; }

    public const string REQUEST_URL = "http://www.dneonline.com/calculator.asmx";
    private const string MEDIA_TYPE = "text/xml";
    private const string SOAP_ACTION = "\"http://tempuri.org/Add\"";
    private const string SOAP_HEADER_NAME = "SOAPAction";
    private readonly XNamespace m_soap = "http://schemas.xmlsoap.org/soap/envelope/";
    private readonly XNamespace m_tem = "http://tempuri.org/";
    private readonly Encoding m_encoding = Encoding.UTF8;

    public StringContent GetContent()
    {
       var envelope = Envelope();
       string soapRequestString = envelope.Declaration + envelope.ToString();
       var httpRequestContent = new StringContent(soapRequestString, m_encoding, MEDIA_TYPE);
       httpRequestContent.Headers.Add(SOAP_HEADER_NAME, SOAP_ACTION);
       return httpRequestContent;
    }
    
    private XDocument Envelope()
    {
       var soapEnvelope = new XDocument(
          new XElement(m_soap + "Envelope",
             new XAttribute(XNamespace.Xmlns + "soap", m_soap),
             new XAttribute(XNamespace.Xmlns + "tem", m_tem),
             new XElement(m_soap + "Header"),
             new XElement(m_soap + "Body",
                new XElement(m_tem + "Add",
                   new XElement(m_tem + "intA", A),
                   new XElement(m_tem + "intB", B)
                )
             )
          )
       );
       return soapEnvelope;
    }
    
    public int LeerRespuesta(string resultado)
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