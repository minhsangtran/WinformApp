using QuanLyGaraOto.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto
{
    public partial class fTableManager : Form
    {
        public string ChoosingWageID;
        public int MaxNumAcceptFormCanGetPerDay = 30;
        public string customerID;
        public string lastCustomerID = DataProviderDAO.Instance.GetLastIdOfTable("TableKhachHang", "MaKhachHang");
        public bool addNewCustomer = true;
        public bool addNewCar = true;
        public string acceptFormID = "0";
        public string repairTabAcceptFormIDSelected;
        public string accessaryAmountMaxRepairFormTab = "0";
        public string ChoosingAccessaryID;
        public string ChoosingAccessaryImportID;
        public string AccessaryDeleteNumber;
        public string AccessaryDeleteID;
        public string detailRepairFormIDSelected;
        public string ChoosingAccessaryName;
        public string billTabIDRepairForm;
        public string billTabIDBill;

        public fTableManager()
        {
            
            InitializeComponent();

            txbDateCreateAcceptForm.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            lbNumAcceptFormGot.Text = "Đã tiếp nhận: " + AcceptFormDAO.Instance.GetNumAcceptFormByDate();
            WageDAO.Instance.LoadAllWage(dtgvAllWageInfo);

            dtpkSearchAcceptFormByDate.MaxDate = DateTime.Now.Date;
            dtpkRepairTabDateRepair.MaxDate = DateTime.Now.Date;
            dtpkRepairTabAcceptDate.MaxDate = DateTime.Now.Date;

            dtpkSearchAcceptFormByDate.Value = DateTime.Now.Date;
            dtpkRepairTabDateRepair.Value = DateTime.Now.Date;
            dtpkRepairTabAcceptDate.Value = DateTime.Now.Date;

            txbSearchAcceptFormDate.Text = dtpkSearchAcceptFormByDate.Value.Date.ToString("dd/MM/yyyy");
            txbRepairTabDateRepair.Text = dtpkRepairTabDateRepair.Value.Date.ToString("dd/MM/yyyy");
            txbRepairTabAcceptDate.Text = dtpkRepairTabAcceptDate.Value.Date.ToString("dd/MM/yyyy");

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


        private void ĐăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fCreateAccount fCreateAccount = new fCreateAccount();
            this.Hide();
            fCreateAccount.ShowDialog();
            this.Show();
        }

        private void ĐăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


   

        private void BtnGetNewTransaction_Click(object sender, EventArgs e)
        {
            int NumAcceptFormGot = AcceptFormDAO.Instance.GetNumAcceptFormByDate();
            if (txbCustomerName.Text =="" || txbCustomerBirthday.Text == "" | txbCustomerAddress.Text == "" || txbCustomerPhone.Text == "" || txbCustomerEmail.Text == "" || txbCarNumber.Text == "" || cbxCarType.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (addNewCustomer)
            {
                try
                {
                    CustomersDAO.Instance.AddNewCustomer(txbCustomerName, txbCustomerBirthday, txbCustomerAddress, txbCustomerPhone, txbCustomerEmail);
                    MessageBox.Show("Đã lưu khách hàng mới", "Thông báo", MessageBoxButtons.OK);
                    TxbCustomerName_TextChanged(sender, e);
                }
                catch (Exception)
                {
                    MessageBox.Show("Tên khách hàng đã tồn tại", "Thông báo", MessageBoxButtons.OK);
                }

            }
            if (addNewCar)
            {
                try
                {
                    CarsDAO.Instance.AddNewCar(txbCarNumber.Text, customerID, cbxCarType.Text);
                    MessageBox.Show("Đã lưu xe mới", "Thông báo", MessageBoxButtons.OK);
                    TxbCarNumber_TextChanged(sender, e);
                }
                catch (Exception)
                {
                    MessageBox.Show("Xe này đã tồn tại", "Thông báo", MessageBoxButtons.OK);
                }

            }
            if(NumAcceptFormGot >= MaxNumAcceptFormCanGetPerDay)
            {
                MessageBox.Show("Đã đủ số lượng cho ngày hôm nay", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            try
            {
                AcceptFormDAO.Instance.InsertNewAcceptForm(customerID, txbCarNumber);
                MessageBox.Show("Đã lập phiếu tiếp nhận", "Thông báo", MessageBoxButtons.OK);
                DtpkRepairTabAcceptDate_ValueChanged(sender, e);
            }
            catch (Exception)
            {
                
            }

            lbNumAcceptFormGot.Text = "Đã tiếp nhận: " + NumAcceptFormGot;
        }

        private void TxbCarNumber_TextChanged(object sender, EventArgs e)
        {
            string BienSoXe = txbCarNumber.Text;

            CarsDAO.Instance.GetCarsInfo(BienSoXe, dtgvCarInfor, cbxCarType);
            CarsDAO.Instance.GetCustomerInforByCarInfor(txbCarNumber.Text, dtgvCustomerInfor);

        }

        private void CbxCarType_DropDown(object sender, EventArgs e)
        {
            cbxCarType.DataSource = CarsDAO.Instance.GetCarTypes();
        }

        private void TxbCustomerName_TextChanged(object sender, EventArgs e)
        {
            if (ckbAddNewCustomer.Checked)
            {
                customerID = (int.Parse(lastCustomerID) + 1).ToString();
            }
            
            CustomersDAO.Instance.GetCustomersInfo(txbCustomerName, dtgvCustomerInfor);
        }

        private void DtgvCarInfor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= dtgvCarInfor.RowCount - 1)
            {
                return;
            }
            if(dtgvCarInfor.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                ckbAddNewCar.Checked = false;
                txbCarNumber.Text = dtgvCarInfor.Rows[e.RowIndex].Cells["Biển số xe"].FormattedValue.ToString();
                cbxCarType.Text = dtgvCarInfor.Rows[e.RowIndex].Cells["Tên hiệu xe"].FormattedValue.ToString();
            }
            return;
        }

        private void DtgvCustomerInfor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= dtgvCustomerInfor.RowCount - 1)
            {
                return;
            }
            if ((dtgvCustomerInfor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null))
            {
                customerID = dtgvCustomerInfor.Rows[e.RowIndex].Cells["Mã khách hàng"].FormattedValue.ToString();
                ckbAddNewCustomer.Checked = false;
                txbCustomerName.Text = dtgvCustomerInfor.Rows[e.RowIndex].Cells["Tên khách hàng"].FormattedValue.ToString();
                txbCustomerBirthday.Text = dtgvCustomerInfor.Rows[e.RowIndex].Cells["Năm sinh"].FormattedValue.ToString();
                txbCustomerAddress.Text = dtgvCustomerInfor.Rows[e.RowIndex].Cells["Địa chỉ"].FormattedValue.ToString();
                txbCustomerPhone.Text = dtgvCustomerInfor.Rows[e.RowIndex].Cells["Số điện thoại"].FormattedValue.ToString();
                txbCustomerEmail.Text = dtgvCustomerInfor.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString();
                CustomersDAO.Instance.MakeListCarByCustomerName(txbCustomerName, dtgvCarInfor);
            }
            return;
        }

        private void CkbAddNewCustomer_CheckedChanged(object sender, EventArgs e)
        {
            addNewCustomer = ckbAddNewCustomer.Checked;
        }

        private void CkbAddNewCar_CheckedChanged(object sender, EventArgs e)
        {
            addNewCar = ckbAddNewCar.Checked;
        }


        private void CbxSearchAcceptFormByCarType_DropDown(object sender, EventArgs e)
        {
            cbxSearchAcceptFormByCarType.DataSource = CarsDAO.Instance.GetCarTypes();
        }

        private void CkbSearchDay_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSearchDay.Checked)
            {
                ckbSearchMonth.Checked = true;
                ckbSearchYear.Checked = true;
            }
        }

        private void CkbSearchMonth_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbSearchMonth.Checked == false)
            {
                ckbSearchDay.Checked = false;
            }
            else
            {
                ckbSearchYear.Checked = true;
            }
        }

        private void CkbSearchYear_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbSearchYear.Checked == false)
            {
                ckbSearchMonth.Checked = false;
                ckbSearchDay.Checked = false;
            }
        }

        private void BtnSearchAcceptFormList_Click(object sender, EventArgs e)
        {
            string querySearchAcceptForm = AcceptFormDAO.Instance.SearchAcceptForm(ckbSearchDay, ckbSearchMonth, ckbSearchYear, dtpkSearchAcceptFormByDate.Value.Day.ToString(), dtpkSearchAcceptFormByDate.Value.Month.ToString(), dtpkSearchAcceptFormByDate.Value.Year.ToString(), txbSearchAcceptFormByCustomerName.Text, txbSearchAcceptFormByCustomerPhone.Text, txbSearchAcceptFormByCustomerEmail.Text, txbSearchAcceptFormByCarNumber.Text, cbxSearchAcceptFormByCarType.Text);
            dtgvSearchAcceptFormListResult.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchAcceptForm);
            for (int i = 0; i <= dtgvCarInfor.Columns.Count - 1; i++)
            {
                dtgvSearchAcceptFormListResult.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            txbCountSearchAcceptFormListResult.Text = (dtgvSearchAcceptFormListResult.Rows.Count-1).ToString();
        }

        private void BtnShowAllAcceptForm_Click(object sender, EventArgs e)
        {
            AcceptFormDAO.Instance.ShowAllAcceptFormsList(dtgvSearchAcceptFormListResult);
            for (int i = 0; i <= dtgvCarInfor.Columns.Count - 1; i++)
            {
                dtgvSearchAcceptFormListResult.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void DtgvSearchAcceptFormListResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= dtgvSearchAcceptFormListResult.RowCount - 1)
            {
                return;
            }
            if ((dtgvSearchAcceptFormListResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null))
            {
                acceptFormID = dtgvSearchAcceptFormListResult.Rows[e.RowIndex].Cells["Mã phiếu"].FormattedValue.ToString();
                AcceptFormDAO.Instance.GetAcceptFormRelateiveInforByID(dtgvSearchAcceptFormListResult, acceptFormID, txbSearchAcceptFormByCustomerName, txbSearchAcceptFormByCustomerPhone, txbSearchAcceptFormByCustomerEmail, txbSearchAcceptFormByCarNumber, cbxSearchAcceptFormByCarType);

            }
            return;
        }

        private void BtnDeleteAcceptForm_Click(object sender, EventArgs e)
        {
            AcceptFormDAO.Instance.DeleteAcceptFormByID(acceptFormID);
            BtnShowAllAcceptForm_Click(sender,e);
        }



        private void TxbWageName_TextChanged(object sender, EventArgs e)
        {
            WageDAO.Instance.LoadWageByName(txbWageName, dtgvWageInfor);
        }

        private void BtnWageUpdate_Click(object sender, EventArgs e)
        {
            if (ckbAddNewWage.Checked == true)
            {
                WageDAO.Instance.AddNewWage(txbWageName, txbWageValue);
            }
            else if (ckbAddNewWage.Checked == false)
            {
                WageDAO.Instance.UpdateWage(ChoosingWageID, txbWageName, txbWageValue);
            }
            WageDAO.Instance.LoadAllWage(dtgvAllWageInfo);
            ckbAddNewWage.Checked = true;
        }

        private void BtnDeleteWage_Click(object sender, EventArgs e)
        {
            WageDAO.Instance.DeleteWage(ChoosingWageID, txbWageName);
            WageDAO.Instance.LoadAllWage(dtgvAllWageInfo);
            ckbAddNewWage.Checked = true;
        }

        private void DtgvAllWageInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= dtgvAllWageInfo.RowCount - 1)
            {
                return;
            }
            if (dtgvAllWageInfo.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {

                ChoosingWageID = dtgvAllWageInfo.Rows[e.RowIndex].Cells["Mã tiền công"].FormattedValue.ToString();
                txbWageName.Text = dtgvAllWageInfo.Rows[e.RowIndex].Cells["Tên loại tiền công"].FormattedValue.ToString();
                txbWageValue.Text = dtgvAllWageInfo.Rows[e.RowIndex].Cells["Trị giá"].FormattedValue.ToString();
                ckbAddNewWage.Checked = false;

            }
            return;
        }

        private void DtgvWageInfor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= dtgvWageInfor.RowCount - 1)
            {
                return;
            }
            if (dtgvWageInfor.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                ChoosingWageID = dtgvWageInfor.Rows[e.RowIndex].Cells["Mã tiền công"].FormattedValue.ToString();
                txbWageName.Text = dtgvWageInfor.Rows[e.RowIndex].Cells["Tên loại tiền công"].FormattedValue.ToString();
                txbWageValue.Text = dtgvWageInfor.Rows[e.RowIndex].Cells["Trị giá"].FormattedValue.ToString();
                ckbAddNewWage.Checked = false;
            }
            return;
        }

        private void TxbWageValue_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxbWageValue_TextChanged(object sender, EventArgs e)
        {
            WageDAO.Instance.LoadWageByValue(txbWageValue, dtgvWageInfor);
        }

        private void DtpkRepairTabDateRepair_ValueChanged(object sender, EventArgs e)
        {
            txbRepairTabDateRepair.Text = dtpkRepairTabDateRepair.Value.Date.ToString("dd/MM/yyyy");
            RepairFormDAO.Instance.ShowListRepairToDataGridViewByDate(dtpkRepairTabDateRepair, dtgvRepairTabRepairFormListByDate);
            dtpkRepairTabAcceptDate.Value = dtpkRepairTabDateRepair.Value;
        }


        private void DtpkRepairTabAcceptDate_ValueChanged(object sender, EventArgs e)
        {
            txbRepairTabAcceptDate.Text = dtpkRepairTabAcceptDate.Value.Date.ToString("dd/MM/yyyy");
            AcceptFormDAO.Instance.ShowListAcceptFormToDataGridViewByDate(dtpkRepairTabAcceptDate, dtgvRepairTabAcceptFormListByDate);
            dtpkRepairTabDateRepair.Value = dtpkRepairTabAcceptDate.Value;
        }

        private void DtpkSearchAcceptFormByDate_ValueChanged(object sender, EventArgs e)
        {
            txbSearchAcceptFormDate.Text = dtpkSearchAcceptFormByDate.Value.Date.ToString("dd/MM/yyyy");
        }

        private void DtgvRepairTabAcceptFormListByDate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= dtgvRepairTabAcceptFormListByDate.RowCount - 1)
            {
                return;
            }
            if (dtgvRepairTabAcceptFormListByDate.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                repairTabAcceptFormIDSelected = dtgvRepairTabAcceptFormListByDate.Rows[e.RowIndex].Cells["Mã phiếu tiếp nhận"].FormattedValue.ToString();
                AcceptFormDAO.Instance.GetAcceptFormInforByIDInRepairTab(txbRepairTabAcceptFormID, txbRepairTabCustomerName, txbRepairTabCarNumber, txbRepairTabCarType, repairTabAcceptFormIDSelected);
                
            }
            DtpkRepairTabAcceptDate_ValueChanged(sender, e);
            return;
        }

        private void BtnRepairTabAddNewRepairForm_Click(object sender, EventArgs e)
        {
            RepairFormDAO.Instance.InsertNewRepairForm(txbRepairTabAcceptFormID, dtpkRepairTabDateRepair);
            DtpkRepairTabDateRepair_ValueChanged(sender, e);


        }

        private void DtgvRepairTabRepairFormListByDate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= dtgvRepairTabRepairFormListByDate.RowCount - 1)
            {
                return;
            }
            if (dtgvRepairTabAcceptFormListByDate.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                txbRepairTabRepairFormID.Text = dtgvRepairTabRepairFormListByDate.Rows[e.RowIndex].Cells["Mã phiếu sửa chữa"].FormattedValue.ToString();
                repairTabAcceptFormIDSelected = dtgvRepairTabAcceptFormListByDate.Rows[e.RowIndex].Cells["Mã phiếu tiếp nhận"].FormattedValue.ToString();
                AcceptFormDAO.Instance.GetAcceptFormInforByIDInRepairTab(txbRepairTabAcceptFormID, txbRepairTabCustomerName, txbRepairTabCarNumber, txbRepairTabCarType, repairTabAcceptFormIDSelected);
                TxbRepairTabRepairFormID_TextChanged(sender, e);
            }
            return;
        }

        private void BtnRepairTabAddNewDetailRepairForm_Click(object sender, EventArgs e)
        {
            if(accessaryAmountMaxRepairFormTab!="0")
            {
                RepairFormDAO.Instance.InsertNewDetailRepairFormByRepairFormID(txbRepairTabRepairFormID, txbRepairFormDetailContent, cbxRepairTabAccessaryName, nmrRepairTabAccessaryAmount, cbxReportTabWageName, txbRepairTabCustomerName);
                RepairFormDAO.Instance.ShowDetailRepairFormByRepairFormIDToDataGridView(txbRepairTabRepairFormID, dtgvRepairTabDetailRepairFormListByRepairFormID);
                CbxRepairTabAccessaryName_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("Phụ tùng này đã hết", "Thông báo", MessageBoxButtons.OK);
            }
            
        }

        private void CbxRepairTabAccessaryName_DropDown(object sender, EventArgs e)
        {
            cbxRepairTabAccessaryName.DataSource = AccessaryDAO.Instance.GetAccessaryName();
        }

        private void CbxReportTabWageName_DropDown(object sender, EventArgs e)
        {
            cbxReportTabWageName.DataSource = WageDAO.Instance.GetWageName();
        }

        private void TxbRepairTabRepairFormID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RepairFormDAO.Instance.ShowDetailRepairFormByRepairFormIDToDataGridView(txbRepairTabRepairFormID, dtgvRepairTabDetailRepairFormListByRepairFormID);
            }
            catch (Exception)
            {

              
            }
            
        }

        private void CbxRepairTabAccessaryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            accessaryAmountMaxRepairFormTab = AccessaryDAO.Instance.GetAccessaryAmountBySeletectingAccessaryName(cbxRepairTabAccessaryName);
            nmrRepairTabAccessaryAmount.Maximum = Int16.Parse(accessaryAmountMaxRepairFormTab);
        }

        private void TxbCustomerPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void NmrRepairTabAccessaryAmount_ValueChanged(object sender, EventArgs e)
        {
            if (nmrRepairTabAccessaryAmount.Value > nmrRepairTabAccessaryAmount.Maximum)
            {
                nmrRepairTabAccessaryAmount.Value = nmrRepairTabAccessaryAmount.Maximum;
            }
        }



        private void BtnRepairTabDeleteRepairForm_Click(object sender, EventArgs e)
        {
            RepairFormDAO.Instance.DeleteRepairFormIfNotHaveDetailRepairForm(txbRepairTabRepairFormID);
            txbRepairTabRepairFormID.Text = "";
            DtpkRepairTabDateRepair_ValueChanged(sender, e);
        }

        private void BtnRepairTabRemoveRepairForm_Click(object sender, EventArgs e)
        {
            RepairFormDAO.Instance.DeleteDetailRepairFormByRepairFormID(detailRepairFormIDSelected, cbxRepairTabAccessaryName, nmrRepairTabAccessaryAmount, txbRepairTabCustomerName, txbRepairTabRepairFormID);
            TxbRepairTabRepairFormID_TextChanged(sender, e);
        }

        private void DtgvRepairTabDetailRepairFormListByRepairFormID_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(dtgvRepairTabDetailRepairFormListByRepairFormID.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
                {
                    detailRepairFormIDSelected = dtgvRepairTabDetailRepairFormListByRepairFormID.Rows[e.RowIndex].Cells["Mã CT"].FormattedValue.ToString();
                    txbRepairFormDetailContent.Text = dtgvRepairTabDetailRepairFormListByRepairFormID.Rows[e.RowIndex].Cells["Nội dung"].FormattedValue.ToString();
                    cbxRepairTabAccessaryName.Text = dtgvRepairTabDetailRepairFormListByRepairFormID.Rows[e.RowIndex].Cells["Phụ tùng"].FormattedValue.ToString();
                    CbxRepairTabAccessaryName_SelectedIndexChanged(sender, e);
                    nmrRepairTabAccessaryAmount.Value = Int32.Parse(dtgvRepairTabDetailRepairFormListByRepairFormID.Rows[e.RowIndex].Cells["Số lượng"].FormattedValue.ToString());
                    cbxReportTabWageName.Text = dtgvRepairTabDetailRepairFormListByRepairFormID.Rows[e.RowIndex].Cells["Tiền công"].FormattedValue.ToString();
                    
                }
            }
            catch (Exception)
            {

            }
        }

        private void BtnViewSales_Click(object sender, EventArgs e)
        {
            DataTable dataSaleReport = SaleNotifyDAO.Instance.getSaleReport(dtSelectSalesMonth, dtSelectSalesYear);

            dtgvSalesReport.DataSource = dataSaleReport;
            txbSumOfSales.Text = dataSaleReport.AsEnumerable()
                          .Sum(x => x.Field<int>("Thành tiền"))
                          .ToString();
            int i = 1;
            foreach (DataGridViewRow dtgvSalesReportRow in dtgvSalesReport.Rows)
            {
                if(dtgvSalesReportRow != dtgvSalesReport.Rows[dtgvSalesReport.Rows.Count - 1])
                {
                    dtgvSalesReportRow.Cells["STT"].Value = (i++).ToString();
                    dtgvSalesReportRow.Cells["TiLe"].Value = (Convert.ToDouble(dtgvSalesReportRow.Cells["ThanhTien"].Value) * 100 / int.Parse(txbSumOfSales.Text)).ToString() + "%";
                }
                
            }
            

        }

        private void DtpkBillTabDate_ValueChanged(object sender, EventArgs e)
        {
            if(ckbBillTabSearchByDate.Checked)
            {
                txbBillTabCustomerName.Text = "";
                txbBillTabCustomerEmail.Text = "";
                txbBillTabCustomerPhone.Text = "";
                txbBillTabCarNumber.Text = "";
                BillDAO.Instance.ShowRepairFormListByDate(dtgvBillTabRpairFormList, dtpkBillTabDate, txbBillTabCustomerName, txbBillTabCarNumber);
            }

        }



        private void TxbBillTabCustomerName_TextChanged(object sender, EventArgs e)
        {
            BillDAO.Instance.ShowRepairFormList(dtgvBillTabRpairFormList, txbBillTabCustomerName, txbBillTabCarNumber);
            BillDAO.Instance.ShowBillListToDataGridView(txbBillTabCustomerName, txbBillTabCarNumber, dtgvBillTabListBill);
            CustomersDAO.Instance.GetCustomersInfoToTextBox(txbBillTabCustomerName, txbBillTabCustomerPhone, txbBillTabCustomerEmail);
        }

        private void TxbBillTabCarNumber_TextChanged(object sender, EventArgs e)
        {
            BillDAO.Instance.ShowRepairFormList(dtgvBillTabRpairFormList, txbBillTabCustomerName, txbBillTabCarNumber);
            BillDAO.Instance.ShowBillListToDataGridView(txbBillTabCustomerName, txbBillTabCarNumber, dtgvBillTabListBill);
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
            catch (Exception)
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
                ckbAccessaryAddNew.Checked = false;
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

        private void BtnAccessaryExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ckbAccessaryAddNew.Checked == true))
                {                   
                    AccessaryDAO.Instance.InsertAccessary(txbAccessaryName, txbAccessaryPrice, txbAccessaryNumberGet, dtgvAccessaryList);
                }
                else
                {
                    AccessaryDAO.Instance.UpdateAccessary(txbAccessaryName, txbAccessaryNumberInStock, txbAccessaryPrice, txbAccessaryNumberGet, dtgvAccessaryList);
                    ckbAccessaryAddNew.Checked = true;
                }
                AccessaryDAO.Instance.SearchAccessaryIDByName(txbAccessaryName, dtgvAccessaryList);
                ChoosingAccessaryID = dtgvAccessaryList.Rows[0].Cells["Mã phụ tùng"].FormattedValue.ToString();
                AccessaryDAO.Instance.InsertAccessaryImportForm(ChoosingAccessaryID, txbAccessaryNumberGet, txbAccessaryPrice, dtgvAccessaryList);
                AccessaryDAO.Instance.UpdateInventoryReportBySupply(ChoosingAccessaryID, txbAccessaryNumberGet.Text, dtgvAccessaryReport);
                AccessaryDAO.Instance.SearchAccessaryByName(txbAccessaryName, dtgvAccessaryList);
                AccessaryDAO.Instance.ShowAccessaryImportTable(dtgvAccessaryImportList);
                AccessaryDAO.Instance.ShowInventoryReportTable(dtgvAccessaryReport);
               
                txbAccessaryName.Clear();
            }
            catch (Exception)
            {

            }
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

        private void NudInventoryReportMonth_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                AccessaryDAO.Instance.SearchInventoryReport(nudInventoryReportMonth, nudInventoryReportYear, dtgvAccessaryReport);
            }
            catch (Exception)
            {

            }
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

        private void TxbCarBrandName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CarsDAO.Instance.SearchCarBrandByName(txbCarBrandName, dtgvCarBrandList);
            }
            catch (Exception)
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

        private void TabControl1_Click(object sender, EventArgs e)
        {
            CarsDAO.Instance.SearchCarBrandByName(txbCarBrandName, dtgvCarBrandList);
            CarsDAO.Instance.ViewAllCarList(dtgvCarCustom_CarList);
            CustomersDAO.Instance.ViewAllCustomerList(dtgvCarCustomer_CustomerList);
            CkbBillTabSearchByDate_CheckedChanged(sender, e);
            txbCustomerName.Clear();
            TxbCustomerName_TextChanged(sender, e);
            txbCustomerBirthday.Clear();
            txbCustomerAddress.Clear();
            txbCustomerPhone.Clear();
            txbCustomerEmail.Clear();
            txbCarNumber.Clear();
            cbxCarType.Text = "";
        }

        private void CkbBillTabSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbBillTabSearchByDate.Checked)
            {
                DtpkBillTabDate_ValueChanged(sender, e);
            }
            else
            {
                BillDAO.Instance.ShowRepairFormList(dtgvBillTabRpairFormList, txbBillTabCustomerName, txbBillTabCarNumber);
            }
        }

        private void BtnBillTabAddNewBill_Click(object sender, EventArgs e)
        {

            BillDAO.Instance.InsertNewBill(txbBillTabTotalPrize, txbBillTabCustomerName, txbBillTabCarNumber, billTabIDRepairForm) ;
            BillDAO.Instance.ShowRepairFormList(dtgvBillTabRpairFormList, txbBillTabCustomerName, txbBillTabCarNumber);
            BillDAO.Instance.ShowBillListToDataGridView(txbBillTabCustomerName, txbBillTabCarNumber, dtgvBillTabListBill);
        }

        private void DtgvBillTabRpairFormList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            txbBillTabTotalPrize.Clear();
        }

        private void TxbBillTabTotalPrize_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DtgvBillTabRpairFormList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex == dtgvBillTabRpairFormList.RowCount - 1)
                {
                    txbBillTabCustomerEmail.Clear();
                    txbBillTabCustomerPhone.Clear();
                }
                txbBillTabTotalPrize.Text = "0";
                txbBillTabCustomerName.Text = dtgvBillTabRpairFormList.Rows[e.RowIndex].Cells["customerName"].FormattedValue.ToString();
                txbBillTabCarNumber.Text = dtgvBillTabRpairFormList.Rows[e.RowIndex].Cells["carNumber"].FormattedValue.ToString();
                billTabIDRepairForm = dtgvBillTabRpairFormList.Rows[e.RowIndex].Cells["repairFormID"].FormattedValue.ToString();
                txbBillTabTotalPrize.Text = dtgvBillTabRpairFormList.Rows[e.RowIndex].Cells["totalPrize"].FormattedValue.ToString(); ;
            }
            catch (Exception)
            {

            }

        }

        private void TxbBillTabDeleteBill_Click(object sender, EventArgs e)
        {
            BillDAO.Instance.DeleteBill(txbBillTabTotalPrize, txbBillTabCustomerName, billTabIDBill, billTabIDRepairForm);
            BillDAO.Instance.ShowRepairFormList(dtgvBillTabRpairFormList, txbBillTabCustomerName, txbBillTabCarNumber);
            BillDAO.Instance.ShowBillListToDataGridView(txbBillTabCustomerName, txbBillTabCarNumber, dtgvBillTabListBill);

        }

        private void DtgvBillTabListBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == dtgvBillTabListBill.RowCount - 1)
                {
                    txbBillTabCustomerEmail.Clear();
                    txbBillTabCustomerPhone.Clear();
                }
                billTabIDBill = dtgvBillTabListBill.Rows[e.RowIndex].Cells["Mã phiếu"].FormattedValue.ToString();
                
                billTabIDRepairForm = DataProviderDAO.Instance.ExecuteScalar("SELECT MaPhieuSuaChua FROM dbo.TablePhieuThuTien WHERE MaPhieuThuTien = " + billTabIDBill).ToString();
                TxbBillTabCustomerName_TextChanged(sender, e);
                TxbBillTabCarNumber_TextChanged(sender, e);
                txbBillTabTotalPrize.Text = dtgvBillTabListBill.Rows[e.RowIndex].Cells["Số tiền thu"].FormattedValue.ToString();

            }
            catch (Exception)
            {
               

            }
        }

        private void ThôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TxbRepairTabDateRepair_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
