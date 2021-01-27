using CW_10.DTOs.Request;
using CW_10.DTOs.Response;
using CW_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW_10.Services
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public Student EditStudent(Student student, string id);

        public Student DeleteStudent(string id);
        public Student CreateStudent(Student student);
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        StudentPromotionResponse StudentPromotion(StudentPromotionRequest request);
    }
}
