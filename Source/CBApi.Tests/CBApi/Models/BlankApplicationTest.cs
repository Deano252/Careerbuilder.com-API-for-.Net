﻿using CBApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.IO;
using System.Xml.Linq;

namespace Tests.CBApi.models {
    [TestClass]
    public class BlankApplicationTest {
        [TestMethod]
        [DeploymentItem("testdata\\ResponseBlankApplication.xml")]
        public void DeserializationWorks_WhenPassedRightXML() {
            var xmlpath = Path.Combine(Environment.CurrentDirectory, "ResponseBlankApplication.xml");
            var doc = XDocument.Load(xmlpath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<BlankApplication>(new RestResponse() { Content = doc.ToString() });

            Assert.IsNotNull(output);
            Assert.AreEqual("http://api.careerbuilder.com/v1/application/submit", output.ApplicationSubmitServiceURL);
            Assert.AreEqual("http://api.careerbuilder.com/v1/application/applylink?TrackingID=V8YDVG7V&JobDID=JHP3GR6135GF0QC5Q5B", output.ApplyURL);
            Assert.AreEqual("JHP3GR6135GF0QC5Q5B", output.JobDID);
            Assert.AreEqual("Field Service Technician", output.JobTitle);
            Assert.IsNotNull(output.Requirements);
            Assert.AreEqual(4, output.TotalQuestions);
            Assert.AreEqual(3, output.TotalRequiredQuestions);
            Assert.IsNotNull(output.Questions);
            Assert.AreEqual(4, output.Questions.Count);
        }
    }
}
