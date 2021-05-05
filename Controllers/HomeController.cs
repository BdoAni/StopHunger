using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StopHunger.Models;

namespace StopHunger.Controllers
{
    public class HomeController : Controller
    {
        private StopHungerContext db; //we paset same name as a our Context name 
        public HomeController(StopHungerContext context)
        {
            db = context;
        }
        // *************checking if user logged in to let type in the url*****\\\\\\\\\\\
        // **********we are using in the success page******\\\\\\\\\\\\\\\\\\\\\\
        private int? uid//checking to user is in the session or not
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }

        private bool isLoggedIn//checking is logged in or not to restrict they accsess
        {
            get
            {
                return uid != null;
            }
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            if (isLoggedIn)
            {
                return RedirectToAction("All", "Product");
            }

            return View();
        }

        [HttpPost("/register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                bool existingUser = db.Users.Any(u => u.Email == newUser.Email);

                if (existingUser)
                {
                    // Normally you don't want to reveal info like this b/c hackers can use it.
                    // But for testing purposes, we will make our errors descriptive.
                    ModelState.AddModelError("Email", "is taken.");
                }
            }

            /* 
            We could potentially have multiple conditions that invalidate the
            model state above so we have a catch-all check so we can display
            all error messages at once.
            */
            if (ModelState.IsValid == false)
            {
                // So error messages will be displayed.
                return View("Index");
            }

            // hash the password
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);

            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("FullName", newUser.FullName());
            return RedirectToAction("All", "Product");
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginUser loginUser)
        {
            /* 
            For security, don't reveal what was invalid.
            You can make your error messages more specific to help with testing
            but on a live site it should be ambiguous.
            */
            string genericErrMsg = "Invalid Email or Password";

            if (ModelState.IsValid == false)
            {
                // So error messages will be displayed.
                return View("Index");
            }

            User dbUser = db.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginEmail", genericErrMsg);
                Console.WriteLine(new String('*', 30) + "Login: Email not found");
                // So error messages will be displayed.
                return View("Index");
            }

            // User found b/c the above didn't return.
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

            if (pwCompareResult == 0)
            {
                ModelState.AddModelError("LoginEmail", genericErrMsg);
                Console.WriteLine(new String('*', 30) + "Login: Password incorrect.");
                return View("Index");
            }

            // Password matched.
            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            HttpContext.Session.SetString("FullName", dbUser.FullName());
            return RedirectToAction("All", "Product");
        }

        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
