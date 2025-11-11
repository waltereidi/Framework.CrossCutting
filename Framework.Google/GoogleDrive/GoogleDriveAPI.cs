using Framework.Google.Configuration;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

public class GoogleDriveAPI : GoogleCredentialsSetup
{
    private string _pageToken { get; set; }
    public GoogleDriveAPI() : base()
    { 
    }
    public GoogleDriveAPI(string testConfigDir) : base(testConfigDir)
    {
    }
    public IEnumerable<Google.Apis.Drive.v3.Data.File> GetDriveFiles()
    {

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = base.GetCredentials(),
            ApplicationName = "MyDriveApp",
        });
        var request = service.Files.List();
        request.PageSize = 50;
        request.PageToken = _pageToken;

        var result = request.Execute();

        return result.Files;

    }
}
