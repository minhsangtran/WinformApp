using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyGaraOto.DAO;

namespace QuanLyGaraOto.GUI
{
    public partial class fQuanLyTienCong : Form
    {
        string ChoosingWageID;
        public fQuanLyTienCong()
        {
            InitializeComponent();
            WageDAO.Instance.LoadAllWage(dtgvAllWageInfo);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }



        private void TxbWageName_TextChanged(object sender, EventArgs e)
        {
            WageDAO.Instance.LoadWageByName(txbWageName, dtgvWageInfor);
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

        private void BtnWageUpdate_Click(object sender, EventArgs e)
        {
            if(ckbAddNewWage.Checked == true)
            {
                WageDAO.Instance.AddNewWage(txbWageName, txbWageValue);
            }
            else if(ckbAddNewWage.Checked == false)
            {
                WageDAO.Instance.UpdateWage(ChoosingWageID, txbWageName, txbWageValue);
            }
            WageDAO.Instance.LoadAllWage(dtgvAllWageInfo);
            ckbAddNewWage.Checked = true;
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

        private void BtnDeleteWage_Click(object sender, EventArgs e)
        {
            WageDAO.Instance.DeleteWage(ChoosingWageID, txbWageName);
            WageDAO.Instance.LoadAllWage(dtgvAllWageInfo);
            ckbAddNewWage.Checked = true;
        }

        private void TxbWageValue_TextChanged(object sender, EventArgs e)
        {
            WageDAO.Instance.LoadWageByValue(txbWageValue, dtgvWageInfor);
        }
    }
}
