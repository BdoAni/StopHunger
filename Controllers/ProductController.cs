using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StopHunger.Models;

namespace StopHunger.Controllers
{
    public class ProductController : Controller
    {
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }

        private bool isLoggedIn
        {
            get
            {
                return uid != null;
            }
        }

        private StopHungerContext db;
        public ProductController(StopHungerContext context)
        {
            db = context;
        }


        [HttpGet("/product")]
        public IActionResult All()
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Product> products = db.Products
                .Include(p => p.CreatedBy)
                .ToList();

            return View("All", products);
        }

        [HttpGet("/products/new")]
        public IActionResult New()
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("New");
        }

        [HttpPost("/products/create")]
        public IActionResult Create(Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                // To display validation errors.
                return View("New");
            }


            // WILL GET THIS ERROR if FK is not assigned:
            // "foreign key constraint fails"
            newProduct.UserId = (int)uid;
            db.Products.Add(newProduct);
            db.SaveChanges(); // after this newTrip has it's TripId from DB.

            /* 
            WHENEVER REDIRECTING to a method that has params, you must pass in
            a 'new' dictionary: new { paramName = valueForParam }
            */
            return RedirectToAction("Details", new { productId = newProduct.ProductId });
        }

        [HttpGet("/products/{productId}")]
        public IActionResult Details(int productId)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            Product product = db.Products
                .Include(p => p.CreatedBy)
                .Include(p => p.ProductDonutions)
                .ThenInclude(td => td.ShelterInfoType)
                .FirstOrDefault(p=> p.ProductId == productId);

            if (product == null)
            {
                return RedirectToAction("New");
            }

            List<ShelterInfoType> allDonations = db.ShelterInfoType.ToList();
            List<ShelterInfoType> unrelatedDonations = new List<ShelterInfoType>();

            foreach (ShelterInfoType donat in allDonations)
            {
                bool isRelated = product.ProductDonutions
                    .Any(pd => pd.ShelterInfoTypeId == donat.ShelterInfoTypeId);

                if (!isRelated)
                {
                    unrelatedDonations.Add(donat);
                }
            }

            ViewBag.UnrelatedDonations = unrelatedDonations

                .OrderBy(donat => donat.Location, System.StringComparer.CurrentCultureIgnoreCase);

            return View("Details", product);
        }

        [HttpGet("/products/{productId}/edit")]
        public IActionResult Edit(int productId)
        {
            Product product = db.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null || product.UserId != uid)
            {
                return RedirectToAction("All");
            }

            return View("Edit", product);
        }

        [HttpPost("/products/{productId}/update")]
        public IActionResult Update(Product editedProduct, int productId)
        {
            if (!ModelState.IsValid)
            {
                /* 
                "Object reference not set to an instance of an object" if we
                don't pass the editedTrip back with the TripId because the form
                has asp-route-tripId="@Model.TripId" so it needs the TripId.
                */
                editedProduct.ProductId = productId;
                // Go back to form to display errors.
                return View("Edit", editedProduct);
            }

            Product dbProduct= db.Products.FirstOrDefault(t => t.ProductId == productId);

            if (dbProduct == null)
            {
                return RedirectToAction("All");
            }

            dbProduct.UpdatedAt = DateTime.Now;
            dbProduct.Categorie = editedProduct.Categorie;
            dbProduct.Name = editedProduct.Name;
            dbProduct.Description = editedProduct.Description;
            dbProduct.Quantity = editedProduct.Quantity;
            dbProduct.Price = editedProduct.Price;
            dbProduct.Date = editedProduct.Date;

            db.Products.Update(dbProduct);
            db.SaveChanges();

            /* 
            WHENEVER REDIRECTING to a method that has params, you must pass in
            a 'new' dictionary: new { paramName = valueForParam }
            */
            return RedirectToAction("Details", new { productId = dbProduct.ProductId });
        }

        [HttpPost("/products/{productId}/delete")]
        public IActionResult Delete(int productId)
        {
            Product product = db.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return RedirectToAction("All");
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpPost("/products/{productId}/add-donation")]
        public IActionResult AddDonation(ProductDonutionToShelter newDonation, int productId)
        {
            db.ProductDonutionToShelters.Add(newDonation);
            db.SaveChanges();
            return RedirectToAction("Details", new { productId = productId });
        }

        [HttpPost("/product/{productId}")]
        public IActionResult Donate(int productId)
        {
            if (!isLoggedIn)// checking to see is user loggedin or not
            {
                return RedirectToAction("Index", "Home");
            }

            UserProductDonation existingdonation = db.UserProductDonations
                .FirstOrDefault(d => d.UserId == uid && d.ProductId == productId);

            if (existingdonation == null)
            {
                UserProductDonation donate = new UserProductDonation()
                {
                    ProductId = productId,
                    UserId = (int)uid
                };

                db.UserProductDonations.Add(donate);
            }
            else
            {
                db.UserProductDonations.Remove(existingdonation);
            }
            db.SaveChanges();
            return RedirectToAction("Details");
        }
    }
}