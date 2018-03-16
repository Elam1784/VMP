using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using OpenXmlPowerTools;
using DocumentFormat.OpenXml;

namespace VMP_1._0
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        { 
           //  SearchAndReplace(@"C:\\Publish\\test.docx");
            // replaceFooter(@"C:\\Publish\\test.docx");
            // replaceHeader();
            // replaceFooterNew();
            converttoPDF();
        }

        private void converttoPDF()
        {
           Spire.Doc.Document doc = new Spire.Doc.Document();
            doc.LoadFromFile(@"C:\\Publish\\test1.docx");
            doc.SaveToFile(@"C:\\Publish\\test1.PDF", Spire.Doc.FileFormat.PDF);
        }

        public static void SearchAndReplace(string document)
        {
            using (WordprocessingDocument doc =
            WordprocessingDocument.Open(document, true))
            {
                SimplifyMarkupSettings settings = new SimplifyMarkupSettings
                {
                    NormalizeXml = true, // Merges Run's in a paragraph with similar formatting
                };
                MarkupSimplifier.SimplifyMarkup(doc, settings);
                //string docText1 = null;
                //using (StreamReader sr1 = new StreamReader(doc.MainDocumentPart.GetStream()))
                //{
                //    docText1 = sr1.ReadToEnd();
                //}
                Regex regexText = new Regex("<BusinessDescription>");
               
                ReplaceParagraphParts(doc.MainDocumentPart.Document, regexText);
            }
        }

        private static void replaceFooter(string document)
        {
            //open file
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(document, true))
            {
                SimplifyMarkupSettings settings = new SimplifyMarkupSettings
                {
                    NormalizeXml = true, // Merges Run's in a paragraph with similar formatting
                };
                MarkupSimplifier.SimplifyMarkup(wordDoc, settings);

                Regex regex = new Regex("<Text>", RegexOptions.Compiled);
                foreach (FooterPart footerPart in wordDoc.MainDocumentPart.FooterParts)
                {
                    ReplaceParagraphParts(footerPart.Footer, regex);
                }
                regex= new Regex("<Sample>", RegexOptions.Compiled);
                foreach (FooterPart footerPart in wordDoc.MainDocumentPart.FooterParts)
                {
                    ReplaceParagraphParts(footerPart.Footer, regex);
                }
            }
        }

        private static void ReplaceParagraphParts(OpenXmlElement element, Regex regex)
        {
            foreach (var paragraph in element.Descendants<Paragraph>())
            {
                Match match = regex.Match(paragraph.InnerText);
                if (match.Success)
                {
                    //create a new run and set its value to the correct text
                    //this must be done before the child runs are removed otherwise
                    //paragraph.InnerText will be empty
                    Run newRun = new Run();
                    newRun.AppendChild(new Text(paragraph.InnerText.Replace(match.Value, "some new value")));
                    //remove any child runs
                    paragraph.RemoveAllChildren<Run>();
                    //add the newly created run
                    paragraph.AppendChild(newRun);
                }
            }
        }

        private void replaceFooterNew()
        {
            //file name
            string folder = @"C:\\Publish";
            string targetFileName = folder + @"\Template.docx";
            string templateFileNameTemplate = folder + @"\test.docx";

            //copy template file
            if (File.Exists(targetFileName) == true)
            {
                File.Delete(targetFileName);
            }

            File.Copy(templateFileNameTemplate, targetFileName);

            //open file
            using (var file = WordprocessingDocument.Open(targetFileName, true))
            {
                //find the Date field within the header stream and replace it
                string content = null;

                using (StreamReader reader = new StreamReader(
                    file.MainDocumentPart.FooterParts.First().GetStream()))
                {
                    content = reader.ReadToEnd();
                }

                Regex expression = new Regex("My_Date");
                content = expression.Replace(content,"Sathvik");

                using (StreamWriter writer = new StreamWriter(file.MainDocumentPart.FooterParts.First().GetStream(FileMode.Create)))
                {
                    writer.Write(content);
                }

                //save                
                file.MainDocumentPart.Document.Save();
            }
        }
    }
}