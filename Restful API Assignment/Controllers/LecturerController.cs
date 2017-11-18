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
    [Route("api/Lecturer")]
    public class LecturerController : Controller
    {
        //private string connString = @"Server=tcp:100568977.database.windows.net,1433;Initial Catalog=Assessment3_100568977;Persist Security Info=False;User ID=KyleJarman;Password=4d3oh2NEE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string connString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = UniversityDB; Integrated Security = True; Connect Timeout = 60; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: api/Lecturer
        [HttpGet]
        public IEnumerable<Lecturer> Get()
        {
            return FindLecturer();
        }



        // GET: api/Lecturer/5
        [HttpGet("{lectId}", Name = "GetLecturer")]
        public Lecturer Get(int lectId)
        {
            Lecturer result = new Lecturer();

            foreach (var lecturer in FindLecturer())
            {
                if (lecturer.LectId == lectId)
                {
                    result = lecturer;
                }

            }

            return result;

        }

        // DELETE api/Lecturer/5
        [HttpDelete("{lectId}")]
        public void DeleteLecturer(int lectId)
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

            SqlCommand sqlCommand = new SqlCommand("DELETE FROM Lecturer WHERE lectId = @LectId", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@lectId", lectId));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Lecturer/5
        [HttpPut("{lectId}")]
        public void PutLecturer(int lectId, [FromForm]string lectName)
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

            SqlCommand sqlCommand = new SqlCommand("UPDATE Lecturer SET LectName = @lectName WHERE LectId = @lectId", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@lectName", lectName));
            sqlCommand.Parameters.Add(new SqlParameter("@lectId", lectId));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }



        // POST: api/Lecturer
        [HttpPost]
        public void Post
            ([FromForm] int lectId, [FromForm] string lectName)
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

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Lecturer (lectId, lectName) VALUES (@lectId, @lectName)", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@lectId", lectId));
            sqlCommand.Parameters.Add(new SqlParameter("@lectName", lectName));

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
        List<Lecturer> FindLecturer()
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

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Lecturer", sqlConnection);

            List<Lecturer> result = new List<Lecturer>();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Lecturer lecturer = new Lecturer(
                    Int32.Parse(sqlDataReader[0].ToString()),
                    sqlDataReader[1].ToString());

                result.Add(lecturer);
            }

            sqlConnection.Close();

            return result;
        }

    }
}



