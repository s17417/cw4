using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw4.Models;

namespace cw4.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
