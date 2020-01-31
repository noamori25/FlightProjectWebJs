using ProjectManagmentSystem.Facade.Interfaces;
using ProjectManagmentSystem.POCO;
using ProjectManagmentSystem.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Facade
{
    /// <summary>
    /// This class represents options of anonymous user (without login)
    /// </summary>
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {
        /// <summary>
        /// Search airline companies
        /// </summary>
        /// <returns>list of all airline comapnies</returns>
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }

        /// <summary>
        /// Search flights 
        /// </summary>
        /// <returns>list of all flights</returns>
        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }

        /// <summary>
        /// Search all vacancy flights
        /// </summary>
        /// <returns>dictionary key = flight, value = remaining tickets</returns>
        public Dictionary<Flight, int> GetAllVacancyFlights()
        {
            return _flightDAO.GetAllVacancyFlights();
        }


        /// <summary>
        /// Search flight by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flight object</returns>
        public Flight GetFlightById(long id)
        {
            return _flightDAO.GetFlightById(id);
        }

        /// <summary>
        /// Search all flights by departure date
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns>list of flights by departure date</returns>
        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepartureDate(departureDate);
        }

        /// <summary>
        /// Search all flights by destination country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>list of flights by destination country</returns>
        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }

        /// <summary>
        /// Search flights by landing date
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns>list of flights by landing date</returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }

        /// <summary>
        /// Search flights by origin country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>list of flights by origin country</returns>
        public IList<Flight> GetFlightsByOriginCountry(long countryCode)
        {
            return _flightDAO.GetFlightByOriginCountry(countryCode);
        }
        /// <summary>
        /// Search airline by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Airline company object</returns>
        public AirlineCompany GetAirlineById (long id)
        {
            return _airlineDAO.Get(id);
        }

        /// <summary>
        /// Present departure flights for the next 12 hours
        /// </summary>
        /// <returns>list of departure view object</returns>
        public IList<DepartureView> DepartureForTheNext12H ()
        {
            return _flightDAO.GetDepartureFlightsNext12H();
        }

        /// <summary>
        /// Present landing flights for the next 12 hours and last 4 hours
        /// </summary>
        /// <returns>list of landing view object</returns>
        public IList<LandingView> LandingNext12hAndLast4h()
        {
           return _flightDAO.GetLandingFlightsNext12hAndLast4h();
        }

        /// <summary>
        /// Present landing or departure by airline name
        /// </summary>
        /// what return?
        public IList<SearchView> SearchByParams (SearchQueryParams searchParams)
        {
            if (searchParams != null)
            {
                //if (searchParams.FlightType == null)
                if (string.IsNullOrEmpty(searchParams.FlightType))
                searchParams.FlightType = "";
                if (!string.IsNullOrEmpty(searchParams.AirlineName))
                    return _flightDAO.SearchFlightsByAirlineName(searchParams.AirlineName, searchParams.FlightType);
               if (searchParams.FlightNumber > 0)
                    return _flightDAO.SearchFlightsByFlightNum(searchParams.FlightNumber, searchParams.FlightType);
               if (!string.IsNullOrEmpty(searchParams.DesCountry))
                        return _flightDAO.SearchFlightsByDesCountry(searchParams.DesCountry, searchParams.FlightType);
                if (!string.IsNullOrEmpty(searchParams.OriCountry))
                    return _flightDAO.SearchFlightsByOriCountry(searchParams.OriCountry, searchParams.FlightType);
            }
            return null;

        }
    }
}
