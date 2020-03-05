using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.DAO
{
    class AccessaryDAO
    {
        private static AccessaryDAO instance;

        internal static AccessaryDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AccessaryDAO();
                }
                return instance;
            }

            private set
            {
                instance = value;
            }

        }

        /// Show Table
        public void ShowAccessaryTable(DataGridView dtgvAccessaryList)
        {
            string queryShowAccessaryTable = "SELECT MaPhuTung AS [Mã phụ tùng] , TenPhuTung AS [Tên phụ tùng] , DonGia AS [Đơn giá] , SoLuong AS [Số lượng] FROM dbo.TablePhuTung";
            dtgvAccessaryList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowAccessaryTable);
        }

        public void UpdateInventoryReportEachMonth()
        {
            string queryUpdateInventoryReportEachMonth = "EXEC dbo.USP_UpdateInventoryReportEachMonth";
            DataProviderDAO.Instance.ExecuteQuery(queryUpdateInventoryReportEachMonth);
        }

        public void ShowAccessaryImportTable(DataGridView dtgvAccessaryImportList)
        {
            string queryShowAccessaryImportTable = "SELECT	pps.MaPhieuPhatSinh AS [Mã phiếu] ,pps.MaPhuTung AS [Mã phụ tùng],pt.TenPhuTung AS [Tên phụ tùng] ,pps.DonGia AS [Đơn giá] ,pps.SoLuong AS [Số lượng], DAY(pps.NgayNhap)	AS	[Ngày], MONTH(pps.NgayNhap)	AS	[Tháng],YEAR(pps.NgayNhap)	AS	[Năm]FROM	dbo.TablePhieuPhatSinh pps INNER JOIN dbo.TablePhuTung pt ON pt.MaPhuTung = pps.MaPhuTung";
            dtgvAccessaryImportList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowAccessaryImportTable);
        }

        public void ShowInventoryReportTable(DataGridView dtgvAccessaryReport)
        {
            string queryShowInventoryReportTable = "SELECT bct.Thang AS [Tháng],bct.Nam AS [Năm],pt.TenPhuTung AS [Tên phụ tùng],ctbct.TonDau AS [Tồn đầu],ctbct.PhatSinh AS [Phát sinh],ctbct.TonCuoi AS [Tồn cuối]FROM	dbo.TableCTBaoCaoTon ctbct INNER JOIN (dbo.TableBaoCaoTon bct INNER JOIN dbo.TablePhuTung pt ON pt.MaPhuTung = bct.MaPhuTung)ON ctbct.MaBaoCaoTon = bct.MaBaoCaoTon";
            dtgvAccessaryReport.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryShowInventoryReportTable);
        }



        public void SearchAccessaryByName(TextBox txbAccessaryName, DataGridView dtgvAccessaryList)
        {
            string querySearchAccessaryByName = "EXEC dbo.USP_SearchAccessaryByName @accessaryName";
            dtgvAccessaryList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchAccessaryByName, new object[] { txbAccessaryName.Text });

        }

        public void SearchAccessaryImportByID(string txbAccessaryID, DataGridView dtgvAccessaryImportList)
        {
            string querySearchAccessaryImportByID = "EXEC dbo.USP_SearchAccessaryImportByID @accessaryID";
            dtgvAccessaryImportList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchAccessaryImportByID, new object[] { txbAccessaryID });
        }

        public void SearchAccessaryImportByName(TextBox txbAccessaryName, DataGridView dtgvAccessaryImportList)
        {
            string querySearchAccessaryImportByName = "EXEC dbo.USP_SearchAccessaryImportByName @accessaryName";
            dtgvAccessaryImportList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchAccessaryImportByName, new object[] { txbAccessaryName.Text });
        }

        public void SearchAccessaryIDByName(TextBox txbAccessaryName, DataGridView dtgvAccessaryList)
        {
            string querySearchAccessaryIDByName = "SELECT MaPhuTung AS [Mã phụ tùng] FROM dbo.TablePhuTung WHERE TenPhuTung = @txbAccessaryName";
            dtgvAccessaryList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchAccessaryIDByName, new object[] { txbAccessaryName.Text });
        }

        public void SearchAccessaryImportByDate(TextBox txbAccessaryDay, TextBox txbAccessaryMonth, TextBox txbAccessaryYear, DataGridView dtgvAccessaryImportList)
        {
            string querySearchAccessaryImportByDate = "EXEC dbo.USP_SearchAccessaryImportByDate @accessaryDay , @accessaryMonth ,  @accessaryYear";
            dtgvAccessaryImportList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchAccessaryImportByDate, new object[] { txbAccessaryDay.Text , txbAccessaryMonth.Text, txbAccessaryYear.Text});
        }

        public void InsertAccessaryImportForm(string txbAccessaryID, TextBox txbAccessaryNumberGet, TextBox txbAccessaryPrice, DataGridView dtgvAccessaryInfor)
        {
            //string queryUpdateInventoryReportBySupply = "EXEC dbo.USP_UpdateInventoryReportBySupply @accessaryID , @accessaryNumberGet";
            //DataProviderDAO.Instance.ExecuteQuery(queryUpdateInventoryReportBySupply, new object[] { txbAccessaryID, txbAccessaryNumberGet.Text});
            //dtgvAccessaryReport.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryUpdateInventoryReportBySupply, new object[] { txbAccessaryID, txbAccessaryNumberGet.Text});
            string queryInsertAccessaryImportForm = "EXEC dbo.USP_InsertAccessaryImportForm @accessaryID , @accessaryNumberGet , @accessaryPrice";
            dtgvAccessaryInfor.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryInsertAccessaryImportForm, new object[] { txbAccessaryID, txbAccessaryNumberGet.Text, txbAccessaryPrice.Text });
            MessageBox.Show("Đã thêm phiếu phát sinh", "Thông báo", MessageBoxButtons.OK);
        }

        public void UpdateInventoryReportBySupply(string txbAccessaryID, string txbAccessaryNumberGet, DataGridView dtgvAccessaryReport)
        {
            string queryUpdateInventoryReportBySupply = "EXEC dbo.USP_UpdateInventoryReportBySupply @accessaryID , @accessaryNumberGet";
            dtgvAccessaryReport.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryUpdateInventoryReportBySupply, new object[] { txbAccessaryID, txbAccessaryNumberGet });
        }

        public void UpdateAccessary(TextBox txbAccessaryName, TextBox txbAccessaryNumberInStock, TextBox txbAcessaryPrice, TextBox txbAccessaryNumberGet, DataGridView dtgvAccessaryList)
        {
            string queryUpdateAccessary = "EXEC dbo.USP_UpdateAccessary  @accessaryName , @accessaryNumberInStock , @accessaryPrice , @accessaryNumberGet ";
            dtgvAccessaryList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryUpdateAccessary, new object[] {  txbAccessaryName.Text, txbAccessaryNumberInStock.Text, txbAcessaryPrice.Text, txbAccessaryNumberGet.Text });
            //MessageBox.Show("Đã cập nhật giá trị cho phụ tùng: " + txbAccessaryName.Text, "Thông báo", MessageBoxButtons.OK);
        }

        public void InsertAccessary(TextBox txbAccessaryName, TextBox txbAccessaryPrice, TextBox txbAccessaryNumberGet, DataGridView dtgvAccessaryList)
        {
            string queryInsertAccessary = "EXEC dbo.USP_InsertAccessary @accessaryName , @accessaryPrice , @accessaryNumberGet";
            dtgvAccessaryList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryInsertAccessary, new object[] { txbAccessaryName.Text, txbAccessaryPrice.Text, txbAccessaryNumberGet.Text });
            MessageBox.Show("Đã thêm phụ tùng: " + txbAccessaryName.Text, "Thông báo", MessageBoxButtons.OK);
        }


        public void EditAccessary(string txbAccessaryID, TextBox txbAccessaryName, TextBox txbAccessaryNumberInStock, TextBox txbAcessaryPrice, DataGridView dtgvAccessaryInfor)
        {
            string queryEditAccessary = "EXEC dbo.USP_EditAccessary @accessaryID , @accessaryName , @accessaryNumberInStock , @accessaryPrice";
            dtgvAccessaryInfor.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryEditAccessary, new object[] { txbAccessaryID, txbAccessaryName.Text, txbAccessaryNumberInStock.Text, txbAcessaryPrice.Text });
            MessageBox.Show("Đã cập nhật giá trị cho phụ tùng: " + txbAccessaryName.Text, "Thông báo", MessageBoxButtons.OK);
        }

        //public string CalculateTotalPrize (TextBox txbAccessaryNumberGet, TextBox txbAccessaryPrice)
        //{
        //    if (txbAccessaryNumberGet.Text != "" && txbAccessaryPrice.Text != "")
        //    {
        //        return (Int32.Parse(txbAccessaryNumberGet.Text) * Int32.Parse(txbAccessaryPrice.Text)).ToString();

        //    }
        //    return "";

        //}

        public void DeleteAccessary(string txbAccessaryID, DataGridView dtgvAccessaryList)
        {
            string queryDeleteAccessary = "EXEC dbo.USP_DeleteAccessary @accessaryID";
            dtgvAccessaryList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryDeleteAccessary, new object[] { txbAccessaryID });
            MessageBox.Show("Đã xóa phụ tùng: " + txbAccessaryID, "Thông báo", MessageBoxButtons.OK);
        }

        public void DeleteAccessaryImport(string txbAccessaryImportID, string txbAccessarID, string txbAccessaryNumberGet,  DataGridView dtgvAccessaryImportList)
        {
            string queryDeleteAccessaryImport = "dbo.USP_DeleteAccessaryImportForm @accessaryImportID , @accessaryID , @accessaryNumberGet";
            dtgvAccessaryImportList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryDeleteAccessaryImport, new object[] { txbAccessaryImportID, txbAccessarID , txbAccessaryNumberGet });
            MessageBox.Show("Đã xóa phiếu nhập phụ tùng: " + txbAccessaryImportID, "Thông báo", MessageBoxButtons.OK);
        }

        public void SearchAccessaryByID(string txbAccessaryID, DataGridView dtgvAccessaryList)
        {
            string querySearchAccessaryByID = "EXEC dbo.USP_SearchAccessaryByID @accessaryID";
            dtgvAccessaryList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchAccessaryByID, new object[] { txbAccessaryID });
        }


        public void AutoDeleteEmptyRecord()
        {
            string queryAutoDeleteEmptyRecord = "EXEC USP_AutoDeleteEmptyRecord";
            DataProviderDAO.Instance.ExecuteQuery(queryAutoDeleteEmptyRecord);
        }

        public void SearchInventoryReport(NumericUpDown nudInventoryReportMonth, NumericUpDown nudInventoryReportYear, DataGridView dtgvAccessaryReport)
        {
            string querySearchInventoryReport = "EXEC dbo.USP_SearchInventoryReportByDate @accessaryMonth , @accessaryYear";
            dtgvAccessaryReport.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchInventoryReport, new object[] { nudInventoryReportMonth.Value.ToString() , nudInventoryReportYear.Value.ToString() });
        }

        public List<String> GetAccessaryName()
        {
            string queryGetAccessaryName = "SELECT TenPhuTung FROM dbo.TablePhuTung";

            return DataProviderDAO.Instance.ExecuteQuery(queryGetAccessaryName).AsEnumerable().Select(r => r.Field<string>("TenPhuTung")).ToList();
        }

        public string GetAccessaryAmountBySeletectingAccessaryName(ComboBox cbxRepairTabAccessaryName)
        {
            string queryGetAccessaryAmountBySeletectingAccessaryName = "SELECT SoLuong FROM dbo.TablePhuTung WHERE TenPhuTung = N'" + cbxRepairTabAccessaryName.Text + "'";
            return DataProviderDAO.Instance.ExecuteScalar(queryGetAccessaryAmountBySeletectingAccessaryName).ToString();
        }
    }
}
