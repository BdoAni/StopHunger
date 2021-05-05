using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StopHunger.Models
{
    public class ShelterInfoType
    {
        [Key] // Primary Key (PK)
        public int ShelterInfoTypeId { get; set; }
        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "is required.")]
        [Display(Name = "Media Url")]
        public string Src { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        [MinLength(4, ErrorMessage = "must be between 4 and 255 characters.")]
        [MaxLength(255, ErrorMessage = "must be between 4 and 255 characters.")]
        public string ShortDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /* Relationship Properties */
        // Foreign Key and Navigation Property for 1 User : M DestinationMedia
        // 1 user can create many destination media
        public int UserId { get; set; }
        public User CreatedBy { get; set; }

        // Many Product  : Many ShelterInfoType
        public List<ProductDonutionToShelter> ProductDonutionToShelters { get; set; }
    }
}