using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications;
using EAStructures;

namespace EAAddIn.Windows
{
    public partial class GetNewGenServiceNumber : Form
    {
        public const string EAREP1ID = "initial catalog=EA_Release1;data source=DRPSQL007\\SQL07";
        private readonly SqlConnection MappingToolConnection;
        private readonly SqlConnection MigrateToolConnection;
        public EaCaliberGenEngine EAGen;
        public SecurityInfo secinfo;

        public GetNewGenServiceNumber()
        {
            InitializeComponent();
            //CurrentRepository = rc;

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            btnGenNumber.Enabled = false;

            // Get selected packages
            Package p = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            secinfo.packageDestination = p.PackageGUID;

            MigrateToolConnection = SqlHelpers.MigrationToolDbConnection;
            MappingToolConnection = SqlHelpers.MappingDbConnection;

            //new SqlConnection(secinfo.MigrateToolDB);

            //try
            //{
            //    MigrateToolConnection.Open();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error connecting with Migrate tool. " + ex.ToString());
            //    return;
            //}
            // Mapping
             //    new SqlConnection(secinfo.EAGenCaliberSQL2005Repository);
            //MappingToolConnection.Open();


            txtLoggedUser.Text = AddInRepository.Instance.Repository.GetCurrentLoginUser(false);

            if (txtLoggedUser.Text.Length > 12)
                txtLoggedUser.Text = txtLoggedUser.Text.Substring(7, 6);

          
        }

        // ---------------------------------------------------------
        //                 Close click
        // ---------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
         
            Close();
        }


        // ---------------------------------------------------------
        //                 Activate event
        // ---------------------------------------------------------
        private void GetNewGenServiceNumber_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'eACaliberCoolgenDataSet1.Release' table. You can move, or remove it, as needed.
            this.releaseTableAdapter.Fill(this.eACaliberCoolgenDataSet1.Release);
            EditReleasesButton.Enabled = AddInRepository.Instance.UserHasSolutionArchitectRole;
            // Check if element is valid to link
            string elValid = checkIfElementIsValid();


            // Start output tab
            AddInRepository.Instance.InitialiseGenResults();

            txtLoadModuleName.Text = "";
            txtFullName.Text = "";
            txtPrefix.Text = "";
            txtType.Text = "";
            txtCABDescription.Text = "";

            if (elValid != "Ok")
            {
                btnGenNumber.Enabled = false;

                if (elValid != "Ok")
                {
                    AddInRepository.Instance.WriteGenResults(elValid, 2001);
                }

                return;
            }


            // Get selected item in EA
            //
            Element element;

