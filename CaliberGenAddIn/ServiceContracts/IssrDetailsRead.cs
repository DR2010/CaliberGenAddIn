﻿
    
	
	
    
//------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by Code Generator for 
//     Wrapper        : ZEIS0D45
//     Generated User : WL0035
//     Generated Date : 15:43:22 03 June 2009
//     Runtime Version: 1.0.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//
// </auto-generated>
//------------------------------------------------------------------------------
using System.ServiceModel;
using Employment.Esc.Shared.Contracts.Faults;
using Employment.Esc.Shared.Security;
using Employment.Esc.StreamServiceReview.Contracts.DataContracts;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;

namespace Employment.Esc.StreamServiceReview.Contracts.ServiceContracts
{

		
	public partial interface IStreamServiceReview
	{
		
		[OperationContract]
		[FaultContract(typeof(ValidationFault))]
		[FaultContract(typeof(AuthorisationFault))]
		// add roles here
		[TransactionAcl("DAD","DHD","DES","DEU","DEL","DDT","DFP","DVO","SPS","SPC","SPN")]
		ssrDetailsReadResponse ssrDetailsReadEXECUTE(ssrDetailsReadRequest Request);
	
	}
}
    
  