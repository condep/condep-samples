namespace ConDep.Samples.Deployment.ApplicationInfrastructure.Iis
{
    public class WwwApplicationWebSite : WebSiteRunbook
    {
        public WwwApplicationWebSite() : base("www", 100, "wb-samples-user", false) { }
    }

    public class WebApiWebSite : WebSiteRunbook
    {
        public WebApiWebSite() : base("api", 200, "api-samples-user", false) { }
    }
}