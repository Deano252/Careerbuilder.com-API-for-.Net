﻿using CBApi.Models;
using CBApi.Models.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System.Collections.Generic;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests
{
    [TestClass]
    public class EmployeeTypesTest
    {
        [TestMethod]
        public void GetRequestURL_BuildsCorrectEndpointAddress()
        {
            var request = new EmployeeTypesStub("DevKey", "api.careerbuilder.com");
            Assert.AreEqual("https://api.careerbuilder.com/v1/employeetypes", request.RequestURL);
        }

        [TestMethod]
        public void WhereCountryCode_ReturnsCategoryRequest()
        {
            var request = new EmployeeTypesStub("DevKey", "api.careerbuilder.com");
            Assert.IsInstanceOfType(request.WhereCountryCode(CountryCode.SE), typeof (IEmployeeTypesRequest));
        }

        [TestMethod]
        public void WhereCountryCode_SetsCountryCode()
        {
            var request = new EmployeeTypesStub("DevKey", "api.careerbuilder.com");
            request.WhereCountryCode(CountryCode.SE);
            Assert.AreEqual("SE", request.CountryCode);
        }

        [TestMethod]
        public void WhereHostSite_ReturnsCategoryRequest()
        {
            var request = new EmployeeTypesStub("DevKey", "api.careerbuilder.com");
            Assert.IsInstanceOfType(request.WhereHostSite(HostSite.EU), typeof (IEmployeeTypesRequest));
        }

        [TestMethod]
        public void WhereHostSite_SetsCountryCode()
        {
            var request = new EmployeeTypesStub("DevKey", "api.careerbuilder.com");
            request.WhereHostSite(HostSite.EU);
            Assert.AreEqual("EU", request.CountryCode);
        }

        [TestMethod]
        public void ListAll_PerformsCorrectRequest()
        {
            //Setup
            var request = new EmployeeTypesStub("DevKey", "api.careerbuilder.com");

            //Mock crap
            var response = new RestResponse<List<EmployeeType>> {Data = new List<EmployeeType>()};

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("CountryCode", "NL"));
            restReq.SetupSet(x => x.RootElement = "EmployeeTypes");

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/employeetypes");
            restClient.Setup(x => x.Execute<List<EmployeeType>>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            List<EmployeeType> cats = request.WhereCountryCode(CountryCode.NL).ListAll();
            Assert.IsTrue(cats.Count == 0);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}