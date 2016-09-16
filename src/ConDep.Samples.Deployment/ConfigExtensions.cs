using ConDep.Dsl.Config;

namespace ConDep.Samples.Deployment
{
    public static class ConfigExtensions
    {
        public static string Environment(this ConDepSettings settings)
        {
            if (settings.IsTestEnv())
                return "Test";
            if (settings.IsProdEnv())
                return "Prod";
            return "";
        }

        public static bool IsTestEnv(this ConDepSettings settings)
        {
            var env = settings.Config.EnvironmentName.ToLowerInvariant();
            return env == "testwebserver"
                || env == "testapiserver";
        }

        public static bool IsProdEnv(this ConDepSettings settings)
        {
            var env = settings.Config.EnvironmentName.ToLowerInvariant();
            return env == "prodwebserver"
                || env == "prodapiserver";
        }
    }
}
