namespace ProjectManager.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Finished { get; set; }  
        public DateTime Created_At { get; set; }
    }
}
