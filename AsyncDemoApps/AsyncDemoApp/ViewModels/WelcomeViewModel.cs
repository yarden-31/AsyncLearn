using System.Windows.Input;

namespace ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        public ICommand NavigateToAsyncDemoCommand { get; }


        public WelcomeViewModel()
        {
            Title = "ברוכים הבאים";
            NavigateToAsyncDemoCommand = new Command(async () => await Shell.Current.GoToAsync("//AsyncDemoPage"));
        }
    }
}