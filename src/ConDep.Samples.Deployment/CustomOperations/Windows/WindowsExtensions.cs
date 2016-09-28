using ConDep.Dsl.Builders;
using ConDep.Samples.Deployment.CustomOperations.Windows;

namespace ConDep.Dsl
{
    public static class WindowsExtensions
    {
        /// <summary>
        /// Disables User Account Control. This operation triggers a restart of the machine.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IOfferRemoteConfiguration DisableUserAccountControl(this IOfferRemoteConfiguration configuration)
        {
            var operation = new DisableUserAccountControlOperation();
            OperationExecutor.Execute((RemoteBuilder) configuration, operation);
            return configuration;
        }
    }
}
