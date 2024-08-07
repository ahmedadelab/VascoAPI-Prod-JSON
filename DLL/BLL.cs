using System.Net;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;
using static VascoAPI.SVASCOJSON;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;

namespace VascoAPI
{
    public class BLL
    {
        BAL bAL = new BAL();


        public string VascoJson(DataTable Dtattribute)
        {
            string jsonString = string.Empty;
  
            string? ReturnCodeEnum = string.Empty;
            string? StatusCodeEnum = string.Empty;
            string? SReturnCode = string.Empty;
            string? SStatusCode = string.Empty;
            string strr = string.Empty;
            string? ErrorCode = string.Empty;
            string? ErrorDesc = string.Empty;
            string? soapResult = string.Empty;
            string? CREDFLD_USERID = string.Empty;
            string? CREDFLD_PASSWORD_FORMAT = string.Empty;
            string? CREDFLD_COMPONENT_TYPE = string.Empty;
            string? CREDFLD_PASSWORD = string.Empty;
            string? CREDFLD_DOMAIN = string.Empty;
            string? CREDFLD_ORGANIZATIONAL_UNIT = string.Empty;
            foreach (DataRow row in Dtattribute.Rows)
            {

                string Value = (string)row["Value"];
                string AttributeID = (string)row["AttributeID"];
                if (AttributeID == "CREDFLD_USERID")
                {
                    CREDFLD_USERID = Value;
                }

                if (AttributeID == "CREDFLD_PASSWORD_FORMAT")
                {
                    CREDFLD_PASSWORD_FORMAT = Value;
                }

                if (AttributeID == "CREDFLD_COMPONENT_TYPE")
                {
                    CREDFLD_COMPONENT_TYPE = Value;
                }

                if (AttributeID == "CREDFLD_PASSWORD")
                {
                    CREDFLD_PASSWORD = Value;
                }

                if (AttributeID == "CREDFLD_DOMAIN")
                {
                    CREDFLD_DOMAIN = Value;
                }

                if (AttributeID == "CREDFLD_ORGANIZATIONAL_UNIT")
                {
                    CREDFLD_ORGANIZATIONAL_UNIT = Value;
                }
            }
                string RequestID = "MW-Vasco-" + CREDFLD_USERID + "-" + DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                string Request = JsonConvert.SerializeObject(new
                {
                    CREDFLD_USERID = CREDFLD_USERID,
                    CREDFLD_PASSWORD_FORMAT = CREDFLD_PASSWORD_FORMAT,
                    CREDFLD_COMPONENT_TYPE = CREDFLD_COMPONENT_TYPE,            
                    CREDFLD_PASSWORD = "**********",
                    CREDFLD_DOMAIN = CREDFLD_DOMAIN,
                    CREDFLD_ORGANIZATIONAL_UNIT = CREDFLD_ORGANIZATIONAL_UNIT,
                }, Newtonsoft.Json.Formatting.Indented);

                bAL.InsertLog(Request, RequestID, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss:fff tt"));
                XmlDocument soapEnvelopeXml = new XmlDocument();
                XmlDocument XDoc = new XmlDocument();
                HttpWebRequest request = HTTPs.CreateWebRequest();
            soapEnvelopeXml.LoadXml(@"<soapenv:Envelope
xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
xmlns:aut=""http://www.vasco.com/IdentikeyServer/IdentikeyTypes/Authentication"">
<soapenv:Header/>
   <soapenv:Body>
	<aut:authUser>
	<credentialAttributeSet>
		<attributes>
		<value xsi:type=""xsd:string"">" + CREDFLD_USERID + @"</value>
		<attributeID>CREDFLD_USERID</attributeID>
		</attributes>
		<attributes>
		<value xsi:type=""xsd:unsignedInt"">"+ CREDFLD_PASSWORD_FORMAT + @"</value>
		<attributeID>CREDFLD_PASSWORD_FORMAT</attributeID>
		</attributes>
		<attributes>
		<value xsi:type=""xsd:string"">"+ CREDFLD_COMPONENT_TYPE + @"</value>
		<attributeID>CREDFLD_COMPONENT_TYPE</attributeID>
		</attributes>
		<attributes>
		<value xsi:type=""xsd:string"">"+ CREDFLD_PASSWORD + @"</value>
		<attributeID>CREDFLD_PASSWORD</attributeID>
		</attributes>
		<attributes>
                <value xsi:type=""xsd:string"">"+ CREDFLD_DOMAIN + @"</value>
                <attributeID>CREDFLD_DOMAIN</attributeID>
                </attributes>
                <attributes>
                <value xsi:type=""xsd:string"">"+ CREDFLD_ORGANIZATIONAL_UNIT + @"</value>
                <attributeID>CREDFLD_ORGANIZATIONAL_UNIT</attributeID>
                </attributes>		
	</credentialAttributeSet>
	</aut:authUser>
   </soapenv:Body>
</soapenv:Envelope>
");

            //    soapEnvelopeXml.LoadXml(Req);
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
                    XmlSerializer serializer = new XmlSerializer(typeof(SVascoResp.Body));
                    using (StringReader reader = new StringReader(strr))
                    {
                        SVascoResp.Body body = (SVascoResp.Body)serializer.Deserialize(reader);

                        var resultCodes = body.AuthUserResponse.AuthUserResults.Results.ResultCodes;
                        ReturnCodeEnum = resultCodes.ReturnCodeEnum;
                        StatusCodeEnum = resultCodes.StatusCodeEnum;
                        SReturnCode = Convert.ToString(resultCodes.ReturnCode);
                        SStatusCode = Convert.ToString(resultCodes.StatusCode);

                        foreach (var error in body.AuthUserResponse.AuthUserResults.Results.ErrorStack.Errors)
                        {
                            ErrorCode = error.ErrorCode;
                            ErrorDesc = error.ErrorDesc;
                        }

                       var root = new Root
                        {
                            Results = new SVASCOJSON.Results
                            {
                                ResultCodes = new ResultCodes
                                {
                                    ReturnCodeEnum = ReturnCodeEnum,
                                    StatusCodeEnum = StatusCodeEnum,
                                    ReturnCode = SReturnCode,
                                    StatusCode = SStatusCode
                                },
                                ErrorStack = new ErrorStack
                                {
                                    Errors = new List<Error>
                    {
                        new Error
                        {
                            ErrorCode = ErrorCode,
                            ErrorDesc = ErrorDesc
                        }
                    }

                                }
                            }
                        };
                        jsonString = JsonConvert.SerializeObject(root, Newtonsoft.Json.Formatting.Indented);
                    }
                 

            }
                }

            

           return jsonString;
        }
        public string Vasco(string Req)
        {

            var root = string.Empty;
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
            string jsonString = string.Empty;
            string? ReturnCodeEnum = string.Empty;
            string? StatusCodeEnum = string.Empty;
            string? SReturnCode = string.Empty;
            string? SStatusCode = string.Empty;

            string? ErrorCode = string.Empty;
            string? ErrorDesc = string.Empty;

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

                        bAL.updateLog(soapResult, RequestID, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss:fff tt"));
                    }
                }
            }

            catch (Exception ex)
            {
                bAL.updateLog(ex.Message, RequestID, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss:fff tt"));
            }
            return soapResult;
        }


    }
}
