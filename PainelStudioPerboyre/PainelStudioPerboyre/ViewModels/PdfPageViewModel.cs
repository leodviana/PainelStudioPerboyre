using Acr.UserDialogs;
using PainelStudioPerboyre.Helpers;
using PainelStudioPerboyre.Interface;
using PainelStudioPerboyre.Models;
using PainelStudioPerboyre.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PainelStudioPerboyre.ViewModels
{
    public class PdfPageViewModel : ViewModelBase, IInitialize
    {
        IApiService apiService;
        private bool isRunning;

        public paciente _paciente;
        private string mensagem;

        public string Mensagem
        {
            get { return mensagem; }
            set
            {
                SetProperty(ref mensagem, value);

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

        private ICommand _navegar;
        private ICommand _selecionarItem3;

        public ObservableCollection<ArqImagens> _pdf;
        public ObservableCollection<ArqImagens> pdf

        {
            get { return _pdf; }
            set
            {
                SetProperty(ref _pdf, value);

            }
        }
        

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
        public PdfPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
            apiService =  ApiService;
            mostra_label = false;
            mostra_listview = true;
           
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
           

        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
           
        }
        private async void GetExames()
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
                var Lista = await apiService.getExamespdf(_paciente);
                if (Lista == null)
                {
                    IsRunning = false;
                    isVisible = false;
                    // await _navigationService.GoBackAsync();
                    Mostra_label = true;
                    Mostra_listview = false;
                    Mensagem = "Paciente sem Arquivos Pdf!";
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
                    Mensagem = "Paciente sem Arquivos Pdf!";
                    return;
                }

                pdf = new ObservableCollection<ArqImagens>(Lista);

                /*foreach (var item in Lista)
                {
                    imgs.Add(new ArqImagens()
                    {
                        nome_arquivo_completo = item.nome_arquivo_completo

                    });
                }*/
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            IsRunning = false;
            isVisible = false;
        }


        public ICommand profileTappedCommand
        {
            get
            {
                return _navegar ?? (_navegar = new Command(async objeto =>
                {
                    ArqImagens teste = (ArqImagens)objeto;

                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("paciente", teste);
                    await NavigationService.NavigateAsync("ExibePdfPage", navigationParams);
                    //   await _dialogService.DisplayAlertAsync("teste", teste.nome_arquivo_completo, "Ok");
                }));
            }
        }
        public ICommand SelecionarItem3
        {
            get
            {
                return _selecionarItem3 ?? (_selecionarItem3 = new Command<ArqImagens>(objeto =>
                {
                    ArqImagens arqselecionado = objeto;

                    compartilhaImagemAsync(arqselecionado);
                    // string NomeSelecionado = teste.nome_arquivo_completo;
                   
                }));
            }
        }

        private async void compartilhaImagemAsync(ArqImagens arqselecionado)
        {
            using (var Dialog = UserDialogs.Instance.Loading("Compartilhando...", null, null, true, MaskType.Clear))
            {

                /* if (await FileManager.ExistsAsync(arqselecionado.nome_arquivo) == false)
                 {
                     await FileManager.DownloadDocumentsAsync(arqselecionado);
                 }
                 var file = FileManager.GetFilePathFromRoot(arqselecionado.nome_arquivo);*/
                var retorno = await RestApiHelper.DownloadFileAsync(arqselecionado.nome_arquivo_completo);
              //  var retorno = await FileManager.DownloadDocumentsAsyncshare(arqselecionado);

                Xamarin.Forms.DependencyService.Get<IFileService>().Savefile(arqselecionado.nome_arquivo, retorno, "Download");
               
            }
            var filePath = Xamarin.Forms.DependencyService.Get<IFileStore>().GetFilePath(arqselecionado.nome_arquivo);
            //antiga forma 
          /*  var share = Xamarin.Forms.DependencyService.Get<IShare>();
            // file = "/storage/emulated/0/Download/ImageName.jpg";
            await share.Show("Paciente", "Exames do paciente - Pdfs", filePath);*/

             await Share.RequestAsync(new ShareFileRequest()
            {
                Title = "Exames do paciente - Pdfs",

                File = new ShareFile(filePath)
            });


        }

        public void Initialize(INavigationParameters parameters)
        {
            _paciente = (paciente)parameters["paciente"];
            GetExames();
        }
    }
}
