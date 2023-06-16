using Hospital_Web_API.Companion_object;
using Hospital_Web_API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;


namespace Hospital_Web_API.Controllers
{
    public class StatisticsController : ApiController
    {
        //https://localhost:44323/api/statistics
        //Метод GET возвращающий статистику ИМТ всех пациентов из БД
        [HttpGet]
        [Route("api/statistics")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await RequestMSSQL.StatisticsPatient());
        }

        //https://localhost:44323/api/statistics/group_age
        //Метод GET возвращающий статистику ИМТ всех пациентов из БД, сгруппированных по возрасту
        [HttpGet]
        [Route("api/statistics/group_age")]
        public async Task<IHttpActionResult> GetByAgeGroup()
        {
            return Ok(await RequestMSSQL.StatisticsPatient_GroupAge());
        }

    }
}