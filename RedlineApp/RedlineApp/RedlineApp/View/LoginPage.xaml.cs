using Xamarin.Forms;

namespace RedlineApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void SignUpLinkTapped(object sender, System.EventArgs e)
        {
            // Set register page as current page.
            Application.Current.MainPage = new RegisterPage();
        }

        private void LoginButtonClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Success", "Login button placeholder", "Ok");
        }

        private void HelpButtonClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Success", "Forgot password?", "Ok");
        }
    }
}
