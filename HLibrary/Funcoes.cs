using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HLibrary
{
    public class Funcoes
    {
        public static string GetDir  // Pegar diretorio do sistema
        {
            get
            {
                string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
                return curDir;
            }
        }
        public static string FormataCep(string cep)
        {
            int n;
            if (!string.IsNullOrEmpty(cep) || !string.IsNullOrWhiteSpace(cep))
            {
                cep = cep.Replace("-", "").Replace(".", "");
                if (cep.Length == 8)
                {
                    if (Int32.TryParse(cep, out n))
                    {
                        n = Convert.ToInt32(cep);
                        cep = string.Format(@"{0:00000-000}", n);
                    }
                }
            }
            return cep;
        }
        public static string NomeSobrenome(string nomeCompleto)
        {
            string nomeSobrenome;
            string[] partes = nomeCompleto.Split(' ');
            if (partes.Length > 1)
                nomeSobrenome = partes[0] + " " + partes[(partes.Length - 1)];
            else
                nomeSobrenome = partes[0];

            return nomeSobrenome;            
        }
        public static string CortaTexto(string texto, int tam) //Corta o tamanha do texto
        {
            string dados = "";
            if (!string.IsNullOrEmpty(texto))
            {
                if (texto.Length > tam)
                {
                    dados = texto.Substring(0, tam);
                }
                else
                    dados = texto;
            }
            
            return dados;
        }
        public static bool ValidarEmail(string email)
        {
            Regex regExpEmail = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
            Match match = regExpEmail.Match(email);
            return match.Success;
        }
        public static string RemoverEspacosEmBrancoExtras(string texto)
        {
            return Regex.Replace(texto, @"\s{2,}", " ");
        }
        public static string RemoverEspacosEmBranco(string texto)
        {
            return Regex.Replace(texto, @"\s", "");
        }
        public static string TrocarEspacoBrancoPor(string text, string trocar)
        {
            return Regex.Replace(text, @"\s", trocar);
        }
        public static string FormataContabil(int valor)
        {
            string nvalor = valor.ToString();
            nvalor = nvalor.PadLeft(10, '0');

            string convertido = nvalor.Substring(0, 1) + "." + nvalor.Substring(1, 2) + "." + nvalor.Substring(3, 3) + "." + nvalor.Substring(6, 4);

            return convertido;
        }
        public static string GerarCodigo(int nb = 1)
        {
            string numeroAleatorio = "";
            var random = new Random();
            var possibilidades = Enumerable.Range(0, nb).ToList();
            var resultado = possibilidades.OrderBy(number => random.Next()).Take(10).ToArray();
            return numeroAleatorio = String.Join("", resultado).PadLeft(nb, '0');
        }

        public static string RemoverCaracterEspecial(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û", "´" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U","'" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }

            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", "#", "$", "?", ".", "(", ")", "@", "!", "%", "&", "*", "º", "/" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }

            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por  " " **/
            str = str.Replace("\\s+", " ");

            return str;

        }
        public static string RemoverCaracterEspecialLink(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }

            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", "#", "$", "?", ".", "(", ")", "@", "!", "%", "&", "*", "º", "/" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }

            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por  " " **/
            str = str.Replace("\\s+", " ");

            return str;

        }
        public static string GerarNumCartao(string nmb)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador3 = new int[11] { 6, 5, 4, 3, 2, 7, 8, 9, 10, 11, 12 };
            string tempNmb;
            string digito;
            int soma;
            int resto;

            nmb = nmb.PadLeft(9, '0');

            tempNmb = nmb;

            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(nmb[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            nmb = tempNmb + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(nmb[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            nmb = tempNmb + digito;

            soma = 0;
            for (int i = 0; i < 11; i++)
                soma += int.Parse(nmb[i].ToString()) * multiplicador3[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            nmb = tempNmb + digito;

            return nmb;
        }
        public static bool ValidaNumCartao(string nmb)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador3 = new int[11] { 6, 5, 4, 3, 2, 7, 8, 9, 10, 11, 12 };
            string tempNmb;
            string digito;
            int soma;
            int resto;

            

            if (string.IsNullOrEmpty(nmb))
                return false;

            nmb = nmb.Trim();
            nmb = nmb.Replace(".", "").Replace("-", "").Replace(" ", "");


            if (nmb.Length != 12)
                return false;

            tempNmb = nmb.Substring(0, 9);

            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempNmb[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempNmb = tempNmb + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempNmb[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            tempNmb = tempNmb + resto.ToString();

            soma = 0;
            for (int i = 0; i < 11; i++)
                soma += int.Parse(tempNmb[i].ToString()) * multiplicador3[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return nmb.EndsWith(digito);
        }
        public static string FormataNumCartao(string nmb)
        {
            nmb = nmb.Trim();
            nmb = nmb.Replace(".", "").Replace("-", "").Replace(" ", "");

            return nmb.Substring(0, 4) + " " + nmb.Substring(4, 4) + " " + nmb.Substring(8, 4);
        }
    }
}
