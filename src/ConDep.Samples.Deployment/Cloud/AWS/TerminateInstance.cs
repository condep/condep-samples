using ConDep.Dsl;
using ConDep.Dsl.Config;
using System;

namespace ConDep.Samples.Deployment.Cloud.AWS
{
    public class TerminateInstance : Runbook
    {
        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            dsl.Local.Aws(aws => aws.Ec2.TerminateInstances(new Guid().ToString()));
        }
    }
}
