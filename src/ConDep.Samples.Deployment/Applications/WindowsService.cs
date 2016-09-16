using ConDep.Samples.Deployment.Infrastructure.WindowsServices;

namespace ConDep.Samples.Deployment.Applications
{
    public class WindowsService : WindowsServiceRunbook
    {
        public WindowsService() : base("ConDep.Samples.WindowsService") { }
    }
}
