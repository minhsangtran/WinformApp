using QuanLyGaraOto.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.GUI
{
    public partial class fQuanLyPhuTung1 : Form
    {
        string ChoosingAccessaryID;
        string ChoosingAccessaryImportID;
        string AccessaryDeleteNumber;
        string AccessaryDeleteID;
        string ChoosingAccessaryName;
        public fQuanLyPhuTung1()
        {
            InitializeComponent();
            dtgvAccessaryList.ColumnCount = 0;
            dtgvAccessaryImportList.ColumnCount = 0;
            dtgvAccessaryReport.ColumnCount = 0;
            dtgvCarCustomer_CustomerList.ColumnCount = 0;
            dtgvCarCustom_CarList.ColumnCount = 0;


            try
            {
                // Update Inventory Report Each Month
                if (DateTime.Today.Day == 1) AccessaryDAO.Instance.UpdateInventoryReportEachMonth();

                // Default on Add New Mode
                ckbAccesaryImportSearch.Checked = false;
                ckbAccessaryAddNew.Checked = true;
                txbAccessaryNumberInStock.Enabled = false;

                // Disable textbox accessary day
                txbAccessaryDay.Enabled = false;
                txbAccessaryMonth.Enabled = false;
                txbAccessaryYear.Enabled = false;

                nudInventoryReportMonth.Text = DateTime.Now.Month.ToString();
                nudInventoryReportYear.Text = DateTime.Now.Year.ToString();

                txbAccessaryDay.Text = DateTime.Now.Day.ToString();
                txbAccessaryMonth.Text = DateTime.Now.Month.ToString();
                txbAccessaryYear.Text = DateTime.Now.Year.ToString();

                AccessaryDAO.Instance.ShowAccessaryImportTable(dtgvAccessaryImportList);
                AccessaryDAO.Instance.ShowAccessaryTable(dtgvAccessaryList);
                AccessaryDAO.Instance.ShowInventoryReportTable(dtgvAccessaryReport);

                CarsDAO.Instance.ViewAllCarList(dtgvCarCustom_CarList);
                CarsDAO.Instance.SearchCarBrandByName(txbCarBrandName, dtgvCarBrandList);
                CustomersDAO.Instance.ViewAllCustomerList(dtgvCarCustomer_CustomerList);

            }
            catch (Exception)
            {

            }
        }


        private void CkbAccessaryAddNew_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbAccessaryAddNew.Checked == true)
                {
                    ckbAccesaryImportSearch.Checked = false;
                    txbAccessaryNumberInStock.Enabled = false;
                    txbAccessaryPrice.Enabled = true;
                    txbAccessaryNumberGet.Enabled = true;

                    // Disable & Clear textbox Accessary Day
                    txbAccessaryDay.Enabled = false;
                    txbAccessaryMonth.Enabled = false;
                    txbAccessaryYear.Enabled = false;
                    txbAccessaryDay.Clear();
                    txbAccessaryMonth.Clear();
                    txbAccessaryYear.Clear();
                    AccessaryDAO.Instance.ShowAccessaryImportTable(dtgvAccessaryImportList);
                }
            }
            catch(Exception)
            {

            }

        }

        private void CkbAccesaryImportSearch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbAccesaryImportSearch.Checked == true)
                {
                    // Uncheck other checkbutton
                    ckbAccessaryAddNew.Checked = false;

                    // Enable txb Accessary Number Get & Disable txb Accessary NumberInStock
                    txbAccessaryNumberInStock.Enabled = false;
                    txbAccessaryPrice.Enabled = false;
                    txbAccessaryNumberGet.Enabled = false;

                    //Enable textbox Accessary Day
                    txbAccessaryDay.Enabled = true;
                    txbAccessaryMonth.Enabled = true;
                    txbAccessaryYear.Enabled = true;

                    txbAccessaryDay.Clear();
                    txbAccessaryMonth.Clear();
                    txbAccessaryYear.Clear();

                }
            }
            catch (Exception)
            {

            }

        }

        private void TxbAccessaryNumberGet_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

            }

        }

        private void TxbAccessaryPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

            }

        }


        private void TxbAccessaryNumberInStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

            }

        }


        private void DtgvAccessaryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txbAccessaryPrice.Text = dtgvAccessaryList.Rows[e.RowIndex].Cells["Đơn giá"].FormattedValue.ToString();
                txbAccessaryNumberInStock.Text = dtgvAccessaryList.Rows[e.RowIndex].Cells["Số lượng"].FormattedValue.ToString();
                txbAccessaryName.Text = dtgvAccessaryList.Rows[e.RowIndex].Cells["Tên phụ tùng"].FormattedValue.ToString();
            }
            catch (Exception)
            {

            }

        }

        private void TxbAccessaryName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AccessaryDAO.Instance.SearchAccessaryByName(txbAccessaryName, dtgvAccessaryList);
                ChoosingAccessaryName = dtgvAccessaryList.Rows[0].Cells["Tên phụ tùng"].FormattedValue.ToString();
                ChoosingAccessaryID = dtgvAccessaryList.Rows[0].Cells["Mã phụ tùng"].FormattedValue.ToString();
                txbAccessaryNumberInStock.Text = dtgvAccessaryList.Rows[0].Cells["Số lượng"].FormattedValue.ToString();
                txbAccessaryPrice.Text = dtgvAccessaryList.Rows[0].Cells["Đơn giá"].FormattedValue.ToString();
                if (ChoosingAccessaryID == "")
                {
                    txbAccessaryNumberInStock.Clear();
                }
                if (ckbAccesaryImportSearch.Checked == true)
                    AccessaryDAO.Instance.SearchAccessaryImportByName(txbAccessaryName, dtgvAccessaryImportList);
            }
            catch (Exception)
            {

            }

        }


        private void TxbAccessaryNumberInStock_TextChanged(object sender, EventArgs e)
        {

        }



        private void BtnAccessaryExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ckbAccessaryAddNew.Checked == true))
                {
                    AccessaryDAO.Instance.UpdateAccessary(txbAccessaryName, txbAccessaryNumberInStock, txbAccessaryPrice, txbAccessaryNumberGet, dtgvAccessaryList);
                    AccessaryDAO.Instance.InsertAccessary(txbAccessaryName, txbAccessaryPrice, txbAccessaryNumberGet, dtgvAccessaryList);

                    AccessaryDAO.Instance.SearchAccessaryIDByName(txbAccessaryName, dtgvAccessaryList);
                    ChoosingAccessaryID = dtgvAccessaryList.Rows[0].Cells["Mã phụ tùng"].FormattedValue.ToString();
                    AccessaryDAO.Instance.InsertAccessaryImportForm(ChoosingAccessaryID, txbAccessaryNumberGet, txbAccessaryPrice, dtgvAccessaryList);
                    AccessaryDAO.Instance.UpdateInventoryReportBySupply(ChoosingAccessaryID, txbAccessaryNumberGet.Text, dtgvAccessaryReport);
                }


                AccessaryDAO.Instance.SearchAccessaryByName(txbAccessaryName, dtgvAccessaryList);
                AccessaryDAO.Instance.ShowAccessaryImportTable(dtgvAccessaryImportList);
                AccessaryDAO.Instance.ShowInventoryReportTable(dtgvAccessaryReport);
                txbAccessaryName.Clear();
            }
            catch (Exception)
            {

            }

        }

        private void TxbAccessaryID_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnAccessaryDelete_Click(object sender, EventArgs e)
        {

            try
            {
                AccessaryDAO.Instance.DeleteAccessary(ChoosingAccessaryID, dtgvAccessaryList);
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa phụ tùng: " + ChoosingAccessaryID, "Thông báo", MessageBoxButtons.OK);
            }

            try
            {

            }
            catch (Exception)
            {
                txbAccessaryName.Clear();
                AccessaryDAO.Instance.SearchAccessaryByName(txbAccessaryName, dtgvAccessaryList);
                AccessaryDAO.Instance.ShowAccessaryImportTable(dtgvAccessaryImportList);
            }

        }

        private void BtnAccessaryImportDelete_Click(object sender, EventArgs e)
        {

            try
            {
                int currentRow = dtgvAccessaryImportList.CurrentCell.RowIndex;
                ChoosingAccessaryImportID = dtgvAccessaryImportList.Rows[currentRow].Cells["Mã phiếu"].FormattedValue.ToString();
                AccessaryDeleteNumber = dtgvAccessaryImportList.Rows[currentRow].Cells["Số lượng"].FormattedValue.ToString();
                AccessaryDeleteID = dtgvAccessaryImportList.Rows[currentRow].Cells["Mã phụ tùng"].FormattedValue.ToString();
                AccessaryDAO.Instance.UpdateInventoryReportBySupply(AccessaryDeleteID, "-" + AccessaryDeleteNumber, dtgvAccessaryReport);
                AccessaryDAO.Instance.DeleteAccessaryImport(ChoosingAccessaryImportID, AccessaryDeleteID, AccessaryDeleteNumber, dtgvAccessaryImportList);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa phiếu nhập phụ tùng: " + ChoosingAccessaryImportID, "Thông báo", MessageBoxButtons.OK);
            }

            try
            {
                AccessaryDAO.Instance.SearchAccessaryByName(txbAccessaryName, dtgvAccessaryList);
                AccessaryDAO.Instance.ShowAccessaryImportTable(dtgvAccessaryImportList);
                AccessaryDAO.Instance.ShowInventoryReportTable(dtgvAccessaryReport);
            }
            catch (Exception)
            {

            }

        }

        private void DtgvAccessaryImportList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ckbAccesaryImportSearch.Checked = false;
                ChoosingAccessaryImportID = dtgvAccessaryImportList.Rows[e.RowIndex].Cells["Mã phiếu"].FormattedValue.ToString();
                AccessaryDeleteNumber = dtgvAccessaryImportList.Rows[e.RowIndex].Cells["Số lượng"].FormattedValue.ToString();
                AccessaryDeleteID = dtgvAccessaryImportList.Rows[e.RowIndex].Cells["Mã phụ tùng"].FormattedValue.ToString();

                txbAccessaryDay.Text = dtgvAccessaryImportList.Rows[e.RowIndex].Cells["Ngày"].FormattedValue.ToString();
                txbAccessaryMonth.Text = dtgvAccessaryImportList.Rows[e.RowIndex].Cells["Tháng"].FormattedValue.ToString();
                txbAccessaryYear.Text = dtgvAccessaryImportList.Rows[e.RowIndex].Cells["Năm"].FormattedValue.ToString();
                txbAccessaryPrice.Text = dtgvAccessaryImportList.Rows[0].Cells["Đơn giá"].FormattedValue.ToString();
                txbAccessaryNumberGet.Text = dtgvAccessaryImportList.Rows[e.RowIndex].Cells["Số lượng"].FormattedValue.ToString();

                AccessaryDAO.Instance.SearchAccessaryByID(AccessaryDeleteID, dtgvAccessaryList);
                txbAccessaryName.Text = dtgvAccessaryList.Rows[0].Cells["Tên phụ tùng"].FormattedValue.ToString();
            }
            catch (Exception)
            {

            }

        }

        private void TxbAccessaryDay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbAccesaryImportSearch.Checked == true) AccessaryDAO.Instance.SearchAccessaryImportByDate(txbAccessaryDay, txbAccessaryMonth, txbAccessaryYear, dtgvAccessaryImportList);
                if (txbAccessaryDay.Text == "") txbAccessaryDay.Text = DateTime.Now.Day.ToString();
            }
            catch (Exception)
            {

            }

        }

        private void TxbAccessaryMonth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbAccesaryImportSearch.Checked == true) AccessaryDAO.Instance.SearchAccessaryImportByDate(txbAccessaryDay, txbAccessaryMonth, txbAccessaryYear, dtgvAccessaryImportList);
                if (txbAccessaryMonth.Text == "") txbAccessaryMonth.Text = DateTime.Now.Month.ToString();
            }
            catch (Exception)
            {

            }

        }

        private void TxbAccessaryYear_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbAccesaryImportSearch.Checked == true) AccessaryDAO.Instance.SearchAccessaryImportByDate(txbAccessaryDay, txbAccessaryMonth, txbAccessaryYear, dtgvAccessaryImportList);
                if (txbAccessaryYear.Text == "") txbAccessaryYear.Text = DateTime.Now.Year.ToString();
            }
            catch (Exception)
            {

            }

        }

        private void DtgvAccessaryReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridView gridView = sender as DataGridView;
                if (null != gridView)
                {
                    foreach (DataGridViewRow r in gridView.Rows)
                    {
                        gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void nudInventoryReportMonth_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                AccessaryDAO.Instance.SearchInventoryReport(nudInventoryReportMonth, nudInventoryReportYear, dtgvAccessaryReport);
            }
            catch (Exception)
            {

            }
            
        }

        private void NudInventoryReportMonth_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void NudInventoryReportYear_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                AccessaryDAO.Instance.SearchInventoryReport(nudInventoryReportMonth, nudInventoryReportYear, dtgvAccessaryReport);
            }
            catch (Exception)
            {

            }
            
        }

        private void TxbCarID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbCarListViewAll.Checked == false)
                    CarsDAO.Instance.SearchCar(txbCarID, txbCarOwnerName, txbCarOwnerPhoneNumber, dtgvCarCustom_CarList);
                else
                    CarsDAO.Instance.ViewAllCarList(dtgvCarCustom_CarList);

                if (ckbCustomerViewAll.Checked == false)
                    CustomersDAO.Instance.SearchCustomer(txbCarID, txbCarOwnerName, txbCarOwnerPhoneNumber, dtgvCarCustom_CarList);
                else
                    CustomersDAO.Instance.ViewAllCustomerList(dtgvCarCustomer_CustomerList);
            }
            catch (Exception)
            {

            }

        }


        private void TxbCarOwnerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbCarListViewAll.Checked == false)
                    CarsDAO.Instance.SearchCar(txbCarID, txbCarOwnerName, txbCarOwnerPhoneNumber, dtgvCarCustom_CarList);
                else
                    CarsDAO.Instance.ViewAllCarList(dtgvCarCustom_CarList);

                if (ckbCustomerViewAll.Checked == false)
                    CustomersDAO.Instance.SearchCustomer(txbCarID, txbCarOwnerName, txbCarOwnerPhoneNumber, dtgvCarCustom_CarList);
                else
                    CustomersDAO.Instance.ViewAllCustomerList(dtgvCarCustomer_CustomerList);
            }
            catch (Exception)
            {

            }

        }

        private void TxbCarOwnerPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbCarListViewAll.Checked == false)
                    CarsDAO.Instance.SearchCar(txbCarID, txbCarOwnerName, txbCarOwnerPhoneNumber, dtgvCarCustom_CarList);
                else
                    CarsDAO.Instance.ViewAllCarList(dtgvCarCustom_CarList);

                if (ckbCustomerViewAll.Checked == false)
                    CustomersDAO.Instance.SearchCustomer(txbCarID, txbCarOwnerName, txbCarOwnerPhoneNumber, dtgvCarCustom_CarList);
                else
                    CustomersDAO.Instance.ViewAllCustomerList(dtgvCarCustomer_CustomerList);
            }
            catch (Exception)
            {

            }

        }

        private void TxbCarBrandName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CarsDAO.Instance.SearchCarBrandByName(txbCarBrandName, dtgvCarBrandList);
            }
            catch(Exception)
            {

            }
        }

        private void BtnCarBrandAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                CarsDAO.Instance.InsertCarBrandByName(txbCarBrandName, dtgvCarBrandList);
            }
            catch (Exception)
            {

            }
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {
            CarsDAO.Instance.SearchCarBrandByName(txbCarBrandName, dtgvCarBrandList);
            CarsDAO.Instance.SearchCar(txbCarID, txbCarOwnerName, txbCarOwnerPhoneNumber, dtgvCarCustom_CarList);
            CustomersDAO.Instance.SearchCustomer(txbCarID, txbCarOwnerName, txbCarOwnerPhoneNumber, dtgvCarCustom_CarList);
        }
    }
}
