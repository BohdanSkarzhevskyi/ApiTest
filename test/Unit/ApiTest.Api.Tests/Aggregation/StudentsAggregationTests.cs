using ApiTest.Models;
using System.Collections.Generic;
using Xunit;

namespace ApiTest.Api.Tests.Aggregation
{
    public class StudentsAggregationTests
    {
        [Fact]
        public void AggregateStudents_Test()
        {
            // Arrange

            // Act
            var studentsAggregation = new StudentsAggregate(_testStudents);

            // Assert
            Assert.Equal(2012, studentsAggregation.YearWithHighestAttendance);
            Assert.Equal(2008, studentsAggregation.YearWithHighestOverallGpa);
            Assert.Equal(10, studentsAggregation.Top10StudentIdsWithHighestGpa.Count);
            Assert.Equal(6, studentsAggregation.StudentIdMostInconsistent);
        }

        [Fact]
        public void AggregateStudents_WithNullInput_Test()
        {
            // Arrange

            // Act
            var studentsAggregation = new StudentsAggregate(null);

            // Assert
            Assert.Equal(0, studentsAggregation.YearWithHighestAttendance);
            Assert.Equal(0, studentsAggregation.YearWithHighestOverallGpa);
            Assert.Null(studentsAggregation.Top10StudentIdsWithHighestGpa);
            Assert.Equal(0, studentsAggregation.StudentIdMostInconsistent);
        }

        [Fact]
        public void CalculateHighestAttendanceYear_Test()
        {
            // Arrange

            // Act
            int year = StudentsAggregate.GetHighestAttendanceYear(_testStudents);

            // Assert
            Assert.Equal(2012, year);
        }

        [Fact]
        public void CalculateHighestAttendanceYear_WithNullInput_Test()
        {
            // Arrange

            // Act
            int year = StudentsAggregate.GetHighestAttendanceYear(null);

            // Assert
            Assert.Equal(0, year);
        }

        [Fact]
        public void GetHighestOverallGPAYear_Test()
        {
            // Arrange

            // Act
            int year = StudentsAggregate.GetHighestOverallGpaYear(_testStudents);

            // Assert
            Assert.Equal(2008, year);
        }

        [Fact]
        public void GetHighestOverallGPAYear_WithNullInput_Test()
        {
            // Arrange

            // Act
            int year = StudentsAggregate.GetHighestOverallGpaYear(null);

            // Assert
            Assert.Equal(0, year);
        }

        [Fact]
        public void GetTop10HighestGpaStudents_Test()
        {
            // Arrange

            // Act
            var ids = StudentsAggregate.GetTop10HighestGpaStudents(_testStudents);

            // Assert
            Assert.Equal(10, ids.Count);
        }

        [Fact]
        public void GetTop10HighestGpaStudents_WithNullInput_Test()
        {
            // Arrange

            // Act
            var ids = StudentsAggregate.GetTop10HighestGpaStudents(null);

            // Assert
            Assert.Null(ids);
        }

        [Fact]
        public void GetMostInconsistentStudentId_Test()
        {
            // Arrange

            // Act
            var id = StudentsAggregate.GetMostInconsistentStudentId(_testStudents);

            // Assert
            Assert.Equal(6, id);
        }

        [Fact]
        public void GetMostInconsistentStudentId_WithNullInput_Test()
        {
            // Arrange

            // Act
            var id = StudentsAggregate.GetMostInconsistentStudentId(null);

            // Assert
            Assert.Equal(0, id);
        }

        private readonly List<Student> _testStudents;
        public StudentsAggregationTests()
        {
            _testStudents = new List<Student>
            {
                new()
                {
                    Id = 1,
                    Name = "Test1",
                    StartYear = 2013,
                    EndYear = 2016,
                    GPARecord = new List<decimal>
                    {
                        3.4m,
                        2.1m,
                        1.2m,
                        3.6m
                    }
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    StartYear = 2010,
                    EndYear = 2013,
                    GPARecord = new List<decimal>
                    {
                        3.3m,
                        2.3m,
                        1.1m,
                        3.7m
                    }
                },
                new()
                {
                    Id = 3,
                    Name = "Test3",
                    StartYear = 2010,
                    EndYear = 2012,
                    GPARecord = new List<decimal>
                    {
                        2.3m,
                        2.5m,
                        2.8m
                    }
                },
                new()
                {
                    Id = 4,
                    Name = "Test4",
                    StartYear = 2013,
                    EndYear = 2016,
                    GPARecord = new List<decimal>
                    {
                        3.6m,
                        2.9m,
                        3.4m,
                        3.7m
                    }
                },
                new()
                {
                    Id = 5,
                    Name = "Test5",
                    StartYear = 2012,
                    EndYear = 2015,
                    GPARecord = new List<decimal>
                    {
                        3.3m,
                        2.5m,
                        1.1m,
                        3.7m
                    }
                },
                new()
                {
                    Id = 6,
                    Name = "Test6",
                    StartYear = 2011,
                    EndYear = 2014,
                    GPARecord = new List<decimal>
                    {
                        3.8m,
                        2.7m,
                        1.1m,
                        3.7m
                    }
                },
                new()
                {
                    Id = 7,
                    Name = "Test7",
                    StartYear = 2009,
                    EndYear = 2012,
                    GPARecord = new List<decimal>
                    {
                        3.1m,
                        2.1m,
                        1.1m,
                        3.7m
                    }
                },
                new()
                {
                    Id = 8,
                    Name = "Test8",
                    StartYear = 2009,
                    EndYear = 2011,
                    GPARecord = new List<decimal>
                    {
                        3.6m,
                        2.2m,
                        1.1m
                    }
                },
                new()
                {
                    Id = 9,
                    Name = "Test9",
                    StartYear = 2015,
                    EndYear = 2016,
                    GPARecord = new List<decimal>
                    {
                        3.3m,
                        2.3m
                    }
                },
                new()
                {
                    Id = 10,
                    Name = "Test10",
                    StartYear = 2016,
                    EndYear = 2016,
                    GPARecord = new List<decimal>
                    {
                        3.3m
                    }
                },
                new()
                {
                    Id = 11,
                    Name = "Test11",
                    StartYear = 2012,
                    EndYear = 2015,
                    GPARecord = new List<decimal>
                    {
                        3.6m,
                        2.4m,
                        2.3m,
                        3.7m
                    }
                },
                new()
                {
                    Id = 12,
                    Name = "Test12",
                    StartYear = 2008,
                    EndYear = 2012,
                    GPARecord = new List<decimal>
                    {
                        3.3m,
                        2.7m,
                        1.1m,
                        3.7m,
                        2.4m
                    }
                }
            };
        }
    }
}
