using cw4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;

namespace cw4.Services
{
    public class EnrollStudentDbService : IEnrollStudentDbService
    {
        public EnrollmentDTO EnrollStudent(EnrollStudentDTO enrollStudentDTO)
        {
            string connectionString = "Data Source=db-mssql;Initial Catalog=s17417;Integrated Security=True";
            EnrollmentDTO enrollment;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {

                    int IdStudy;
               
                connection.Open();
                
                var transaction=connection.BeginTransaction();
                 try
                 {
                    // enrollment = new EnrollmentDTO();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    
                    command.CommandText = "Select IdStudy from Studies where name=@Name";
                    command.Parameters.AddWithValue("Name", enrollStudentDTO.Studies);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    
                    if (!reader.HasRows)return null;
                    IdStudy = reader.GetInt32(reader.GetOrdinal("IdStudy"));
                    reader.Close();
                    /*enrollment.Semester = 1;
                    enrollment.IdEnrollment = 1;
                    enrollment.IdStudy = 1;
                    enrollment.StarDate = DateTime.Now.ToString();
                    return enrollment;*/
                    command.CommandText = "Select * from Enrollment where Semester=1 AND IdStudy="+IdStudy;
                    reader = command.ExecuteReader();
                    reader.Read();
                    
                    enrollment = new EnrollmentDTO();
                    if (reader.HasRows)
                    {
                        enrollment.IdEnrollment = reader.GetInt32(reader.GetOrdinal("IdEnrollment"));
                        enrollment.Semester = reader.GetInt32(reader.GetOrdinal("Semester"));
                        enrollment.StarDate = reader["StartDate"].ToString();
                        enrollment.IdStudy = reader.GetInt32(reader.GetOrdinal("IdEnrollment"));
                        reader.Close();
                    }
                    else
                    {
                        enrollment.IdStudy = IdStudy;
                        enrollment.Semester = 1;
                        enrollment.StarDate = DateTime.Now.ToString();
                        command.CommandText = "insert into Enrollment (idEnrollment, Semester,IdStudy,StartDate)" +
                                                   "values((Select Max(IdEnrollment) + 1 from Enrollment),"+enrollment.Semester+"," + enrollment.IdStudy + ","+enrollment.StarDate+")";
                        command.ExecuteNonQuery();
                        command.CommandText = "Select IdEnrollment,StartDate from Enrollment where " + enrollment.Semester + " AND IdStudy=" + enrollment.IdStudy;
                        //reader = command.ExecuteReader();
                        reader.Read();
                        
                        enrollment.IdEnrollment = reader.GetInt32(reader.GetOrdinal("IdEnrollment"));
                        enrollment.StarDate = reader["StartDate"].ToString();
                        reader.Close();
                    }

                    command.CommandText = "select 1 from Student where IndexNumber=@IndexNumber";
                    command.Parameters.AddWithValue("IndexNumber", enrollStudentDTO.IndexNumber);
                    reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows) 
                    {
                        reader.Close();
                        transaction.Rollback();
                        return null;
                    }
                    reader.Close();
                    command.CommandText = "Insert into Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment)" +
                                            "values(@IndexNumber, @FirstName,@LastName,@BirthDate,"+enrollment.IdEnrollment+")";
                    command.Parameters.AddWithValue("FirstName", enrollStudentDTO.FirstName);
                    command.Parameters.AddWithValue("LastName", enrollStudentDTO.LastName);
                    command.Parameters.AddWithValue("BirthDate", enrollStudentDTO.BirthDate);
                    command.ExecuteNonQuery();

                    

                } catch(SqlException e)
                {
                    transaction.Rollback();
                    return null;
                }
                
                transaction.Commit();
               
                return enrollment;
            }
        }

        public void PromoteStudents(string studies, int semester)
        {
            throw new NotImplementedException();
        }
    }
}
