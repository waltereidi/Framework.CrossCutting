using FrameworkGoogleApi.GoogleDrive;
using FrameworkGoogleApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.GoogleTest
{
    public class GoogleDriveAPITest : Configuration
    {
        private readonly GoogleDriveRead _serviceRead;
        public GoogleDriveAPITest() :base("FrameworkGoogleApi" , "Configuration")
        {
            _serviceRead = new GoogleDriveRead();
        }
        [Fact]
        public void TestConfiguration()
        {
            var result = _serviceRead.GetDriveFiles();
            var result2 = _serviceRead.GetDriveFiles();
            var result3 = _serviceRead.GetDriveFiles();
            var result4 = _serviceRead.GetDriveFiles();

            Assert.NotNull(result);
        }
        [Fact]
        public void TestDownload()
        {

            var result = _serviceRead.GetDriveFiles();
            var file = result.First();
            
            IGoogleDriveDownloadStrategy downloadService = new GoogleDriveDownload(file,base._fileOutputDir);


            var fi = downloadService.Start();
            Assert.True(fi.Exists); 
        }

    }
}
