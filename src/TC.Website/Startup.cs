using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TC.Website.Startup))]

namespace TC.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //requires ASP.Net Compatability Mode...I think there is a better way
            //RouteTable.Routes.Add(new ServiceRoute("PatientFacade", new WebServiceHostFactory(), typeof(TC.Svcs.PatientFacadeSvc) ));
        }
    }
}
