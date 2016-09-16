# HERE ARE SOME EXAMPLES OF HOW YOU RUN CONDEP.EXE TO DEPLOY DIFFERENT SERVERS, APPLICATIONS AND SERVERS.

# Deploy www.condep-samples.no to test
.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll WwwApplication TestWebServer 

# Deploy www.condep-samples.no to production
.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll WwwApplication ProdWebServer

# Deploy api.condep-samples.no to test
.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll WebApi TestApiServer 

# Deploy api.condep-samples.no to production
.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll WebApi ProdApiServer

# Deploy service to test
.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll WindowsService TestServiceServer 

# Deploy service to production
.output\artifact\ConDep.Samples.Deployment\ConDep.exe deploy dll WindowsService ProdServiceServer