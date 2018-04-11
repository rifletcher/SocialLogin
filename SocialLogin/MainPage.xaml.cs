using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SimpleAuth;

namespace SocialLogin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var clientId = "627982777551085";


            // Odd, you need to add the client secret as the third parameter here, if using UWP
            var fb = new SimpleAuth.Providers.FacebookApi("facebook", clientId, "");

            var account = await AuthAsync(fb);

            Acr.UserDialogs.UserDialogs.Instance.Alert(
                "Account Returned",
                account.ToJson(), "Ok");
        }

        async Task<Account> AuthAsync(OAuthApi api)
        {
            Account result = null;
            try
            {
                result = await api.Authenticate();
            }
            catch (TaskCanceledException ex1)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(
                    "Exception",
                    ex1.ToString(), "Ok");

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(
                    "Exception",
                    ex.ToString(), "Ok");
                Console.WriteLine(ex);
            }

            if (result != null)
                Console.WriteLine(await result.ToJsonAsync());

            return result;
        }

    }
}
