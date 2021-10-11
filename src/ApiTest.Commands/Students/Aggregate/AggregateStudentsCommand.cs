using ApiTest.Models;
using MediatR;

namespace ApiTest.Commands.Students.Aggregate
{
    public class AggregateStudentsCommand : IRequest<AggregateStudentsResult>
    {
        public StudentsAggregate StudentAggregate { get; set; }
    }
}
