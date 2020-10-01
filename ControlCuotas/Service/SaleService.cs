using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Schema;

namespace ControlCuotas.Service
{
    public class SaleService
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable AddSale(DateTime saleDate, int idClient, int idVendor, string subTotalSale, string totalSale, int quotaSale, int? interest,
            int? discount, DateTime? dateEnd, string productString, string userName , int userId)
        {
            int idSale = 0 ;
            con = new SqlConnection(Connection.stringConn);
            SqlTransaction objTrans;
            con.Open();
            objTrans = con.BeginTransaction();
            try
            {

                comando = new SqlCommand(spName.spAddSale, con);
                comando.Parameters.AddWithValue("@saleDate", saleDate);
                comando.Parameters.AddWithValue("@idClient", idClient);
                comando.Parameters.AddWithValue("@idVendor", idVendor);
                comando.Parameters.AddWithValue("@subTotal", subTotalSale);
                comando.Parameters.AddWithValue("@totalSale", totalSale);
                comando.Parameters.AddWithValue("@quota", quotaSale);
                comando.Parameters.AddWithValue("@interest", interest);
                comando.Parameters.AddWithValue("@discount", discount);
                comando.Parameters.AddWithValue("@dateEnd", dateEnd);
                comando.Parameters.AddWithValue("@userName", userName);
                comando.Parameters.AddWithValue("@userId", userId);
                comando.CommandType = CommandType.StoredProcedure;

                objTrans.Commit();
                SqlDataAdapter da1 = new SqlDataAdapter(comando);
                da1.Fill(dt);
                idSale = Convert.ToInt32(dt.Rows[0][0]);
                var lenIni = productString.Length;
                string[] splitString = productString.Substring(0,lenIni-1).Split(',').ToArray();

                foreach (var product in splitString)
                {
                    var idPro = product.Split('-')[0];
                    var quantity = product.Split('-')[1];
                    var salePrice = product.Split('-')[2];
                    var subAmount = product.Split('-')[3];

                    comando = new SqlCommand(spName.spAddSaleDetail, con);
                    comando.Parameters.AddWithValue("@idSale", idSale);
                    comando.Parameters.AddWithValue("@idProduct", idPro);
                    comando.Parameters.AddWithValue("@quantity", quantity);
                    comando.Parameters.AddWithValue("@salePrice", salePrice);
                    comando.Parameters.AddWithValue("@subAmount", subAmount);
                    comando.Parameters.AddWithValue("@userName", userName);
                    comando.Parameters.AddWithValue("@userId", userId);
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da2 = new SqlDataAdapter(comando);
                    da2.Fill(dt);
                }
                double quoteCal = Math.Round((double.Parse(totalSale) / quotaSale),2);
                var calculo = quotaSale;
                for (var item = 1; item <= quotaSale; item++) 
                {
                    comando = new SqlCommand(spName.spAddSaleQuota, con);
                    comando.Parameters.AddWithValue("@idSale", idSale);
                    comando.Parameters.AddWithValue("@quotaNumber", item);
                    comando.Parameters.AddWithValue("@totalQuota", quoteCal);
                    comando.Parameters.AddWithValue("@userName", userName);
                    comando.Parameters.AddWithValue("@userId", userId);
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da3 = new SqlDataAdapter(comando);
                    da3.Fill(dt2);

                }               
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                if (idSale != 0)
                {
                    comando = new SqlCommand(spName.spAddSaleError, con);
                    comando.Parameters.AddWithValue("@idSale", idSale);
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da3 = new SqlDataAdapter(comando);
                    da3.Fill(dt2);
                }

            }
            finally
            {
                con.Close();
            }



            return dt2;
        }
    }
}