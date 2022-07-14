using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace EaAuditConverter.AuditScheduler
{
    class XmlHelper
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

        public static XElement UnZipStream(Stream inStream)
        {
            var convertedText = string.Empty;
            byte[] buffer = new byte[2048];
            int size;
            int total = 0;
            var encoder = new UnicodeEncoding();
            using (var input = new ZipInputStream(inStream))
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
            var convertedXml = XElement.Parse(convertedText);

            return convertedXml;
        }
    }
}
