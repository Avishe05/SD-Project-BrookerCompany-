using BrookerCompany.Models;
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

namespace BrookerForm
{
    public partial class BrookerForm : Form
    {
        BrookerService bService;
        DepartmentService dService;
        TownService tService;
        AddressService aService;
        private int currentPage = 1;
        private int itemsPerPage = 5;
        private int totalPages = 0;
        private int currentEmployeeIndex = 0;
        public BrookerForm()
        {
            InitializeComponent();
            dService = new DepartmentService();
            bService = new BrookerService();
            aService = new AddressService();
            tService = new TownService();
        }

        private void BrookerForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> departments = dService.GetDepartmentName();
                departments.ForEach(x => comboBoxDepartment.Items.Add(x));
                comboBoxDepartment.SelectedIndex = 0;
                List<string> cities = tService.GetTownsNames();
                cities.ForEach(x => comboBoxCities.Items.Add(x));
                comboBoxCities.SelectedIndex = 0;
                List<string> employees = bService.GetBrookerBasicInfo();
                employees.ForEach(x => listBoxEmployee.Items.Add(x));

                totalPages = bService.GetBrookerPagesCount(itemsPerPage);
                List<string> eS = bService.GetBrookerBasicInfo(currentPage, itemsPerPage);
                eS.ForEach(x => listBoxEmployee.Items.Add(x));


                labelPageNum.Text = $"{currentPage} / {totalPages}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                string result = null;

                string name = textBoxName.Text;
                string lastName = textBoxLastName.Text;
                string address = txtBoxAddress.Text;
                string ciry = comboBoxCities.Text;
                string departments = comboBoxDepartment.Text;
                string phoneNum = textBoxPhoneNum.Text;
                string email = textBoxEmail.Text;

                result = bService.AddBrooker(name, lastName, address, ciry, departments, phoneNum, email);

                MessageBox.Show(result);

                ClearAddGroupBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearAddGroupBox()
        {
            textBoxName.Text = string.Empty;
            textBoxLastName.Text = string.Empty;
            txtBoxAddress.Text = string.Empty;
            textBoxPhoneNum.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
            comboBoxDepartment.SelectedIndex = 0;
            comboBoxCities.SelectedIndex = 0;
        }
        private void rBtnDelete_CheckedChanged(object sender, EventArgs e)
        {
            textBoxName.Enabled = true;
            textBoxLastName.Enabled = true;
            txtBoxAddress.Enabled = true;
            comboBoxCities.Enabled = true;
            comboBoxDepartment.Enabled = true;
            textBoxPhoneNum.Enabled = true;
            textBoxEmail.Enabled = true;
            btnAdd.Enabled = false;
            btnDischarge.Enabled = true;
        }
        private void rBtnAdd_CheckedChanged(object sender, EventArgs e)
        {
            textBoxName.Enabled = true;
            textBoxLastName.Enabled = true;
            txtBoxAddress.Enabled = true;
            comboBoxCities.Enabled = true;
            comboBoxDepartment.Enabled = true;
            textBoxPhoneNum.Enabled = true;
            textBoxEmail.Enabled = true;
            btnAdd.Enabled = true;
            btnDischarge.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnDischarge_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(bService.DeleteBrookerById(currentEmployeeIndex));
                ClearAddGroupBox();
            }
            catch (Exception)
            {
                MessageBox.Show("This brooker is currently working on a project and cannot be discharged...");
            }

        }

        private void listBoxEmployee_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string employeeInfo = listBoxEmployee.Text;
                currentEmployeeIndex = int.Parse(employeeInfo.Split(' ').First());
                Brooker brooker = bService.GetBrookerById(currentEmployeeIndex);
                if (brooker != null)
                {
                    textBoxName.Text = brooker.FirstName;
                    textBoxLastName.Text = brooker.LastName;
                    txtBoxAddress.Text = brooker.Address.Name;
                    comboBoxCities.Text = brooker.Address.Town.Name;
                    comboBoxDepartment.Text = brooker.Department.Name;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxEmployee_DoubleClick_1(object sender, EventArgs e)
        {
            try
            {
                string employeeInfo = listBoxEmployee.Text;
                currentEmployeeIndex = int.Parse(employeeInfo.Split(' ').First());
                Brooker brooker = bService.GetBrookerById(currentEmployeeIndex);
                if (brooker != null)
                {
                    textBoxName.Text = brooker.FirstName;
                    textBoxLastName.Text = brooker.LastName;
                    txtBoxAddress.Text = aService.GetAddresssName(currentEmployeeIndex);
                    comboBoxCities.Text = tService.GetTownName(currentEmployeeIndex);
                    comboBoxDepartment.Text = dService.GetDepartmentName(currentEmployeeIndex);
                    textBoxPhoneNum.Text = brooker.PhoneNumber;
                    textBoxEmail.Text = brooker.Email;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string newNum = textBoxPhoneNum.Text;
                MessageBox.Show(bService.UpdateBrookerPhoneNum(currentEmployeeIndex, newNum));
                ClearAddGroupBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioButtonUpdate_CheckedChanged(object sender, EventArgs e)
        {
            textBoxName.Enabled = false;
            textBoxLastName.Enabled = false;
            txtBoxAddress.Enabled = false;
            comboBoxCities.Enabled = false;
            comboBoxDepartment.Enabled = false;
            textBoxPhoneNum.Enabled = true;
            textBoxEmail.Enabled = false;
            btnAdd.Enabled = false;
            btnDischarge.Enabled = false;
            btnUpdate.Enabled = true;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage <= 1) { return; }
                listBoxEmployee.Items.Clear();
                List<string> list = bService.GetBrookerBasicInfo(--currentPage, itemsPerPage);
                list.ForEach(p => listBoxEmployee.Items.Add(p));
                labelPageNum.Text = $"{currentPage} / {totalPages}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage >= totalPages) { return; }
                listBoxEmployee.Items.Clear();
                List<string> list = bService.GetBrookerBasicInfo(++currentPage, itemsPerPage);
                list.ForEach(p => listBoxEmployee.Items.Add(p));
                labelPageNum.Text = $"{currentPage} / {totalPages}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
