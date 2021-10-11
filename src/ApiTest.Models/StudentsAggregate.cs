using ApiTest.Utils;
using ApiTest.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ApiTest.Models
{
    public class StudentsAggregate
    {
        public StudentsAggregate() { }

        public StudentsAggregate(List<Student> students)
        {
            AggregateStudents(students);
        }

        public string YourName => Constants.SUBMITTER_NAME;
        public string YourEmail => Constants.SUBMITTER_EMAIL;
        public int YearWithHighestAttendance { get; set; }
        public int YearWithHighestOverallGpa { get; set; }
        public List<int> Top10StudentIdsWithHighestGpa { get; set; }
        public int StudentIdMostInconsistent { get; set; }


        private void AggregateStudents(List<Student> students)
        {
            if (students.IsNullOrEmpty()) return;

            (int Year, int StudentsCount) attendanceTuple = (int.MaxValue, 0);
            (int Year, decimal OverallGpa) overallGpaTuple = (0, 0.0m);

            for (var i = students.Min(s => s.StartYear); i <= students.Max(s => s.EndYear); i++)
            {
                var filteredStudents = students.Where(s => s.StartYear <= i && s.EndYear >= i).ToList();

                // calculate the year which saw the highest attendance
                var studentsCount = filteredStudents.Count;
                if (studentsCount > attendanceTuple.StudentsCount || (studentsCount == attendanceTuple.StudentsCount && i < attendanceTuple.Year))
                {
                    attendanceTuple = (i, studentsCount);
                }


                // calculate the year with highest overall GPA
                var overallGpa = filteredStudents.Sum(s => s.GPARecord[i - s.StartYear]) / filteredStudents.Count;
                if (overallGpa > overallGpaTuple.OverallGpa)
                {
                    overallGpaTuple = (i, overallGpa);
                }
            }

            this.YearWithHighestAttendance = attendanceTuple.Year;
            this.YearWithHighestOverallGpa = overallGpaTuple.Year;

            this.Top10StudentIdsWithHighestGpa = GetTop10HighestGpaStudents(students);
            this.StudentIdMostInconsistent = GetMostInconsistentStudentId(students);
        }

        // expose separate aggregation methods
        public static int GetHighestAttendanceYear(List<Student> students)
        {
            if (students.IsNullOrEmpty()) return 0;

            (int Year, int StudentsCount) attendanceTuple = (int.MaxValue, 0);

            for (var i = students.Min(s => s.StartYear); i <= students.Max(s => s.EndYear); i++)
            {
                var count = students.Count(s => s.StartYear <= i && s.EndYear >= i);
                if (count > attendanceTuple.StudentsCount || (count == attendanceTuple.StudentsCount && i < attendanceTuple.Year))
                {
                    attendanceTuple = (i, count);
                }
            }

            return attendanceTuple.Year;
        }

        public static int GetHighestOverallGpaYear(List<Student> students)
        {
            if (students.IsNullOrEmpty()) return 0;

            (int Year, decimal OverallGpa) overallGpaTuple = (0, 0.0m);

            for (var i = students.Min(s => s.StartYear); i <= students.Max(s => s.EndYear); i++)
            {
                var filteredStudents = students.Where(s => s.StartYear <= i && s.EndYear >= i).ToList();
                var overallGpa = filteredStudents.Sum(s => s.GPARecord[i - s.StartYear]) / filteredStudents.Count;

                if (overallGpa > overallGpaTuple.OverallGpa)
                {
                    overallGpaTuple = (i, overallGpa);
                }
            }

            return overallGpaTuple.Year;
        }

        public static List<int> GetTop10HighestGpaStudents(List<Student> students)
        {
            return students.IsNullOrEmpty() ? null : students.OrderByDescending(s => s.GPARecord.Average()).Take(10).Select(s => s.Id).ToList();
        }

        public static int GetMostInconsistentStudentId(List<Student> students)
        {
            if (students.IsNullOrEmpty()) return 0;

            var foundStudent = students.Aggregate((i, j) =>
                (i.GPARecord.Max() - i.GPARecord.Min()) > (j.GPARecord.Max() - j.GPARecord.Min()) ? i : j);

            return foundStudent.Id;
        }

        //public struct AttendanceGpa
        //{
        //    public AttendanceGpa(int attendance, decimal overallGpa)
        //    {
        //        Attendance = attendance;
        //        OverallGpa = overallGpa;
        //    }

        //    public int Attendance { get; set; }
        //    public decimal OverallGpa { get; set; }
        //}


    }
}
