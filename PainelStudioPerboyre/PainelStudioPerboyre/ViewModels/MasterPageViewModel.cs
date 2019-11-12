using PainelStudioPerboyre.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PainelStudioPerboyre.ViewModels
{
	public class MasterPageViewModel : ViewModelBase
    {
       

        private bool isPresented;

        public bool IsPresented
        {
            get { return this.isPresented; }
            set { this.SetProperty(ref this.isPresented, value); }
        }

        public MasterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
           
            //LoadMenu();
        }

        public async Task PageChangeAsync(MenuItem menuItem)
        {
            await NavigationService.NavigateAsync($"NavigationPage/{menuItem.PageName}");
            this.IsPresented = false;
        }
    }
}
