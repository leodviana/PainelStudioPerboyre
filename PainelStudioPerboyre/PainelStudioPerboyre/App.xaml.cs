using DLToolkit.Forms.Controls;
using Newtonsoft.Json;
using PainelStudioPerboyre.Models;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using PainelStudioPerboyre.Services;
using PainelStudioPerboyre.ViewModels;
using PainelStudioPerboyre.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PainelStudioPerboyre
{
    public partial class App : PrismApplication
    {
        public static Dentista usuariologado { get; set; }
        public App()
            : this(null)
        {

        }

        public App(IPlatformInitializer initializer)
            : this(initializer, true)
        {

        }

        public App(IPlatformInitializer initializer, bool setFormsDependencyResolver)
            : base(initializer, setFormsDependencyResolver)
        {

        }


        protected override async void OnInitialized()
        {
            InitializeComponent();

            FlowListView.Init();
            // await NavigationService.NavigateAsync("NavigationPage/MainPage");
            //  await this.NavigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");
            // await this.NavigationService.NavigateAsync("/LoginPage");
            // await NavigationService.NavigateAsync("/NavigationPage/PermissaoPage"); nao da certo pois nao inicializa a master page 
            //await NavigationService.NavigateAsync("app:////NavigationPage/PermissaoPage");
            string usuario_logado = Preferences.Get("dentistaserializado","");
            App.usuariologado = JsonConvert.DeserializeObject<Dentista>(usuario_logado);

            if (App.usuariologado == null)
            {

                await this.NavigationService.NavigateAsync("LoginPage");
            }
            else
            {
                if (App.usuariologado.Id == 999999999)
                {
                    App.usuariologado.tipo = "Administrador";
                    await this.NavigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");
                }
                else
                {
                    App.usuariologado.tipo = "Dentista";
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("paciente", App.usuariologado);

                    // await _navigationService.NavigateAsync("ExamesPage", navigationParams);
                    // await this.NavigationService.NavigateAsync("/MasterPage/NavigationPage/ExamesPage", navigationParams);
                    await this.NavigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");

                    /*var navigationParams = new NavigationParameters();
                     navigationParams.Add("usuario", App.usuariologado);
                     helper.Settings.Default.usuarioLogado = App.usuariologado;
                     await _navigationService.NavigateAsync("MainPage", navigationParams);
                     //MainPage = new MainPage();*/
                }
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            /*containerRegistry.RegisterForNavigation<MasterPage>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<ExamesPage>();
            containerRegistry.RegisterForNavigation<PerfilPage>();
            containerRegistry.RegisterForNavigation<LocalizacaoPage>();

            containerRegistry.RegisterForNavigation<PacientesPage>();
            containerRegistry.RegisterForNavigation<Imagens>();
            containerRegistry.RegisterForNavigation<ImagensDetalhes>();
            containerRegistry.RegisterForNavigation<ImagensDetalhes2>();
            containerRegistry.RegisterForNavigation<DentistaPage>();*/
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            /* containerRegistry.RegisterForNavigation<PermissaoPage, PermissaoPageViewModel>();
             containerRegistry.RegisterForNavigation<RecuperarPage, RecuperarPageViewModel>();
             containerRegistry.RegisterForNavigation<LoginPacientePage, LoginPacientePageViewModel>();
             containerRegistry.RegisterForNavigation<ExamesTab, ExamesTabViewModel>();
             containerRegistry.RegisterForNavigation<PdfPage, PdfPageViewModel>();
             containerRegistry.RegisterForNavigation<ExibePdfPage, ExibePdfPageViewModel>();
             */

            containerRegistry.RegisterSingleton<IApiService, ApiService>();
        }
    }
}
