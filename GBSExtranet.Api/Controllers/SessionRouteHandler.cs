using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;

namespace GBSExtranet.Api
{
    public class SessionRouteHandler : HttpControllerRouteHandler 
    {
        protected override IHttpHandler GetHttpHandler(
            System.Web.Routing.RequestContext requestContext)
        {
            return new SessionControllerHandler(requestContext.RouteData);
        }
    }
}