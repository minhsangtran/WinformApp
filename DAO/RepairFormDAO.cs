using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.DAO
{
    class RepairFormDAO
    {
        private static RepairFormDAO instance;

        public static RepairFormDAO Instance
        {
            get
            {
                if (instance == null) 
                {
                    instance = new RepairFormDAO();
                }
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        public void InsertNewRepairForm(TextBox txbAcceptFormID, DateTimePicker dtpkRepairDate)
        {
            string queryInsertNewRepairForm = "INSERT dbo.TablePhieuSuaChua ( MaPhieuTiepNhan, NgaySuaChua, ThanhToan ) VALUES ( " + txbAcceptFormID.Text + ", '" + dtpkRepairDate.Value.Date.ToString("yyyy-MM-dd") + "', 0 )";
            try
            {
                DataProviderDAO.Instance.ExecuteNonQuery(queryInsertNewRepairForm);
                MessageBox.Show("Đã thêm thành công phiếu: " + txbAcceptFormID.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Phiếu sữa chữa cho phiếu tiếp nhận " + txbAcceptFormID.Text + " đã có hoặc ID của phiếu không tồn tại", "Thông báo", MessageBoxButtons.OK);
            }
           
        }

        public void ShowListRepairToDataGridViewByDate(DateTimePicker dtpkGetRepairList, DataGridView dtgvShowListRepairFormByDate)
        {
            string queryShowListAcceptFormToDataGridViewByDate = "SELECT MaPhieuSuaChua AS [Mã phiếu sửa chữa], MaPhieuTiepNhan AS [Mã phiếu tiếp nhận], NgaySuaChua AS [Ngày sửa chữa], ThanhToan AS [Thanh toán] FROM dbo.TablePhieuSuaChua WHERE NgaySuaChua = '" + dtpkGetRepairList.Value.Date.ToString("yyyy-MM-dd") + "'";
            dtgvShowListRepairFormByDate.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowListAcceptFormToDataGridViewByDate);
            for (int i = 0; i <= dtgvShowListRepairFormByDate.Columns.Count - 1; i++)
            {
                dtgvShowListRepairFormByDate.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public void InsertNewDetailRepairFormByRepairFormID(TextBox txbRepairFormID, TextBox txbContent, ComboBox cbxAccessaryName, NumericUpDown nmrAccessaryAmount, ComboBox cbxWageName, TextBox txbCustomerName)
        {
            try
            {
                if (DataProviderDAO.Instance.ExecuteScalar("SELECT * FROM dbo.TablePhieuSuaChua WHERE ThanhToan = 0 AND MaPhieuSuaChua = "+ txbRepairFormID.Text) == null)
                {
                    MessageBox.Show("Phiếu này đã được thanh toán", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                string queryInsertNewDetailRepairFormByRepairFormID = "INSERT dbo.TableCTPhieuSuaChua (MaPhieuSuaChua, NoiDung, MaPhuTung, SoLuong, MaTienCong, ThanhTien ) VALUES ( " + txbRepairFormID.Text + ", N'" + txbContent.Text + "', (SELECT MaPhuTung FROM dbo.TablePhuTung WHERE TenPhuTung = N'" + cbxAccessaryName.Text + "'), " + nmrAccessaryAmount.Text + ", (SELECT MaTienCong FROM dbo.TableTienCong WHERE LoaiTienCong = N'" + cbxWageName.Text + "'), ((SELECT DonGia FROM dbo.TablePhuTung WHERE TenPhuTung = N'" + cbxAccessaryName.Text + "') * " + nmrAccessaryAmount.Text + ")+(SELECT SoTienCong FROM dbo.TableTienCong WHERE LoaiTienCong = N'" + cbxWageName.Text + "') )";
                DataProviderDAO.Instance.ExecuteQuery(queryInsertNewDetailRepairFormByRepairFormID);
                string queryReduceAmountAccessary = "UPDATE dbo.TablePhuTung SET SoLuong = SoLuong - " + nmrAccessaryAmount.Text + " WHERE TenPhuTung = N'" + cbxAccessaryName.Text + "'";
                DataProviderDAO.Instance.ExecuteQuery(queryReduceAmountAccessary);
                string accessaryUnitPrize = DataProviderDAO.Instance.ExecuteScalar("SELECT DonGia FROM dbo.TablePhuTung WHERE TenPhuTung = N'" + cbxAccessaryName.Text + "'").ToString();
                string wageUnitPrize = DataProviderDAO.Instance.ExecuteScalar("SELECT SoTienCong FROM dbo.TableTienCong WHERE LoaiTienCong = N'" + cbxWageName.Text + "'").ToString();

                string totalPrize = ((Int32.Parse(accessaryUnitPrize) * Int32.Parse(nmrAccessaryAmount.Text)) + Int32.Parse(wageUnitPrize)).ToString();
                string queryAddDeptForCustomer = "UPDATE dbo.TableKhachHang SET TienNo = TienNo + "+totalPrize+" WHERE TenKhachHang = N'"+txbCustomerName.Text+"'";
                DataProviderDAO.Instance.ExecuteNonQuery(queryAddDeptForCustomer);
                MessageBox.Show("Đã thêm chi tiết cho phiếu sửa chữa: " + txbRepairFormID.Text, "Thông báo", MessageBoxButtons.OK);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể thêm chi tiết", "Thông báo", MessageBoxButtons.OK);
            }
        }

        public void DeleteDetailRepairFormByRepairFormID(string detailRepairFormID, ComboBox cbxAccessaryName, NumericUpDown nmrAccessaryAmount, TextBox txbCustomerName, TextBox txbRepairFormID)
        {
            try
            {
                if (DataProviderDAO.Instance.ExecuteScalar("SELECT * FROM dbo.TablePhieuSuaChua WHERE ThanhToan = 0 AND MaPhieuSuaChua = " + txbRepairFormID.Text) == null)
                {
                    MessageBox.Show("Phiếu này đã được thanh toán", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                // Increase Amount of Accessary when detail repair form is deleted
                string queryIncreaseAmountAccessary = "UPDATE dbo.TablePhuTung SET SoLuong = SoLuong + " + nmrAccessaryAmount.Text + " WHERE TenPhuTung = N'" + cbxAccessaryName.Text + "'";
                DataProviderDAO.Instance.ExecuteQuery(queryIncreaseAmountAccessary);
                // Query to find total prize for detail repair form will be deleted
                string queryTotalPrize = "SELECT ThanhTien FROM dbo.TableCTPhieuSuaChua WHERE MaCT = " + detailRepairFormID;
                string totalPrize = DataProviderDAO.Instance.ExecuteScalar(queryTotalPrize).ToString();
                // Remove the customer's dept by total prize of detail repair form
                string queryRemoveDeptForCustomer = "UPDATE dbo.TableKhachHang SET TienNo = TienNo - " + totalPrize + " WHERE TenKhachHang = N'" + txbCustomerName.Text + "'";
                DataProviderDAO.Instance.ExecuteNonQuery(queryRemoveDeptForCustomer);
                // Remove detail repair form
                DataProviderDAO.Instance.ExecuteNonQuery("DELETE dbo.TableCTPhieuSuaChua WHERE MaCT = " + detailRepairFormID);
                MessageBox.Show("Đã xóa chi tiết cho phiếu sửa chữa: " + detailRepairFormID, "Thông báo", MessageBoxButtons.OK);

            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa chi tiết chi tiết sửa chữa "+ detailRepairFormID, "Thông báo", MessageBoxButtons.OK);
            }
        }
        public void ShowDetailRepairFormByRepairFormIDToDataGridView(TextBox txbRepairFormID, DataGridView dtgvDetailRepairForm)
        {
            string queryShowDetailRepairFormByRepairFormIDToDataGridView = "SELECT CTPhieuSuaChua.NoiDung AS [Nội dung], PhuTung.TenPhuTung AS [Phụ tùng], PhuTung.DonGia AS [Đơn giá], CTPhieuSuaChua.SoLuong AS [Số lượng], TienCong.LoaiTienCong AS [Tiền công], CTPhieuSuaChua.ThanhTien AS [Thành tiền], CTPhieuSuaChua.MaCT AS [Mã CT] FROM dbo.TableCTPhieuSuaChua AS [CTPhieuSuaChua], dbo.TablePhuTung AS [PhuTung], dbo.TableTienCong AS [TienCong] WHERE CTPhieuSuaChua.MaPhuTung = PhuTung.MaPhuTung AND CTPhieuSuaChua.MaTienCong = TienCong.MaTienCong AND CTPhieuSuaChua.MaPhieuSuaChua =" + txbRepairFormID.Text;
            dtgvDetailRepairForm.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowDetailRepairFormByRepairFormIDToDataGridView);
            for (int i = 0; i <= dtgvDetailRepairForm.Columns.Count - 1; i++)
            {
                dtgvDetailRepairForm.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public void DeleteRepairFormIfNotHaveDetailRepairForm(TextBox txbRepairFormID)
        {
            try
            {
                string queryDeleteRepairFormIfNotHaveDetailRepairForm = "DELETE dbo.TablePhieuSuaChua WHERE MaPhieuSuaChua = " + txbRepairFormID.Text; /*WHERE NOT EXISTS( SELECT * FROM dbo.TableCTPhieuSuaChua WHERE MaPhieuSuaChua = " + txbRepairFormID.Text + " ) AND TablePhieuSuaChua.MaPhieuSuaChua = " + txbRepairFormID.Text;*/
                DataProviderDAO.Instance.ExecuteNonQuery(queryDeleteRepairFormIfNotHaveDetailRepairForm);
                MessageBox.Show("Đã xóa phiếu sửa chữa: " + txbRepairFormID.Text, "Thông báo", MessageBoxButtons.OK);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Phiếu sửa chữa: " + txbRepairFormID.Text + " đã có chi tiết phiếu sửa chữa", "Thông báo", MessageBoxButtons.OK);
            }

         }

 
    }
}
