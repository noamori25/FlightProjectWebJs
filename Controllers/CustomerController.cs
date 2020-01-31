using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.Facade;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    [BasicAuthentication]
    public class CustomerController : AutenticationDetails
    {
        //AuthenticationDetails _authen { get; set; }
        public CustomerController()
        {
           // _authen = new AuthenticationDetails();
        }

        // GetAllMyFlights: api/Customer/AllMyFlights
        [ResponseType(typeof(List<Flight>))]
        [Route("api/customer/AllMyFLights")]
        [HttpGet]
        public IHttpActionResult GetAllMyFlights()
        {
            //getCustomerLoginToken();
            IList<Flight> myFLights = ((LoggedInCustomerFacade)LogFacade).GetAllMyFlights((LoginToken<Customer>)Login);
            if (myFLights.Count > 0)
                return Ok(myFLights);
            else
                return StatusCode(HttpStatusCode.NotFound);
        }

        // PurchaseTicket: api/Customer/PurchaseTicket
        [Route("api/customer/PurchaseTicket/{flightId}")]
        [HttpPost]
        public IHttpActionResult PurchaseTicket(int flightId)
        {
            //getCustomerLoginToken();
            if (flightId > 0)
            {
                Flight flight = ((LoggedInCustomerFacade)LogFacade).GetFlightById(flightId);
                if (flight != null)
                {
                    try
                    {
                        ((LoggedInCustomerFacade)LogFacade).PurchaseTicket((LoginToken<Customer>)Login, flight);
                        return Ok($"{((LoginToken<Customer>)Login).User.UserName} purchased ticket for this flight details:{flight}");
                    }

                    catch (Exception e)
                    {
                        return Content(HttpStatusCode.NotAcceptable, $"{e.Message}");
                    }

                }
            }
            return Content(HttpStatusCode.NotAcceptable, $"Id {flightId} is not valid");
        }

        // CancelTicket: api/Customer/CancelTicket
        [Route("api/customer/CancelTicket/{id}")]
        [HttpDelete]
        public IHttpActionResult CancelTicket(int id)
        {
            //getCustomerLoginToken();
            if (id > 0)
            {
                Ticket ticket = ((LoggedInCustomerFacade)LogFacade).GetAllMyTickets((LoginToken<Customer>)Login, ((LoginToken<Customer>)Login).User).ToList().Find(t => t.Id == id);
                if (ticket != null)
                {
                    ((LoggedInCustomerFacade)LogFacade).CancelTicket((LoginToken<Customer>)Login, ticket);
                    return Ok($"{ticket} caneles by {((LoginToken<Customer>)Login).User.UserName}");
                }
                else
                {
                    return NotFound();
                }
            }
            return Content(HttpStatusCode.NotAcceptable, $"{id} is not valid");


        }

        //private AuthenticationDetails getCustomerLoginToken()
        //{
        //    Request.Properties.TryGetValue("CustomerUser", out object token);
        //    Request.Properties.TryGetValue("CustomerFacade", out object facade);
        //    LoggedInCustomerFacade customerFacade = (LoggedInCustomerFacade)facade;
        //    LoginToken<Customer> customerToken = (LoginToken<Customer>)token;
        //    _authen.CustomerFacade = customerFacade;
        //    _authen.Customer = customerToken;
        //    return _authen;
        //}
    }
}
