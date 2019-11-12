using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PainelStudioPerboyre.Utils
{
    static class Navegacao_aux
    {
        public static INavigation Navigation { get; private set; }

        public static Page GetMainPage()
        {
            var rootPage =new Views.LoginPage();

            Navigation = rootPage.Navigation;
           // rootPage.BarBackgroundColor =Color.White;
            // rootPage.BarTextColor = Color.FromHex(AppColors.GRAY);

            return rootPage;
        }

        
    }
}
