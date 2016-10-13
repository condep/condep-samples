using ConDep.Dsl;
using ConDep.Samples.Deployment.ApplicationInfrastructure.WindowsServices;
using ConDep.Samples.Deployment.Cloud.AWS;

namespace ConDep.Samples.Deployment.Applications
{
    public class WindowsService : WindowsServiceRunbook
    {
        public WindowsService() : base("ConDep.Samples.WindowsService") { }
    }
}
