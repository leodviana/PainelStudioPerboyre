using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PainelStudioPerboyre.ViewModels
{
	public class PacientesPageViewModel :ViewModelBase
	{
        public PacientesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {

        }
	}
}
