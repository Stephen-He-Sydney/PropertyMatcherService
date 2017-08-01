using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using DomainTest.Domain.Models;

namespace DomainTest.Domain.Utilities
{
    public static class MapHelper
    {
        /// <summary>
        /// User 2 coordinates to get distance
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns>Distance in metres</returns>
        public static int GetDistanceBetween(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            var sCoord = new GeoCoordinate(Convert.ToDouble(lat1), Convert.ToDouble(lon1));
            var eCoord = new GeoCoordinate(Convert.ToDouble(lat2), Convert.ToDouble(lon2));

            return Convert.ToInt32(sCoord.GetDistanceTo(eCoord));
        }
    }
}
