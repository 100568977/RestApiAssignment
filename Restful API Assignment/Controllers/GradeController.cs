using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restful_API_Assignment.Models;
using System.Data.SqlClient;

namespace Restful_API_Assignment.Controllers
{
    [Produces("application/json")]
    [Route("api/Grade")]
    public class GradeController : Controller
    {
        //private string connString = @"Server=tcp:100568977.database.windows.net,1433;Initial Catalog=Assessment3_100568977;Persist Security Info=False;User ID=KyleJarman;Password=4d3oh2NEE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string connString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = UniversityDB; Integrated Security = True; Connect Timeout = 60; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: api/Grade
        [HttpGet]
        public IEnumerable<Grade> Get()
        {
            return FindGrade();
        }


        // GET: api/Grade/5
        [HttpGet("{codeInput}", Name = "GetGrade")]
        public Grade Get(string codeInput)
        {
            Grade result = new Grade();

            foreach (var grade in FindGrade())
            {
                if (grade.Code == codeInput)
                {
                    result = grade;
                }

            }

            return result;

        }

        // DELETE api/Grade/5
        [HttpDelete("{code}")]
        public void DeleteGrade(string code)
        {
            SqlConnection sqlConnection = new SqlConnection(connString);

            try
            {
                sqlConnection.Open();
            }
            catch (SqlException)
            {
                throw;
            }

            SqlCommand sqlCommand = new SqlCommand("DELETE FROM Grade WHERE code = @Code", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@code", code));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Grade/5
        [HttpPut("{code}")]
        public void PutGrade(string code, [FromForm]string description)
        {
            SqlConnection sqlConnection = new SqlConnection(connString);

            try
            {
                sqlConnection.Open();
            }
            catch (SqlException)
            {
                throw;
            }

            SqlCommand sqlCommand = new SqlCommand("UPDATE Grade SET Description = @description WHERE Code = @code", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@description", description));
            sqlCommand.Parameters.Add(new SqlParameter("@code", code));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }



        // POST: api/Grade
        [HttpPost]
        public void Post
            ([FromForm] string code, [FromForm] string description)
        {
            SqlConnection sqlConnection = new SqlConnection(connString);

            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                throw;
            }

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Grade (code, description) VALUES (@code, @description)", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@code", code));
            sqlCommand.Parameters.Add(new SqlParameter("@description", description));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            sqlConnection.Close();
        }
        List<Grade> FindGrade()
        {
            SqlConnection sqlConnection = new SqlConnection(connString);

            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                throw;
            }

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Grade", sqlConnection);

            List<Grade> result = new List<Grade>();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Grade grade = new Grade(
                    sqlDataReader[0].ToString(),
                    sqlDataReader[1].ToString());

                result.Add(grade);
            }

            sqlConnection.Close();

            return result;
        }

    }
}



