using System.Threading;
using ConDep.Dsl;
using ConDep.Dsl.Config;

namespace ConDep.Samples.Deployment.CustomOperations.Iis
{
    public class InstallUrlRewrite2Operation : RemoteOperation
    {
        public override Result Execute(IOfferRemoteOperations remote, ServerConfig server, ConDepSettings settings, CancellationToken token)
        {
            remote.Execute.PowerShell("WebPICMD /Install /Products:UrlRewrite2 /AcceptEULA");
            return new Result(true, false);
        }

        public override string Name
        {
            get { return "Install URL Rewrite 2"; }
        }
    }
}