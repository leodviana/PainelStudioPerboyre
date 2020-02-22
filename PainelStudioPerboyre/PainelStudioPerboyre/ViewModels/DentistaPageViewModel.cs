using PainelStudioPerboyre.Models;
using PainelStudioPerboyre.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace PainelStudioPerboyre.ViewModels
{
    public class DentistaPageViewModel : ViewModelBase
    {
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
        private bool _isVisible;

        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                SetProperty(ref _isVisible, value);

            }
        }

        private ICommand _refresh;
        private bool isRunning2;

        //active indicator do rodape 
        public bool IsRunning2
        {
            get { return isRunning2; }
            set
            {
                SetProperty(ref isRunning2, value);

            }
        }
        private bool _isVisible2;
         public bool isVisible2
        {
            get { return _isVisible2; }
            set
            {
                SetProperty(ref _isVisible2, value);

            }
        }

        //public InfiniteScrollCollection<Dentista> pacs { get; }
        //private const int PageSize = 10;
        public ObservableCollection<Dentista> _dentistas;
        public ObservableCollection<Dentista> dentistas
        {
            get { return _dentistas; }
            set
            {
                SetProperty(ref _dentistas, value);

            }
        }

        List<Dentista> Lista;
        private string _DentistaFilter = "";

        public string DentistaFilter
        {
            get { return _DentistaFilter; }
            set
            {
                SetProperty(ref _DentistaFilter, value);
                Filtro();
            }
        }

        private void Filtro()
        {
            GetDentistas();
        }
        public DentistaPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
           
            apiService =  ApiService;
            List<Dentista> Lista = new List<Dentista>();
           
                       
        }

        
        private async void GetDentistas()
        {
            isVisible = true;
            IsRunning = true;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // GetDentistas();
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("app", "Por favor Verifique sua conexao!", "Ok");
                IsRunning = false;
                isVisible = false;
                await NavigationService.GoBackAsync();
                return;

            }
            

            Lista = await apiService.getDentistas();
            if (Lista == null)
            {
                await PageDialogService.DisplayAlertAsync("app", "lista nula", "Ok");
            }
            else
            {
                if (DentistaFilter.Trim().Length > 0)
                {
                   // dentistas.Clear();
                    dentistas = new ObservableCollection<Dentista>(Lista.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper())));
                    // _dentistas.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper()));
                   /* foreach (var item in Lista.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper())))
                    {
                        dentistas.Add(new Dentista()
                        {
                            Id = item.Id,
                            nome = item.nome,
                            Email = item.Email


                        });
                    }*/
                }
                else
                {
                    dentistas = new ObservableCollection<Dentista>(Lista);
                }
            }
            IsRunning = false;
            isVisible = false;


        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
           // throw new NotImplementedException();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
          //  throw new NotImplementedException();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
          //  throw new NotImplementedException();
        }

        public DelegateCommand<Dentista> NavigateCommand
        {
            get
            {
                return new DelegateCommand<Dentista>((item) =>
                {

                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("paciente", item);
                    NavigationService.NavigateAsync("ExamesPage", navigationParams);
                });

            }
        }
                    
        public ICommand RefreshCommand
        {
            get
            {
                return _refresh ?? (_refresh = new Command(objeto =>
                {
                    GetDentistas();
                    isRunning = false;

                }));
            }
        }
    }
}
