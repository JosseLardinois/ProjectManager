namespace ProjectManager.Models
{
    public class Phase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public string Completed_By { get; set; }
        public DateTime Completed_at { get; set; }
        public Guid ProjectId { get; set; }
    }
}
