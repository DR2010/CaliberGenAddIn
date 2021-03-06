///////////////////////////////////////////////////////////
//  AuditDetail.cs
//  Implementation of the Class AuditDetail
//  Generated by Enterprise Architect
//  Created on:      15-Jul-2010 15:23:52
//  Original author: Wayne Lombard
///////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace EaAuditConverter.AuditScheduler
{

    public class AuditDetail
    {
        public int audit_detail_id
        {
            get;
            set;
        }

        public string property
        {
            get;
            set;
        }

        public string old_value
        {
            get;
            set;
        }

        public string new_value
        {
            get;
            set;
        }



        public static List<AuditDetail> AddValues(List<BinContent1> binContent1List)
        {
            var auditDetails = new List<AuditDetail>();
            int incrementedauditdetailid = 1;

            foreach (BinContent1 binContent1 in binContent1List)
            {
                
                var locAudit = new AuditDetail
                                   {
                                       audit_detail_id = incrementedauditdetailid++,
                                       property = binContent1.ColumnName,
                                       old_value = binContent1.ColumnOldValue,
                                       new_value = binContent1.ColumnNewValue, 
                                       
                                   };

                auditDetails.Add(locAudit);
            }
            return auditDetails;
        }
    }
}