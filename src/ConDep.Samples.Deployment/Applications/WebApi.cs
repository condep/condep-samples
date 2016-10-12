using ConDep.Dsl;
using ConDep.Dsl.Config;
using ConDep.Samples.Deployment.ApplicationInfrastructure.Iis;
using ConDep.Samples.Deployment.Cloud.AWS;

namespace ConDep.Samples.Deployment.Applications
{
    public class WebApi : Runbook, IDependOn<WebApiWebSite>, IDependOn<BootstrappedInstance> //Deploy this app to a bootstrapped aws instance
    {
        private const string PublishedWebsitesPath = @"_PublishedWebSites\ConDep.Samples.WebApi\";

        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            var env = settings.Environment()
                ;
            //Transform the web config for the api according to given environment
            dsl.Local.TransformConfigFile(PublishedWebsitesPath, "web.config", string.Format("web.{0}.config", env));

            //Deploy the web api
            dsl.Remote(server => server.Deploy.IisWebApplication(PublishedWebsitesPath, @"E:\api.condep-samples.no", "api.condep-samples.no"));
        }
    }
}
