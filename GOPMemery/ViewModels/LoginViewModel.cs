using GOPMemery.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GOPMemery.ViewModels
{
    class LoginViewModel
    {
        public ICommand GoToRegisterCommand { get; set; }

        public LoginViewModel()
        {
            GoToRegisterCommand = new Command(async () => await ExecuteGoToRegisterCommand());
        }

        private async Task ExecuteGoToRegisterCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
