using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PainelStudioPerboyre.ViewModels
{
	public class ImagensDetalhesViewModel : ViewModelBase
    {
       

        private string _nome_arquivo_completo;

        public string nome_arquivo_completo
        {
            get { return _nome_arquivo_completo; }
            set
            {
                SetProperty(ref _nome_arquivo_completo, value);

            }
        }
        public ImagensDetalhesViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
           
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
           
           

        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            nome_arquivo_completo = (string)parameters["imagem"];
        }
    }
}
