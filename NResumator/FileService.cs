using System;
using System.IO;
using System.Net;
using System.Text;
using Spire.Pdf;

namespace NResumator
{
    public class FileService
    {
        public static string GetTextFromUrl(string resumeFolder, string url)
        {
            if (url.ToLower().EndsWith("pdf"))
            {
                return GetTextFromPdf(resumeFolder, url);
            }

            throw new ApplicationException("URL is not in a supported format");
        }

        private static string GetTextFromPdf(string resumeFolder, string url)
        {
            //TODO: could improve this guard with a URI validation of url
            if (url.ToLower().EndsWith("pdf") && url.ToLower().StartsWith("http"))
            {
                string pdfFilePath = GetFileFromUrl(resumeFolder, url);

                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(pdfFilePath);

                StringBuilder buffer = new StringBuilder();

                foreach (PdfPageBase page in doc.Pages)
                {
                    buffer.Append((string)page.ExtractText());
                }

                doc.Close();

                return buffer.ToString();
            }

            throw new ApplicationException("GetTextFromPdfUrl only accepts urls that end with pdf");
        }

        private static string GetFileFromUrl(string resumeFolder, string url)
        {
            try
            {
                //ensure directory doesn't exist, create it
                if (!Directory.Exists(resumeFolder))
                    Directory.CreateDirectory(resumeFolder);

                WebClient Client = new WebClient();
                Guid g = Guid.NewGuid();
                string file = Path.Combine(resumeFolder, String.Format("{0}.pdf", g));
                Client.DownloadFile(url, file);

                return file;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Something happened while download resume!");
            }
        }
    }
}
