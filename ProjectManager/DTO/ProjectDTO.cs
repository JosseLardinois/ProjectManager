namespace ProjectManager.DTOs
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created_At { get; set; }
        public int PhaseID { get; set; }
    }
}
