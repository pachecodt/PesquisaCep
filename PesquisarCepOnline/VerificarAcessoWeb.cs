using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PesquisarCepOnline
{
    class VerificarAcessoWeb
    {
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        public static Boolean EstaConectado()
        {
            int descricao;
            return InternetGetConnectedState(out descricao, 0);
        }
    }
}
