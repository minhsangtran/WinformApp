using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.DAO
{
    class AcceptFormDAO
    {
        private static AcceptFormDAO instance;

        public static AcceptFormDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AcceptFormDAO();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        public void InsertNewAcceptForm(string customerID, TextBox txbCarNumber)
        {
            string queryInsertNewAcceptForm = "EXEC dbo.USP_CreateNewAcceptForm @customerID , @carNumber ";
            DataProviderDAO.Instance.ExecuteNonQuery(queryInsertNewAcceptForm, new object[] { customerID, txbCarNumber.Text});
        }
        public int GetNumAcceptFormByDate()
        {
            try
            {
                return DataProviderDAO.Instance.ExecuteQuery("SELECT * FROM dbo.TablePhieuTiepNhan WHERE NgayTiepNhan = '" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'").Rows.Count;
            }
            catch (Exception)
            {

                return 0;
            }
            
        }

        public string SearchAcceptForm(CheckBox ckbDay, CheckBox ckbMonth, CheckBox ckbYear, string Day, string Month, string Year, string CustomerName, string CustomerPhone, string CustomerEmail, string CarNumber, string CarType)
        {
            bool continueSearch = false;
            string querySearchAcceptForm = "SELECT PhieuTiepNhan.MaPhieuTiepNhan AS [Mã Phiếu], KhachHang.TenKhachHang AS [Tên khách hàng], PhieuTiepNhan.BienSoXe AS [Biển số xe], CONVERT(NVARCHAR(100), PhieuTiepNhan.NgayTiepNhan, 103) AS [Ngày tiếp nhận] FROM dbo.TablePhieuTiepNhan AS PhieuTiepNhan, dbo.TableKhachHang AS KhachHang, dbo.TableHieuXe AS HieuXe, dbo.TableXe AS Xe WHERE ";
            if (ckbDay.Checked)
            {
                querySearchAcceptForm += "DAY(PhieuTiepNhan.NgayTiepNhan) = " + Day + " AND MONTH(PhieuTiepNhan.NgayTiepNhan) = " + Month + "AND YEAR(PhieuTiepNhan.NgayTiepNhan) = " + Year;
                continueSearch = true;
            }
            else if (ckbMonth.Checked)
            {
                querySearchAcceptForm += "MONTH(PhieuTiepNhan.NgayTiepNhan) = " + Month + " AND YEAR(PhieuTiepNhan.NgayTiepNhan) = " + Year;
                continueSearch = true;
            }
            else if (ckbDay.Checked) 
            {
                querySearchAcceptForm += "YEAR(PhieuTiepNhan.NgayTiepNhan) = " + Year;
                continueSearch = true;
            }
            if(CustomerName != "")
            {
                if(continueSearch)
                {
                    querySearchAcceptForm += " AND ";
                }
                querySearchAcceptForm += "KhachHang.TenKhachHang LIKE N'%" + CustomerName + "%'";
                continueSearch = true;
            }
            if (CustomerPhone != "")
            {
                if (continueSearch)
                {
                    querySearchAcceptForm += " AND ";
                }
                querySearchAcceptForm += "KhachHang.SoDienThoai LIKE N'%" + CustomerPhone + "%'";
                continueSearch = true;
            }
            if (CustomerEmail != "")
            {
                if (continueSearch)
                {
                    querySearchAcceptForm += " AND ";
                }
                querySearchAcceptForm += "KhachHang.Email LIKE N'%" + CustomerEmail + "%'";
                continueSearch = true;
            }
            if (CarNumber != "")
            {
                if (continueSearch)
                {
                    querySearchAcceptForm += " AND ";
                }
                querySearchAcceptForm += "Xe.BienSoXe LIKE N'%" + CarNumber + "%'";
                continueSearch = true;
            }
            if (CarType != "")
            {
                if (continueSearch)
                {
                    querySearchAcceptForm += " AND ";
                }
                querySearchAcceptForm += "HieuXe.TenHieuXe = N'" + CarType + "'";
                continueSearch = true;
            }
            if (continueSearch)
            {
                querySearchAcceptForm += " AND ";
            }
            querySearchAcceptForm += "PhieuTiepNhan.MaKhachHang = KhachHang.MaKhachHang AND PhieuTiepNhan.BienSoXe = Xe.BienSoXe AND Xe.MaHieuXe = HieuXe.MaHieuXe";
            return querySearchAcceptForm;
        }
        public void ShowAllAcceptFormsList(DataGridView dtgvSearchAcceptFormListResult)
        {
            string queryShowAllAcceptFormsList = "SELECT PhieuTiepNhan.MaPhieuTiepNhan AS [Mã Phiếu], KhachHang.TenKhachHang AS [Tên khách hàng], PhieuTiepNhan.BienSoXe AS [Biển số xe], CONVERT(NVARCHAR(100), PhieuTiepNhan.NgayTiepNhan, 103) AS [Ngày tiếp nhận] FROM dbo.TablePhieuTiepNhan AS PhieuTiepNhan, dbo.TableKhachHang AS KhachHang, dbo.TableHieuXe AS HieuXe, dbo.TableXe AS Xe WHERE PhieuTiepNhan.MaKhachHang = KhachHang.MaKhachHang AND PhieuTiepNhan.BienSoXe = Xe.BienSoXe AND Xe.MaHieuXe = HieuXe.MaHieuXe";
            dtgvSearchAcceptFormListResult.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowAllAcceptFormsList);
            for (int i = 0; i <= dtgvSearchAcceptFormListResult.Columns.Count - 1; i++)
            {
                dtgvSearchAcceptFormListResult.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public void DeleteAcceptFormByID(string AcceptFormID)
        {
            if (AcceptFormID == "0")
            {
                MessageBox.Show("Bạn hãy chọn phiếu cần xóa", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            string queryDeleteAcceptFormByID = "DELETE dbo.TablePhieuTiepNhan WHERE MaPhieuTiepNhan = " + AcceptFormID;
            DataProviderDAO.Instance.ExecuteNonQuery(queryDeleteAcceptFormByID);
            MessageBox.Show("Đã xóa phiếu có mã số phiếu:" + AcceptFormID, "Thông báo", MessageBoxButtons.OK);
            AcceptFormID = "0";
        }

        public void GetAcceptFormRelateiveInforByID(DataGridView dtgvSearchAcceptFormListResult, string AcceptFormID, TextBox txbCustomerName, TextBox txbCustomerPhone, TextBox txbCustomerEmail, TextBox txbCarNumber, ComboBox cbxCarType)
        {
            string queryShowAcceptFormInforByID = "SELECT * FROM dbo.TablePhieuTiepNhan AS PhieuTiepNhan, dbo.TableKhachHang AS KhachHang, dbo.TableXe AS Xe, dbo.TableHieuXe AS HieuXe WHERE PhieuTiepNhan.MaKhachHang = KhachHang.MaKhachHang AND PhieuTiepNhan.BienSoXe = Xe.BienSoXe AND Xe.MaHieuXe = HieuXe. MaHieuXe AND PhieuTiepNhan.MaPhieuTiepNhan =" + AcceptFormID;
            DataTable data = new DataTable();
            data = DataProviderDAO.Instance.ExecuteQuery(queryShowAcceptFormInforByID);
            txbCustomerName.Text = data.Rows[0]["TenKhachHang"].ToString();
            txbCustomerPhone.Text = data.Rows[0]["SoDienThoai"].ToString();
            txbCustomerEmail.Text = data.Rows[0]["Email"].ToString();
            txbCarNumber.Text = data.Rows[0]["BienSoXe"].ToString();
            cbxCarType.Text = data.Rows[0]["TenHieuXe"].ToString();
        }

        public void ShowListAcceptFormToDataGridViewByDate(DateTimePicker dtpkGetAcceptList, DataGridView dtgvShowListAcceptFormByDate)
        {
            string queryShowListAcceptFormToDataGridViewByDate = "SELECT PhieuTiepNhan.MaPhieuTiepNhan AS [Mã phiếu tiếp nhận], KhachHang.TenKhachHang AS [Tên khách hàng], Xe.BienSoXe AS [Biển số xe] FROM dbo.TablePhieuTiepNhan AS PhieuTiepNhan, dbo.TableKhachHang AS KhachHang, dbo.TableXe AS Xe WHERE PhieuTiepNhan.MaKhachHang = KhachHang.MaKhachHang AND PhieuTiepNhan.BienSoXe = Xe.BienSoXe AND PhieuTiepNhan.NgayTiepNhan = '" + dtpkGetAcceptList.Value.Date.ToString("yyyy-MM-dd") + "'";
            dtgvShowListAcceptFormByDate.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowListAcceptFormToDataGridViewByDate);
            for (int i = 0; i <= dtgvShowListAcceptFormByDate.Columns.Count - 1; i++)
            {
                dtgvShowListAcceptFormByDate.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public void GetAcceptFormInforByIDInRepairTab(TextBox txbAcceptFormID, TextBox txbCustomerName, TextBox txbCarNumber, TextBox txbCarType, string acceptFormID)
        {
            string queryGetAcceptFormInforByIDInRepairTab = "SELECT PhieuTiepNhan.MaPhieuTiepNhan AS [Mã phiếu tiếp nhận], KhachHang.TenKhachHang AS [Tên khách hàng], Xe.BienSoXe AS [Biển số xe], HieuXe.TenHieuXe AS [Hiệu xe] FROM dbo.TablePhieuTiepNhan AS PhieuTiepNhan, dbo.TableKhachHang AS KhachHang, dbo.TableXe AS Xe, dbo.TableHieuXe AS HieuXe WHERE PhieuTiepNhan.MaKhachHang = KhachHang.MaKhachHang AND PhieuTiepNhan.BienSoXe = Xe.BienSoXe AND HieuXe.MaHieuXe = Xe.MaHieuXe AND PhieuTiepNhan.MaPhieuTiepNhan =" + acceptFormID;
            DataTable AcceptFormInforByIDInRepairTab = new DataTable();
            AcceptFormInforByIDInRepairTab = DataProviderDAO.Instance.ExecuteQuery(queryGetAcceptFormInforByIDInRepairTab);
            txbAcceptFormID.Text = AcceptFormInforByIDInRepairTab.Rows[0]["Mã phiếu tiếp nhận"].ToString();
            txbCustomerName.Text = AcceptFormInforByIDInRepairTab.Rows[0]["Tên khách hàng"].ToString();
            txbCarNumber.Text = AcceptFormInforByIDInRepairTab.Rows[0]["Biển số xe"].ToString();
            txbCarType.Text = AcceptFormInforByIDInRepairTab.Rows[0]["Hiệu xe"].ToString();
        }



    }
}
