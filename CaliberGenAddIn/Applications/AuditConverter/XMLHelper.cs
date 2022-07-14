using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace EAAddIn.Applications.AuditConverter
{
    class XMLHelper
    {

        public static string GetAttribute(XElement xmlToSearch, string elementLevel, string attributeName)
        {
            if (xmlToSearch.Descendants(elementLevel).Attributes(attributeName).Count() > 0)
            {
                var attribDetail = from e in xmlToSearch.Descendants(elementLevel)
                                   where e.Attribute(attributeName).Value != null
                                   select e;

                return attribDetail.Attributes(attributeName).First().Value;
            }
            return string.Empty;
        }

        public static string GetAttribute(XElement xmlToSearch, string attributeName)
        {
            return xmlToSearch.Attribute(attributeName).Value;
           
        }

        public static DateTime GetAttributeDateTime(XElement xmlToSearch, string attributeName)
        {
            return DateTime.Parse(xmlToSearch.Attribute(attributeName).Value);

        }

        public static XAttribute AddAttribute(string attribName, string attribvalue)
        {
            if (attribvalue != null)
            {
                return new XAttribute(attribName, attribvalue);
            }
            return new XAttribute(attribName, string.Empty);
            
        }

        public static XElement ConvertFromBase64(string binContents, bool unzip)
        {
            var convertedBytes = Convert.FromBase64String(binContents);
            MemoryStream stream = new MemoryStream(convertedBytes);

            var convertedText = string.Empty;
            byte[] buffer = new byte[2048];
            int size;
            int total = 0;
            var encoder = new UnicodeEncoding();


            if (unzip)
            {
                using (var input = new ZipInputStream(stream))
                {
                    ZipEntry e;
                    while ((e = input.GetNextEntry()) != null)
                    {
                        if (e.IsDirectory) continue;
                        while ((size = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            convertedText += encoder.GetString(buffer, 0, size);
                            total += size;
                        }
                    }
                }
            }
            else
            {
                while ((size = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    convertedText += encoder.GetString(buffer, 0, size);
                    total += size;
                }
            }

            var covertedXml = XElement.Parse(convertedText);
            return covertedXml;
        }
    }
}
