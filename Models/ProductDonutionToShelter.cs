
using System;
using System.ComponentModel.DataAnnotations;


namespace StopHunger.Models
{
    public class ProductDonutionToShelter // Many Trip : Many DestinationMedia <TripDestination>
    {
        [Key] // Primary Key
        public int ProductDonutionToShelterId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /**********************************************************************
        Relationship properties: foreign keys and navigation properties. 
        Navigation properties are null unless .Include is used. 
        "Object reference not set to an instance of an object"
        will be the error if accessed but not included. 
        **********************************************************************/
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShelterInfoTypeId { get; set; }
        public ShelterInfoType ShelterInfoType { get; set; }
    }
}