using System.Text;
using System.Xml.Linq;
namespace SOAPGeneral;
public class MedicoLabCentralWs
{
    public string claveEstd { get; set; }
    public int idLaboratorio { get; set; }
    public string apiKey { get; set; }
    public int medicoId { get; set; }
    public string matricula { get; set; }
    public string nombre { get; set; }
    
    public const string RequestUrl = "https://aswe.santafe.gov.ar/proxy.php/Salud/WSLabCentralApi";
    private XNamespace envelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
    private XNamespace wsl = "https://www.santafe.gob.ar/labcentral/ws/WSLabCentralApi/";
    
    private string soapAction = "\"https://www.santafe.gob.ar/labcentral/ws/WSLabCentralApi/cargaMedico\"";
    private string mediaType = "text/xml";
    private Encoding encoding = Encoding.UTF8;
    public StringContent GetContent()
    {
        var envelope = Envelope();
        string soapRequestString = envelope.Declaration + envelope.ToString();
        var httpRequestContent = new StringContent(soapRequestString, encoding, mediaType);
        httpRequestContent.Headers.Add("SOAPAction", soapAction);
        return httpRequestContent;
    }
    
    public XDocument Envelope()
    {
        var soapEnvelope = new XDocument(
            new XElement(envelopeNamespace + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", envelopeNamespace),
                new XAttribute(XNamespace.Xmlns + "wsl", wsl),
                new XElement(envelopeNamespace + "Header",
                    new XElement("id_laboratorio", idLaboratorio),
                    new XElement("claveestd", claveEstd),
                    new XElement("api_key", apiKey)
                ),
                new XElement(envelopeNamespace + "Body",
                    new XElement(wsl + "cargaMedico",
                        new XElement("medico_id", medicoId),
                        new XElement("matricula", matricula),
                        new XElement("nombre", nombre)
                    )
                )
            )
        );
        return soapEnvelope;
    }

    public string LeerRespuesta()
    {
        throw new NotImplementedException();
    }
}