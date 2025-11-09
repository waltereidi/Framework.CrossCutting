using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

public class PdfPig
{
    public void GetPdfPage(FileInfo file ,DirectoryInfo output)
    {

        using (var pdf = PdfDocument.Open(file.FullName))
        {
            int pageNumber = 1;
            foreach (var page in pdf.GetPages())
            {
                foreach (var img in page.GetImages())
                {
                    File.WriteAllBytes(Path.Combine(output.FullName,"img_{pageNumber}.png"), img.RawBytes.ToArray());
                }
                pageNumber++;
            }
        }
    }
    
}
