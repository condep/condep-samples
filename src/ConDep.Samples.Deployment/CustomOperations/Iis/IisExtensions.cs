using ConDep.Dsl.Builders;
using ConDep.Samples.Deployment.CustomOperations.Iis;

namespace ConDep.Dsl
{
    public static class IisExtensions
    {
        public static IOfferRemoteConfiguration IisMachineKey(this IOfferRemoteConfiguration configuration, string validationKey, string decryptionKey, string validation)
        {
            var operation = new ConfigureIisMachineKeyOperation(validationKey, decryptionKey, validation);
            OperationExecutor.Execute((RemoteBuilder) configuration, operation);
            return configuration;
        }

        public static IOfferRemoteInstallation IisNode(this IOfferRemoteInstallation installation)
        {
            var operation = new InstallIisNodeOperation();
            OperationExecutor.Execute((RemoteBuilder)installation, operation);
            return installation;
        }

        public static IOfferRemoteInstallation ApplicationRequestRouting3(this IOfferRemoteInstallation installation)
        {
            var operation = new InstallApplicationRequestRouting3Operation();
            OperationExecutor.Execute((RemoteBuilder) installation, operation);
            return installation;
        }

        public static IOfferRemoteInstallation UrlRewrite2(this IOfferRemoteInstallation installation)
        {
            var operation = new InstallUrlRewrite2Operation();
            OperationExecutor.Execute((RemoteBuilder) installation, operation);
            return installation;
        }
    }
}
