namespace ProjectManager.DTO
{
    public class PhaseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public Guid Completed_By { get; set; }
        public DateTime Completed_At { get; set; }
        public Guid ProjectId { get; set; }


    }
}
