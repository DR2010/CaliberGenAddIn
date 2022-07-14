using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CICSWeb.Net
{
    public class Response
    {
        private readonly string response;
        private readonly Dictionary<string, string> statusMessages = new Dictionary<string, string>();

        public Response(string response)
        {
            this.response = response;
        }
        public string ConstructXmlResponse()
        {
            var xmlResponseBuilder = new StringBuilder();
            var xmlResponseWriter = new XmlTextWriter(new StringWriter(xmlResponseBuilder));

            xmlResponseWriter.WriteStartElement("CVSXML");

            // CVS blocks are tab-separated.
            var blocks = this.response.Split('\t');
            foreach (var cvsBlock in blocks)
            {
                // Ignore empty blocks (caused by double tab characters).
                if (string.IsNullOrEmpty(cvsBlock))
                {
                    continue;
                }

                // Separate field and value
                var fieldValueSeparatorIndex = cvsBlock.IndexOf('=');
                if (fieldValueSeparatorIndex == -1)
                {
                    throw new ArgumentException(String.Format("Invalid CVS field data: '{0}'.", cvsBlock));
                }

                // Some field names contain a ',' character. If so, then the field name must be prefixed by "A",
                // and an attribute called "i" must be added with the number after the comma.
                string[] fieldNameAndIndex = cvsBlock.Substring(0, fieldValueSeparatorIndex).Split(new[] { ',' }, 2);
                if (fieldNameAndIndex.Length == 2)
                {
                    // eg. <A5 i="123" />
                    xmlResponseWriter.WriteStartElement(string.Format("A{0}", fieldNameAndIndex[0]));
                    xmlResponseWriter.WriteAttributeString("i", fieldNameAndIndex[1]);
                }
                else
                {
                    // eg. <theFieldName />
                    xmlResponseWriter.WriteStartElement(fieldNameAndIndex[0]); // Field name.
                }


                // Write the data.
                xmlResponseWriter.WriteString(cvsBlock.Substring(fieldValueSeparatorIndex + 1));

                // Close.
                xmlResponseWriter.WriteEndElement(); // Field name.
            }

            xmlResponseWriter.WriteEndElement(); // CVSXML

            xmlResponseWriter.Flush();
            xmlResponseWriter.Close();
            return xmlResponseWriter.ToString();

        }
        public WrapperConfig ReadResponseToData(WrapperConfig data)
        {
            string[] blocks = this.response.Split('\t');
            WrapperConfigItem previous = null;
            //data.ChildrenExport = new CICSParameterCollection();
            foreach (var cvsBlock in blocks)
            {
                // Ignore empty blocks (caused by double tab characters).
                if ( string.IsNullOrEmpty(cvsBlock))
                {
                    continue;
                }

                // Separate field and value
                int fieldValueSeparatorIndex = cvsBlock.IndexOf('=');
                if (fieldValueSeparatorIndex == -1)
                {
                    throw new ArgumentException(String.Format("Invalid CVS field data: '{0}'.", cvsBlock));
                }

                // Some field names contain a ',' character. If so, then the field name must be prefixed by "A",
                // and an attribute called "i" must be added with the number after the comma.
                var fieldNameAndIndex = cvsBlock.Substring(0, fieldValueSeparatorIndex).Split(new[] { ',' }, 2);
                if (fieldNameAndIndex.Length == 2)
                {
                    //WrapperConfigItem item = data.exports[int.Parse(fieldNameAndIndex[0]) - 1 - offset];
                    var item = data.GetValueByArrayKey(1, string.Format("{0},{1}", fieldNameAndIndex[0], fieldNameAndIndex[1]), null);
                    if (item == null)
                    {
                        data.ExtendGroup(previous);
                        item = data.GetValueByArrayKey(1, string.Format("{0},{1}", fieldNameAndIndex[0], fieldNameAndIndex[1]), previous);
                    }
                    if (item != null)
                    {
                        item.Value = cvsBlock.Substring(fieldValueSeparatorIndex + 1);
                        previous = item;
                    }
                }
                else
                {
                    statusMessages.Add(fieldNameAndIndex[0], cvsBlock.Substring(fieldValueSeparatorIndex + 1));
                }
            }
            return data;
        }
    }
}
