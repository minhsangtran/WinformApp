using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.DAO
{
    class CarsDAO
    {
        private static CarsDAO instance;

        public static CarsDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CarsDAO();
                }
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        public List<String> GetCarTypes()
        {
            string queryCarTypes = "SELECT TenHieuXe FROM dbo.TableHieuXe";

            return DataProviderDAO.Instance.ExecuteQuery(queryCarTypes).AsEnumerable().Select(r => r.Field<string>("TenHieuXe")).ToList();
        }

        public void GetCarsInfo(string BienSoXe, DataGridView dtgvCarInfor, ComboBox cbxCarType)
        {
            // Get car information to dtgvCarInfor and enable cbxCarType if it is a new car. 
            string query = "EXEC dbo.USP_SearchCarByCarNumber @carNumber";
            dtgvCarInfor.DataSource = DataProviderDAO.Instance.ExecuteQuery(query, new object[] { BienSoXe });
            for (int i = 0; i <= dtgvCarInfor.Columns.Count - 1; i++)
            {
                dtgvCarInfor.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            if (dtgvCarInfor.Rows.Count == 0)
            {
                cbxCarType.Enabled = true;
            }
            else if (dtgvCarInfor.Rows.Count == 1)
            {
                //cbxCarType.Text = dtgvCarInfor.Rows[0].Cells["TenHieuXe"].FormattedValue.ToString();
            }
        }
        public void GetCustomerInforByCarInfor(string CarNumber, DataGridView dtgvCustomerInfor)
        {
            string queryGetCustomerInforByCarNumber = "EXEC dbo.USP_SearchCustomerByCarNumber @carNumber";
            dtgvCustomerInfor.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryGetCustomerInforByCarNumber, new object[] {CarNumber });
        }

        public void AddNewCar(string CarNumber, string CarOwner, string CarType)
        {
            try
            {
                if (DataProviderDAO.Instance.ExecuteScalar("SELECT * FROM dbo.TableHieuXe WHERE TenHieuXe = N'" + CarType +"'") == null)
                {
                    DataProviderDAO.Instance.ExecuteNonQuery("INSERT dbo.TableHieuXe ( TenHieuXe, XuatXu ) VALUES ( N'" + CarType + "', N'None' )");
                    MessageBox.Show("Đã thêm hiệu xe mới: " + CarType, "Thông báo", MessageBoxButtons.OK);
                }
                string queryGetCarTypeID = "SELECT * FROM dbo.TableHieuXe AS HieuXe WHERE HieuXe.TenHieuXe = N'" + CarType + "'";
                string CarTypeID = DataProviderDAO.Instance.ExecuteScalar(queryGetCarTypeID).ToString();

                string queryAddNewCar = "EXEC dbo.USP_AddNewCar @carNumber , @carOwner , @carType";
                DataProviderDAO.Instance.ExecuteQuery(queryAddNewCar, new object[] { CarNumber, CarOwner, CarTypeID });
            }
            catch (Exception)
            {
            }

        }
        public void ViewAllCarList(DataGridView dtgvCarCustom_CarList)
        {
            string queryViewAllCarList = "SELECT BienSoXe AS [Biển số xe], MaHieuXe AS [Hiệu Xe], ChuXe as [Chủ xe] FROM dbo.TableXe";
            dtgvCarCustom_CarList.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryViewAllCarList, new object[] { });
        }

        public void SearchCarBrandByName(TextBox txbCarBrandName, DataGridView dtgvCarBrandList)
        {
            string querySearchCarBrandByName = "EXEC dbo.USP_CarBrandSearchByName @CarBrandName";
            dtgvCarBrandList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchCarBrandByName, new object[] { txbCarBrandName.Text });
        }

        public void SearchCar(TextBox txbCarID, TextBox txbCarOwnerName, TextBox txbCarOwnerPhoneNumber, DataGridView dtgvCarList)
        {
            string querySearchAccessaryImportByName = "EXEC dbo.USP_CarSearch @CarID , @CustomerName , @CustomerPhoneNumber";
            dtgvCarList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchAccessaryImportByName, new object[] { txbCarID.Text, txbCarOwnerName.Text, txbCarOwnerPhoneNumber.Text });
        }


        public void InsertCarBrandByName(TextBox txbCarBrandName, DataGridView dtgvCarBrandList)
        {
            string querySearchCarBrandByName = "EXEC dbo.USP_InsertCarBrand @CarBrandName";
            dtgvCarBrandList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchCarBrandByName, new object[] { txbCarBrandName.Text });
            MessageBox.Show("Đã thêm hiệu xe: " + txbCarBrandName.Text, "Thông báo", MessageBoxButtons.OK);
            txbCarBrandName.Clear();
        }
    }


}
