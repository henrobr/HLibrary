using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HLibrary
{
    public class Calendario
    {
        public static DateTime DtMinima { get; set; } = Convert.ToDateTime("1957-01-01 00:00:00");
        public static bool ValidaData(DateTime dt)
        {
            DateTime resultado = DtMinima;
            if ((dt.Year >= DtMinima.Year && dt.Year <= 2100) && DateTime.TryParse(dt.ToString(), out resultado))
                return true;
            return false;
        }
        public static DateTime DateTimeBrasil
        {
            get
            {
                DateTime dateTime = DateTime.UtcNow;
                TimeZoneInfo horaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(dateTime, horaBrasilia);
            }
        }
        public static DateTime UltimoDiaMes(DateTime data)
        {
            DateTime primeiroDia = Convert.ToDateTime(data.ToString("yyyy-MM-01"));

            DateTime ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
            return ultimoDia;
        }
        // dias entre datas
        public static int DiasEntreDatas(DateTime dataInicio, DateTime dataFim)
        {
            //TimeSpan diferenca = DateTime.Now.Date - Convert.ToDateTime("15/06/1998");
            int dias = 0;
            dias = (DateTime.Parse(dataFim.ToShortDateString()).Subtract(DateTime.Parse(dataInicio.ToShortDateString()))).Days;
            return dias;
        }
        //Retorna a data por extenso
        public static string DataExtenso(DateTime data, bool S = false)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            int dia = data.Day;
            int ano = data.Year;
            string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(data.Month));
            string dataExtenso = dia + " de " + mes + " de " + ano;
            string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(data.DayOfWeek));
            if (S)
                dataExtenso = diasemana + ", " + dia + " de " + mes + " de " + ano;

            return dataExtenso;
        }
        //Retorna Mes Por extenso
        public static string NomeMes(int nMes, int abreviado = 0)//Para abreviado digitar 1
        {
            string[] mesN = { "", "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
            string[] mesA = { "", "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
            return (abreviado == 1 ? mesA[nMes] : mesN[nMes]);
        }
        public static int DiasNoMes(DateTime dt)
        {
            DateTime primeiroDia = Convert.ToDateTime("01/" + dt.Month + "/" + dt.Year);

            DateTime dias = primeiroDia.AddMonths(1).AddDays(-1);

            return dias.Day;
        }

        public static int CalculaIdade(DateTime Dtn)
        {
            var birthdate = new DateTime(Dtn.Year, Dtn.Month, Dtn.Day);
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) 
                age--;

            return Convert.ToInt32(age);
        }
    }
}
