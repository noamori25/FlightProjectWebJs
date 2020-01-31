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
    public class CompanyFacadeController : AutenticationDetails
    {
        //private AuthenticationDetails _authen { get; set; }

        public CompanyFacadeController()
        {
            //_authen = new AuthenticationDetails();
        }

        // GetAllTickets: api/CompanyFacade/GetAllTickets
        [ResponseType(typeof(List<Ticket>))]
        [Route("api/companyFacade/GetAllTickets")]
        [HttpGet]
        public IHttpActionResult GetAllTickets()
        {
            //GetCompanyTokenAndFacade();
            IList<Ticket> allTickets = ((LoggedInAirlineFacade)LogFacade).GetAllMyTickets((LoginToken<AirlineCompany>)Login);
            if (allTickets.Count == 0)
                return StatusCode(HttpStatusCode.NotFound);
            return Ok(allTickets);
        }

        // GetAllFlights: api/CompanyFacade/GetAllFlights
        [ResponseType(typeof(List<Flight>))]
        [Route("api/companyFacade/GetAllFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
           // GetCompanyTokenAndFacade();
            IList<Flight> allFlights = ((LoggedInAirlineFacade)LogFacade).GetAllFlights((LoginToken<AirlineCompany>)Login);
            if (allFlights.Count == 0)
                return StatusCode(HttpStatusCode.NotFound);
            return Ok(allFlights);
        }

        // CancelFlight: api/CompanyFacade/CancelFlight
        [ResponseType(typeof(Flight))]
        [Route("api/companyFacade/CancelFlight/{id}")]
        [HttpDelete]
        public IHttpActionResult CancelFlight(int id)
        {
           // GetCompanyTokenAndFacade();
            if (id <= 0)
                return Content(HttpStatusCode.NotAcceptable, $"{id} not valid");
            Flight flight = ((LoggedInAirlineFacade)LogFacade).GetFlightById(id);
            //Flight flight = _authen.AirlineFacade.GetAllFlights(_authen.Airline).ToList().Find(f => f.Id == id);
            if (flight == null)
                return Content(HttpStatusCode.NotFound, $"{id} was not found");
            ((LoggedInAirlineFacade)LogFacade).CancelFlight((LoginToken<AirlineCompany>)Login, flight);
            return Ok($"{flight} Canceled by {((LoginToken<AirlineCompany>)Login).User.UserName}");
        }


        // CreateFlight: api/CompanyFacade/PostFlight
        [ResponseType(typeof(Flight))]
        [Route("api/companyFacade/CreateFlight")]
        [HttpPost]
        public IHttpActionResult CrearteFlight([FromBody]Flight flight)
        {
            //GetCompanyTokenAndFacade();
            if (flight == null)
                return Content(HttpStatusCode.NotAcceptable, $"you didn't send flight to post");
            try
            {
                ((LoggedInAirlineFacade)LogFacade).CreateFlight((LoginToken<AirlineCompany>)Login, flight);
                return Ok($"{flight} Added by {((LoginToken<AirlineCompany>)Login).User.UserName}");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, $"{e.Message}");
            }
        }

        //UpdateFlight: api/CompanyFacade/UpdateFlight
        [ResponseType(typeof(Flight))]
        [Route("api/companyFacade/UpdateFlight")]
        [HttpPut]
        public IHttpActionResult UpdateFlight([FromBody] Flight flight)
        {
            //GetCompanyTokenAndFacade();
            if (flight == null || flight.Id <= 0)
                return Content(HttpStatusCode.NotAcceptable, $"{flight} details have not been completed properly");
            try
            {
                ((LoggedInAirlineFacade)LogFacade).UpdateFlight((LoginToken<AirlineCompany>)Login, flight);
                return Ok($"{flight} Updated by {((LoginToken<AirlineCompany>)Login).User.UserName}");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, $"{e.Message}");
            }


        }

        //ChangeMyPassword: api/CompanyFacade/ChangeMyPassword
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/companyFacade/ChangeMyPassword/{newPassword}")]
        [HttpPut]
        public IHttpActionResult ChangeMyPassword(string newPassword)
        {
            //GetCompanyTokenAndFacade();
            if (newPassword == string.Empty)
                return Content(HttpStatusCode.NotAcceptable, "new password have not been completed properly");
            try
            {
                ((LoggedInAirlineFacade)LogFacade).ChangeMyPassword((LoginToken<AirlineCompany>)Login, ((LoginToken<AirlineCompany>)Login).User.Password, newPassword);
                return Ok($"{((LoginToken<AirlineCompany>)Login).User.UserName} changed his password");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"{e.Message}");
            }

        }


        //ModifyAirlineDetails: api/companyFacade/ModifyAirlineDetails
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/companyFacade/ModifyAirlineDetails")]
        [HttpPut]
        public IHttpActionResult ModifyAirline([FromBody] AirlineCompany airline)
        {
            //GetCompanyTokenAndFacade();
            if (airline == null || airline.Id == 0)
                return Content(HttpStatusCode.NotAcceptable, $"{airline} details have not been completed properly");
            try
            {
                ((LoggedInAirlineFacade)LogFacade).ModifyAirlineDetails((LoginToken<AirlineCompany>)Login, airline);
                return Ok($"{airline} updated by {((LoginToken<AirlineCompany>)Login).User.UserName}");
            }

            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"ID {airline.Id} was not found");
            }
        }

        //GetAllTicketsByFlight: api/companyFcade/GetAllTicketsByFlight
        [ResponseType(typeof(List<Ticket>))]
        [Route("api/companyFacade/GetAllTicketsByFlight/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllTIcketsByFlight(int id)
        {
            //GetCompanyTokenAndFacade();
            if (id <= 0)
                return Content(HttpStatusCode.NotAcceptable, $"{id} is not valid");
            try
            {
                IList<Ticket> tickets = ((LoggedInAirlineFacade)LogFacade).GetAllTicketsByFlight((LoginToken<AirlineCompany>)Login, id);
                if (tickets.Count == 0)
                    return NotFound();
                return Ok(tickets);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"{e.Message}");
            }
        }

        //private AuthenticationDetails GetCompanyTokenAndFacade()
        //{
        //    Request.Properties.TryGetValue("AirlineUser", out object token);
        //    Request.Properties.TryGetValue("AirlineFacade", out object facade);
        //    LoginToken<AirlineCompany> airlineToken = (LoginToken<AirlineCompany>)token;
        //    LoggedInAirlineFacade airlineFacade = (LoggedInAirlineFacade)facade;
        //    _authen.Airline = airlineToken;
        //    _authen.AirlineFacade = airlineFacade;
        //    return _authen;
        //}


    }
}
