using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Xml;

namespace VascoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CVASCO : Controller
    {
        BLL bll = new BLL();

        [HttpPost]
        [Produces("application/xml")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult Post([FromBody] XElement xmlReq)
        {
            var XML = bll.Vasco(xmlReq.ToString());
            XmlDocument XDoc = new XmlDocument();
            var xmlDoc = new XmlDocument();


            // Create the header of the response


            // Create the response body

            xmlDoc.LoadXml(XML);


            // Add other elements to the response
            // ...

            return Content(xmlDoc.OuterXml, "text/xml");
        }
    }
}
