using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EAAddIn.Applications
{
    [XmlRoot("ReportViewData")]
    public class SearchResults
    {
        public SearchResults()
        {
            Fields = new List<Field>();
            Rows = new List<Row>();
        }
        [XmlElement(Type = typeof(List<Field>))]

        public List<Field> Fields
        {
            get;
            set;
        }
        [XmlElement(Type = typeof(List<Row>))]
        public List<Row> Rows
        {
            get;
            set;
        }


        //[XmlElement(Type = typeof(List<Message>))]
        //public List<Message> Messages
        //{
        //    get;
        //    set;
        //}

    }
    public class Field
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }

        public Field()
        {
        }
    }

    public class Row
    {
        [XmlElement("Field")]
        public List<Field> Fields
        {
            get;
            set;
        }
        public Row()
        {
            Fields = new List<Field>();
        }
    }
}
