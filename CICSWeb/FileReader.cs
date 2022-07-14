using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CICSWeb.Net
{
    public class ConfigItem
    {
        public const string PARAMETER_SITE = "IN W01_IES_TEMPLATE";
        internal long level;
        internal long length;
        internal long presition;
        internal long repeats;
        internal char type;
        internal char domain;
        internal string name;

        public ConfigItem(string line)
        {
            string[] values = line.Split(new[] { ',' });
            level = long.Parse(values[0]);
            length = long.Parse(values[1]);
            presition = long.Parse(values[2]);
            repeats = long.Parse(values[3]);
            type = values[4].ToCharArray()[0];
            domain = values[4].ToCharArray()[1];
            name = values[5].Replace("'", ""); ;
        }
    }

    public class ConfigFileReader
    {
        readonly static char[] DELIMITER = new char[] { ' ' };
        readonly string fileName;
        string wName;

        public ConfigFileReader(string fileName)
        {
            this.fileName = fileName;

        }
        private static WrapperConfigItem AddTo(WrapperConfig a
            , ConfigItem item
            , long m
            , long attnum
            , long comnum
            , IWrapperConfigItem parent)
        {
            var b = new WrapperConfigItem();
            b.name = item.name;
            if (item.type.Equals('P'))
            {

                b.Key = item.name.Replace(" ", "").Replace("_", "").Replace("'", "");
                var pos = item.name.LastIndexOf(' ');
                b.QName = item.name.Substring(0, pos).Trim();
                b.AName = item.name.Substring(pos, item.name.Length - b.QName.Length).Trim();
            }

            b.Length = item.length;
            b.Repeats = item.repeats;
            b.Level = item.level;
            b.Type = item.type;
            b.Domain = item.domain;

            b.Attnum = attnum;
            //b.owngrp = owngrp;
            b.ArrayKey = string.Format("{0},{1}", attnum, 1);

            if (parent == null)
            {
                if (m == 0)
                    a.AddImport(b);
                else
                    a.AddExport(b);
            }
            else
            {
                parent.Templates.Add(b);
            }


            return b;
        }

        public WrapperConfig LoadConfig()
        {
            var data = new WrapperConfig();
            data.Source = ConfigSourceType.Template;
            WrapperConfigItem owngrp = null;
            long m = 0;
            long attnum = 0;
            long comnum = 0;
            var fi = new FileInfo(fileName);
            wName = string.IsNullOrEmpty(fi.Extension) ? fi.Name : fi.Name.Substring(0, fi.Name.LastIndexOf(fi.Extension));
            data.Wname = wName;
            using (TextReader text = new StreamReader(fileName))
            {
                string line = text.ReadLine();
                if (line != null)
                {
                    data.rawText.AppendLine(line);
                    var param = line.Split(DELIMITER);
                    data.Bname = param[0];
                    var integrity = line.Substring(param[0].Length + 1);
                    var array = Encoding.ASCII.GetBytes(integrity);
                    for (int i = 0; i < data.Integrity.Length; i++)
                    {
                        data.Integrity[i] = (byte)' ';
                    }
                    Array.Copy(array, data.Integrity, array.Length < data.Integrity.Length ? array.Length : data.Integrity.Length);
                    data.Time = Helper.StringToDate(param[1], param[2]);
                    line = text.ReadLine();
                    while (line != null)
                    {
                        data.rawText.AppendLine(line);
                        if (line.StartsWith("IMPORT"))
                        {
                            m = 0;
                            attnum = 1;
                            comnum = 1;
                            owngrp = null;
                        }
                        else if (line.StartsWith("EXPORT"))
                        {
                            m = 1;
                            attnum = 1;
                            comnum = 1;
                            owngrp = null;
                            data.Exportstring = long.Parse(line.Split(DELIMITER)[1]);
                        }
                        else if (line.StartsWith("MAPS"))
                        {
                            throw new NotImplementedException();
                        }
                        else if (line.StartsWith("FLOW"))
                            throw new NotImplementedException();
                        else if (line.StartsWith("XFER"))
                            throw new NotImplementedException();
                        else if (line.StartsWith("RTRN"))
                            throw new NotImplementedException();
                        else if (line.Length < 11)
                        { }
                        else if (m < 2)
                        {
                            var item = new ConfigItem(line);
                            if (owngrp != null)
                            {
                                if (item.type.CompareTo('G') == 0)
                                {
                                    owngrp = null;
                                }
                                else
                                {
                                    if (!item.name.StartsWith(owngrp.name)) owngrp = null;
                                }
                            }
                            var temp = AddTo(data, item, m, attnum, comnum, owngrp);
                            attnum++;
                            if (owngrp == null && item.type.CompareTo('G') == 0)
                            {
                                owngrp = temp;
                            }


                        }
                        else if ((m == 2) || (m == 3))
                        {
                            throw new NotImplementedException();
                        }
                        line = text.ReadLine();
                    }

                }

            }
            return data;
        }
    }
}
