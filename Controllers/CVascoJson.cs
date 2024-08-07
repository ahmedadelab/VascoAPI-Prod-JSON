using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Mime;
using static VascoAPI.SVASCOJSON;

namespace VascoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CVascoJson : Controller
    {

        BLL DLLCOde = new BLL();
        [HttpPost]

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]

        [ProducesResponseType(typeof(SVASCOJSON.Root), 200)]
        public ActionResult<string> OnGet([FromBody] CredentialRequest request)
        {

            DataTable Dtattribute = new DataTable("credentialAttributeSet");
            Dtattribute.Columns.Add(new DataColumn("value"));
            Dtattribute.Columns.Add(new DataColumn("attributeID"));

            foreach (var attribute in request.CredentialAttributeSet)
            {
                DataRow Drattribute = Dtattribute.NewRow();
                Drattribute["attributeID"] = attribute.AttributeID;
                Drattribute["Value"] = attribute.Value;
                Dtattribute.Rows.Add(Drattribute);
              
            }
                return Content(DLLCOde.VascoJson(Dtattribute), "application/json");
        }
    }
}
