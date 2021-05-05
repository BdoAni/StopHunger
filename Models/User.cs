using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StopHunger.Models
{
    public class User
    {
        [Key] // the below prop is the primary key, [Key] is not needed if named with pattern: ModelNameId
        public int UserId { get; set; }
        // *************************First Name********************\\
        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        // *************************Last Name********************\\

        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        // *************************Email********************\\
        [Required(ErrorMessage = "is required")]
        [MinLength(4, ErrorMessage = "must be at least 4 characters")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        // *************************Password********************\\
        [Required(ErrorMessage = "is required")]
        [MinLength(8, ErrorMessage = "must be at least 8characters")]
        [DataType(DataType.Password)]//auto fills input type attr
        public string Password { get; set; }
        // *************************COnfirm Password********************\\
        [NotMapped]// don't add to DB
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Doesn't match password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        //************************navigation propert.***********\\\\\\\\\\
        // public List<Party> PlannedActivities { get; set; }
        // public List<UserActivityJoin> Join { get; set; }



        // ////**********To display on the page user full name**************\\\\\\\\\\\
        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}