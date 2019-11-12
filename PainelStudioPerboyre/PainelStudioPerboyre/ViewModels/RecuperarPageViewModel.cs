using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace PainelStudioPerboyre.ViewModels
{
    public class RecuperarPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private ICommand _VoltarCommand;

        public RecuperarPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
           
        }
        public ICommand VoltarCommand
        {
            get
            {
                return _VoltarCommand ?? (_VoltarCommand = new Command(objeto =>
                {
                    NavigationService.GoBackAsync();
                }));
            }
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
