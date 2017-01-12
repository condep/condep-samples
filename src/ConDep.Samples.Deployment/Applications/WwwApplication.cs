using ConDep.Dsl;
using ConDep.Dsl.Config;
using ConDep.Samples.Deployment.ApplicationInfrastructure.Iis;
using ConDep.Samples.Deployment.Cloud.AWS;

namespace ConDep.Samples.Deployment.Applications
{
    public class WwwApplication : Runbook
    {
        private const string PublishedWebsitesPath = @"_PublishedWebsites\ConDep.Samples.WwwApplication\";

        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            var env = settings.Environment();

            //Need server in aws to deploy to. This runbook bootstraps an instance in aws
            Runbook.Execute<AwsWebServerRunbook>(dsl, settings);

            //Need IIS Web Site
            Runbook.Execute<WwwApplicationWebSite>(dsl, settings);

            //Transform the web config for the application according to given environment
            dsl.Local.TransformConfigFile(PublishedWebsitesPath, "web.config", string.Format("web.{0}.config", env));

            //Deploy the www application
            dsl.Remote(server => server.Deploy.IisWebApplication(PublishedWebsitesPath, @"E:\www.condep-samples.io", "www.condep-samples.io"));
        }
    }
}