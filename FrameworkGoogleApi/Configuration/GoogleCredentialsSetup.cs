using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkGoogleApi.Configuration
{
    public abstract class GoogleCredentialsSetup 
    {
        private GoogleCredential _credentials;
        private DirectoryInfo _projectDir;
        private readonly string _configFileName = "credentials.json";
        public GoogleCredentialsSetup()
        {
            _projectDir = new
                (
                    Path.Combine(
                        Directory.GetParent(AppContext.BaseDirectory)
                            .Parent.Parent.Parent.Parent.FullName, "FrameWorkGoogleApi")
                );

            string credentialPath = Path.Combine("Configuration", _configFileName);
            if (File.Exists(credentialPath))
            {
                using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
                {
                    _credentials = GoogleCredential.FromStream(stream)
                        .CreateScoped(DriveService.ScopeConstants.Drive);
                }
            }

            using (var stream = new FileStream(Path.Combine(_projectDir.FullName ,"Configuration",_configFileName), FileMode.Open, FileAccess.Read))
            {
                _credentials = GoogleCredential.FromStream(stream)
                    .CreateScoped(DriveService.ScopeConstants.Drive);
            }
            
        }
        public GoogleCredential GetCredentials() => _credentials;

    }
}
