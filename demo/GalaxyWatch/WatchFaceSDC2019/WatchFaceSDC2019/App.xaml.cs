using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchFaceSDC2019
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
    }
}