            try
            {
                element = (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            catch (Exception)
            {
                return;
            }

            txtName.Text = element.Name;

            var cabTag = (TaggedValue) element.TaggedValues.GetByName("Load Module");
            if (cabTag != null && cabTag.Value != null && cabTag.Value != "")
            {
                btnGenNumber.Enabled = false;
                txtLoadModuleName.Text = cabTag.Value;
                txtFullName.Text = element.Name;

                AddInRepository.Instance.WriteGenResults( "Element is in EA already." +
                                                                            txtLoadModuleName.Text, 2001);
            }
            else
            {
                btnGenNumber.Enabled = true;
                if (element.Name.Length > 26)
                {
                    txtName.Text = element.Name.Substring(0, 27);
                }
            }

            string prefix =
                mtCABType.getGenPrefix(MappingToolConnection, element.Stereotype);

            txtPrefix.Text = prefix;
            txtType.Text = element.Stereotype;
            txtCABDescription.Text = element.Notes;
        }


        // -------------------------------------------------
        //   Get a new number push-button click
        // -------------------------------------------------
        private void btnGenNumber_Click(object sender, EventArgs e)
        {
            if (cbxRelease.Text == "")
            {
                MessageBox.Show("Please select the release.");
                return;
            }

            mtCABNumber genservice;
            string elValid = checkIfElementIsValid();

            if (elValid == "Ok")
            {
                string resp =
                    MessageBox.Show(
                        "You are about to get a new number \n" +
                        "for a " + txtPrefix.Text + "\n" +
                        "Press OK to proceed.", "Service Number",
                        MessageBoxButtons.OKCancel).ToString();

                Element element;

                // Get selected item in EA
                //
                try
                {
                    element = (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
                }
                catch (Exception)
                {
                    MessageBox.Show("Element select in EA is not valid to link.");
                    return;
                }


                if (resp == "OK")
                {
                    // If the class has been identified as a valid coolgen service
                    // set up the new cab 
                    genservice = new mtCABNumber();
                    // Truncate to make it 32 in total.
                    genservice.type = txtPrefix.Text;
                    genservice.ActionBlockName = txtName.Text;
                    genservice.ComponentContact = txtLoggedUser.Text;
                    genservice.Developer = txtLoggedUser.Text;
                    genservice.Release = cbxRelease.Text;

                    string ret = genservice.getNewLoadModuleName(MigrateToolConnection);

                    if (ret == "Error")
                    {
                        return;
                    }

                    txtLoadModuleName.Text = genservice.loadName;
                    txtFullName.Text = genservice.ActionBlockName;

                    // Get selected packages
                    Package currentPackage =
                        AddInRepository.Instance.Repository.GetTreeSelectedPackage();

                    // update ea with name and load module name as a tag
                    element.Author = txtLoggedUser.Text;
                    element.Name = genservice.ActionBlockName.Trim();
                    element.Update();
                    currentPackage.Elements.Refresh();

                    // Create output tab
                    //
                    AddInRepository.Instance.InitialiseGenResults();
                    AddInRepository.Instance.WriteGenResults(
                                                                    "New CAB Number Generated: " + genservice.loadName +
                                                                    " - " +
                                                                    genservice.ActionBlockName
                                                                    , 1000);

                    // Add tagged value
                    //
                    EaAccess.AddTaggedValue(element, "Load Module", genservice.loadName);

                    MessageBox.Show("New service number generated");

                    currentPackage.Elements.Refresh();
                    currentPackage.Diagrams.Refresh();
                    currentPackage.Packages.Refresh();
                    currentPackage.Connectors.Refresh();

                    AddInRepository.Instance.Repository.RefreshOpenDiagrams(true);

                    btnGenNumber.Enabled = false;
                }
            }
        }


        // --------------------------------------
        //   Check if element is valid to link
        // --------------------------------------
        public string checkIfElementIsValid()
        {
            Element element;
            string ret = "Ok";

            // Get selected item in EA
            //
            try
            {
                element = (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            catch (Exception)
            {
                ret = "Element select in EA is not valid to link.";
                return ret;
            }

            // Check if element can be linked
            if (element.Type != "Class")
            {
                ret = "Element selected is valid. Not a class";
                return ret;
            }

            // Check if selected item in EA is a class with a valid stereotype

            if (!element.Stereotype.StartsWith("GEN"))
            {
                ret = "Element stereotype is not GEN%. \n" +
                      "Fix element stereotype and retry the link.";
                return ret;
            }


            if (element.Stereotype == "GEN Service" ||
                element.Stereotype == "GEN Public" ||
                element.Stereotype == "GEN Private" ||
                element.Stereotype == "GEN Wrapper" ||
                element.Stereotype == "GEN CAB Component EAR" ||
                element.Stereotype == "GEN EAB Component EAR" ||
                element.Stereotype == "GEN Determinator CAB" ||
                element.Stereotype == "GEN Determinator EAB" ||
                element.Stereotype == "GEN External" ||
                element.Stereotype == "GEN CAB")
            {
                // ok
            }
            else
            {
                ret = "Invalid GEN stereotype to get a number from EA.";
            }

            return ret;
        }

        private void EditReleasesButton_Click(object sender, EventArgs e)
        {
            var maintenanceForm = new ReleaseMaintenance();

            maintenanceForm.Show();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

    }
}
