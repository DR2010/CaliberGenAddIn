﻿
    
//------------------------------------------------------------------------------
// <auto-generated>
  //
  //     This code was generated by Code Generator for
  //     Wrapper        : StreamServiceReview
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
    public class SSRSaveResponse : IResponseWithExecutionResult
	{
	  	
		[FieldAlias("reviewSeqNum")]
		[DataMember]
		public int?  ReviewSeqNum
		{
			get;
			set;
		}
        
		[FieldAlias("ssrStatusCd")]
		[DataMember]
		public String  SsrStatusCd
		{
			get;
			set;
		}
        
		[FieldAlias("ssrOutcomeCd")]
		[DataMember]
		public String  SsrOutcomeCd
		{
			get;
			set;
		}
        
		[FieldAlias("sourceOutcomeCd")]
		[DataMember]
		public String  SourceOutcomeCd
		{
			get;
			set;
		}
        
			// field for SsrEndDate, to handle the Daylight Savings. 
			private DateTime?  m_ssrEndDate;
			
			[DataMember]
			[FieldAlias("ssrEndDate")]
			public DateTime?  SsrEndDate
			{
				get
				{
					if(m_ssrEndDate != null)
					{
						return DateTime.SpecifyKind(m_ssrEndDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_ssrEndDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_ssrEndDate  = null;
					}
				}
			}
			
		[FieldAlias("streamType")]
		[DataMember]
		public String  StreamType
		{
			get;
			set;
		}
        
		[FieldAlias("streamDays")]
		[DataMember]
		public int?  StreamDays
		{
			get;
			set;
		}
        
		[FieldAlias("totalServiceDays")]
		[DataMember]
		public int?  TotalServiceDays
		{
			get;
			set;
		}
        
		[FieldAlias("commentTxtId")]
		[DataMember]
		public long?  CommentTxtId
		{
			get;
			set;
		}
        
			// field for SsrcreationDate, to handle the Daylight Savings. 
			private DateTime?  m_ssrcreationDate;
			
			[DataMember]
			[FieldAlias("ssrcreationDate")]
			public DateTime?  SsrcreationDate
			{
				get
				{
					if(m_ssrcreationDate != null)
					{
						return DateTime.SpecifyKind(m_ssrcreationDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_ssrcreationDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_ssrcreationDate  = null;
					}
				}
			}
			
		[FieldAlias("ssrintegrityControlNumber")]
		[DataMember]
		public int?  SsrintegrityControlNumber
		{
			get;
			set;
		}
        
		[FieldAlias("ssrcreationUserId")]
		[DataMember]
		public String  SsrcreationUserId
		{
			get;
			set;
		}
        
		[FieldAlias("ssrupdateUserId")]
		[DataMember]
		public String  SsrupdateUserId
		{
			get;
			set;
		}
        
			// field for SsrupdateDate, to handle the Daylight Savings. 
			private DateTime?  m_ssrupdateDate;
			
			[DataMember]
			[FieldAlias("ssrupdateDate")]
			public DateTime?  SsrupdateDate
			{
				get
				{
					if(m_ssrupdateDate != null)
					{
						return DateTime.SpecifyKind(m_ssrupdateDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_ssrupdateDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_ssrupdateDate  = null;
					}
				}
			}
			
		[FieldAlias("prvCnfrmPlcFl")]
		[DataMember]
		public String  PrvCnfrmPlcFl
		{
			get;
			set;
		}
        
			// field for PrvCnfrmPlcDate, to handle the Daylight Savings. 
			private DateTime?  m_prvCnfrmPlcDate;
			
			[DataMember]
			[FieldAlias("prvCnfrmPlcDate")]
			public DateTime?  PrvCnfrmPlcDate
			{
				get
				{
					if(m_prvCnfrmPlcDate != null)
					{
						return DateTime.SpecifyKind(m_prvCnfrmPlcDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_prvCnfrmPlcDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_prvCnfrmPlcDate  = null;
					}
				}
			}
			
		[FieldAlias("comments")]
		[DataMember]
		public String  Comments
		{
			get;
			set;
		}
        
		[FieldAlias("contractId")]
		[DataMember]
		public String  ContractId
		{
			get;
			set;
		}
        
		[FieldAlias("ctyContractType")]
		[DataMember]
		public String  CtyContractType
		{
			get;
			set;
		}
        
			// field for ContractStartDate, to handle the Daylight Savings. 
			private DateTime?  m_contractStartDate;
			
			[DataMember]
			[FieldAlias("contractStartDate")]
			public DateTime?  ContractStartDate
			{
				get
				{
					if(m_contractStartDate != null)
					{
						return DateTime.SpecifyKind(m_contractStartDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_contractStartDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_contractStartDate  = null;
					}
				}
			}
			
		[FieldAlias("orgOwningOrganisation")]
		[DataMember]
		public String  OrgOwningOrganisation
		{
			get;
			set;
		}
        
		[FieldAlias("sequenceNumber")]
		[DataMember]
		public int?  SequenceNumber
		{
			get;
			set;
		}
        
		[FieldAlias("disSiteCode")]
		[DataMember]
		public String  DisSiteCode
		{
			get;
			set;
		}
        
			// field for ReferralDate, to handle the Daylight Savings. 
			private DateTime?  m_referralDate;
			
			[DataMember]
			[FieldAlias("referralDate")]
			public DateTime?  ReferralDate
			{
				get
				{
					if(m_referralDate != null)
					{
						return DateTime.SpecifyKind(m_referralDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_referralDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_referralDate  = null;
					}
				}
			}
			
		[FieldAlias("referralSequenceNumber")]
		[DataMember]
		public int?  ReferralSequenceNumber
		{
			get;
			set;
		}
        
		[FieldAlias("crpStatusCode")]
		[DataMember]
		public String  CrpStatusCode
		{
			get;
			set;
		}
        
			// field for ActualStartDate, to handle the Daylight Savings. 
			private DateTime?  m_actualStartDate;
			
			[DataMember]
			[FieldAlias("actualStartDate")]
			public DateTime?  ActualStartDate
			{
				get
				{
					if(m_actualStartDate != null)
					{
						return DateTime.SpecifyKind(m_actualStartDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_actualStartDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_actualStartDate  = null;
					}
				}
			}
			
		[FieldAlias("crpSequenceNumber")]
		[DataMember]
		public int?  CrpSequenceNumber
		{
			get;
			set;
		}
        
		[FieldAlias("placementType")]
		[DataMember]
		public String  PlacementType
		{
			get;
			set;
		}
        
		[FieldAlias("placementStatus")]
		[DataMember]
		public String  PlacementStatus
		{
			get;
			set;
		}
        
			// field for PlacementDate, to handle the Daylight Savings. 
			private DateTime?  m_placementDate;
			
			[DataMember]
			[FieldAlias("placementDate")]
			public DateTime?  PlacementDate
			{
				get
				{
					if(m_placementDate != null)
					{
						return DateTime.SpecifyKind(m_placementDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_placementDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_placementDate  = null;
					}
				}
			}
			
			// field for CommencementDate, to handle the Daylight Savings. 
			private DateTime?  m_commencementDate;
			
			[DataMember]
			[FieldAlias("commencementDate")]
			public DateTime?  CommencementDate
			{
				get
				{
					if(m_commencementDate != null)
					{
						return DateTime.SpecifyKind(m_commencementDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_commencementDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_commencementDate  = null;
					}
				}
			}
			
			// field for ActualEndDate, to handle the Daylight Savings. 
			private DateTime?  m_actualEndDate;
			
			[DataMember]
			[FieldAlias("actualEndDate")]
			public DateTime?  ActualEndDate
			{
				get
				{
					if(m_actualEndDate != null)
					{
						return DateTime.SpecifyKind(m_actualEndDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_actualEndDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_actualEndDate  = null;
					}
				}
			}
			
		[FieldAlias("outcomeCode")]
		[DataMember]
		public String  OutcomeCode
		{
			get;
			set;
		}
        
		[FieldAlias("commencementUserid")]
		[DataMember]
		public String  CommencementUserid
		{
			get;
			set;
		}
        
			// field for CommencementInputDate, to handle the Daylight Savings. 
			private DateTime?  m_commencementInputDate;
			
			[DataMember]
			[FieldAlias("commencementInputDate")]
			public DateTime?  CommencementInputDate
			{
				get
				{
					if(m_commencementInputDate != null)
					{
						return DateTime.SpecifyKind(m_commencementInputDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_commencementInputDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_commencementInputDate  = null;
					}
				}
			}
			
			// field for ExpectedEndDate, to handle the Daylight Savings. 
			private DateTime?  m_expectedEndDate;
			
			[DataMember]
			[FieldAlias("expectedEndDate")]
			public DateTime?  ExpectedEndDate
			{
				get
				{
					if(m_expectedEndDate != null)
					{
						return DateTime.SpecifyKind(m_expectedEndDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_expectedEndDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_expectedEndDate  = null;
					}
				}
			}
			
			// field for OutcomeEnteredDate, to handle the Daylight Savings. 
			private DateTime?  m_outcomeEnteredDate;
			
			[DataMember]
			[FieldAlias("outcomeEnteredDate")]
			public DateTime?  OutcomeEnteredDate
			{
				get
				{
					if(m_outcomeEnteredDate != null)
					{
						return DateTime.SpecifyKind(m_outcomeEnteredDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_outcomeEnteredDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_outcomeEnteredDate  = null;
					}
				}
			}
			
		[FieldAlias("outcomeEnteredUserid")]
		[DataMember]
		public String  OutcomeEnteredUserid
		{
			get;
			set;
		}
        
		[FieldAlias("managedBy")]
		[DataMember]
		public String  ManagedBy
		{
			get;
			set;
		}
        
			// field for NextIsContactDate, to handle the Daylight Savings. 
			private DateTime?  m_nextIsContactDate;
			
			[DataMember]
			[FieldAlias("nextIsContactDate")]
			public DateTime?  NextIsContactDate
			{
				get
				{
					if(m_nextIsContactDate != null)
					{
						return DateTime.SpecifyKind(m_nextIsContactDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_nextIsContactDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_nextIsContactDate  = null;
					}
				}
			}
			
		[FieldAlias("jdhLocationDisadvFlag")]
		[DataMember]
		public String  JdhLocationDisadvFlag
		{
			get;
			set;
		}
        
		[FieldAlias("jdhHighlyDisadvFlag")]
		[DataMember]
		public String  JdhHighlyDisadvFlag
		{
			get;
			set;
		}
        
		[FieldAlias("jsaResidentialPostcode")]
		[DataMember]
		public String  JsaResidentialPostcode
		{
			get;
			set;
		}
        
			// field for UeStartDate, to handle the Daylight Savings. 
			private DateTime?  m_ueStartDate;
			
			[DataMember]
			[FieldAlias("ueStartDate")]
			public DateTime?  UeStartDate
			{
				get
				{
					if(m_ueStartDate != null)
					{
						return DateTime.SpecifyKind(m_ueStartDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_ueStartDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_ueStartDate  = null;
					}
				}
			}
			
		[FieldAlias("extentionDuration")]
		[DataMember]
		public int?  ExtentionDuration
		{
			get;
			set;
		}
        
		[FieldAlias("nextIsContactType")]
		[DataMember]
		public String  NextIsContactType
		{
			get;
			set;
		}
        
		[FieldAlias("transferFromCode")]
		[DataMember]
		public String  TransferFromCode
		{
			get;
			set;
		}
        
		[FieldAlias("transferTextId")]
		[DataMember]
		public long?  TransferTextId
		{
			get;
			set;
		}
        
		[FieldAlias("transferReasonCode")]
		[DataMember]
		public String  TransferReasonCode
		{
			get;
			set;
		}
        
		[FieldAlias("parentOutcomeCode")]
		[DataMember]
		public String  ParentOutcomeCode
		{
			get;
			set;
		}
        
		[FieldAlias("reReferralFlag")]
		[DataMember]
		public String  ReReferralFlag
		{
			get;
			set;
		}
        
		[FieldAlias("transitionContinuum")]
		[DataMember]
		public String  TransitionContinuum
		{
			get;
			set;
		}
        
			// field for OutcomeEffectiveDate, to handle the Daylight Savings. 
			private DateTime?  m_outcomeEffectiveDate;
			
			[DataMember]
			[FieldAlias("outcomeEffectiveDate")]
			public DateTime?  OutcomeEffectiveDate
			{
				get
				{
					if(m_outcomeEffectiveDate != null)
					{
						return DateTime.SpecifyKind(m_outcomeEffectiveDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_outcomeEffectiveDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_outcomeEffectiveDate  = null;
					}
				}
			}
			
			// field for TransferFromExpectedEndDate, to handle the Daylight Savings. 
			private DateTime?  m_transferFromExpectedEndDate;
			
			[DataMember]
			[FieldAlias("transferFromExpectedEndDate")]
			public DateTime?  TransferFromExpectedEndDate
			{
				get
				{
					if(m_transferFromExpectedEndDate != null)
					{
						return DateTime.SpecifyKind(m_transferFromExpectedEndDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_transferFromExpectedEndDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_transferFromExpectedEndDate  = null;
					}
				}
			}
			
			// field for IntensiveSupportStartDate, to handle the Daylight Savings. 
			private DateTime?  m_intensiveSupportStartDate;
			
			[DataMember]
			[FieldAlias("intensiveSupportStartDate")]
			public DateTime?  IntensiveSupportStartDate
			{
				get
				{
					if(m_intensiveSupportStartDate != null)
					{
						return DateTime.SpecifyKind(m_intensiveSupportStartDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_intensiveSupportStartDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_intensiveSupportStartDate  = null;
					}
				}
			}
			
			// field for SpecialUeDate, to handle the Daylight Savings. 
			private DateTime?  m_specialUeDate;
			
			[DataMember]
			[FieldAlias("specialUeDate")]
			public DateTime?  SpecialUeDate
			{
				get
				{
					if(m_specialUeDate != null)
					{
						return DateTime.SpecifyKind(m_specialUeDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_specialUeDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_specialUeDate  = null;
					}
				}
			}
			
			// field for OriginalExpectedEndDate, to handle the Daylight Savings. 
			private DateTime?  m_originalExpectedEndDate;
			
			[DataMember]
			[FieldAlias("originalExpectedEndDate")]
			public DateTime?  OriginalExpectedEndDate
			{
				get
				{
					if(m_originalExpectedEndDate != null)
					{
						return DateTime.SpecifyKind(m_originalExpectedEndDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_originalExpectedEndDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_originalExpectedEndDate  = null;
					}
				}
			}
			
		[FieldAlias("cvfContractId")]
		[DataMember]
		public String  CvfContractId
		{
			get;
			set;
		}
        
		[FieldAlias("cvfSeqNumber")]
		[DataMember]
		public int?  CvfSeqNumber
		{
			get;
			set;
		}
        
		[FieldAlias("recordStatus")]
		[DataMember]
		public String  RecordStatus
		{
			get;
			set;
		}
        
		[FieldAlias("primaryDisabilityFlag")]
		[DataMember]
		public String  PrimaryDisabilityFlag
		{
			get;
			set;
		}
        
		[FieldAlias("vltuReviewMessageCode")]
		[DataMember]
		public String  VltuReviewMessageCode
		{
			get;
			set;
		}
        
		[FieldAlias("remoteServicingMessageCode")]
		[DataMember]
		public String  RemoteServicingMessageCode
		{
			get;
			set;
		}
        
			// field for m_CommenceTime, to handle the Daylight Savings. 
			private DateTime?  m_commenceTime;
			
			[DataMember]
			[FieldAlias("commenceTime")]
			public DateTime?  CommenceTime
			{
				get
				{
					if(m_commenceTime != null)
					{
						return new DateTime(1,1,1, m_commenceTime.Value.Hour, m_commenceTime.Value.Minute, m_commenceTime.Value.Second, m_commenceTime.Value.Millisecond, DateTimeKind.Unspecified);
					}
					
					return null;
			}
			set
			{
				if(value != null)
				{
					m_commenceTime  = new DateTime(1,1,1, value.Value.Hour, value.Value.Minute, value.Value.Second, value.Value.Millisecond, DateTimeKind.Unspecified);
				}
				else
				{
					m_commenceTime  = null;
				}
			}
		}
		
		[FieldAlias("clientNote")]
		[DataMember]
		public String  ClientNote
		{
			get;
			set;
		}
        
			// field for WrkExpStartDt, to handle the Daylight Savings. 
			private DateTime?  m_wrkExpStartDt;
			
			[DataMember]
			[FieldAlias("wrkExpStartDt")]
			public DateTime?  WrkExpStartDt
			{
				get
				{
					if(m_wrkExpStartDt != null)
					{
						return DateTime.SpecifyKind(m_wrkExpStartDt.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_wrkExpStartDt  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_wrkExpStartDt  = null;
					}
				}
			}
			
			// field for WrkExpEligDate, to handle the Daylight Savings. 
			private DateTime?  m_wrkExpEligDate;
			
			[DataMember]
			[FieldAlias("wrkExpEligDate")]
			public DateTime?  WrkExpEligDate
			{
				get
				{
					if(m_wrkExpEligDate != null)
					{
						return DateTime.SpecifyKind(m_wrkExpEligDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_wrkExpEligDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_wrkExpEligDate  = null;
					}
				}
			}
			
		[FieldAlias("crpsqlTimestamp")]
		[DataMember]
		public DateTime?  CrpsqlTimestamp
		{
			get;
			set;
		}
        
		[FieldAlias("crpCreationUserId")]
		[DataMember]
		public String  CrpCreationUserId
		{
			get;
			set;
		}
        
			// field for CrpCreationDate, to handle the Daylight Savings. 
			private DateTime?  m_crpCreationDate;
			
			[DataMember]
			[FieldAlias("crpCreationDate")]
			public DateTime?  CrpCreationDate
			{
				get
				{
					if(m_crpCreationDate != null)
					{
						return DateTime.SpecifyKind(m_crpCreationDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_crpCreationDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_crpCreationDate  = null;
					}
				}
			}
			
			// field for m_CrpcreationTime, to handle the Daylight Savings. 
			private DateTime?  m_crpcreationTime;
			
			[DataMember]
			[FieldAlias("crpcreationTime")]
			public DateTime?  CrpcreationTime
			{
				get
				{
					if(m_crpcreationTime != null)
					{
						return new DateTime(1,1,1, m_crpcreationTime.Value.Hour, m_crpcreationTime.Value.Minute, m_crpcreationTime.Value.Second, m_crpcreationTime.Value.Millisecond, DateTimeKind.Unspecified);
					}
					
					return null;
			}
			set
			{
				if(value != null)
				{
					m_crpcreationTime  = new DateTime(1,1,1, value.Value.Hour, value.Value.Minute, value.Value.Second, value.Value.Millisecond, DateTimeKind.Unspecified);
				}
				else
				{
					m_crpcreationTime  = null;
				}
			}
		}
		
		[FieldAlias("crpUpdateUserId")]
		[DataMember]
		public String  CrpUpdateUserId
		{
			get;
			set;
		}
        
			// field for CrpUpdateDate, to handle the Daylight Savings. 
			private DateTime?  m_crpUpdateDate;
			
			[DataMember]
			[FieldAlias("crpUpdateDate")]
			public DateTime?  CrpUpdateDate
			{
				get
				{
					if(m_crpUpdateDate != null)
					{
						return DateTime.SpecifyKind(m_crpUpdateDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_crpUpdateDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_crpUpdateDate  = null;
					}
				}
			}
			
			// field for m_CrpUpdateTime, to handle the Daylight Savings. 
			private DateTime?  m_crpUpdateTime;
			
			[DataMember]
			[FieldAlias("crpUpdateTime")]
			public DateTime?  CrpUpdateTime
			{
				get
				{
					if(m_crpUpdateTime != null)
					{
						return new DateTime(1,1,1, m_crpUpdateTime.Value.Hour, m_crpUpdateTime.Value.Minute, m_crpUpdateTime.Value.Second, m_crpUpdateTime.Value.Millisecond, DateTimeKind.Unspecified);
					}
					
					return null;
			}
			set
			{
				if(value != null)
				{
					m_crpUpdateTime  = new DateTime(1,1,1, value.Value.Hour, value.Value.Minute, value.Value.Second, value.Value.Millisecond, DateTimeKind.Unspecified);
				}
				else
				{
					m_crpUpdateTime  = null;
				}
			}
		}
		
		[FieldAlias("crpIntegrityControlNumber")]
		[DataMember]
		public int?  CrpIntegrityControlNumber
		{
			get;
			set;
		}
        
		[FieldAlias("jobseekerId")]
		[DataMember]
		public long?  JobseekerId
		{
			get;
			set;
		}
        
		[FieldAlias("crnNumber")]
		[DataMember]
		public String  CrnNumber
		{
			get;
			set;
		}
        
		[FieldAlias("title")]
		[DataMember]
		public String  Title
		{
			get;
			set;
		}
        
		[FieldAlias("firstGivenName")]
		[DataMember]
		public String  FirstGivenName
		{
			get;
			set;
		}
        
			// field for BirthDate, to handle the Daylight Savings. 
			private DateTime?  m_birthDate;
			
			[DataMember]
			[FieldAlias("birthDate")]
			public DateTime?  BirthDate
			{
				get
				{
					if(m_birthDate != null)
					{
						return DateTime.SpecifyKind(m_birthDate.Value, DateTimeKind.Utc);
					}
					
					return null;
				}
				set
				{
					if(value != null)
					{
						m_birthDate  = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
					}
					else
					{
						m_birthDate  = null;
					}
				}
			}
			
		[FieldAlias("surname")]
		[DataMember]
		public String  Surname
		{
			get;
			set;
		}
        
		[FieldAlias("jskIsPlaced")]
		[DataMember]
		public String  JskIsPlaced
		{
			get;
			set;
		}
        
		[FieldAlias("jskIsVulnerableYouth")]
		[DataMember]
		public String  JskIsVulnerableYouth
		{
			get;
			set;
		}
        
		[FieldAlias("outMoreStatusData")]
		[DataMember]
		public String  OutMoreStatusData
		{
			get;
			set;
		}
        
		[FieldAlias("outMoreJsciData")]
		[DataMember]
		public String  OutMoreJsciData
		{
			get;
			set;
		}
        
		[FieldAlias("outMoreJcaData")]
		[DataMember]
		public String  OutMoreJcaData
		{
			get;
			set;
		}
        
		[FieldAlias("outMoreSsrData")]
		[DataMember]
		public String  OutMoreSsrData
		{
			get;
			set;
		}
        
		[FieldAlias("srvClockType")]
		[DataMember]
		public String  SrvClockType
		{
			get;
			set;
		}
        
		[FieldAlias("daysInStream")]
		[DataMember]
		public int?  DaysInStream
		{
			get;
			set;
		}
        
		[FieldAlias("weeksInStream")]
		[DataMember]
		public int?  WeeksInStream
		{
			get;
			set;
		}
        
		[FieldAlias("monthsInStream")]
		[DataMember]
		public int?  MonthsInStream
		{
			get;
			set;
		}
        
		[FieldAlias("outGroupStatus")]
		[DataMember]
		public outGroupStatus[]  OutGroupStatus
		{
			get;
			set;
		}  
	  
		[FieldAlias("outGroupJsci")]
		[DataMember]
		public outGroupJsci[]  OutGroupJsci
		{
			get;
			set;
		}  
	  
		[FieldAlias("outGroupJca")]
		[DataMember]
		public outGroupJca[]  OutGroupJca
		{
			get;
			set;
		}  
	  
		[FieldAlias("outGroupSsr")]
		[DataMember]
		public outGroupSsr[]  OutGroupSsr
		{
			get;
			set;
		}  
	  
	  
		[DataMember]
		public ExecutionResult ExecutionResult
		{
			get;
			set;
		}
    }
}
