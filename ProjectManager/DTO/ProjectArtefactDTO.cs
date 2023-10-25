namespace ProjectManager.DTO
{
    public class ProjectArtefactDTO
    {
        Guid ArtefactId { get; set; }
        public string Status { get; set; }
        public Guid Completed_By { get; set; }
        public DateTime Completed_At {  get; set; }
        public string DefaultArtefactName { get; set; }
        public string Artefact_Type { get; set; }



    }
}
