using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using SOAPGeneral;

namespace SOAPGeneral;
class Program
{
    static async Task Main()
    {
        var sumarWs = new SumarWs
        {
            A = 65,
            B = 3
        };
        var envelope = sumarWs.GetContent();
        var resultado = await HttpClientCaller.CallSum(envelope, SumarWs.REQUEST_URL);
        int resultadoSuma = sumarWs.LeerRespuesta(resultado);
        Console.WriteLine("respuesta: " + resultadoSuma);
    }
}