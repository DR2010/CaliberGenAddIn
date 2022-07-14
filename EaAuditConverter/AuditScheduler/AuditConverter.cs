///////////////////////////////////////////////////////////
//  AuditControl.cs
//  Implementation of the Class AuditControl
//  Generated by Enterprise Architect
//  Created on:      15-Jul-2010 15:23:46
//  Original author: Wayne Lombard
///////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;

namespace EaAuditConverter.AuditScheduler
{
    public static class AuditConverter
    {
        #region Main Audit Methods

        public static AuditUpdate.AuditTables CreateAuditHeader(ref MessageHandler messageHandler, int maxposition, out AuditUpdate.Auditheader linqAuditHeader, ref bool ok, string auditConnectionString)
        {
            var db = new AuditUpdate.AuditTables(auditConnectionString);

            linqAuditHeader = new AuditUpdate.Auditheader
            {
                PositionFrom = maxposition,
                ImportDateTime = DateTime.Now,
                Status = "in progress",
                ImportUser = WindowsIdentity.GetCurrent().Name.Split('\\')[1]
            };

            db.Auditheader.InsertOnSubmit(linqAuditHeader);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ok = false;
                messageHandler.WriteToConsole("Error inserting audit header: " + ex.Message, MessageType.Error);
            }
            return db;
        }

        public static bool CreateAuditRows(SqlDataReader reader, int auditHeaderId, int auditId, AuditUpdate.AuditTables db, ref MessageHandler messageHandler)
        {
            bool ok = true;
             try
            {
                //Populate BinContents1
                List<BinContent1> bc1List = BinContent1.DecodeBin(ReadZippedBytes(reader, 9));

                //Populate BinContents2
                var bc2 = new BinContent2();
                bc2.Decode(ReadBytes(reader, 10));

                // build up audit record for insertion
                var linqAudit = new AuditUpdate.Audit
                                    {
                                        Audit_header_id = auditHeaderId,
                                        Audit_id = auditId,
                                        Ea_object_type = reader.GetString(3),
                                        //sn.snapshot name
                                        Change_type = reader.GetString(5),
                                        //sn.style
                                        Object_name = bc2.levelName,
                                        //bc2.ln
                                        Object_GUID = bc2.levelGUID,
                                        //bc2.levelguid
                                        Object_type = bc2.levelLevelName,
                                        //bc2.lln
                                        Audit_user = bc2.detailsUser,
                                        //bc2.du
                                        Audit_datetime = DateTime.Parse(bc2.detailsDateTime,
                                                                        System.Globalization.CultureInfo.
                                                                            CreateSpecificCulture(
                                                                                "en-AU").
                                                                            DateTimeFormat)
                                        //bc2.au
                                    };

                //Populate AuditDetails
                List<AuditDetail> auditDetails = AuditDetail.AddValues(bc1List);

                int maxOldValue = 0;
                int maxNewValue = 0;
                foreach (AuditDetail auditDetail in auditDetails)
                {

                    // build up audit details record for insertion
                    var linqAuditDetail = new AuditUpdate.Auditdetail
                                              {
                                                  Audit_header_id = auditHeaderId,
                                                  Audit_id = auditId,
                                                  Audit_detail_id = auditDetail.audit_detail_id,
                                                  Property = auditDetail.property,
                                                  Old_value = auditDetail.old_value,
                                                  New_value = auditDetail.new_value
                                              };

                    linqAudit.Auditdetail.Add(linqAuditDetail);
                }
                messageHandler.WriteToConsole("Object name: " + bc2.levelName, MessageType.Error);

                db.Audit.InsertOnSubmit(linqAudit);

                // submit the database inserts!
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                messageHandler.WriteToConsole("Error inserting audit details: " + ex.Message, MessageType.Error);
                ok = false;
            }

            return ok;
        }

        #endregion

        #region helper methods used by Main Audit Methods

        private static XElement ReadZippedBytes(SqlDataReader reader, int position)
        {
            var data = (byte[])reader[position];

            var sqlMemoryStream = new MemoryStream(data);

            var xml = XmlHelper.UnZipStream(sqlMemoryStream);

            return xml;
        }

        private static XElement ReadBytes(SqlDataReader reader, int position)
        {
            var data = (byte[])reader[position];

            var convertedText = string.Empty;
            var encoder = new UnicodeEncoding();

            convertedText += encoder.GetString(data, 0, data.Length);


            return XElement.Parse(convertedText);
        }
        
        #endregion

    }
}
