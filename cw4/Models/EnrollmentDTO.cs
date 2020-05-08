using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace cw4.Models
{
    public class EnrollmentDTO
    {
        public int IdEnrollment { get; set; }
        public int IdStudy { get; set; }
        public int Semester { get; set; }
        public string StarDate { get; set; }

        

    }
}
