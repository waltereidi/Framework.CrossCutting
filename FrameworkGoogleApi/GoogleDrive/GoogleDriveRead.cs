using FrameworkGoogleApi.Configuration;
using Google.Apis.Drive.v3;
using Google.Apis.Services;


namespace FrameworkGoogleApi.GoogleDrive
{
    public class GoogleDriveRead : GoogleCredentialsSetup
{
    private string _pageToken { get; set; }
    public GoogleDriveRead() 
    { 
        _pageToken = string.Empty;
    }
    public IEnumerable<Google.Apis.Drive.v3.Data.File> GetDriveFiles()
    {

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = base.GetCredentials(),
            ApplicationName = "",
        });

        var request = service.Files.List();
        request.PageSize = 50;
        request.PageToken = _pageToken;
        
        var result = request.Execute();
        _pageToken = result.NextPageToken;
        return result.Files;
    }



}
}