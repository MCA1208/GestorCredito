using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class SecurityService
    {
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
    }
}