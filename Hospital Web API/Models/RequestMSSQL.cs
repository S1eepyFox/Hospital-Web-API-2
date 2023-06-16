using Hospital_Web_API.Companion_object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static Hospital_Web_API.Error.ResponseError;

namespace Hospital_Web_API.Models
{
    public class RequestMSSQL
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static SqlConnection conn = new SqlConnection(connectionString);
        //Добавление данных пациента
        //без проверки на дубликат!
        async static public Task<Response<string>> AddPatient(string last_name, string first_name, string patronymic, double height, double mass, int age, double BMI)
        {
            Response<string> response = new Response<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlAdd = $"INSERT INTO Patients (last_name, first_name, patronymic, height, mass, age, BMI) " +
                                 $"values('{last_name}', '{first_name}', '{patronymic}',{height},{mass},{age},{BMI})";

                    SqlCommand commandAdd = new SqlCommand(sqlAdd, conn);

                    if (conn.State == System.Data.ConnectionState.Closed)
                        await conn.OpenAsync();

                    commandAdd.ExecuteNonQuery();

                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();

                    response.Status = true;
                    response.Result = "Запись создана";

                    return response;
                }
            }
            catch (Exception e)
            {
                response.Status = false;
                response.Result = "";
                response.ErrorMessage = e.Message;

                return response;
            }
        }


        async static public Task<Response<DataTable>> StatisticsPatient()//Получает из базы данных статистику ИМТ всех пациентов
        {
            Response<DataTable> response = new Response<DataTable>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlAdd = "EXEC StatisticsBMI";

                    SqlCommand command = new SqlCommand(sqlAdd, conn);

                    if (conn.State == System.Data.ConnectionState.Closed)
                        await conn.OpenAsync();

                    SqlDataReader dr = await command.ExecuteReaderAsync();
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();

                    response.Status = true;
                    response.Result = dt;
                }

                return response;
            }
            catch (Exception e)
            {
                response.Status = false;
                response.Result = new DataTable();
                response.ErrorMessage = e.Message;

                return response;
            }
        }


        async static public Task<Response<DataTable>> StatisticsPatient_GroupAge() //Получает из базы данных статистику ИМТ всех пациентов, сгруппированных по возрасту
        {
            Response<DataTable> response = new Response<DataTable>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlAdd = "EXEC StatisticsBMI_GroupedByAge;";

                    SqlCommand command = new SqlCommand(sqlAdd, conn);

                    if (conn.State == System.Data.ConnectionState.Closed)
                        await conn.OpenAsync();

                    SqlDataReader dr = await command.ExecuteReaderAsync();
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();

                    response.Status = true;
                    response.Result = dt;
                }

                return response;
            }
            catch (Exception e)
            {
                response.Status = false;
                response.Result = new DataTable();
                response.ErrorMessage = e.Message;

                return response;
            }
        }
    }
}