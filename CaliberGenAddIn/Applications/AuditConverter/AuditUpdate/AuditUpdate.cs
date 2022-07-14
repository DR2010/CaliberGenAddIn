using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EAAddIn.Applications.AuditConverter.AuditUpdate
{
    internal static class AuditUpdate
    {
     
        private static int _totalRecordsInserted;

        public static string UpdateAuditTables(string convertedXmlFileName)
        {
            string auditConnectionString = AddInRepository.Instance.ConnectionStringSql;
            var db = new AuditTables(auditConnectionString);
            XDocument convertedXMLLog;
            Auditheader linqAuditHeader;
            linqAuditHeader = new Auditheader();

            var sw = Stopwatch.StartNew();

            //Load the XML
            convertedXMLLog = XDocument.Load(convertedXmlFileName);

            ////Get the AuditHeader Element - should only be one!
            //var xAuditHeader = (from xml in convertedXMLLog.Elements("AuditHeader") select xml) as XElement;
            var xAuditHeader = new XElement("AuditHeader");
            var xAuditHeaderList = from xml in convertedXMLLog.Elements("AuditHeader") select xml;

            foreach (var xElement in xAuditHeaderList)
            {
                xAuditHeader = xElement;
                break;
            }


            ////Add the AuditHeader to the LINQ AuditHeader Construct
            sw.Start();
            AddAuditHeader(xAuditHeader, linqAuditHeader);
            db.Auditheader.InsertOnSubmit(linqAuditHeader);
            db.SubmitChanges();

            MessageBox.Show(linqAuditHeader.Audit_header_id.ToString());

            //Get all the Audits (and their Audit Details)
            var xAudits = from xml in convertedXMLLog.Elements("AuditHeader").Elements("Audit") select xml;

            foreach (var xAudit in xAudits)
            {
                if (xAudit.Name == "Audit")
                {
                    //Create a new Audit & AuditDetail Linq Construct
                    var linqAudit = new Audit();

                    linqAudit.Audit_header_id = linqAuditHeader.Audit_header_id;
                    //Add the Audit (and children -> AuditDetails) to the Audit Linq construct
                    AddAudit(xAudit, linqAudit);

                    //Add the Audit Linq Construct to the AuditHeader Linq Construct
                    linqAuditHeader.Audit.Add(linqAudit);

                    db.Audit.InsertOnSubmit(linqAudit);
                    //Submit the inserts to the DB!
                    db.SubmitChanges();
                }
            }
            //Add the AuditHeader (and Audits and AuditDetails) Linq construct to the Linq database connection
            //db.Auditheader.InsertOnSubmit(linqAuditHeader);
            //sw.Start();
            //Submit the inserts to the DB!
            //db.SubmitChanges();
            sw.Stop();
            return string.Format("{0} records inserted in {1} hours from file {2} on {3}.", _totalRecordsInserted,
                                 sw.Elapsed, convertedXmlFileName, DateTime.Now);
        }


        private static void AddAuditHeader(XElement xAuditHeader, Auditheader linqAuditHeader)
        {
            linqAuditHeader.OriginalFileName = xAuditHeader.Attribute("OriginalFileName").Value;
            linqAuditHeader.Status = xAuditHeader.Attribute("Status").Value;
            linqAuditHeader.ImportDateTime = DateTime.Parse(xAuditHeader.Attribute("ImportDateTime").Value,
                                                            System.Globalization.CultureInfo.CreateSpecificCulture(
                                                                "en-AU").DateTimeFormat);
            linqAuditHeader.ArchivedUpTo = DateTime.Parse(xAuditHeader.Attribute("ArchivedUpTo").Value,
                                                            System.Globalization.CultureInfo.CreateSpecificCulture(
                                                                "en-AU").DateTimeFormat);
            linqAuditHeader.ImportUser = xAuditHeader.Attribute("ImportUser").Value;

        }

        private static void AddAudit(XElement xAudit, Audit linqAudit)
        {
            linqAudit.Ea_object_type = xAudit.Attribute("EA_Object_Type").Value;
            linqAudit.Change_type = xAudit.Attribute("Change_Type").Value;
            linqAudit.Object_name = xAudit.Attribute("object_Name").Value;
            linqAudit.Object_GUID = xAudit.Attribute("object_GUID").Value;
            linqAudit.Object_type = xAudit.Attribute("object_type").Value;
            linqAudit.Audit_user = xAudit.Attribute("audit_user").Value;
            linqAudit.Audit_datetime = DateTime.Parse(xAudit.Attribute("audit_datetime").Value,
                                                      System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").
                                                          DateTimeFormat);

            AddAuditDetail(xAudit, linqAudit);

            _totalRecordsInserted++;
        }

        private static void AddAuditDetail(XElement xaduit, Audit linqAudit)
        {
            var xmlAuditDetails = from xml in xaduit.Elements("AuditDetail") select xml;

            foreach (var xAuditDetail in xmlAuditDetails)
            {
                var linqAuditDetail = new Auditdetail
                                          {
                                              Property = xAuditDetail.Attribute("property").Value,
                                              Old_value = xAuditDetail.Attribute("old_value").Value,
                                              New_value = xAuditDetail.Attribute("new_value").Value
                                          };

                linqAudit.Auditdetail.Add(linqAuditDetail);

            }
        }
    }
}