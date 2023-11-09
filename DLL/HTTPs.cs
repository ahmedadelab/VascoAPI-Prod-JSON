using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace VascoAPI
{
    public class HTTPs
    {
        public static HttpWebRequest CreateWebRequest()
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var url = MyConfig.GetValue<string>("AppSettings:URL");
          //  var Cert1 = MyConfig.GetValue<string>("AppSettings:Cert");
         //   X509Certificate Cert = X509Certificate.CreateFromCertFile(Cert1);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //  webRequest.ClientCertificates.Add(Cert);
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
            webRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}
