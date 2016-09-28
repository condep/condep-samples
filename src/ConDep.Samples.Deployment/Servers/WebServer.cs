using ConDep.Dsl;
using ConDep.Dsl.Config;

namespace ConDep.Samples.Deployment.Servers
{
    public class WebServer : Runbook
    {
        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            dsl.Remote(server =>
            {
                server.Configure.DisableUserAccountControl();
                server.Configure.IisMachineKey(
                    validationKey: (string)settings.Config.OperationsConfig.MachineKey.ValidationKey,
                    decryptionKey: (string)settings.Config.OperationsConfig.MachineKey.DecryptionKey,
                    validation: "SHA1");
                //server.Install.WebPlatformInstaller();
                //server.Install.WebPlatformInstallerCommandLine();
                server.Install.ApplicationRequestRouting3();
                server.Install.UrlRewrite2();
                //server.Install.DotNet46();
                //server.Install.NodeJs("4.2.1");
                //server.Install.Java();
                server.Install.IisNode();
                //server.Install.Mvc3();
                //server.Install.Mvc4();
                //server.Install.WindowsUpdate("KB2992080", "Microsoft ASP.Net Web Frameworks 5.0 Security Update (KB2992080)");
                //server.Install.WindowsUpdate("KB2994397", "Microsoft ASP.Net Web Frameworks 5.1 Security Update (KB2994397)");
                server.Configure.Windows(win => win.InstallFeature("MSMQ-Server"));
                server.Configure.Windows(win => win.InstallFeature("Web-Request-Monitor"));
                server.Configure.Windows(win => win.InstallFeature("Web-AppInit"));
                server.Execute.PowerShell("netsh int tcp set global ecncapability=disabled");
            });
        }
    }
}
