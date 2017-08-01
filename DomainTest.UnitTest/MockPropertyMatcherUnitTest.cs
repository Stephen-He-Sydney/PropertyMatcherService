using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainTest.Domain.Models;
using DomainTest.Domain.Interfaces;
using DomainTest.Business;
using Moq;

namespace DomainTest.UnitTest
{
    [TestClass]
    public class MockPropertyMatcherUnitTest
    {
        PropertyService propertyService;
        Mock<IRepository<Property>> mockPropertyRepository;

        /// <summary>
        /// Mock the db access layer to test service logic
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            mockPropertyRepository = new Mock<IRepository<Property>>();
            mockPropertyRepository.Setup(x => x.GetById(It.IsAny<int>()));
            propertyService = new PropertyService(mockPropertyRepository.Object);
        }

        /// <summary>
        /// A property is considered to be a match if both the property name and address match when punctuation is excluded.  
        /// </summary>
        [TestMethod]
        public void OTBREMatcherTest()
        {
            // arrange  
            Property agencyProperty = new Property()
            {
                AgencyCode = "OTBRE",
                Name = "*Super*-High! APARTMENTS (Sydney)",
                Address = "32 Sir John-Young Crescent, Sydney, NSW."
            };

            Property databaseProperty = new Property()
            {
                Name = "Super High Apartments, Sydney",
                Address = "32 Sir John Young Crescent, Sydney NSW"
            };

            //IPropertyMatcher matcher = new PropertyService();

            // act
            bool expected = propertyService.IsMatch(agencyProperty, databaseProperty); //matcher.IsMatch(agencyProperty, databaseProperty);

            // assert
            mockPropertyRepository.Verify();
            Assert.IsTrue(expected);
        }

        /// <summary>
        /// A property is considered to be a match if the agency code is the same and the property is within 200 metres or less of the actual property location.
        /// </summary>
        [TestMethod]
        public void LREMatcherTest()
        {
            // arrange
            Property agencyProperty = new Property()
            {
                AgencyCode = "LRE",
                Longitude = 22,
                Latitude = 33
            };

            Property databaseProperty = new Property()
            {
                Longitude = 22,
                Latitude = 33
            };

            //IPropertyMatcher matcher = new PropertyService();

            // act
            bool expected = propertyService.IsMatch(agencyProperty, databaseProperty); //matcher.IsMatch(agencyProperty, databaseProperty);

            // assert 
            mockPropertyRepository.Verify();
            Assert.IsTrue(expected);
        }

        /// <summary>
        /// A property is considered a match if the names match when the words in the name of the property are reversed.
        /// </summary>
        [TestMethod]
        public void CREMatcherTest()
        {
            // arrange
            Property agencyProperty = new Property()
            {
                AgencyCode = "CRE",
                Name = "Apartments Summit The"
            };

            Property databaseProperty = new Property()
            {
                Name = "The Summit Apartments"
            };

            //IPropertyMatcher matcher = new PropertyService();

            // act
            bool expected = propertyService.IsMatch(agencyProperty, databaseProperty); //matcher.IsMatch(agencyProperty, databaseProperty);

            // assert 
            mockPropertyRepository.Verify();
            Assert.IsTrue(expected);
        }

        /// <summary>
        /// Get all available agency property info in default way
        /// </summary>
        [TestMethod]
        public void GetAgencyPropertyByDefaultTest()
        {
            // act
            var result = propertyService.GetAgencyPropertyByDefault();

            //assert
            mockPropertyRepository.Verify();
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Get db property by curresponding agencyCode
        /// </summary>
        [TestMethod]
        public void GetDatabasePropertyByAgencyCodeTest()
        {
            // act
            var dbProperty = propertyService.GetDatabasePropertyByAgencyCode("CRE");

            //assert
            mockPropertyRepository.Verify();
        }
    }
}
