using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.IO;
using System.Reflection;

public class GoogleDriveAPI 
{
    private GoogleCredential _credentials;
    private readonly string _configFileName = "windy-ellipse-399512-d0795419b188.json";
    public GoogleDriveAPI()
    {
        string credentialPath = Path.Combine("Configuration","windy-ellipse-399512-d0795419b188.json");
        using (var stream = new FileStream( credentialPath , FileMode.Open, FileAccess.Read))
        {
            _credentials = GoogleCredential.FromStream(stream)
                .CreateScoped(DriveService.ScopeConstants.Drive);
        }
    }
    public GoogleDriveAPI(string credentialPath)
    {
        using (var stream = new FileStream(Path.Combine(credentialPath , _configFileName ), FileMode.Open, FileAccess.Read))
        {
            _credentials = GoogleCredential.FromStream(stream)
                .CreateScoped(DriveService.ScopeConstants.Drive);
        }
    }
    public void GetDrive()
    {

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = _credentials,
            ApplicationName = "MyDriveApp",
        });

        var drives = service.Drives.List();
        var drivesResult = drives.Execute();
        var request = service.Files.List();

        var result = request.Execute();

        foreach (var file in result.Files)
        {
            Console.WriteLine($"{file.Id} - {file.Name}");
        }
    }
}
