using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class SecurityService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        /// Encripta una cadena
        public string Encryp(string _stringEncryp)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_stringEncryp);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string Decrypt(string _stringDecryp)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_stringDecryp);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        public DataTable GetAllPermitsByUserProgram(int userId )
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllPermitsApplication, con);
            comando.Parameters.AddWithValue("@idUser", userId);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
    }
}