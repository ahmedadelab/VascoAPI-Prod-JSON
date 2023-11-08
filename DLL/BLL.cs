using System.Net;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace VascoAPI
{
    public class BLL
    {
        BAL bAL = new BAL();

        public string Vasco(string Req)
        {
            string soapResult = "";
            var str = XElement.Parse(Req);
            var strr = "";
            XmlDocument soapEnvelopeXml = new XmlDocument();
            XmlDocument XDoc = new XmlDocument();
            HttpWebRequest request = HTTPs.CreateWebRequest();
            string? CREDFLD_USERID = string.Empty;
            string? CREDFLD_COMPONENT_TYPE = string.Empty;
            string? CREDFLD_DOMAIN = string.Empty;
            string? RequestID = string.Empty;

            soapEnvelopeXml.LoadXml(Req);
            try
            {

                XmlDocument xmlDoc1 = new XmlDocument();
                xmlDoc1.LoadXml(Req);
                XmlNodeList elemlistCode = xmlDoc1.GetElementsByTagName("aut:authUser");
                var credentialAttributeSet = elemlistCode[0]?.InnerXml;
                XmlDocument? xmlDoc2 = new XmlDocument();
                xmlDoc2?.LoadXml(credentialAttributeSet);
                XmlNodeList? XNcifAdditionalFields = xmlDoc2.GetElementsByTagName("attributes");
                for (int i = 0; i < XNcifAdditionalFields.Count; i++)
                {
                    var value = XNcifAdditionalFields[i]?.SelectSingleNode("value")?.InnerText;
                    var attributeID = XNcifAdditionalFields[i]?.SelectSingleNode("attributeID")?.InnerText;
                    if (attributeID == "CREDFLD_USERID")
                    {
                        CREDFLD_USERID = value;

                    }
                    if (attributeID == "CREDFLD_COMPONENT_TYPE")
                    {
                        CREDFLD_COMPONENT_TYPE = value;
                    }

                    if (attributeID == "CREDFLD_DOMAIN")
                    {
                        CREDFLD_DOMAIN = value;
                    }
                }
                RequestID = "MW-Vasco-" + CREDFLD_USERID + "-" + DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                string Request = JsonConvert.SerializeObject(new
                {
                    CREDFLD_USERID = CREDFLD_USERID,
                    CREDFLD_PASSWORD_FORMAT = "*******",
                    CREDFLD_COMPONENT_TYPE = CREDFLD_COMPONENT_TYPE,
                    CREDFLD_DOMAIN = CREDFLD_DOMAIN,
                    CREDFLD_PASSWORD = "**********",
                }, Newtonsoft.Json.Formatting.Indented);

                bAL.InsertLog(Request, RequestID, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss:fff tt"));


                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                        strr = (string)XElement.Parse(soapResult);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(soapResult);
                    }
                }
                bAL.updateLog(soapResult, RequestID, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss:fff tt"));
            }

            catch (Exception ex)
            {
                bAL.updateLog(ex.Message, RequestID, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss:fff tt"));
            }
            return soapResult;
        }


    }
}
