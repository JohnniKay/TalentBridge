using System;
using System.ComponentModel.DataAnnotations;

namespace ClientConnect.Models
{
    /// <summary>
    /// Represents an application submitted by a freelancer for a specific project listing.
    /// Includes the submitted cover letter, submission date, and navigation to related entities.
    /// </summary>
    public class Application
    {
        // Primary key for the application.
        public int Id { get; set; }

        // Foreign key referencing the associated project listing.
        [Required]
        public int ProjectListingId { get; set; }

        // Foreign key referencing the freelancer who submitted the application.
        [Required]
        public int FreelancerId { get; set; }

        // Cover letter content submitted by the freelancer.
        [Required]
        public string CoverLetter { get; set; } = string.Empty;

        // Timestamp indicating when the application was submitted.
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        // Navigation property for the related Freelancer.
        public Freelancer? Freelancer { get; set; }

        // Navigation property for the related ProjectListing.
        public ProjectListing? ProjectListing { get; set; }
    }
}
