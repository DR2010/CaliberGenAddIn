using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip;

namespace EAAddIn.Applications
{
    public static class XmlHelpers
    {
        public static string SerializeObject<T>(object o)
        {
            MemoryStream ms = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8);
            xs.Serialize(xtw, o);
            ms = (MemoryStream)xtw.BaseStream;
            return StringHelpers.UTF8ByteArrayToString(ms.ToArray());
        }

        public static T DeserializeObject<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(StringHelpers.StringToUTF8ByteArray(xml));
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8);
            return (T)xs.Deserialize(ms);
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