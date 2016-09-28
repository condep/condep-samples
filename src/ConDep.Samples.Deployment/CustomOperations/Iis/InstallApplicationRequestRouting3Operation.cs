using System.Threading;
using ConDep.Dsl;
using ConDep.Dsl.Config;

namespace ConDep.Samples.Deployment.CustomOperations.Iis
{
    public class InstallApplicationRequestRouting3Operation : RemoteOperation
    {
        public override Result Execute(IOfferRemoteOperations remote, ServerConfig server, ConDepSettings settings, CancellationToken token)
        {
            remote.Execute.PowerShell("WebPICMD /Install /Products:ARRv3_0 /AcceptEULA");
            return new Result(true, false);
        }

        public override string Name
        {
            get { return "Install Application Request Routing v3.0"; }
        }
    }
}