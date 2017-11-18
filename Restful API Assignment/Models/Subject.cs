using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Assignment.Models
{
    public class Subject
    {
        public string SubCode { get; set;}
        public string SubTitle { get; set; }

        public Subject()
        {
            SubCode = null;
            SubTitle = null;
        }

        public Subject(string subCode, string subTitle)
        {
            SubCode = subCode;
            SubTitle = subTitle;
        }
    }
}
