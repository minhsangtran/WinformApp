using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }

            private set
            {
                instance = value;
            }
        }
        public bool Login(string userName, string passWord)
        {
            string query = "EXEC dbo.USP_Login @userName , @passWord";
            DataTable data = DataProviderDAO.Instance.ExecuteQuery(query, new object[] {userName,passWord});
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
