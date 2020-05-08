using Microsoft.AspNetCore.Mvc;
using cw4.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace cw4.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //IDbService sciagnac z porzedniech zajec

        string conString = "Data Source=db-mssql;Initial Catalog=s17417;Integrated Security=True";
        List<StudentInfoDTO> list = new List<StudentInfoDTO>();

        //zad 4.1 i 4.2
       // [HttpGet]
       /* public IActionResult GetStudent() 
        {
           
            using (SqlConnection con=new SqlConnection(conString))
                using (SqlCommand request=new SqlCommand()){
                request.Connection=con;
                request.CommandText = "select FirstName, LastName, BirthDate, Name, Semester from Studies" +
                    " left join Enrollment on Studies.IdStudy = Enrollment.IdStudy" +
                    " left join Student on Enrollment.IdEnrollment = Student.IdEnrollment";
                con.Open();
                var reader = request.ExecuteReader();
               

                while (reader.Read())
                {
                    StudentInfoDTO student = new StudentInfoDTO();
                    student.firstName = reader["FirstName"].ToString();
                    student.lastName = reader["LastName"].ToString();
                    student.birthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
                    student.name = reader["Name"].ToString();
                    student.semester = reader.GetInt32(reader.GetOrdinal("Semester"));
                    list.Add(student);
                }
            }  
            return Ok(list);
        }


        //zad 4.3
        /*[HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {

            using SqlConnection con = new SqlConnection(conString);
            using SqlCommand request = new SqlCommand();
            request.Connection = con;
            request.CommandText = $"select FirstName, LastName, BirthDate, Name, Semester from Studies" +
                $" left join Enrollment on Studies.IdStudy = Enrollment.IdStudy" +
                $" left join Student on Enrollment.IdEnrollment = Student.IdEnrollment Where Student.IndexNumber='{id}'";
            con.Open();
            var reader = request.ExecuteReader();


            reader.Read();
            if (!reader.HasRows) return NotFound("Nie znaleziono studenta o takim indeksie");
            StudentInfoDTO student = new StudentInfoDTO();

            student.firstName = reader["FirstName"].ToString();
            student.lastName = reader["LastName"].ToString();
            student.birthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
            student.name = reader["Name"].ToString();
            student.semester = reader.GetInt32(reader.GetOrdinal("Semester"));
            return Ok(student);
        }*/

        //zad 4.4 Żeby przeprowadzic SQL Injcection w procedurze powyzej zapytanie powinno przyjac strukture
        //https://localhost:44304/api/students/s11112';DROP TABLE student;SELECT '1
        

        //zad4.5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetStudent(string id)
        {
            
           
            using SqlConnection con = new SqlConnection(conString);
            using SqlCommand request = new SqlCommand();
            request.Connection = con;
            request.CommandText = "select FirstName, LastName, BirthDate, Name, Semester from Studies" +
                " left join Enrollment on Studies.IdStudy = Enrollment.IdStudy" +
                " left join Student on Enrollment.IdEnrollment = Student.IdEnrollment Where Student.IndexNumber=@id";
            request.Parameters.AddWithValue("id", id);
            con.Open();
            var reader = request.ExecuteReader();


            reader.Read();
            if (!reader.HasRows) return NotFound("Nie znaleziono studenta o takim indeksie");
            StudentInfoDTO student = new StudentInfoDTO();

            student.firstName = reader["FirstName"].ToString();
            student.lastName = reader["LastName"].ToString();
            student.birthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
            student.name = reader["Name"].ToString();
            student.semester = reader.GetInt32(reader.GetOrdinal("Semester"));
            return Ok(student);
        }
    }
}