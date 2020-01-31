using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.POCO;
using System.Web.Http;
using ProjectManagmentSystem.BLL.Intefaces;
using ProjectManagmentSystem.Facade;

namespace WebAPI.Controllers
{
    public class AutenticationDetails : ApiController
    {
        private ILoginToken login;
        private FacadeBase logFacade;

        public ILoginToken Login
        {
            get
            {
                if (Request.Properties.TryGetValue("AdminUser", out object tokenAd))
                {
                    login = (LoginToken<Administrator>)tokenAd;
                    return login;
                }
                else if (Request.Properties.TryGetValue("CustomerUser", out object tokenC))
                {
                    login = (LoginToken<Customer>)tokenC;
                    return login;
                }
                else if (Request.Properties.TryGetValue("AirlineUser", out object tokenAi))
                {
                    login = (LoginToken<AirlineCompany>)tokenAi;
                    return login;
                }
                else
                {
                    return null;
                }
            }
        }

        public FacadeBase LogFacade
        {
            get
            {
                if (Request.Properties.TryGetValue("AdminFacade", out object facadeAd))
                {
                    logFacade = (LoggedInAdministratorFacade)facadeAd;
                    return logFacade;
                }
                else if (Request.Properties.TryGetValue("CustomerFacade", out object facadeC))
                {
                    logFacade = (LoggedInCustomerFacade)facadeC;
                    return logFacade;
                }
                else if (Request.Properties.TryGetValue("AirlineFacade", out object facadeAi))
                {
                    logFacade = (LoggedInAirlineFacade)facadeAi;
                    return logFacade;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
