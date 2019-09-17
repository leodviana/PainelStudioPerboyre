using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace PainelStudioPerboyre.Helpers
{
    public class conectionHelper
    {
        public static bool testaConexao()
        {
            bool conexao = false;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available
                conexao = true;
            }
           
            /*if (CrossConnectivity.Current.IsConnected)
            {
                conexao = true;
            }*/
            return conexao;
        }
    }
}
