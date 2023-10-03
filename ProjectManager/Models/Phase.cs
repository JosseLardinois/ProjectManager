namespace ProjectManager.Models
{
    public class Phase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public bool Active { get; set; }
        public string Completed_By { get; set; }
        public DateTime Completed_At { get; set; }
        public Guid ProjectId { get; set; }
    }
}
