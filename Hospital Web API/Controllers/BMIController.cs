using Hospital_Web_API.Models;
using System.Web.Http;


namespace Hospital_Web_API.Controllers
{

    [RoutePrefix("api/bmi")]
    public class BMIController : ApiController
    {

        //  https://localhost:44323/api/bmi?mass=70&height=170    
        [ActionName("bmi")]
        [HttpGet]
        public IHttpActionResult GetBMI(int mass, int height)
        {
            bool checkMass = DataExistenceCheck.GetMass(mass);
            bool checkHeight = DataExistenceCheck.GetHeight(height);

            if (checkMass || checkHeight)
            {
                return NotFound();
            }

            BodyMassIndex bmi = CalculationPatientData.CalculationBMI(mass, height); // Расчет индекса массы тела

            return Ok(bmi);
        }

      
    }
}