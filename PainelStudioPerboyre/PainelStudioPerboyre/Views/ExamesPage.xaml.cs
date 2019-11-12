using Xamarin.Forms;

namespace PainelStudioPerboyre.Views
{
    public partial class ExamesPage : ContentPage
    {
        public ExamesPage()
        {
            InitializeComponent();
        }

        private void LstPacientes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            LstPacientes.SelectedItem = null;
        }
    }
}
