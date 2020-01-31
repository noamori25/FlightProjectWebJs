using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.POCO.Views
{
   public class SearchView
    {
        public string AirlineName;
        public long FlightId;
        public string OriginCountry;
        public string DestinationCountry;
        public DateTime DepartureTime;
        public DateTime LandingTime;

        public SearchView()
        {

        }

        public SearchView(string airlineName, long flightId, string originCountry, string DestinationCountry, DateTime departureTime, DateTime landingTime)
        {
            this.AirlineName = airlineName;
            this.FlightId = flightId;
            this.OriginCountry = originCountry;
            this.DestinationCountry = DestinationCountry;
            this.DepartureTime = departureTime;
            this.LandingTime = landingTime;
        }
        /// <summary>
        /// Override the real function To string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return $"Airline name {AirlineName}, Flight id {FlightId}," +
                $" From {OriginCountry}, To {DestinationCountry}," +
                $"  Departure{DepartureTime}, Landing {LandingTime}";
        }

    }
}
