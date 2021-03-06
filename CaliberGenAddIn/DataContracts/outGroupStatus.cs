
//------------------------------------------------------------------------------
// <auto-generated>
  //
  //     This code was generated by Code Generator for
  //     Wrapper        : 
  //     Generated User : WL0035
  //     Generated Date : 15:43:10 03 June 2009
  //     Runtime Version: 1.0.0.0
  //
  //     Changes to this file may cause incorrect behavior and will be lost if
  //     the code is regenerated.
  //
  //
//</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.Serialization;
using Employment.Esc.Shared.Contracts.Execution;
using Employment.Esc.Shared.Contracts;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Employment.Esc.StreamServiceReview.Contracts.DataContracts
{
	[DataContract(Namespace = "http://employment.esc.contracts/2009/03")]
	public class outGroupStatus
	{
		
		[FieldAlias("ssrStatusSeqNum")]
		[DataMember]
		public int?  SsrStatusSeqNum
		{
			get;
			set;
		}
        
		[FieldAlias("statusCd")]
		[DataMember]
		public String  StatusCd
		{
			get;
			set;
		}
        
			// field for StatusDate, to handle the Daylight Savings. 
			private DateTime?  m_statusDate;
			
			[DataMember]
			[FieldAlias("statusDate")]
			public DateTime?  StatusDate
			{
				get
				{
					if(m_statusDate != null)
					{
						return DateTime.SpecifyKind(m_statusDate.Value, DateTimeKind.Utc);
					}
					return null;
				}
				set
				{
					if(value != null)
					{
						m_statusDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
				}
			}
			
			// field for CreationDate, to handle the Daylight Savings. 
			private DateTime?  m_creationDate;
			
			[DataMember]
			[FieldAlias("creationDate")]
			public DateTime?  CreationDate
			{
				get
				{
					if(m_creationDate != null)
					{
						return DateTime.SpecifyKind(m_creationDate.Value, DateTimeKind.Utc);
					}
					return null;
				}
				set
				{
					if(value != null)
					{
						m_creationDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
				}
			}
			
			// field for m_CreationTime, to handle the Daylight Savings. 
			private DateTime?  m_creationTime;
			
			[DataMember]
			[FieldAlias("creationTime")]
			public DateTime?  CreationTime
			{
				get
				{
					if(m_creationTime != null)
					{
						return new DateTime(1,1,1, m_creationTime.Value.Hour, m_creationTime.Value.Minute, m_creationTime.Value.Second, m_creationTime.Value.Millisecond, DateTimeKind.Unspecified);
					}
					return null;
			}
			set
			{
				if(value != null)
					m_creationTime  = new DateTime(1,1,1, value.Value.Hour, value.Value.Minute, value.Value.Second, value.Value.Millisecond, DateTimeKind.Unspecified);
			}
		}
		
		[FieldAlias("histcreationUserId")]
		[DataMember]
		public String  HistcreationUserId
		{
			get;
			set;
		}
        
		[FieldAlias("histupdateUserId")]
		[DataMember]
		public String  HistupdateUserId
		{
			get;
			set;
		}
        
			// field for HistupdateDate, to handle the Daylight Savings. 
			private DateTime?  m_histupdateDate;
			
			[DataMember]
			[FieldAlias("histupdateDate")]
			public DateTime?  HistupdateDate
			{
				get
				{
					if(m_histupdateDate != null)
					{
						return DateTime.SpecifyKind(m_histupdateDate.Value, DateTimeKind.Utc);
					}
					return null;
				}
				set
				{
					if(value != null)
					{
						m_histupdateDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
				}
			}
			
		[FieldAlias("ssrHistIntegrityControlNumber")]
		[DataMember]
		public int?  SsrHistIntegrityControlNumber
		{
			get;
			set;
		}
        
	}
}
	