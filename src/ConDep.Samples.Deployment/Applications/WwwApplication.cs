using ConDep.Dsl;
using ConDep.Dsl.Config;
using ConDep.Samples.Deployment.ApplicationInfrastructure.Iis;

namespace ConDep.Samples.Deployment.Applications
{
    public class WwwApplication : Runbook, IDependOn<WwwApplicationWebSite>
    {
        private const string PublishedWebsitesPath = @"..\deployment\_PublishedWebSites\ConDep.Samples.WwwApplication\";

        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            var env = settings.Environment()
                ;
            //Transform the web config for the application according to given environment
            dsl.Local.TransformConfigFile(PublishedWebsitesPath, "web.config", string.Format("web.{0}.config", env));

            //Deploy the www application
            dsl.Remote(server => server.Deploy.IisWebApplication(PublishedWebsitesPath + @"ConDep.Samples.WwwApplication\", @"E:\www.condep-samples.no", "www.condep-samples.no"));
        }
    }
}