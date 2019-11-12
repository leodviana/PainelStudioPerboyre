using PainelStudioPerboyre.Interface;
using PainelStudioPerboyre.Services;
using Prism.Navigation;
using Prism.Services;
using System;

namespace PainelStudioPerboyre.ViewModels
{
    public class PermissaoPageViewModel : ViewModelBase
    {
       

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }

        public PermissaoPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            IsRunning = true;
           

            verifica_permissao();
            IsRunning = false;

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
        private async void verifica_permissao()
        {
            try
            {
                //var permissao = await Getpermissao();

                if (await ChecapermisaoService.checa_permissao())
                {
                    
                }
                else
                {
                    var exemplo = Xamarin.Forms.DependencyService.Get<ICloseApplication>();
                    exemplo.closeApplication();

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            //   bool teste = await ChecapermisaoService.checa_permissao();
            //   return teste ;

        }
    }
}
