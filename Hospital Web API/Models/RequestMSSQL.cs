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

        async static public Task AddPatient(string last_name, string first_name, string patronymic, double height, double mass, int age, double BMI)
        {
            string sqlAdd = $"INSERT INTO Patients (last_name, first_name, patronymic, height, mass, age, BMI) values('{last_name}', '{first_name}', '{patronymic}',{height},{mass},{age},{BMI})";
            SqlCommand commandAdd = new SqlCommand(sqlAdd, conn);

            if (conn.State == System.Data.ConnectionState.Closed)
                await conn.OpenAsync();

            commandAdd.ExecuteNonQuery();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

        }

        async static public Task<BodyMassIndex[]> StatisticsPatient()
        {



            string sqlAdd = "" +
            "create table #Statistics (_BMI INT,_Descript NVARCHAR(60))" +
                "INSERT INTO #Statistics (_BMI, _Descript) VALUES (ROUND((SELECT SUM (CASE WHEN BMI < 16 THEN 1.0 ELSE 0 END )FROM Patients)/ ( SELECT COUNT(*) FROM Patients )*100 , 0 ),'Выраженный дефицит массы тела')," +
                "(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 16.1 AND 18.5 THEN 1.0 ELSE 0 END)FROM Patients) / ( SELECT COUNT(*) FROM Patients)*100, 0 ),'Недостаточная (дефицит) масса тела')," +
                "(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 18.6 AND 25 THEN 1.0 ELSE 0 END) FROM Patients) / ( SELECT COUNT(*) FROM Patients)*100, 0 ),'Норма')," +
                "(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 25.1 AND 30 THEN 1.0 ELSE 0 END)FROM Patients) / ( SELECT COUNT(*) FROM Patients)*100, 0 ),'Избыточная масса тела (предожирение)')," +
                "(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 30.1 AND 35 THEN 1.0 ELSE 0 END) FROM Patients)/ ( SELECT COUNT(*) FROM Patients)*100  , 0 ),'Ожирение 1 степени')," +
                "(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 35.1 AND 40 THEN 1.0 ELSE 0 END) FROM Patients)/ ( SELECT COUNT(*) FROM Patients)*100 , 0 ),'Ожирение 2 степени')," +
                "(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 40.1 AND 100 THEN 1.0 ELSE 0 END) FROM Patients)/ ( SELECT COUNT(*) FROM Patients)*100, 0 ),'Ожирение 3 степени')" +
                "SELECT * FROM #Statistics ORDER BY _BMI DESC ;" +
                "DROP TABLE #Statistics";


            SqlCommand command = new SqlCommand(sqlAdd, conn);

            if (conn.State == System.Data.ConnectionState.Closed)
                await conn.OpenAsync();

            //SqlCommand myCommand = new SqlCommand("select * FROM Patients", conn);
            SqlDataReader dr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);


            string s = dt.Rows[0][0].ToString();
            double d = Convert.ToDouble(s);
            BodyMassIndex[] bodyMassIndex = new BodyMassIndex[7];


            for (int i = 0; i < bodyMassIndex.Length; i++)
            {
                bodyMassIndex[i] = new BodyMassIndex()
                {
                    BMI = Convert.ToDouble(dt.Rows[i][0].ToString()),
                    Description = dt.Rows[i][1].ToString().Replace(" ", "")
                };
            };




            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            return bodyMassIndex;
        }


        async static public Task<DataTable> StatisticsPatient_GroupAge()
        {

            string sqlAdd = "EXEC StatisticsAgeBMI;";


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