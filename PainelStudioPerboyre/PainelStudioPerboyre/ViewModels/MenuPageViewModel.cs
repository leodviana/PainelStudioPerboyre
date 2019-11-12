using PainelStudioPerboyre.Helpers;
using PainelStudioPerboyre.Models;
using PainelStudioPerboyre.Services;
using PainelStudioPerboyre.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PainelStudioPerboyre.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        
        
        private List<Models.MenuItem> listaordenada;

        public DelegateCommand<Models.MenuItem> NavigateCommand
        {
            get
            {
                return new DelegateCommand<Models.MenuItem>((item) =>
                {
                    if (item.PageName == "ExamesPage")
                    {
                        if (App.usuariologado.tipo == "Administrador")
                        {
                           
                            NavigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");
                        }
                        else
                        {
                            var navigationParams = new NavigationParameters();
                            navigationParams.Add("paciente", App.usuariologado);
                            
                            NavigationService.NavigateAsync("/MasterPage/NavigationPage/" + item.PageName, navigationParams);
                        }
                    }
                    else if (item.PageName == "LogoutPage")
                    {
                        Preferences.Clear();
                        //Preferences.ClearData();
                       // Settings.ClearAllData();
                        App.usuariologado = null;
                        Page nova = Navegacao_aux.GetMainPage();
                        App.Current.MainPage = nova;
                        //_navigationService.NavigateAsync("LoginPage");
                    }
                    else if (item.PageName == "PerfilPage")
                    {
                        var navigationParams = new NavigationParameters();
                        navigationParams.Add("paciente", App.usuariologado);
                        NavigationService.NavigateAsync("/MasterPage/NavigationPage/" + item.PageName, navigationParams);
                    }
                    else
                    {
                        NavigationService.NavigateAsync("/MasterPage/NavigationPage/" + item.PageName);
                    }
                });

            }
        }
        public ObservableCollection<Models.MenuItem> Menus { get; set; }



        private void LoadMenu()
        {

            Menus = new ObservableCollection<Models.MenuItem>
            {
                new Models.MenuItem()
                {
                    Icon = "Dentes",
                    Title = "Exames",
                    PageName = "ExamesPage"
                },
                new Models.MenuItem()
                {
                    Icon = "perfil",
                    Title = "Perfil",
                    PageName = "PerfilPage"
                },
                new Models.MenuItem()
                {
                    Icon = "localizacao",
                    Title = "Localização",
                    PageName = "LocalizacaoPage"
                },
                new Models.MenuItem()
                {
                    Icon = "localizacao",
                    Title = "Logout",
                    PageName = "LogoutPage"
                }
            };



        }

        private string _photo;
        public string Photo
        {
            get { return _photo; }
            set
            {
                SetProperty(ref _photo, value);

            }
        }

        public MenuPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

            Photo = App.usuariologado.ImagePath;
            LoadMenu();

           
            // NavigateCommand = new DelegateCommand<MenuItem>(Navigate);
        }

     
    }
}
