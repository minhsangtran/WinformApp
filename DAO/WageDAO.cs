using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.DAO
{
    class WageDAO
    {
        private static WageDAO instance;
        public static WageDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WageDAO();
                }
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        public void LoadAllWage(DataGridView dtgvAllWageInfo) 
        {
            string queryLoadAllWage = "SELECT MaTienCong AS [Mã tiền công], LoaiTienCong AS [Tên loại tiền công], SoTienCong AS [Trị giá] FROM dbo.TableTienCong";
            dtgvAllWageInfo.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryLoadAllWage);

           
            for (int i = 0; i <= dtgvAllWageInfo.Columns.Count - 1; i++)
            {
                
                dtgvAllWageInfo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        public void LoadWageByName (TextBox txbWageName,DataGridView dtgvWageInfor)
        {
            string queryLoadWageByName = "EXEC dbo.USP_SearchWageByWageName @wageName";
            dtgvWageInfor.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryLoadWageByName, new object[] {txbWageName.Text});
            for (int i = 0; i <= dtgvWageInfor.Columns.Count - 1; i++)
            {
                dtgvWageInfor.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public void LoadWageByValue(TextBox txbWageValue, DataGridView dtgvWageInfor)
        {
            string queryLoadWageByName = "EXEC dbo.USP_SearchWageByWageValue @wageValue";
            dtgvWageInfor.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryLoadWageByName, new object[] { txbWageValue.Text });
            for (int i = 0; i <= dtgvWageInfor.Columns.Count - 1; i++)
            {
                dtgvWageInfor.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public void AddNewWage(TextBox txbWageName, TextBox txbWageValue)
        {
            string queryAddNewWage = "EXEC dbo.USP_AddNewWage @wageName , @wageValue";
            try
            {
                DataProviderDAO.Instance.ExecuteNonQuery(queryAddNewWage, new object[] { txbWageName.Text, txbWageValue.Text });
                MessageBox.Show("Đã thêm loại tiền công mới: " + txbWageName.Text, "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Tên loại tiền công " + txbWageName.Text + " đã tồn tại", "Thông báo", MessageBoxButtons.OK);
            }

        }
        
        public void UpdateWage(string strWageID, TextBox txbWageName, TextBox txbWageValue)
        {
            string queryUpdateWage = "EXEC dbo.USP_UpdateWage @wageID , @wageName , @wageValue";
            try
            {
                DataProviderDAO.Instance.ExecuteNonQuery(queryUpdateWage, new object[] { strWageID, txbWageName.Text, txbWageValue.Text });
                MessageBox.Show("Đã cập nhật giá trị cho loại tiền công: " + txbWageName.Text, "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Tên loại tiền công " + txbWageName.Text + " đã tồn tại", "Thông báo", MessageBoxButtons.OK);
            }

        }

        public void DeleteWage(string strWageID, TextBox txbWageName)
        {
            string queryDeleteWage = "EXEC dbo.USP_DeleteWage @wageID";
            try
            {
                DataProviderDAO.Instance.ExecuteNonQuery(queryDeleteWage, new object[] { strWageID });
                MessageBox.Show("Đã xóa loại tiền công: " + txbWageName.Text, "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa do loại tiền công " + txbWageName.Text + " đã được sử dụng", "Thông báo", MessageBoxButtons.OK);
            }

        }

        public List<String> GetWageName()
        {
            string queryGetWageName = "SELECT LoaiTienCong FROM dbo.TableTienCong";

            return DataProviderDAO.Instance.ExecuteQuery(queryGetWageName).AsEnumerable().Select(r => r.Field<string>("LoaiTienCong")).ToList();
        }
    }
}
