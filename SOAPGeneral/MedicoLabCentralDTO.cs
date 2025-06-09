using System.Xml.Linq;
namespace SOAPGeneral;
/*
var medicoEnvelope = new MedicoLabCentralDto {
            claveEstd = "abc123",
            idLaboratorio = 5,
            apiKey = "secret",
            medicoId = 42,
            matricula = "M1234",
            nombre = "Dr. Juan Pérez"
        };
 */
public class MedicoLabCentralDto
{
    public string claveEstd { get; set; }
    public int idLaboratorio { get; set; }
    public string apiKey { get; set; }
    public int medicoId { get; set; }
    public string matricula { get; set; }
    public string nombre { get; set; }
    private XNamespace envNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
    private XNamespace wsl = "https://www.santafe.gob.ar/labcentral/ws/WSLabCentralApi/";
    public XDocument GetEnvelope()
    {
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