namespace ProjectManager.DTOs
{
    public class ProjectDTO
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public DateTime Created_At { get; set; }
        public string Owners { get; set; }
        public int PhaseID { get; set; }
    }
}
