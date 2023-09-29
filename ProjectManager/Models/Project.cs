namespace ProjectManager.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created_At { get; set; }
        public int PhaseID { get; set; }
    }
}
