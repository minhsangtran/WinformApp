using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.DAO
{
    class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }

            private set
            {
                instance = value;
            }
        }
        public void ShowRepairFormList(DataGridView dtgvRepairFormList, TextBox txbCustomerName, TextBox txbCarNumber)
        {
            string queryShowRepairFormListByDate = "SELECT PhieuSuaChua.MaPhieuSuaChua AS [Mã phiếu], KhachHang.TenKhachHang AS [Khách hàng], PhieuTiepNhan.BienSoXe AS [Biển số xe], (SELECT SUM(ThanhTien) FROM dbo.TableCTPhieuSuaChua WHERE MaPhieuSuaChua = PhieuSuaChua.MaPhieuSuaChua) AS [Tổng tiền], PhieuSuaChua.NgaySuaChua AS [Ngày sữa chữa] FROM dbo.TablePhieuSuaChua AS PhieuSuaChua, dbo.TablePhieuTiepNhan AS PhieuTiepNhan, dbo.TableKhachHang AS KhachHang WHERE PhieuSuaChua.MaPhieuTiepNhan = PhieuTiepNhan.MaPhieuTiepNhan AND KhachHang.MaKhachHang = PhieuTiepNhan.MaKhachHang AND KhachHang.TenKhachHang LIKE N'%" + txbCustomerName.Text + "%' AND PhieuTiepNhan.BienSoXe LIKE N'%" + txbCarNumber.Text + "%' AND PhieuSuaChua.ThanhToan = 0";
            try
            {
                dtgvRepairFormList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowRepairFormListByDate);
                for (int i = 0; i <= dtgvRepairFormList.Columns.Count - 1; i++)
                {
                    dtgvRepairFormList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception)
            {

            }

        }
        public void ShowRepairFormListByDate(DataGridView dtgvRepairFormList, DateTimePicker dtpkDate, TextBox txbCustomerName, TextBox txbCarNumber)
        {
            string queryShowRepairFormListByDate = "SELECT PhieuSuaChua.MaPhieuSuaChua AS [Mã phiếu], KhachHang.TenKhachHang AS [Khách hàng], PhieuTiepNhan.BienSoXe AS [Biển số xe], (SELECT SUM(ThanhTien) FROM dbo.TableCTPhieuSuaChua WHERE MaPhieuSuaChua = PhieuSuaChua.MaPhieuSuaChua) AS [Tổng tiền], PhieuSuaChua.NgaySuaChua AS [Ngày sữa chữa] FROM dbo.TablePhieuSuaChua AS PhieuSuaChua, dbo.TablePhieuTiepNhan AS PhieuTiepNhan, dbo.TableKhachHang AS KhachHang WHERE PhieuSuaChua.MaPhieuTiepNhan = PhieuTiepNhan.MaPhieuTiepNhan AND KhachHang.MaKhachHang = PhieuTiepNhan.MaKhachHang AND PhieuSuaChua.NgaySuaChua = N'" + dtpkDate.Value.Date.ToString("yyyy-MM-dd") + "' AND KhachHang.TenKhachHang LIKE N'%" + txbCustomerName.Text + "%' AND PhieuTiepNhan.BienSoXe LIKE N'%" + txbCarNumber.Text + "%'";
            try
            {
                dtgvRepairFormList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowRepairFormListByDate);
                for (int i = 0; i <= dtgvRepairFormList.Columns.Count - 1; i++)
                {
                    dtgvRepairFormList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception)
            {

            }

        }

        public void InsertNewBill(TextBox txbTotalPrize, TextBox txbCustomerName, TextBox CarNumber, string repairFormID)
        {

            string queryInsertNewBill = "INSERT dbo.TablePhieuThuTien ( MaPhieuSuaChua, MaKhachHang, BienSoXe, NgayThuTien, SoTienThu ) VALUES ( " + repairFormID + ", (SELECT MaKhachHang FROM dbo.TableKhachHang WHERE TenKhachHang = N'" + txbCustomerName.Text + "'), N'" + CarNumber.Text + "', GETDATE(), " + txbTotalPrize.Text + " )";
            try
            {
                DataProviderDAO.Instance.ExecuteNonQuery(queryInsertNewBill);
                MessageBox.Show("Đã thêm phiếu thu tiền", "Thông báo", MessageBoxButtons.OK);
                DataProviderDAO.Instance.ExecuteNonQuery("UPDATE dbo.TableKhachHang SET TienNo = TienNo - " + txbTotalPrize.Text + " WHERE TenKhachHang = N'" + txbCustomerName.Text + "'");
                DataProviderDAO.Instance.ExecuteNonQuery("UPDATE dbo.TablePhieuSuaChua SET ThanhToan = 1 WHERE MaPhieuSuaChua = " + repairFormID);
            }
            catch (Exception)
            {


            }
        }

        public void ShowBillListToDataGridView(TextBox txbCustomerName, TextBox txbCarNumber, DataGridView dtgvBillList)
        {
            string queryShowBillListToDataGridView = "SELECT PhieuThuTien.MaPhieuThuTien AS [Mã phiếu], (SELECT TenKhachHang FROM dbo.TableKhachHang WHERE MaKhachHang = PhieuThuTien.MaKhachHang) AS [Khách hàng], PhieuThuTien.BienSoXe AS [Biển số xe], PhieuThuTien.SoTienThu AS [Số tiền thu] FROM dbo.TablePhieuThuTien AS PhieuThuTien WHERE PhieuThuTien.MaPhieuSuaChua IN (SELECT MaPhieuSuaChua FROM dbo.TablePhieuSuaChua WHERE MaPhieuTiepNhan IN (SELECT MaPhieuTiepNhan FROM dbo.TablePhieuTiepNhan WHERE MaKhachHang IN (SELECT MaKhachHang FROM dbo.TableKhachHang WHERE TenKhachHang LIKE N'%" + txbCustomerName.Text + "%') AND BienSoXe LIKE N'%" + txbCarNumber.Text + "%'))";
            try
            {
                dtgvBillList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowBillListToDataGridView);
                CustomersDAO.Instance.AutoFormatDataGridView(dtgvBillList);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteBill(TextBox txbTotalPrize, TextBox txbCustomerName, string billID, string repairFormID)
        {
            string queryDeleteBill = "DELETE dbo.TablePhieuThuTien WHERE MaPhieuThuTien = "+ billID;
            try
            {
                DataProviderDAO.Instance.ExecuteNonQuery(queryDeleteBill);
                MessageBox.Show("Đã xóa phiếu thu tiền ID: " + billID, "Thông báo", MessageBoxButtons.OK);
                DataProviderDAO.Instance.ExecuteNonQuery("UPDATE dbo.TableKhachHang SET TienNo = TienNo + " + txbTotalPrize.Text + " WHERE TenKhachHang = N'" + txbCustomerName.Text + "'");
                DataProviderDAO.Instance.ExecuteNonQuery("UPDATE dbo.TablePhieuSuaChua SET ThanhToan = 0 WHERE MaPhieuSuaChua = " + repairFormID);
            }
            catch (Exception)
            {


            }
        }

    }
}
