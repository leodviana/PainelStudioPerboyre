using Xamarin.Forms;

namespace PainelStudioPerboyre.Views
{
    public partial class DentistaPage : ContentPage
    {
        public DentistaPage()
        {
            InitializeComponent();
        }

        private void LstPacientes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
              LstPacientes.SelectedItem = null;
        }
        
    }
}
