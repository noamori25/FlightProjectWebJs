using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class PageController : Controller
    {
        // GET: DepartureFlights
        public ActionResult Departure()
        {
            return new FilePathResult("~/Pages/DeparturesFlights.html", "text/html");
        }
        // GET: LandingFlights
        public ActionResult Landing()
        {
            return new FilePathResult("~/Pages/LandingFlights.html", "text/html");
        }
        // GET: Search
        public ActionResult Search()
        {
            return new FilePathResult("~/Pages/Search.html", "text/html");
        }
    }
}