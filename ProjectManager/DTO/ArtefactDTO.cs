﻿namespace ProjectManager.DTO
{
    public class ArtefactDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public string Completed_By { get; set; }
        public DateTime Completed_At { get; set; }
        public Guid PhaseId { get; set; }
        public string Artefact_Type { get; set; }
    }
}