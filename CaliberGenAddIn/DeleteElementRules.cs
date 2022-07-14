using System;
using System.Collections;
using System.Windows.Forms;
using EA;

namespace EAAddIn
{
    public class DeleteElementRules
    {
        public bool allowConnectorRules(Repository repository,
                                        EventProperties Info)
        {
            bool ret = true;
            Element sourceElement = null;
            Element targetElement = null;
            string user = repository.GetCurrentLoginUser(false);
            EventProperty epConnectorID = null;
            EventProperty epsourceElement = null;
            EventProperty eptargetElement = null;

            int isourceElement = 0;
            int itargetElement = 0;

            try
            {
                epConnectorID = Info.Get("ConnectorId");
                int connectorID = Convert.ToInt32(epConnectorID.Value);

                Connector conn = repository.GetConnectorByID(connectorID);

                if (conn.SupplierID <= 0 && conn.ClientID <= 0)
                {
                    // Error will be issued down there...
                    isourceElement = 0;
                    itargetElement = 0;
                }
                else
                {
                    isourceElement = Convert.ToInt32(conn.SupplierID);
                    itargetElement = Convert.ToInt32(conn.ClientID);
                }
            }
            catch (Exception)
            {
                // ignore
                isourceElement = 0;
                itargetElement = 0;
            }

            if (isourceElement == 0 && itargetElement == 0)
            {
                try
                {
                    epsourceElement = Info.Get("SupplierID");
                    eptargetElement = Info.Get("ClientID");

                    isourceElement = Convert.ToInt32(epsourceElement.Value);
                    itargetElement = Convert.ToInt32(eptargetElement.Value);
                }
                catch (Exception)
                {
                    isourceElement = 0;
                    itargetElement = 0;
                }
            }

            // Something is wrong.
            if (isourceElement <= 0 || itargetElement <= 0)
                return true;


            // Resume retrieving information
            sourceElement = repository.GetElementByID(isourceElement);
            targetElement = repository.GetElementByID(itargetElement);


            //
            // Actor hierarchy
            //
            if (sourceElement.Type == "Actor" && targetElement.Type == "Actor")
            {
                if (sourceElement.Status != "Proposed" &&
                    targetElement.Status != "Proposed")
                {
                    ret = AddInRepository.Instance.UserHasBAAdministratorRole;

                    if (!ret)
                    {
                        MessageBox.Show(
                            "Only BARM team members can change Actor relationship");
                    }
                }
            }


            //
            // Tables associations
            //
            if (sourceElement.Stereotype == "table" &&
                targetElement.Stereotype == "table")
            {
                ret = true;

                if (targetElement.Status != "Proposed" &&
                    sourceElement.Status != "Proposed")
                {
                    ret = AddInRepository.Instance.UserHasDBARole;

                    if (!ret)
                    {
                        MessageBox.Show(
                            "Only DBA's can change non-Proposed table relationships");
                    }
                }
            }

            return ret;
        }

        //
        // Element deletion rules
        //

        public bool allowElementDeletion(Repository repository, EventProperties Info)
        {
            bool ret = false;
            string user = repository.GetCurrentLoginUser(false);

            EventProperty epElement = Info.Get("ElementID");
            int iElement = Convert.ToInt32(epElement.Value);
            Element element = repository.GetElementByID(iElement);

            //if (element.Stereotype != "table")
            //{
            //    return true;
            //}

            //if (element.Status == "Proposed")
            //{
            //    return true;
            //}

            //ret = AddInRepository.Instance.UserHasDBARole;

            //if (!ret)
            //{
            //    MessageBox.Show("Only DBA's can delete tables");
            //}

            bool connectionFound = false;
            foreach (EA.Connector eacon in element.Connectors)
            {
                connectionFound = true;
            }

            
            if (connectionFound)
            {
                var answer = MessageBox.Show(
                     "Element has connectors. Do you still want to delete it? ",
                     "Element Deletion",MessageBoxButtons.YesNo);

                if (answer == DialogResult.Yes)
                    ret = true;
                else
                    ret = false;

            }
            else
            {
                ret = true;
            }
            
            return ret;
        }
    }
}