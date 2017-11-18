using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Assignment.Models
{
    public class Student
    {
        public int StudId { get; set; }
        public string StudName { get; set; }

        public Student()
        {
            StudId = 0;
            StudName = null;
        }

        public Student(int studId, string studName)
        {
            StudId = studId;
            StudName = studName;
        }
    }
}
