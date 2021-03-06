# HERE ARE SOME EXAMPLES OF HOW YOU RUN CONDEP.EXE TO DEPLOY DIFFERENT SERVERS, APPLICATIONS AND SERVERS.

# Deploy www.condep-samples.no to test
#.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll TestWebServer WwwApplication 

# Deploy www.condep-samples.no to production
#.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll ProdWebServer WwwApplication

# Deploy www.condep-samples.no to cloud
#.\output\artifacts\ConDep.exe deploy ConDep.Samples.Deployment.dll CloudServer WwwApplication

# Deploy api.condep-samples.no to test
#.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll TestApiServer WebApi 

# Deploy api.condep-samples.no to production
#.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll ProdApiServer WebApi

# Deploy api.condep-samples.no to cloud
#.\output\artifacts\ConDep.exe deploy ConDep.Samples.Deployment.dll CloudServer WebApi

# Deploy service to test
#.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll TestServiceServer WindowsService 

# Deploy service to production
#.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll ProdServiceServer WindowsService

#Deploy service to cloud
#.\output\artifacts\ConDep.exe deploy ConDep.Samples.Deployment.dll CloudServer WindowsService