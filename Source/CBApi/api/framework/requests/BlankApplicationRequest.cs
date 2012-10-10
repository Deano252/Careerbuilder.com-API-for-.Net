﻿using System;
using RestSharp;
using com.careerbuilder.api.models;

namespace com.careerbuilder.api.framework.requests
{
    internal class BlankApplicationRequest : GetRequest
    {
        protected string JobDid = "";

        public BlankApplicationRequest(string jobDid, string key, string domain, string cobrand, string siteid)
            : base(key, domain, cobrand, siteid)
        {
            if (string.IsNullOrEmpty(jobDid))
            {
                throw new ArgumentNullException();
            }
            
            if (jobDid.Length >= 18 && jobDid.Length <= 20 &&
                     jobDid.StartsWith("J", StringComparison.InvariantCultureIgnoreCase))
            {
                JobDid = jobDid;
            }
            else
            {
                throw new ArgumentException("This does not look like a job did");
            }
        }

        public override string BaseURL
        {
            get { return "/v1/application/blank"; }
        }

        public BlankApplication Retrieve()
        {
            base.BeforeRequest();
            _request.AddParameter("JobDID", JobDid);
            _request.RootElement = "BlankApplication";
            IRestResponse<BlankApplication> response = _client.Execute<BlankApplication>(_request);
            BlankApplication app = response.Data;
            if (app != null)
            {
                app.SiteID = _SiteID;
                app.CoBrand = _CobrandCode;
                app.DeveloperKey = _DevKey;
            }
            return app;
        }
    }
}