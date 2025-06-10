```C#

    using System.Xml.Linq;
    
    interface IEnvelop
    {
    XDocument GetEnvelop();
    object GetResponse(XDocument response);
    }
    
    interface IEnvelop<TResponse> : IEnvelop
    {
    TResponse GetResponse(XDocument response);
    }
    
    public class Envelop<int> : IEnvelop
    {
    
    }
    
    class Sender
    {
    private readonly HttpClient client = new();
    
        TResponse SoapSendMessage(IEnvelop envelop) //, HttpClient client)
        {
            var content = new StringContent(
                envelop.GetEnvelop(), "text/xml");
            // var content = client.GetContent(envelop);
    
            var resp = await client.SendAsync(content);
    
            return envelop.GetResponse(resp);
        }
    }
    
    class MedicoWS : IEnvelop<MedicoWSResponse>
    {
    MedicoWSResponse GetResponse(XDocument response)
    {
    // parseo xml....
    //
    //
    
            return respuesta;
        }
    }
    
    class MedicoWSResponse
    {
    }
    
    var medicoWS = new MedicoWS
    {
    Nombre = "",
    Matricula = "",
    Codigo = 134
    };
    
    using(var channel = new SoapChannel()) {
    channel.SoapSendMessage(medicoWS);
    channel.GetResponse();
    }
    
    
    MedicoWS : IEnvelop<MedicoWSResponse>;
    SumarWs : IEnvelop<int>
    
    MedicoWS a;
    SumarWS b;
    
    a = b;
    
    IEnvelop c;
    c = a;
    c = b;
```
