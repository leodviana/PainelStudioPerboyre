﻿using Acr.UserDialogs;
using PainelStudioPerboyre.Helpers;
using PainelStudioPerboyre.Interface;
using PainelStudioPerboyre.Models;

using PainelStudioPerboyre.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PainelStudioPerboyre.ViewModels
{
    public class ImagensViewModel : ViewModelBase, IInitialize
    {
        public paciente _paciente;

        private bool isRunning;

        public DelegateCommand zoomCommand { get; set; }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                SetProperty(ref isRunning, value);

            }
        }
        private bool _isVisible;

        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                SetProperty(ref _isVisible, value);

            }
        }
        private ICommand _selecionarItem;
        private ICommand _selecionarItem2;
        private ICommand _selecionarItem3;
        private ICommand _compartilhar;
        //public List<ArqImagens> Lista;

        public string _titulo;
        public string titulo

        {
            get { return _titulo; }
            set
            {
                SetProperty(ref _titulo, value);

            }
        }

        private bool mostra_label;

        public bool Mostra_label
        {
            get { return mostra_label; }
            set
            {
                SetProperty(ref mostra_label, value);

            }
        }
        private bool mostra_listview;

        public bool Mostra_listview
        {
            get { return mostra_listview; }
            set
            {
                SetProperty(ref mostra_listview, value);

            }
        }
        IApiService apiService;

        private string mensagem;

        public string Mensagem
        {
            get { return mensagem; }
            set
            {
                SetProperty(ref mensagem, value);

            }
        }



        public ObservableCollection<ArqImagens> _imgs;
        public ObservableCollection<ArqImagens> imgs

          {
              get { return _imgs; }
              set
              {
                  SetProperty(ref _imgs, value);

              }
          }

       

        public ImagensViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
            // imgs = new ObservableCollection<ArqImagens>();
            //   Lista = new List<ArqImagens>();
            //_paciente = new paciente();
            apiService = ApiService;

            // _dialogService = dialogService;
            mostra_label = false;
            mostra_listview = true;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
          
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        private async Task GetExames()
        {

            isVisible = true;
            IsRunning = true;

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {

            }
            else
            {
                await PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
                IsRunning = false;
                isVisible = false;
                await NavigationService.GoBackAsync();
                return;

            }
            try
            {

                var Lista = await apiService.getExames(_paciente);
                if (Lista == null)
                {
                    IsRunning = false;
                    isVisible = false;
                    // await _navigationService.GoBackAsync();
                    Mostra_label = true;
                    Mostra_listview = false;
                    Mensagem = "Sem Imagens!";
                    return;
                }
                if (Lista.Count == 0)
                {
                    // await _dialogService.DisplayAlertAsync("app", "Paciente sem exames", "OK");

                    IsRunning = false;
                    isVisible = false;
                    // await _navigationService.GoBackAsync();
                    Mostra_label = true;
                    Mostra_listview = false;
                    Mensagem = "Sem Imagens!";
                    return;
                }

                imgs = new ObservableCollection<ArqImagens>(Lista);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            IsRunning = false;
            isVisible = false;
        }
            

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            //if (CrossConnectivity.Current.IsConnected)
            // {

            /*}
            else
            {
                _dialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
                IsRunning = false;
                isVisible = false;
                _navigationService.GoBackAsync();
                return;

            }*/

        }

        public ICommand Compartilhar
        {
            get
            {
                return _compartilhar ?? (_compartilhar = new Command<ArqImagens>(objeto =>
                {
                    ArqImagens teste = objeto;


                    string NomeSelecionado = teste.nome_arquivo_completo;
                    //apaguei para saber se era utilizado 
                    /*   var ShareMessage = new Plugin.Share.Abstractions.ShareMessage
                       {

                           Text = "Exemplo de como compartilhar textos ou links em Aplicações Xamarin.Forms. / Example of how to share texts or links in Xamarin.Forms Applications.",
                           Title = "Share",
                           Url = "https://www.julianocustodio.com"

                       };

                       CrossShare.Current.Share(ShareMessage);
                      /* Share.RequestAsync(new ShareFileRequest
                       {
                           Title = Title,
                           File = new ShareFile(file)
                       });*/
                    //showZoom(NomeSelecionado);
                    /*var navigationParams = new NavigationParameters();
                    navigationParams.Add("imagem", NomeSelecionado);
                     _navigationService.NavigateAsync("ImagensDetalhes2", navigationParams);*/
                    //EscolaSelecionado = teste.Escola;*/
                }));
            }
        }

        public ICommand SelecionarItem3
        {
            get
            {
                return _selecionarItem3 ?? (_selecionarItem3 = new Command<ArqImagens>(objeto =>
                {
                    ArqImagens teste = objeto;

                    compartilhaImagemAsync(teste.nome_arquivo_completo);
                    // string NomeSelecionado = teste.nome_arquivo_completo;

                }));
            }
        }

        private async void compartilhaImagemAsync(string nome_arquivo_completo)
        {

            if (conectionHelper.testaConexao())
            {

                paciente pac = new paciente();
                pac.photo = nome_arquivo_completo;
                byte[] imagem = null;
                using (var Dialog = UserDialogs.Instance.Loading("Compartilhando...", null, null, true, MaskType.Clear))
                {
                    imagem = await apiService.getExame(pac);
                }


                try
                {
                    if (imagem == null)
                    {
                        await PageDialogService.DisplayAlertAsync("app", "Imagem nao encontrada!", "Ok");
                    }
                    else
                    {

                        MemoryStream ms = new MemoryStream(imagem);
                        Xamarin.Forms.DependencyService.Get<IFileService>().SavePicture("ImageName.jpg", ms, "Download");
                        var filePath = Xamarin.Forms.DependencyService.Get<IFileStore>().GetFilePath();

                        compartilhaImagem(filePath);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("app", "Por favor Verifique sua conexao!", "Ok");
                IsRunning = false;
                isVisible = false;

                return;
            }
        }

        private void compartilhaImagem(string file)
        {
            /*var share = Xamarin.Forms.DependencyService.Get<IShare>();
            // file = "/storage/emulated/0/Download/ImageName.jpg";
            share.Show("Paciente", "Exames do paciente", file);
            */
            Share.RequestAsync(new ShareFileRequest()
            {
                Title = Title,

                File = new ShareFile(file)
            });
        }

        public ICommand SelecionarItem2
        {
            get
            {
                return _selecionarItem2 ?? (_selecionarItem2 = new Command<ArqImagens>(objeto =>
                {
                    ArqImagens teste = objeto;

                    string NomeSelecionado = teste.nome_arquivo_completo;
                    showZoom(NomeSelecionado);
                    /*var navigationParams = new NavigationParameters();
                    navigationParams.Add("imagem", NomeSelecionado);
                     _navigationService.NavigateAsync("ImagensDetalhes2", navigationParams);*/
                    //EscolaSelecionado = teste.Escola;
                }));
            }
        }


        public ICommand SelecionarItem
        {
            get
            {
                return _selecionarItem ?? (_selecionarItem = new Command<ArqImagens>(objeto =>
                {
                    ArqImagens teste = objeto;

                    string NomeSelecionado = teste.nome_arquivo_completo;
                    showZoom(NomeSelecionado);
                    /*var navigationParams = new NavigationParameters();
                    navigationParams.Add("imagem", NomeSelecionado);
                     _navigationService.NavigateAsync("ImagensDetalhes2", navigationParams);*/
                    //EscolaSelecionado = teste.Escola;
                }));
            }
        }

        private void showZoom(string nomeSelecionado)
        {
            new PhotoBrowser
            {
                Photos = new List<Photo>
                {
                    new Photo

                    {
                        URL = nomeSelecionado,

                    },


                },
                ActionButtonPressed = (index) =>
                {
                    Debug.WriteLine($"Clicked {index}");
                    PhotoBrowser.Close();
                    //  _navigationService.GoBackAsync();
                }

            }.Show();
        }



        public async void Initialize(INavigationParameters parameters)
        {

            _paciente = (paciente)parameters["paciente"];
            titulo = _paciente.nome;
            
            await GetExames();
            
        }
    }
}
