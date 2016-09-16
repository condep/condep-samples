namespace ConDep.Samples.Deployment.Infrastructure.Iis
{
    public class WwwApplicationWebSite : WebSiteRunbook
    {
        public WwwApplicationWebSite() : base("www", 100, "wb-samples-user", true) { }
    }

    public class WebApiWebSite : WebSiteRunbook
    {
        public WebApiWebSite() : base("api", 200, "api-samples-user", true) { }
    }
}