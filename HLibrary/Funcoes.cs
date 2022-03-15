using System;
using System.Collections.Generic;
using System.IO;
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
            if (texto.Length > tam)
            {
                dados = texto.Substring(0, tam);
            }
            else
                dados = texto;
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
    }
}
