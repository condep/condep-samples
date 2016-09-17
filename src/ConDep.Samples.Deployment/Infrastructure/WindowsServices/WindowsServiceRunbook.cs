using ConDep.Dsl;
using ConDep.Dsl.Config;
using System.ServiceProcess;

namespace ConDep.Samples.Deployment.Infrastructure.WindowsServices
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
            var username = "ServiceUser";
            var env = settings.Environment();

            dsl.Local.TransformConfigFile(ServiceRelativeRootPath, ServiceName + ".dll.config", string.Format("App.{0}.config", env));

            dsl.Remote(server =>
            {
                server.Deploy
                    .WindowsService(
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
                                .UserName(username)
                                .Password(Password)
                                .ServiceGroup(ServiceGroup)
                                .OnServiceFailure(43200, failure =>
                                {
                                    failure
                                        .FirstFailure.RestartService(10000)
                                        .SecondFailure.RestartService(10000)
                                        .SubsequentFailures.TakeNoAction();
                                });
                        });
                                
                    server.Execute.DosCommand(string.Format("{0} config \"{1}\" start= \"delayed-auto\"", SERVICE_CONTROLLER_EXE, ServiceName));

                    server.Execute.PowerShell(string.Format("Start-ConDepWinService '{0}' 240 $false", ServiceName));
                });
        }
    }
}
