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
        var envelope = new SumarEnvelope
        {
            a = 65,
            b = 3
        };
        var resultado = await HttpClientCaller.CallSoap(envelope);
        int resultadoSuma = int.Parse(envelope.ReadResponse(resultado).ToString());
        Console.WriteLine("respuesta: " + resultadoSuma);
    }
}