﻿namespace ProjectManager.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string? Name { get; set; }
        public DateTime Created_At { get; set; }
        public string? Owners { get; set; }
        public int PhaseID { get; set; }
    }
}
