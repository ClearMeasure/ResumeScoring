using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Spire.Doc;
using Spire.Pdf;

namespace ResumeScoring
{
    public class FileService
    {
        public static string GetTextFromUrl(string resumeFolder, string url)
        {
            if (url.ToLower().EndsWith("pdf"))
            {
                return GetTextFromPdf(resumeFolder, url);
            }

            if (url.ToLower().EndsWith("doc") || url.ToLower().EndsWith("docx"))
            {
                return GetTextFromDoc(resumeFolder, url);
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

                StringBuilder resume = new StringBuilder();

                foreach (PdfPageBase page in doc.Pages)
                {
                    resume.Append((string)page.ExtractText());
                }

                doc.Close();

                return resume.ToString();
            }

            throw new ApplicationException("GetTextFromPdf only accepts urls that end with pdf");
        }

        private static string GetTextFromDoc(string resumeFolder, string url)
        {
            if ((url.ToLower().EndsWith("doc") || url.ToLower().EndsWith("docx")) && url.ToLower().StartsWith("http"))
            {
                string docFilePath = GetFileFromUrl(resumeFolder, url);

                //Open word document
                Document document = new Document();
                
                document.LoadFromFile(docFilePath);

                //Save doc file.
                string txtFileName = Guid.NewGuid() + ".txt";
                document.SaveToFile(Path.Combine(resumeFolder, txtFileName), Spire.Doc.FileFormat.Txt);

                string resume = File.ReadAllText(Path.Combine(resumeFolder, txtFileName));

                return resume;
            }

            throw new ApplicationException("GetTextFromDoc only accepts urls that end with doc or docx");
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
                string ext = url.Split('.')[url.Split('.').Count() - 1];
                string file = Path.Combine(resumeFolder, String.Format("{0}.{1}", g, ext));
                Console.WriteLine(file);
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
