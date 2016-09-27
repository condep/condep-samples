using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ConDep.Dsl;
using ConDep.Dsl.Config;
using ConDep.Dsl.Operations.Infrastructure.IIS;

namespace ConDep.Samples.Deployment.ApplicationInfrastructure.Iis
{
    public class WebSiteRunbook : Runbook
    {
        private readonly int _webSiteId;
        private readonly string _appPoolUser;
        private readonly bool _ssl;

        protected WebSiteRunbook(string webSitePrefix, int webSiteId, string appPoolUser, bool ssl = true)
        {
            AppPoolIdleTimeout = 720;
            AlwaysOn = true;
            WebSitePrefix = webSitePrefix;
            _webSiteId = webSiteId;
            _appPoolUser = appPoolUser;
            _ssl = ssl;
        }

        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            var env = settings.Environment();
            EnvironmentSettings = Settings[env];

            //Include ASP.NET 3.5
            dsl.Remote(server => server.Configure.IIS(opt => opt.Include.AspNet35()));

            //TODO .OnlyIf(condition => condition.OperatingSystem.BuildNumber >= 9200)
            dsl.Remote(server => server.Configure.IIS(opt => opt.Include.AspNet45().AspNet45Ext()));

            // Deploy SSL cert if website requires ssl binding
            if (_ssl)
            {
                dsl.Remote(server => server.Deploy
                    .SslCertificate().FromFile(
                        EnvironmentSettings.CertificatePath,
                        EnvironmentSettings.CertificatePassword,
                        opt => opt
                            .AddPrivateKeyPermission(_appPoolUser)
                    ));
            }

            dsl.Remote(server => server.Configure
                .IISAppPool(SiteName, opt => opt
                    .Identity.SpecificUser(_appPoolUser, "secureServiceUserPassword")
                    .NetFrameworkVersion(NetFrameworkVersion.Net4_0)
                    .ManagedPipeline(ManagedPipeline.Integrated)
                    .LoadUserProfile(true)
                    .IdleTimeoutInMinutes(AppPoolIdleTimeout)
                    .AlwaysOn(AlwaysOn)
                )
                .IISWebSite(SiteName, _webSiteId, opt =>
                {
                    if (_ssl)
                    {
                        opt.AddHttpsBinding(
                            X509FindType.FindBySubjectName,
                            EnvironmentSettings.CertificateSubjectName,
                            binding => binding.HostName(HostName).Port(443));
                    }
                    else
                    {
                        opt.AddHttpBinding(binding => binding.HostName(HostName).Port(80));
                    }

                    opt
                        .ApplicationPool(SiteName)
                        .PhysicalPath(@"E:\" + SiteName)
                        .LogDirectory(@"F:\" + SiteName);
                }
            ));
        }

        public bool AlwaysOn { get; set; }
        public int AppPoolIdleTimeout { get; set; }
        public EnvironmentSetting EnvironmentSettings { get; set; }
        public string SiteName { get { return WebSitePrefix + ".condep-samples.io"; } }
        public string HostName { get { return WebSitePrefix + "." + EnvironmentSettings.Suffix; } }
        public string WebSitePrefix { get; set; }

        public static readonly Dictionary<string, EnvironmentSetting> Settings = new Dictionary<string, EnvironmentSetting>
        {
            {"Test", new EnvironmentSetting("password_test", "test.condep-samples.io", Environments.Test)},
            {"Prod", new EnvironmentSetting("password", "condep-samples.io", Environments.Prod)},
        };
    }
}