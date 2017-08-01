using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DomainTest.Domain.Interfaces;
using DomainTest.Domain.Models;
using DomainTest.Web.Models;

namespace DomainTest.Web.Controllers
{
    [RoutePrefix("api/propertyApi")]
    public class PropertyApiController : ApiController
    {
        private readonly IPropertyMatcher _propertyMatcher;
        private readonly IPropertyService _propertyService;

        public PropertyApiController(IPropertyMatcher propertyMatcher, IPropertyService propertyService)
        {
            _propertyMatcher = propertyMatcher;
            _propertyService = propertyService;
        }

        /// <summary>
        /// Match the input property info with existing matcher rules
        /// </summary>
        /// <param name="agencyProperty"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("isPropertyMatched")]
        public HttpResponseMessage IsPropertyMatched(PropertyViewModel agencyPropertyViewModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var dbProperty = _propertyService.GetDatabasePropertyByAgencyCode(agencyPropertyViewModel.AgencyCode);

                var agencyProperty = new Property();
                agencyProperty.Address = !string.IsNullOrEmpty(agencyPropertyViewModel.Address) ? agencyPropertyViewModel.Address : string.Empty;
                agencyProperty.AgencyCode = agencyPropertyViewModel.AgencyCode;
                agencyProperty.Name = !string.IsNullOrEmpty(agencyPropertyViewModel.Name) ? agencyPropertyViewModel.Name : string.Empty;
                agencyProperty.Latitude = agencyPropertyViewModel.Latitude;
                agencyProperty.Longitude = agencyPropertyViewModel.Longitude;
                var isMatched = _propertyMatcher.IsMatch(agencyProperty, dbProperty);

                response = Request.CreateResponse(HttpStatusCode.OK, isMatched);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
                return response;
            }

            return response;
        }

        /// <summary>
        /// Assume convert agencies' xml files into a single json file
        /// This is method is to read each first record with different agencyCode for default page form reloading
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAgencyPropertyByDefault/{agencyCode}")]
        public HttpResponseMessage GetAgencyPropertyByDefault(string agencyCode)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var result = new Property();
                result = _propertyService.GetAgencyPropertyByDefault().FirstOrDefault(x => string.Equals(x.AgencyCode.Trim(), agencyCode.Trim(), StringComparison.OrdinalIgnoreCase));

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
                return response;
            }

            return response;
        }
    }
}
