using ConDep.Dsl;
using ConDep.Dsl.Config;

namespace ConDep.Samples.Deployment.Cloud.AWS
{
    public class AwsAppServerRunbook : Runbook
    {
        private const string SubNet = "subnet-1";
        private const string InstanceType = "t2.small";
        private const string SecurityGroupIds = "sg-1";

        public override void Execute(IOfferOperations dsl, ConDepSettings settings)
        {
            dsl.Local.Aws(aws => aws.Ec2.CreateInstances("5cc8f357-b3f9-4106-b05f-7e88b771f0b4",
                opt =>
                {
                    opt.Tags.AddName("condep-sample-server1");
                    opt.AvailabilityZone("eu-central-1a");
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
