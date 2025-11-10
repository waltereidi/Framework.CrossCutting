using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.GoogleTest
{
    public class GoogleDriveAPITest : Configuration
    {
        private readonly GoogleDriveAPI _service;
        public GoogleDriveAPITest() :base("Framework.Google" , "Configuration")
        {
            _service = new GoogleDriveAPI(base._projectDir.FullName);
        }
        [Fact]
        public void TestAPI()
        {
            _service.GetDrive();

        }

    }
}
