using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PainelStudioPerboyre.ViewModels
{
	public class ImagensDetalhes2ViewModel : ViewModelBase
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
        public ImagensDetalhes2ViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
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
            new PhotoBrowser
            {
                Photos = new List<Photo>
                {
                    new Photo
                    {
                        URL = nome_arquivo_completo,
                       
                    },
                  

                },
                ActionButtonPressed = (index) =>
                {
                    Debug.WriteLine($"Clicked {index}");
                    PhotoBrowser.Close();
                    NavigationService.GoBackAsync();
                }
            }.Show();
        }
    }
    }

