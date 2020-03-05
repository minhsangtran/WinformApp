using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGaraOto.DAO
{
    class SaleNotifyDAO
    {
        private static SaleNotifyDAO instance;
        public static SaleNotifyDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaleNotifyDAO();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        

        public DataTable getSaleReport (DateTimePicker dtSelectSalesMonth, DateTimePicker dtSelectSalesYear)
        {
            string queryViewSalesInfo = "EXEC dbo.USP_CreateSaleReport @month , @year";
            return DataProviderDAO.Instance.ExecuteQuery(queryViewSalesInfo, new object[] { dtSelectSalesMonth.Text, dtSelectSalesYear.Text });
        }

       
    }
}
