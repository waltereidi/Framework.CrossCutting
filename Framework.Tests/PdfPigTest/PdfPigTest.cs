namespace Framework.Tests.PdfPigTest
{
    public class PdfPigTest : Configuration
    {
        private readonly PdfPig _service; 
        public PdfPigTest()
        { 
            _service = new PdfPig();
        }
        [Fact]
        public void TestPdfPig()
        {
            var fi = base.GetTestFile("file.pdf");
            _service.GetPdfPage(fi , base._fileOutputDir);

        }
    }
}