using System.Data.SqlClient;
using EA;

namespace EAAddIn
{
    // -------------------------------------------------------------
    //     Mapping Table for CoolGen CABs
    // -------------------------------------------------------------

    public class mtCABMapping
    {
        public string Author;
        public string CAB;
        public string CABName;
        public string CABPrefix;
        public string CABType;
        public string EA_GUID;
        public string EAElementID;
        public string EAElementName;

        public string EAElementType;
        public string EAStatus;
        public string EAStereotype;


        // ------------------------------------------------------
        // Get EA GUID for a given CAB from the Mapping table
        //  29/01/2009 - Using EA tagged value.
        // ------------------------------------------------------
        public bool getGuidByCABName_EA()
        {
            // 29/01/2009 - Retrieve info from Tagged value
            bool ret = false;

            var eaaccess = new EaAccess();
            EaAccess.sElementTag cabByTag =
                eaaccess.getElementByTaggedValue("Load Module", CAB, new string[] {"Snapshot"});

            if (string.IsNullOrEmpty(cabByTag.elemguid))
            {
                EA_GUID = null;
                ret = false;
            }
            else
            {
                ret = true;

                CABName = cabByTag.Name;
                EA_GUID = cabByTag.elemguid;
                CABType = cabByTag.Stereotype;
                EAStatus = cabByTag.Status;
                Author = cabByTag.Author;

                EAElementType = cabByTag.Object_Type;
                EAStereotype = cabByTag.Stereotype;
                EAElementName = cabByTag.Name;
                EAElementID = cabByTag.Object_ID;
            }
            return ret;
        }
   }
}