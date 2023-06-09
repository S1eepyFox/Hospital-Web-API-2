using Hospital_Web_API.Companion_object;
using Hospital_Web_API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;


namespace Hospital_Web_API.Controllers
{
    [RoutePrefix("api/statistics")]
    public class StatisticsController : ApiController

    {  ////  https://localhost:44323/api/statistics/group_age
        [ActionName("group_age_statistics")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGroup_Age()
        {
            return Ok(await RequestMSSQL.StatisticsPatient_GroupAge());
        }


        //// https://localhost:44323/api/statistics/
        // [ActionName("statistics")]
        // [HttpGet]
        // public async Task<IHttpActionResult> GetStatistics()
        // {
        //     return Ok(await RequestMSSQL.StatisticsPatient());
        // }
    }
}