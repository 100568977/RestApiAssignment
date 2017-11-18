using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Assignment.Models
{
    public class Lecturer
    {
    public int LectId { get; set; }
    public string LectName { get; set; }

    public Lecturer()
    {
        LectId = 0;
        LectName = null;
    }

    public Lecturer(int lectId, string lectName)
    {
        LectId = lectId;
        LectName = lectName;
    }
}
}
