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
    public class ShelterInfoTypeController : Controller
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
        public ShelterInfoTypeController(StopHungerContext context)
        {
            db = context;
        }


        [HttpGet("/donations/new")]
        public IActionResult New()
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("New");
        }

        [HttpPost("/donation/create")]
        public IActionResult Create(ShelterInfoType newShelterInfoType)
        {
            if (!ModelState.IsValid)
            {
                // To display validation errors.
            Console.WriteLine("New donation");
                return View("New");
            }
            newShelterInfoType.UserId = (int)uid;
            db.ShelterInfoType.Add(newShelterInfoType);
            db.SaveChanges();

            return RedirectToAction("Details", new { ShelterInfoTypeId = newShelterInfoType.ShelterInfoTypeId });
        }

        [HttpGet("/shelters/{ShelterInfoTypeId}")]
        public IActionResult Details(int shelterInfoTypeId)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            ShelterInfoType shelterInfoType = db.ShelterInfoType
                .FirstOrDefault(s => s.ShelterInfoTypeId == shelterInfoTypeId);

            if (shelterInfoType == null)
            {
                return RedirectToAction("New");
            }

            return View("Details", shelterInfoType);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}