using cw4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Services
{
    public interface IEnrollStudentDbService
    {
        public EnrollmentDTO EnrollStudent(EnrollStudentDTO enrollStudentDTO);

        public void PromoteStudents(string studies, int semester);
    }
}
