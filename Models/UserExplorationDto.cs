using System;

namespace trailAPI.Models
{
    public class UserExplorationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TrailID { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool CompletionStatus { get; set; }
    }
}