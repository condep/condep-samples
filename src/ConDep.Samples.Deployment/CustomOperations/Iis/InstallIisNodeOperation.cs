using System;
using System.Threading;
using ConDep.Dsl;
using ConDep.Dsl.Config;

namespace ConDep.Samples.Deployment.CustomOperations.Iis
{
    public class InstallIisNodeOperation : RemoteOperation
    {
        public override Result Execute(IOfferRemoteOperations remote, ServerConfig server, ConDepSettings settings, CancellationToken token)
        {
            remote.Install
                .Msi("iisnode for iis 7.x (x64) full",
                new Uri("https://github.com/tjanczuk/iisnode/releases/download/v0.2.16/iisnode-full-v0.2.16-x64.msi"));
            return new Result(true, false);
        }

        public override string Name
        {
            get { return "Install IIS Node"; }
        }
    }
}