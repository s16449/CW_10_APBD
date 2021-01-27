using CW_10.DTOs.Request;
using CW_10.DTOs.Response;
using CW_10.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CW_10.Services
{
    public class SqlDbService : IDbService
    {
        public s16449Context _context;

        public SqlDbService(s16449Context context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetStudents()
        {
            return _context.Student.ToList();
        }

     public Student CreateStudent(Student student)
        {
            _context.Student.Add(student);
            _context.SaveChanges();
            return _context.Student.Single(s => s.IndexNumber.Equals(student.IndexNumber));
        }

        public Student EditStudent(Student student, string id)
        {
            var editedSt = new Student
            {
                IndexNumber = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                IdEnrollment = student.IdEnrollment,
                Passwd = student.Passwd
            };

            _context.Attach(editedSt);
            _context.Entry(editedSt).Property("FirstName").IsModified = true;
            _context.Entry(editedSt).Property("LastName").IsModified = true;
            _context.Entry(editedSt).Property("BirthDate").IsModified = true;
            _context.Entry(editedSt).Property("Passwd").IsModified = true;
            _context.SaveChanges();
                return editedSt;
                       
        }

        public Student DeleteStudent(string id)
        {
            var studentDel = _context.Student.Single(s => s.IndexNumber.Equals(id));
            _context.Student.Remove(studentDel);
            _context.SaveChanges();
            return studentDel;
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response = new EnrollStudentResponse();
            Enrollment enrollment = new Enrollment();
            DateTime startDateTime;

            response.IndexNumber = request.IndexNumber;

            var studies = _context.Studies.Where(s => s.Name.Equals(request.Name));

            if(studies.Count() ==0)
            {
                response.answer = "Brak studiów";
                return response;

            }

            int idStudies = studies.First().IdStudy;

            var enroll = _context.Enrollment.Where(e => e.IdStudy == idStudies).Where(e => e.Semester == 1).OrderBy(e => e.StartDate);

            var newEnroll = _context.Enrollment.OrderBy(e => e.IdEnrollment).Last().IdEnrollment;
            int newEnrollId = newEnroll + 1;

            if(enroll.Count() == 0)
            {
                response.answer = "Brak rekrutacji";
                startDateTime = DateTime.Now;

                var enrolTemp = new Enrollment
                {
                    IdEnrollment = newEnrollId,
                    Semester = 1,
                    IdStudy = idStudies,
                    StartDate = startDateTime

                };

                _context.Enrollment.Add(enrolTemp);
                _context.SaveChanges();

            }
            else
            {
                newEnrollId = enroll.Single().IdEnrollment;
                startDateTime = enroll.Single().StartDate;
            }

            enrollment.IdEnrollment = newEnrollId;
            enrollment.Semester = 1;
            enrollment.IdStudy = idStudies;
            enrollment.StartDate = startDateTime;

            response.enrollment = enrollment;

            var stud = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = newEnrollId,
            };

            _context.Student.Add(stud);
            try
            {
                _context.SaveChanges();
            }catch(Exception e)
            {
                response.answer = "Student jest juz w bazie";
            }
            
            return response;



        }

        public StudentPromotionResponse StudentPromotion(StudentPromotionRequest request)
        {
            StudentPromotionResponse response = new StudentPromotionResponse();

            var studies = _context.Studies.Where(s => s.Name.Equals(request.Name));

            if(studies.Count() == 0)
            {
                response.answer = "Brak studiów w bazie";
                return response;
            }

            int idStudies = studies.First().IdStudy;

            Enrollment enrollment = new Enrollment();

            var enroll = _context.Enrollment.Where(e => e.IdStudy.Equals(idStudies)).Where(e => e.Semester.Equals(request.Semester + 1));

            enrollment.IdEnrollment = enroll.Single().IdEnrollment;
            enrollment.IdStudy = enroll.Single().IdStudy;
            enrollment.Semester = enroll.Single().Semester;
            enrollment.StartDate = enroll.Single().StartDate;

            response.enrollment = enrollment;

            return response;
        }
    }
}
