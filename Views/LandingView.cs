using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.POCO.Views
{
   public class LandingView
    {
        public string AirlineName;
        public long FlightId;
        public string OriginCountry;
        public string DestinationCountry;
        public DateTime LandingTime;
        public string Status;
        

        public LandingView()
        {

        }

        public LandingView(string airlineName, long flightId, string originCountry, string destinationCountry, DateTime landingTime, string status)
        {
            this.AirlineName = airlineName;
            this.FlightId = flightId;
            this.OriginCountry = originCountry;
            this.DestinationCountry = destinationCountry;
            this.LandingTime = landingTime;
            this.Status = status;
            
        }
        /// <summary>
        /// Override the real function To string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return $"Airline name {AirlineName}, Flight id {FlightId}," +
                $" From {OriginCountry}, To {DestinationCountry}," +
                $"  EST {LandingTime}, Flight status {Status}";
        }
    }
}
