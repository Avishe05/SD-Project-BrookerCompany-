using BrookerCompany.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class ClientForm : Form
    {
        BrookerService bService;
        DepartmentService dService;
        TownService tService;
        AddressService aService;
        ProjectService pService;
        ClientService cService;
        public ClientForm()
        {
            InitializeComponent();
            dService = new DepartmentService();
            bService = new BrookerService();
            aService = new AddressService();
            tService = new TownService();
            cService = new ClientService();
            pService = new ProjectService();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> towns = tService.GetTownsNames();
                towns.ForEach(x => cbTown.Items.Add(x));
                cbTown.SelectedIndex = 0;


                List<string> townsProject = tService.GetTownsNames();
                towns.ForEach(x => comboBoxProjectTown.Items.Add(x));
                comboBoxProjectTown.SelectedIndex = 0;
                groupBoxProject.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bSingIn_Click(object sender, EventArgs e)
        {
            try
            {
                string projectName = tbProjectName.Text;

                string address = tbProjectAdress.Text;
                string town = comboBoxProjectTown.Text;
                //string result = pService.AddProject(projectName,releasedDate, budget, address, town);

                //MessageBox.Show(result);
                //int clientId = cService.GetClientIdByPhoneNum(tbPhoneNumber.Text);
                int projectId = pService.GetProjectIdByName(tbProjectName.Text);
                //string resultMatches = cService.MatchClientToProject(clientId, projectId);
                //MessageBox.Show(resultMatches);
                buttonSubmit.Enabled = false;
                buttonAddmage.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
