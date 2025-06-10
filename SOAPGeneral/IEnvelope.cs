using System.Xml.Linq;

namespace SOAPGeneral;

public interface IEnvelope
{
    string GetEnvelope();
    object ReadResponse(XDocument response);
}