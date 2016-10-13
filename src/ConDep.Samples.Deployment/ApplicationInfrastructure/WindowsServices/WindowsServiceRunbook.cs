using System.ServiceProcess;
using ConDep.Dsl;
using ConDep.Dsl.Config;
using ConDep.Samples.Deployment.Cloud.AWS;

namespace ConDep.Samples.Deployment.ApplicationInfrastructure.WindowsServices
{
    public class WindowsServiceRunbook : Runbook
    {
        private const string SERVICE_CONTROLLER_EXE = @"%windir%\system32\sc.exe";
        private const string ServiceGroup = "ConDepServices";
        private const string Password = "SuperSecretPassword";

        protected string ServiceName { get; set; }
        protected string ServiceRelativeRootPath { get; set; }
        protected string DestinationDir { get; set; }

        protected WindowsServiceRunbook(string serviceName)
        {
            ServiceName = serviceName;
            ServiceRelativeRootPath = @"_PublishedServices\" + serviceName;
            DestinationDir = @"E:\WindowsServices\" + serviceName;
        }

        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            //Need server in aws to deploy to. This runbook bootstraps an instance in aws
            Runbook.Execute<AwsAppServerRunbook>(dsl, settings);

            var username = "ServiceUser";
            var env = settings.Environment();

            dsl.Local.TransformConfigFile(ServiceRelativeRootPath, ServiceName + ".exe.config", string.Format("App.{0}.config", env));

            dsl.Remote(server =>
            {
                server.Deploy.WindowsService(
                    serviceName: ServiceName,
                    displayName: ServiceName,
                    sourceDir: ServiceRelativeRootPath,
                    destDir: DestinationDir,
                    relativeExePath: ServiceRelativeRootPath + ServiceName + ".exe",
                    options: opt =>
                    {
                        opt
                            .DoNotStartAfterInstall()
                            .StartupType(ServiceStartMode.Automatic)
                            .ServiceGroup(ServiceGroup)
                            .EnableDelayedAutoStart()
                            .TimeoutInSeconds(240)
                            .OnServiceFailure(43200, failure =>
                            {
                                failure
                                    .FirstFailure.RestartService(10000)
                                    .SecondFailure.RestartService(10000)
                                    .SubsequentFailures.TakeNoAction();
                            });
                    });
            });
        }
    }
}
