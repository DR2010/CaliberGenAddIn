using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace EAAddIn.Applications.SpecificationGenerator
{
    public class SpecificationEngine
    {
        protected object EndOfDoc = "\\endofdoc";
        protected _Application Word { get; set; }
        protected _Document Document;
        protected object Missing = System.Reflection.Missing.Value;

        public void CreateDocumentFromTemplate(object template)
        {
            Word = new Application { Visible = false };

            Document = Word.Documents.Add(ref template, ref Missing,
                                          ref Missing, ref Missing);
        }


        public void AddElementNotes(Element element, bool suppressName = false)
        {
            if (!suppressName)
            {
                AddItalisisedText(element.Name + ": ");
            }

            var rtfLinkedDocument = element.GetLinkedDocument();

            if (!string.IsNullOrEmpty(rtfLinkedDocument))
            {
                Clipboard.SetData(DataFormats.Rtf, rtfLinkedDocument);

                AddRtfTextFromClipboard();
            }
            else if (!string.IsNullOrEmpty(element.Notes))
            {
                var notes = element.Notes;

                notes = CleanNotes(notes);
                var formattedText = AddInRepository.Instance.Repository.GetFormatFromField("RTF",
                                                                                            notes);
                Clipboard.SetData(DataFormats.Rtf, formattedText);

                AddRtfTextFromClipboard();
            }
        }

        private void AddItalisisedText(string text)
        {
            Paragraph paragraph;
            object range =
                   Document.Bookmarks.get_Item(ref EndOfDoc).Range;
            paragraph = Document.Content.Paragraphs.Add(ref range);
            paragraph.Range.Italic = 1;
            paragraph.Range.Text = HtmlHelper.StripHtmlAdvanced(text);
            object styleName = WdBuiltinStyle.wdStyleNormal;
            paragraph.Range.set_Style(ref styleName);
            paragraph.Range.Select();
            paragraph.Range.Italic = 1;
            paragraph.Range.InsertParagraphAfter();
        }
        private void AddText(string text)
        {
            Paragraph paragraph;
            object range =
                   Document.Bookmarks.get_Item(ref EndOfDoc).Range;
            paragraph = Document.Content.Paragraphs.Add(ref range);
            paragraph.Range.Text = HtmlHelper.StripHtmlAdvanced(text);
            object styleName = WdBuiltinStyle.wdStyleNormal;
            paragraph.Range.set_Style(ref styleName);

            paragraph.Range.InsertParagraphAfter();
        }
        public void AddRtfTextFromClipboard()
        {
            Paragraph paragraph;
            object range =
                   Document.Bookmarks.get_Item(ref EndOfDoc).Range;
            paragraph = Document.Content.Paragraphs.Add(ref range);
            paragraph.Range.Paste();

            paragraph.Range.InsertParagraphAfter();
        }
        public string CleanNotes(string notes)
        {
            while (notes.Contains(Environment.NewLine + " " + Environment.NewLine))
            {
                notes = notes.Replace(Environment.NewLine + " " + Environment.NewLine, Environment.NewLine);
            }
            while (notes.Contains(Environment.NewLine + Environment.NewLine))
            {
                notes = notes.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
            }

            return notes;
        }
        public void PasteIntoDocument()
        {
            Paragraph paragraph;
            object range =
                   Document.Bookmarks.get_Item(ref EndOfDoc).Range;
            paragraph = Document.Content.Paragraphs.Add(ref range);
            paragraph.Range.Paste();
            paragraph.Range.InsertParagraphAfter();
        }
        public void CreateDocumentHeading(string Name, int level = 1)
        {
            Paragraph paragraph;
            object range = Document.Bookmarks.get_Item(ref EndOfDoc).Range;
            paragraph = Document.Content.Paragraphs.Add(ref range);


            object styleName;

            switch (level)
            {
                case 1:
                    styleName = WdBuiltinStyle.wdStyleHeading1;
                    break;
                case 2:
                    styleName = WdBuiltinStyle.wdStyleHeading2;
                    break;
                case 3:
                    styleName = WdBuiltinStyle.wdStyleHeading3;
                    break;
                case 4:
                    styleName = WdBuiltinStyle.wdStyleHeading4;
                    break;
                case 5:
                    styleName = WdBuiltinStyle.wdStyleHeading5;
                    break;
                default:
                    styleName = WdBuiltinStyle.wdStyleHeading5;
                    break;
            }


            paragraph.Range.Text = Name;
            paragraph.Range.set_Style(styleName);
            paragraph.Range.InsertParagraphAfter();
        }
 
    }
}
