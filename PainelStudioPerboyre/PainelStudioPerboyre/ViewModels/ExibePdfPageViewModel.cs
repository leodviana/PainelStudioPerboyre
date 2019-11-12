using Acr.UserDialogs;
using PainelStudioPerboyre.Helpers;
using PainelStudioPerboyre.Interface;
using PainelStudioPerboyre.Models;
using PainelStudioPerboyre.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PainelStudioPerboyre.ViewModels
{
    public class ExibePdfPageViewModel : ViewModelBase
    {
        ArqImagens _paciente;
        IApiService apiService;
        private string _NomeArquivoSem = "";

        public string NomeArquivoSem
        {
            get { return _NomeArquivoSem; }
            set
            {
                SetProperty(ref _NomeArquivoSem, value);

            }
        }

        private string _NomeArquivo = "";

        public string NomeArquivo
        {
            get { return _NomeArquivo; }
            set
            {
                SetProperty(ref _NomeArquivo, value);

            }
        }

        private string _NomeArquivocompleto = "";

        public string NomeArquivocompleto
        {
            get { return _NomeArquivocompleto; }
            set
            {
                SetProperty(ref _NomeArquivocompleto, value);

            }
        }

        readonly INavigationService _navigationService;
        private IPageDialogService _dialogService;

        public ExibePdfPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
           
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _paciente = (ArqImagens)parameters["paciente"];
            getPdfFile();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
           
        }

        private async void getPdfFile()
        {
            using (var Dialog = UserDialogs.Instance.Loading("Exibindo...", null, null, true, MaskType.Clear))
            {
                if (File.Exists(_paciente.nome_arquivo))

                {

                    var retorno = await RestApiHelper.DownloadFileAsync(_paciente.nome_arquivo);
                    Xamarin.Forms.DependencyService.Get<IFileService>().Savefile(_paciente.nome_arquivo, retorno, "Download");
                }
               /* if (await FileManager.ExistsAsync(_paciente.nome_arquivo) == false)
                {
                    await FileManager.DownloadDocumentsAsync(_paciente);
                }
                */
                //NomeArquivo = FileManager.GetFilePathFromRoot(_paciente.nome_arquivo);
                NomeArquivo = Xamarin.Forms.DependencyService.Get<IFileStore>().GetFilePath(_paciente.nome_arquivo);
                NomeArquivoSem = Path.GetFileName(_paciente.nome_arquivo);
            }
        }
    }
}
