using System;
using System.ComponentModel.DataAnnotations;

namespace StopHunger.Models
{
    public class UserProductDonation // Many Trip : Many <UserUserTripLike> 
    {
        [Key] // Primary Key
        public int UserProductDonationId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /**********************************************************************
        Relationship properties: foreign keys and navigation properties. 
        Navigation properties are null unless .Include is used. 
        "Object reference not set to an instance of an object"
        will be the error if accessed but not included. 
        **********************************************************************/
        public int UserId { get; set; }
        public User User { get; set; } // User who liked.
        public int ProductId { get; set; }
        public Product Product{ get; set; } // Trip that the User liked.
    }
}