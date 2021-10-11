namespace ApiTest.Commands.Students.Aggregate
{
    public class AggregateStudentsResult
    {
        public AggregateStudentsResult(bool created)
        {
            Created = created;
        }

        public bool Created { get; set; }
    }
}
