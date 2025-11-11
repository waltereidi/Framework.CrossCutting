using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Google.Configuration
{
    public abstract class GoogleCredentialsSetup 
    {
        private GoogleCredential _credentials;
        private readonly string _configFileName = "credentials.json";
        public GoogleCredentialsSetup()
        {
            string credentialPath = Path.Combine("Configuration", _configFileName);
            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                _credentials = GoogleCredential.FromStream(stream)
                    .CreateScoped(DriveService.ScopeConstants.Drive);
            }
        }
        public GoogleCredentialsSetup(string credentialPath)
        {
            using (var stream = new FileStream(Path.Combine(credentialPath, _configFileName), FileMode.Open, FileAccess.Read))
            {
                _credentials = GoogleCredential.FromStream(stream)
                    .CreateScoped(DriveService.ScopeConstants.Drive);
            }
        }
        public GoogleCredential GetCredentials() => _credentials;

    }
}
