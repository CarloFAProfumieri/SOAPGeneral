using System.Xml.Linq;
namespace SOAPGeneral;

public class MedicoLabCentralDTO
{
    public string claveEstd { get; set; }
    public int idLaboratorio { get; set; }
    public string apiKey { get; set; }
    public int medicoId { get; set; }
    public string matricula { get; set; }
    public string nombre { get; set; }
            
    public XDocument GetEnvelope()
    {
        XNamespace envNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
        XNamespace wsl = "https://www.santafe.gob.ar/labcentral/ws/WSLabCentralApi/";
        var soapEnvelope = new XDocument(
            new XElement(envNamespace + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", envNamespace),
                new XAttribute(XNamespace.Xmlns + "wsl", wsl),
                new XElement(envNamespace + "Header",
                    new XElement("id_laboratorio", idLaboratorio),
                    new XElement("claveestd", claveEstd),
                    new XElement("api_key", apiKey)
                ),
                new XElement(envNamespace + "Body",
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
}