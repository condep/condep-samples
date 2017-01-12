using ConDep.Dsl;
using ConDep.Dsl.Config;
using ConDep.Samples.Deployment.ApplicationInfrastructure.Iis;
using ConDep.Samples.Deployment.Cloud.AWS;

namespace ConDep.Samples.Deployment.Applications
{
    public class WebApi : Runbook
    {
        private const string PublishedWebsitesPath = @"_PublishedWebSites\ConDep.Samples.WebApi\";

        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            var env = settings.Environment();

            //Need server in aws to deploy to. This runbook bootstraps an instance in aws
            Runbook.Execute<AwsApiServerRunbook>(dsl, settings);

            //Need IIS Web Site
            Runbook.Execute<WebApiWebSite>(dsl, settings);

            //Transform the web config for the api according to given environment
            dsl.Local.TransformConfigFile(PublishedWebsitesPath, "web.config", string.Format("web.{0}.config", env));

            //Deploy the web api
            dsl.Remote(server => server.Deploy.IisWebApplication(PublishedWebsitesPath, @"E:\api.condep-samples.io", "api.condep-samples.io"));
        }
    }
}
