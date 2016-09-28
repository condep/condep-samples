using System.Threading;
using ConDep.Dsl;
using ConDep.Dsl.Config;

namespace ConDep.Samples.Deployment.CustomOperations.Iis
{
    public class ConfigureIisMachineKeyOperation : RemoteOperation
    {
        private readonly string _validationKey;
        private readonly string _decryptionKey;
        private readonly string _validation;

        public ConfigureIisMachineKeyOperation(string validationKey, string decryptionKey, string validation)
        {
            _validationKey = validationKey;
            _decryptionKey = decryptionKey;
            _validation = validation;
        }

        public override Result Execute(IOfferRemoteOperations remote, ServerConfig server, ConDepSettings settings, CancellationToken token)
        {
            //Finds script in ps1 files as long as they are embedded resources.
            remote.Execute.PowerShell("Set-MachineKeys " + _validationKey + " " + _decryptionKey + " " + _validation);
            return new Result(true, false);
        }

        public override string Name { get { return "Setting IIS Machine Key."; } }
    }
}