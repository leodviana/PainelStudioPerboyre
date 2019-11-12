using PainelStudioPerboyre.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PainelStudioPerboyre.ViewModels
{
	public class ExamesTabViewModel : ViewModelBase
    {
        public paciente _paciente;
        public string _titulo;
        public string titulo

        {
            get { return _titulo; }
            set
            {
                SetProperty(ref _titulo, value);

            }
        }
        public ExamesTabViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {

        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
           
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _paciente = (paciente)parameters["paciente"];
            titulo = _paciente.nome;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
           
        }
    }
}
