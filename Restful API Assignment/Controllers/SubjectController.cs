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
    [Route("api/Subject")]
    public class SubjectController : Controller
    {
        //private string connString = @"Server=tcp:100568977.database.windows.net,1433;Initial Catalog=Assessment3_100568977;Persist Security Info=False;User ID=KyleJarman;Password=4d3oh2NEE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string connString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = UniversityDB; Integrated Security = True; Connect Timeout = 60; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: api/Subject
        [HttpGet]
        public IEnumerable<Subject> Get()
        {
            return FindSubject();
        }



        // GET: api/Subject/5
        [HttpGet("{subjectCode}", Name = "GetSubject")]
        public Subject Get(string subjectCode)
        {
            Subject result = new Subject();

            foreach (var subject in FindSubject())
            {
                if (subject.SubCode == subjectCode)
                {
                    result = subject;
                }

            }

            return result;

        }

        // DELETE api/Subject/5
        [HttpDelete("{subCode}")]
        public void DeleteSubject(string subCode)
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

            SqlCommand sqlCommand = new SqlCommand("DELETE FROM Subject WHERE subCode = @SubCode", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@subCode", subCode));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Subject/5
        [HttpPut("{subCode}")]
        public void PutSubject(string subCode, [FromForm]string subTitle)
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

            SqlCommand sqlCommand = new SqlCommand("UPDATE Subject SET SubTitle = @subTitle WHERE SubCode = @subCode", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@subTitle", subTitle));
            sqlCommand.Parameters.Add(new SqlParameter("@subCode", subCode));

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }



        // POST: api/Subject
        [HttpPost]
        public void Post
            ([FromForm] string subCode, [FromForm] string subTitle)
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

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Subject (subCode, subTitle) VALUES (@subCode, @subTitle)", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@subCode", subCode));
            sqlCommand.Parameters.Add(new SqlParameter("@subTitle", subTitle));

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
        List<Subject> FindSubject()
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

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Subject", sqlConnection);

            List<Subject> result = new List<Subject>();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Subject subject = new Subject (
                    sqlDataReader[0].ToString(),
                    sqlDataReader[1].ToString());

                result.Add(subject);
            }

            sqlConnection.Close();

            return result;
        }

    }
}



