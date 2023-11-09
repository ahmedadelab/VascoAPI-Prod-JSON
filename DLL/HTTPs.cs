using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace VascoAPI
{
    public class HTTPs
    {
        public static HttpWebRequest CreateWebRequest()
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var url = MyConfig.GetValue<string>("AppSettings:URL");
            X509Certificate Cert = X509Certificate.CreateFromCertFile("C:\\temp\\PDUATOASIDKV01.eg.albaraka.crt");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}
