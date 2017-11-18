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
    [Route("api/Student")]
    public class StudentController : Controller
    {
        //private string connString = @"Server=tcp:100568977.database.windows.net,1433;Initial Catalog=Assessment3_100568977;Persist Security Info=False;User ID=KyleJarman;Password=4d3oh2NEE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string connString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = UniversityDB; Integrated Security = True; Connect Timeout = 60; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: api/Student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return FindStudent();
        }



        // GET: api/Student/5
        [HttpGet("{studId}", Name = "GetStudent")]
        public Student Get(int studId)
        {
            Student result = new Student();

            foreach (var student in FindStudent())
            {
                if (student.StudId == studId)
                {
                    result = student;
                }

            }

            return result;

        }

        // DELETE api/Student/5
        [HttpDelete("{studId}")]
        public void DeleteStudent(int studId)
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

            SqlCommand sqlCommand = new SqlCommand("DELETE FROM Student WHERE studId = @StudId", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@studId", studId));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Student/5
        [HttpPut("{studId}")]
        public void PutStudent(int studId, [FromForm]string studName)
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

            SqlCommand sqlCommand = new SqlCommand("UPDATE Student SET StudName = @studName WHERE StudId = @studId", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@studName", studName));
            sqlCommand.Parameters.Add(new SqlParameter("@studId", studId));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }



        // POST: api/Student
        [HttpPost]
        public void Post
            ([FromForm] int studId, [FromForm] string studName)
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

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Student (studId, studName) VALUES (@studId, @studName)", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@studId", studId));
            sqlCommand.Parameters.Add(new SqlParameter("@studName", studName));

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
        List<Student> FindStudent()
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

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Student", sqlConnection);

            List<Student> result = new List<Student>();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Student student = new Student(
                    Int32.Parse(sqlDataReader[0].ToString()),
                    sqlDataReader[1].ToString());

                result.Add(student);
            }

            sqlConnection.Close();

            return result;
        }

    }
}



