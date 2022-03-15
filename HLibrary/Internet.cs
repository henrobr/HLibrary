using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace HLibrary
{
    public class Internet
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Criar função para utilizar a API
        public static bool VerificaConexaoInternet
        {
            get
            {
                int Desc;
                return InternetGetConnectedState(out Desc, 0);
            }

        }
    }
}
