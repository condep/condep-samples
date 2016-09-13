namespace ConDep.Samples.Deployment.Infrastructure.Iis
{
    public class WwwApplicationWebSite : WebSiteRunbook
    {
        public WwwApplicationWebSite() : base("www", 100, "wb-samples-user", true) { }
    }
}