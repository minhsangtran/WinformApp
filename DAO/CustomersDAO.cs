using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.DAO
{
    class CustomersDAO
    {
        private static CustomersDAO instance;

        public static CustomersDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CustomersDAO();
                }
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        public void AutoFormatDataGridView(DataGridView dtgvInput)
        {
            for (int i = 0; i <= dtgvInput.Columns.Count - 1; i++)
            {
                dtgvInput.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }
        public void GetCustomersInfo(TextBox txbCustomerName, DataGridView dtgvCustomerInfor)
        {
            string query = "EXEC dbo.USP_SearchCustomerByName @customerName";
            dtgvCustomerInfor.DataSource = DataProviderDAO.Instance.ExecuteQuery(query, new object[] { txbCustomerName.Text });
            AutoFormatDataGridView(dtgvCustomerInfor);

        }

        public void GetCustomersInfoToTextBox(TextBox txbCustomerName, TextBox txbCustomerPhone, TextBox txbCustomerEmail)
        {
            string queryGetEmailByName = "SELECT Email FROM dbo.TableKhachHang WHERE TenKhachHang = N'" + txbCustomerName.Text + "'";
            string queryGetPhoneByName = "SELECT SoDienThoai FROM dbo.TableKhachHang WHERE TenKhachHang = N'" + txbCustomerName.Text + "'";
            try
            {
                txbCustomerEmail.Text = DataProviderDAO.Instance.ExecuteScalar(queryGetEmailByName).ToString();
                txbCustomerPhone.Text = DataProviderDAO.Instance.ExecuteScalar(queryGetPhoneByName).ToString();
            }
            catch (Exception)
            {

           
            }
           
           
        }


        public void MakeListCarByCustomerName(TextBox txbCustomerName, DataGridView dtgvCarInfor)
        {
            string queryGetListCarsByCustomerName = "EXEC dbo.USP_SearchCarListByCustomerName @customerName";
            dtgvCarInfor.DataSource = DataProviderDAO.Instance.ExecuteQuery(queryGetListCarsByCustomerName, new object[] { txbCustomerName.Text });
            AutoFormatDataGridView(dtgvCarInfor);
        }

        public void AddNewCustomer(TextBox txbCustomerName, TextBox txbCustomerBirthday, TextBox txbCustomerAddress, TextBox txbCustomerPhone, TextBox txbCustomerEmail)
        {
            string queryAddNewCustomer = "EXEC dbo.USP_AddNewCustomer @customerName , @customerBirthDay , @customerAddress , @customerPhone , @customerEmail";
            DataProviderDAO.Instance.ExecuteNonQuery(queryAddNewCustomer, new object[] { txbCustomerName.Text, txbCustomerBirthday.Text, txbCustomerAddress.Text, txbCustomerPhone.Text, txbCustomerEmail.Text });
        }

        public void ViewAllCustomerList(DataGridView dtgvCustomerList)
        {
            string querySearchCar = "SELECT TenKhachHang AS [Tên khách hàng],NamSinh AS [Năm sinh],DiaChi AS [Địa chỉ],SoDienThoai AS [SĐT],Email AS [Email], TienNo as [Tiền nợ]  FROM dbo.TableKhachHang";
            dtgvCustomerList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchCar, new object[] { });
        }

        public void SearchCustomer(TextBox txbCarID, TextBox txbCarOwnerName, TextBox txbCarOwnerPhoneNumber, DataGridView dtgvCustomerList)
        {
            string querySearchCar = "EXEC dbo.USP_CustomerSearch @CarID , @CustomerName , @CustomerPhoneNumber";
            try
            {
                dtgvCustomerList.DataSource = DataProviderDAO.Instance.ExecuteQuery(querySearchCar, new object[] { });
            }
            catch (Exception)
            {

            }
            
        }
    }
}
