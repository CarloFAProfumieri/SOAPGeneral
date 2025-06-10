using System.Text;
using System.Xml.Linq;
using static SOAPGeneral.HttpClientCaller;

namespace SOAPGeneral;


public class SumarEnvelope : IEnvelope
{
    public int a { get; set; }
    public int b { get; set; }

    public const string REQUEST_URL = "http://www.dneonline.com/calculator.asmx";
    private const string MEDIA_TYPE = "text/xml";
    private const string SOAP_ACTION = "\"http://tempuri.org/Add\"";
    private const string SOAP_HEADER_NAME = "SOAPAction";
    private readonly Encoding m_encoding = Encoding.UTF8;
    
    private readonly XNamespace m_soap = "http://schemas.xmlsoap.org/soap/envelope/";
    private readonly XNamespace m_tem = "http://tempuri.org/";
    
    public string GetEnvelope()
    {
       var soapEnvelope = new XDocument(
          new XElement(m_soap + "Envelope",
             new XAttribute(XNamespace.Xmlns + "soap", m_soap),
             new XAttribute(XNamespace.Xmlns + "tem", m_tem),
             new XElement(m_soap + "Header"),
             new XElement(m_soap + "Body",
                new XElement(m_tem + "Add",
                   new XElement(m_tem + "intA", a),
                   new XElement(m_tem + "intB", b)
                )
             )
          )
       );
       return soapEnvelope.Declaration + soapEnvelope.ToString();
    }

    public object ReadResponse(XDocument resultadoXml)
    {
       //aca habria que agregar toda la logica de checkeo de errores
       var soapNs = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");
       var tempuriNs = XNamespace.Get("http://tempuri.org/");
       var addResult = resultadoXml
          .Element(soapNs + "Envelope")?
          .Element(soapNs + "Body")?
          .Element(tempuriNs + "AddResponse")?
          .Element(tempuriNs + "AddResult")?
          .Value;
       return addResult;
    }
}