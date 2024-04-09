using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace SIB.Web.Models.Auxiliares
{
    public static class FormataValidaPlaca
    {       
        public static bool validaPlaca(string placa)
        {
            string strPlaca = placa;
            bool eValido = true;

            if (strPlaca.Length != 7 && strPlaca.Length != 8)
            {
                return false;
            }
            else
            {
                //Valida placa
                
                string[] array = new string[7];

                int numero;
                string verifica = "^[0-9]";

                if (strPlaca.Length == 8)
                {
                    string traco = strPlaca.Substring(3, 1);

                    if (!(traco == "-"))
                    {
                        return false;
                    }                    
                }

                strPlaca = strPlaca.Replace("-", "");                    

                for (int i = 0; i < strPlaca.Length; i++)
                {
                    array[i] = strPlaca.Substring(i, 1);
                }

                if (Regex.IsMatch(array[0], verifica) || Regex.IsMatch(array[1], verifica) ||
                    Regex.IsMatch(array[2], verifica) || !int.TryParse(array[3], out numero) ||
                    !int.TryParse(array[4], out numero) || !int.TryParse(array[5], out numero) ||
                    !int.TryParse(array[6], out numero)
                    )
                {
                    return false;
                }
                
                //Válida placa
            }

            return eValido;
        }

        public static string formataPlaca(string strPlaca)
        {
            return strPlaca.Replace("-", "").ToUpper();
        }

    }
}