using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Models
{
    public class StudentInfoDTO
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public DateTime birthDate { get; set; }
        public String name { get; set; }
        public int semester { get; set; }
    }
}
