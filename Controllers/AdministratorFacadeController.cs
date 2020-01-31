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
    public class AdministratorFacadeController : AutenticationDetails
    {
       // private AuthenticationDetails _authen;

        public AdministratorFacadeController()
        {
           // _authen = new AuthenticationDetails();
        }

        //PostAirlineComapny: api/AdministratorFacade/CreateNewAirline
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/administratorFacade/CreateNewAirline")]
        [HttpPost]
        public IHttpActionResult CreateNewAirline([FromBody] AirlineCompany airline)
        {
           // AuthenticationDetails<Administrator> _authen = new AuthenticationDetails<Administrator>();
            if (airline == null)
                return Content(HttpStatusCode.NotAcceptable, "You didn't send airline to post");
            try
            {
                ((LoggedInAdministratorFacade)LogFacade).CreateNewAirLine((LoginToken<Administrator>)Login, airline);
                return Ok($"{airline} Added by {((LoginToken<Administrator>)Login).User.UserName}");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, $"{e.Message}");
            }

        }

        //UpdateAirlineCompany: api/AdministratorFacade/UpdateExistAirline
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/administratorFacade/UpdateExistAirline")]
        [HttpPut]
        public IHttpActionResult UpdateAirlineDetails([FromBody]AirlineCompany airline)
        {
            //AuthenticationDetails _authen = GetAdminTokenAndFacade();
            if (airline == null || airline.Id == 0)
                return Content(HttpStatusCode.NotAcceptable, $"{airline} details have not been completed properly");
            try
            {
                ((LoggedInAdministratorFacade)LogFacade).UpdateAirlineDetails((LoginToken<Administrator>)Login, airline);
                return Ok($"{airline} updated by {((LoginToken<Administrator>)Login).User.UserName}");
            }

            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"ID {airline.Id} was not found");
            }

        }

        // DeleteAirlineCompany: api/AdministratorFacade/DeleteAirlineCompany
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/administratorFacade/DeleteAirline/{id}")]
        [HttpDelete]
        public IHttpActionResult RemoveAirline(int id)
        {
            //AuthenticationDetails _authen = GetAdminTokenAndFacade();
            if (id <= 0)
                return Content(HttpStatusCode.NotAcceptable, "Id is not valid");
            AirlineCompany airline = ((LoggedInAdministratorFacade)LogFacade).GetAirlineById(id);
            //AirlineCompany airline = _authen.AdminFacade.GetAllAirlineCompanies().ToList().Find(a => a.Id == id);
            if (airline == null)
                return Content(HttpStatusCode.NotFound, $"{id} was not found");
            ((LoggedInAdministratorFacade)LogFacade).RemoveAirline((LoginToken<Administrator>)Login, airline);
            return Ok($"{airline} deleted by {((LoginToken<Administrator>)Login).User.UserName}");

        }

        // PostCustomer: api/AdministratorFacade/CreateCustomer
        [ResponseType(typeof(Customer))]
        [Route("api/administratorFacade/CreateNewCustomer")]
        [HttpPost]
        public IHttpActionResult CreateNewCustomer([FromBody] Customer customer)
        {
           // AuthenticationDetails _authen = GetAdminTokenAndFacade();
            if (customer == null)
                return Content(HttpStatusCode.NotAcceptable, "you did not send customer for creation");
            try
            {
                ((LoggedInAdministratorFacade)LogFacade).CreateNewCustomer((LoginToken<Administrator>)Login, customer);
                return Ok($"{customer} Added by {((LoginToken<Administrator>)Login).User.UserName}");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, $"{e.Message}");
            }


        }

        // UpdateCustomer: api/AdministratorFacade/UpdateExistACustomer
        [ResponseType(typeof(Customer))]
        [Route("api/administratorFacade/UpdateExistCustomer")]
        [HttpPut]
        public IHttpActionResult UpdateCustomerDetails([FromBody]Customer customer)
        {
            //AuthenticationDetails _authen = GetAdminTokenAndFacade();
            if (customer == null || customer.Id <= 0)
                return Content(HttpStatusCode.NotAcceptable, $"{customer} details have not been completed properly");
            try
            {
                ((LoggedInAdministratorFacade)LogFacade).UpdateCustomerDetails((LoginToken<Administrator>)Login, customer);
                return Ok($"{customer} updated by {((LoginToken<Administrator>)Login).User.UserName}");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"ID {customer.Id} was not found");
            }

        }

        // DeleteCustomer: api/AdministratorFacade/DeleteCustomer
        [ResponseType(typeof(Customer))]
        [Route("api/administratorFacade/DeleteCustomer/{id}")]
        [HttpDelete]
        public IHttpActionResult RemoveCustomer(int id)
        {
            //AuthenticationDetails _authen = GetAdminTokenAndFacade();
            if (id <= 0)
                return Content(HttpStatusCode.NotAcceptable, "Id is not valid");
            Customer customer = ((LoggedInAdministratorFacade)LogFacade).GetCustomerById((LoginToken<Administrator>)Login, id);
           // Customer customer = _authen.AdminFacade.GetAllCustomers(_authen.Admin).ToList().Find(c => c.Id == id);
            if (customer == null)
                return Content(HttpStatusCode.NotFound, $"{id} was not found");
            ((LoggedInAdministratorFacade)LogFacade).RemoveCustomer((LoginToken<Administrator>)Login, customer);
            return Ok($"{customer} Deleted by {((LoginToken<Administrator>)Login).User.UserName}");
        }

        //private AuthenticationDetails<Administrator> GetAdminTokenAndFacade()
        //{
        //   // AuthenticationDetails<Administrator> _authen = new AuthenticationDetails<Administrator>();
        //    Request.Properties.TryGetValue("AdminUser", out object token);
        //    Request.Properties.TryGetValue("AdminFacade", out object facade);
        //    LoginToken<Administrator> adminToken = (LoginToken<Administrator>)token;
        //    LoggedInAdministratorFacade adminFacade = (LoggedInAdministratorFacade)facade;
        //    //_authen.LoginToken = (LoginToken<Administrator>)token;
        //    //_authen.Facade = (LoggedInAdministratorFacade)facade;
        //    return _authen;
        //}

    }
}
