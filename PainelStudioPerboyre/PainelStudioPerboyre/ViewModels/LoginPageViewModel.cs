﻿using Newtonsoft.Json;
using PainelStudioPerboyre.Models;
using PainelStudioPerboyre.Services;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PainelStudioPerboyre.ViewModels
{

    public class LoginPageViewModel : ViewModelBase
    {
        private ICommand _navegar;
        private ICommand _ForgotPasswordCommand;

        IApiService apiService;

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                SetProperty(ref isRunning, value);

            }
        }


        private bool _mostra_mensagem;

        public bool mostra_mensagem
        {
            get { return _mostra_mensagem; }
            set
            {
                SetProperty(ref _mostra_mensagem, value);

            }
        }

        private string _mensagem;
        public string mensagem
        {
            get { return _mensagem; }
            set
            {
                SetProperty(ref _mensagem, value);

            }
        }

        private string _usuarioid;
        public string Usuarioid
        {
            get { return _usuarioid; }
            set
            {
                SetProperty(ref _usuarioid, value);

            }
        }



        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set
            {
                SetProperty(ref _senha, value);

            }
        }

        // (INavigationService navigationService,
        //       IPageDialogService pageDialogService, IMarvelApiService marvelApiService) : base(navigationService, pageDialogService)

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
            // _navigationService = navigationService;
            // _dialogService = dialogService;
            apiService = ApiService;
            mostra_mensagem = false;
            mensagem = "";
        }

        public ICommand navegar
        {
            get
            {
                return _navegar ?? (_navegar = new Command(objeto =>
                {
                    //_navigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");
                    Login();
                    // _navigationService.NavigateAsync("PermissaoPage");
                }));
            }
        }

        public ICommand ForgotPasswordCommand
        {
            get
            {
                return _ForgotPasswordCommand ?? (_ForgotPasswordCommand = new Command(objeto =>
                {
                    //_navigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");
                    // navigationService.NavigateAsync
                    NavigationService.NavigateAsync("LoginPacientePage");
                }));
            }
        }

        private async void Login()
        {
            var testa = await ChecapermisaoService.checa_permissao();
            if (string.IsNullOrEmpty(Usuarioid))
            {
                await PageDialogService.DisplayAlertAsync("Erro", "Prencha o campo Email!", "OK");
                mostra_mensagem = true;
                mensagem = "Prencha o campo Email!";
                // await dialogServices.ShowMessage("Erro", "Prencha o campo Usuário!");
                return;
            }


            if (string.IsNullOrEmpty(Senha))
            {
                await PageDialogService.DisplayAlertAsync("Erro", "Prencha o campo Senha!", "OK");
                return;
            }

            var response = new Response();
            IsRunning = true;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)

            {
                response = await apiService.Login(Usuarioid, Senha);
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("Erro", "Dispositivo sem Conexâo", "OK");
                //await dialogServices.ShowMessage("Erro", response.Message);
                IsRunning = false;
                return;
            }
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await PageDialogService.DisplayAlertAsync("Erro", response.Message, "OK");
                //await dialogServices.ShowMessage("Erro", response.Message);
                return;
            }

            var User = (Dentista)response.Result;

            if (User.Id == 999999999)
            {
                User.tipo = "Administrador";
                App.usuariologado = User;

                //Settings.Grava_Settings(JsonConvert.SerializeObject(User));
                Preferences.Set("dentistaserializado", JsonConvert.SerializeObject(User));
                //Preferences.Get("dentistaserializado", JsonConvert.SerializeObject(User));
                await NavigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");
            }
            else
            {
                User.tipo = "Dentista";
                App.usuariologado = User;
                // Settings.Grava_Settings(JsonConvert.SerializeObject(User));
                Preferences.Set("dentistaserializado", JsonConvert.SerializeObject(User));
                //Preferences.Get("dentistaserializado", JsonConvert.SerializeObject(User));
                var navigationParams = new NavigationParameters();
                navigationParams.Add("paciente", User);
                await NavigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");
                //  await _navigationService.NavigateAsync("/MasterPage/NavigationPage/ExamesPage", navigationParams);
            }
            //grava usuario


            //

            //await _navigationService.NavigateAsync("MainPage");
            //navigationServices.SetMainPage(User);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}
