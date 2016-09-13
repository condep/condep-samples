namespace ConDep.Samples.Deployment.Infrastructure
{
    public class EnvironmentSetting
    {
        public EnvironmentSetting(string certificatePassword, string suffix, Environments environment)
        {
            CertificatePassword = certificatePassword;
            Suffix = suffix;
            Environment = environment;
        }

        public string CertificatePath
        {
            get
            {
                return string.Format(@"Files\Certificates\wildcard.{0}.pfx", Suffix);
            }
        }

        public string CertificateSubjectName
        {
            get
            {
                return string.Format("*.{0}", Suffix);
            }
        }

        public string CertificatePassword { get; private set; }
        public string Suffix { get; set; }
        public Environments Environment { get; set; }
    }
}