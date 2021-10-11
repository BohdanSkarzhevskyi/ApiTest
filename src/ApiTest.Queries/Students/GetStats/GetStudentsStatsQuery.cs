using ApiTest.Models;
using MediatR;

namespace ApiTest.Queries.Students.GetStats
{
    public class GetStudentsStatsQuery : IRequest<StudentsAggregate>
    {
    }
}
