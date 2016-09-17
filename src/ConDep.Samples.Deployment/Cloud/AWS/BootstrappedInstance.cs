using ConDep.Dsl;
using ConDep.Dsl.Config;
using System;

namespace ConDep.Samples.Deployment.Cloud.AWS
{
    public class BootstrappedInstance : Runbook
    {
        private const string SubNet = "subnet1";
        private const string InstanceType = "c3.large";
        private const string SecurityGroupIds = "sg-1";

        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            dsl.Local.Aws(aws => aws.Ec2.CreateInstances(new Guid().ToString(),
                opt =>
                {
                    opt.Tags.AddName("condep-sample-server1");
                    opt.AvailabilityZone("eu-west-1a");
                    opt.InstanceCount(1, 1);
                    opt.InstanceType(InstanceType);
                    opt.SecurityGroupIds(SecurityGroupIds);
                    opt.ShutdownBehavior(AwsShutdownBehavior.Terminate);
                    opt.SubnetId(SubNet);
                    opt.Disks.Add("xvdb",
                        ebs =>
                        {
                            ebs.DeleteOnTermination(true);
                            ebs.VolumeSize(10);
                        });
                    opt.Image.LatestBaseWindowsImage(AwsWindowsImage.Win2012R2);
                }));
        }
    }
}
