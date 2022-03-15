using System;
using System.Collections.Generic;
using System.Text;

namespace HLibrary
{
    public class CpfCnpj
    {
        public static bool ValidaCpfCnpj(string txt)
        {
            bool tf = false;
            if (string.IsNullOrEmpty(txt))
                tf = false;
            if (txt.Length == 11)
            {
                tf = ValidaCpf(txt);
            }
            else if (txt.Length == 14)
            {
                tf = ValidaCnpj(txt);
            }

            return tf;
        }
        public static string FormatarCpfCnpj(string txt)
        {
            long n;
            txt = txt.Replace(".", "");
            txt = txt.Replace("-", "");
            txt = txt.Replace("/", "");

            if (txt.Length == 11)
            {
                n = Convert.ToInt64(txt);
                txt = string.Format(@"{0:000\.000\.000\-00}", n);
            }
            else if (txt.Length == 14)
            {
                n = Convert.ToInt64(txt);
                txt = string.Format(@"{0:00\.000\.000\/0000\-00}", n);
            }
            else
            {
                txt = "";
            }
            return txt;
        }
        public static bool ValidaCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
        public static bool ValidaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
        public static string LimpaCpfCnpj(string txt)
        {
            if(!string.IsNullOrEmpty(txt))
            {
                txt = txt.Replace(".", "");
                txt = txt.Replace("-", "");
                txt = txt.Replace("/", "");
            }         

            return txt;
        }
    }
}
