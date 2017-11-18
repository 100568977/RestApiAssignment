using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Assignment.Models
{
    public class Grade
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public Grade()
            {
                Code = null;
                Description = null;
            }

        public Grade(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
