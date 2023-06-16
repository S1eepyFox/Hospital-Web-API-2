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



namespace Hospital_Web_API.Models
{
    public class RequestMSSQL
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static SqlConnection conn = new SqlConnection(connectionString);
        //Добавление данных пациента
        //без проверки на дубликат!
        async static public Task AddPatient(string last_name, string first_name, string patronymic, double height, double mass, int age, double BMI)
        {
            string sqlAdd = $"INSERT INTO Patients (last_name, first_name, patronymic, height, mass, age, BMI) " +
                             $"values('{last_name}', '{first_name}', '{patronymic}',{height},{mass},{age},{BMI})";
            SqlCommand commandAdd = new SqlCommand(sqlAdd, conn);

            if (conn.State == System.Data.ConnectionState.Closed)
                await conn.OpenAsync();

            commandAdd.ExecuteNonQuery();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

        }

        async static public Task<DataTable> StatisticsPatient() //Получает из базы данных статистику ИМТ всех пациентов
        {
            string sqlAdd = "EXEC StatisticsBMI";

            SqlCommand command = new SqlCommand(sqlAdd, conn);

            if (conn.State == System.Data.ConnectionState.Closed)
                await conn.OpenAsync();

            SqlDataReader dr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            return dt;
        }


        async static public Task<DataTable> StatisticsPatient_GroupAge() //Получает из базы данных статистику ИМТ всех пациентов, сгруппированных по возрасту
        {

            string sqlAdd = "EXEC StatisticsBMI_GroupedByAge;";

            SqlCommand command = new SqlCommand(sqlAdd, conn);

            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            SqlDataReader dr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }
    }
}