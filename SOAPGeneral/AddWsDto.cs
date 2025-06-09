using System.Text;
using System.Xml.Linq;
using static SOAPGeneral.HttpClientCaller;

namespace SOAPGeneral;

class Program
{
   static async Task Main()
   {
      var dto = new AddWsDto
      {
         A = 65,
         B = 3
      };
      var envelope = dto.GetEnvelope();
      Console.WriteLine(envelope);
      var resultado = await HttpClientCaller.CallSum(envelope);
      Console.WriteLine("respuesta: " + resultado);
      
      
   }
}
public class AddWsDto
{
    public int A { get; set; }
    public int B { get; set; }
    
    private XNamespace envNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
    private XNamespace tem = "http://tempuri.org/";
    
    public XDocument GetEnvelope()
    {
       var soapEnvelope = new XDocument(
          new XElement(envNamespace + "Envelope",
             new XAttribute(XNamespace.Xmlns + "soap", envNamespace),
             new XAttribute(XNamespace.Xmlns + "tem", tem),
             new XElement(envNamespace + "Header"),
             new XElement(envNamespace + "Body",
                new XElement(tem + "Add",
                   new XElement(tem + "intA", A),
                   new XElement(tem + "intB", B)
                )
             )
          )
       );
       return soapEnvelope;
    }
}