using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static StopHunger.Models.CustomValidators;

namespace StopHunger.Models
{
    public class Product
    {

        [Key] // Primary Key (PK)
        public int ProductId { get; set; }
        [Required(ErrorMessage = "is required.")]
        [MinLength(4, ErrorMessage = "must be at least 4 characters.")]
        public string Categorie { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(10, ErrorMessage = "must be at least 10 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "is required.")]
        [Range(1,99999, ErrorMessage = "must be at least 1 item.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "is required.")]
        [Range(0.01, 99999, ErrorMessage = "must be at least 1 item.")]
        public decimal Price { get; set; }

        [Display(Name = "Trip Date")]
        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /**********************************************************************
        Relationship properties: foreign keys and navigation properties. 
        Navigation properties are null unless .Include is used. 
        "Object reference not set to an instance of an object"
        will be the error if accessed but not included. 
        **********************************************************************/
        // 1 user can create many destination media
        public int UserId { get; set; }
        public User CreatedBy { get; set; }

        // Many User : Many Trip
        public List<UserProductDonation> Donations { get; set; }//Liek  userTriplike Likes

        // Many Trip : Many DestinationMedia
        public List<ProductDonutionToShelter> ProductDonutions { get; set; }//TripDestination TripDestinations 
    }
}