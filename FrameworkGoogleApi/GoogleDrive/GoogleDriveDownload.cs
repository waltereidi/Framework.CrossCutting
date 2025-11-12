using FrameworkGoogleApi.Configuration;
using FrameworkGoogleApi.Interfaces;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace FrameworkGoogleApi.GoogleDrive
{
    public class GoogleDriveDownload : GoogleCredentialsSetup , IGoogleDriveDownloadStrategy
    {
        public record DownloadRequest(string fileId , string fileName , DirectoryInfo output);
        private DownloadRequest _request;
        public GoogleDriveDownload(DownloadRequest request){ 
            _request = request;
        }
        public GoogleDriveDownload(Google.Apis.Drive.v3.Data.File file , DirectoryInfo output)
        {
            _request = new DownloadRequest(file.Id , file.Name , output );
        }

        public FileInfo Start()
        {
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = base.GetCredentials(),
                ApplicationName = "",
            });

            var request = service.Files.Get(_request.fileId);

            using (var memoryStream = new MemoryStream())
            {
                // Faz o download para o stream
                request.Download(memoryStream);
                var filePath = Path.Combine(_request.output.FullName, _request.fileName );
                // Salva no caminho especificado
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fileStream);
                    return new FileInfo(filePath);
                }
            }
        }
    }
}
