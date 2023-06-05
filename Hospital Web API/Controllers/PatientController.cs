using Hospital_Web_API.Companion_object;
using Hospital_Web_API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;


namespace Hospital_Web_API.Controllers
{
    [RoutePrefix("api/patient")]
    public class PatientController : ApiController
    {
       
        [ActionName("add")]
        [HttpPost]
        public async Task<IHttpActionResult> PostAdd(string last_name, string first_name, string patronymic, double height, double mass, int age)
        {

            bool checkMass = DataExistenceCheck.GetMass(mass);
            bool checkHeight = DataExistenceCheck.GetHeight(height);
            bool checkAge = DataExistenceCheck.GetAge(age);

            if (checkMass || checkHeight || checkAge)
            {
                return NotFound();
            }

            double massIndex = CalculationPatientData.CalculationBMI(mass, height).BMI;

            await RequestMSSQL.AddPatient(last_name, first_name, patronymic, height, mass, age, massIndex);

            return Ok();
        }

        [ActionName("statistics")]
        [HttpGet]
        public async Task<IHttpActionResult> PostStatistics()
        {
            BodyMassIndex[] bodyMassIndex = await RequestMSSQL.StatisticsPatient();

            return Ok(bodyMassIndex);
        }

    }
}