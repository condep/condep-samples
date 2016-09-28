using System;
using System.Threading;
using ConDep.Dsl;
using ConDep.Dsl.Config;

namespace ConDep.Samples.Deployment.CustomOperations.Windows
{
    public class DisableUserAccountControlOperation : RemoteOperation
    {
        public override Result Execute(IOfferRemoteOperations remote, ServerConfig server, ConDepSettings settings, CancellationToken token)
        {
            const string uacEnabled = @"
$val = Get-ItemProperty -Path hklm:software\microsoft\windows\currentversion\policies\system -Name ""EnableLUA""
if ($val.EnableLUA -eq 1){
    return $true
}
else {
    return $false
}
";

            const string restartNeeded = @"
$val = [Environment]::GetEnvironmentVariable(""CONDEP_RESTART_NEEDED"",""Machine"")

if($val -eq 'true'){
    return $true
}
else {
    return $false
}
";
            throw new NotImplementedException();
            // What about the OnlyIfs now?

            //Assume restart is not necessary.
            remote.Configure.EnvironmentVariable("CONDEP_RESTART_NEEDED", "false", EnvironmentVariableTarget.Machine);

            //Set uac if not set. Set env variable for restarting server necessary.
            //server
            //    .OnlyIf(uacEnabled)
            //        .Configure
            //            .RegistryKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "EnableLUA", "0", "DWord")
            //            .EnvironmentVariable("CONDEP_RESTART_NEEDED", "true", EnvironmentVariableTarget.Machine);

            //Restart server and set env variable for restart NOT necessary, since the machine rebooted.
            //server
            //    .OnlyIf(restartNeeded)
            //        .Restart()
            //        .Configure.EnvironmentVariable("CONDEP_RESTART_NEEDED", "false", EnvironmentVariableTarget.Machine);


            return new Result(true, false);
        }

        public override string Name { get { return "Disable User Account Control (UAC)"; } }
    }
}