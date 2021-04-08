using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicalInformationPage : ContentPage
    {
        public MedicalInformationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        void EditProfile_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        void EditContact_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ContactPage());
        }

        void EditAllergy_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AllergyPage());
        }

        void EditPrescription_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PrescriptionPage());
        }

        void EditPrecondition_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PreconditionPage());
        }
    }
}