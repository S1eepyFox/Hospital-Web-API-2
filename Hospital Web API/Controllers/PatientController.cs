using Hospital_Web_API.Companion_object;
using Hospital_Web_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using static Hospital_Web_API.Error.ResponseError;

namespace Hospital_Web_API.Controllers
{
  
    public class PatientController : ApiController
    {
        // https://localhost:44323/api/patient/add?last_name=Горбунов&first_name=Иван &patronymic=Матвеевич&height=190&mass=33&age=77
        //Метод POST вносит в БД пациента 
        //Без проверки на дубликат записи
        //[ActionName("add")]
        //[HttpPost]
       
        [HttpPost]
        [Route("api/patient/add")]
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
           

            Response<string> response = await RequestMSSQL.AddPatient(last_name, first_name, patronymic, height, mass, age, massIndex);

            if (response.Status)
            {
                return Ok(response.Result);
            }
         
            return NotFound();

        }


    }
}