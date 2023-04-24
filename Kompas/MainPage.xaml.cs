using Microsoft.Maui.Devices;

namespace Kompas;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        if(Compass.Default.IsSupported) 
        {
            Compass.Default.ReadingChanged += Default_ReadingChanged;
        }
        else
        {
            lbHodnotaKompasu.Text = "Vaše platforma nemá kompas";
        }
	}

    private void Default_ReadingChanged(object sender, CompassChangedEventArgs e)
    {
        lbHodnotaKompasu.Text = $": {e.Reading}";
    }

    private async void btnPosliZpravu_Clicked(object sender, EventArgs e)
    {
           if(entZprava.Text == null)
        {
            DisplayAlert("Poser si záda","Zadej text", "OK");
        }
           else
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = entZprava.Text,
                Title = "Sdílení tajné zprávy"
            });
        }
    }

    private async void btnInfoZarizeni_Clicked(object sender, EventArgs e)
    {
        string volbaInfo = await DisplayActionSheet("Jaké informace chcete zobrazit?", "Poser si záda", "Potvrdit", "O zařízení", "Konektivita");
        if ((volbaInfo != null) && (volbaInfo != "Zrušit"))
        {
            if(volbaInfo == "O zařízení")
            {
                string deviceInfo = "";
                deviceInfo += $"Model: {DeviceInfo.Current.Model} \n";
                deviceInfo += $"Výrobce: {DeviceInfo.Current.Manufacturer} \n";
                deviceInfo += $"Jméno: {DeviceInfo.Current.Name} \n";
                deviceInfo += $"Verze OS: {DeviceInfo.Current.VersionString} \n";
                deviceInfo += $"Platforma: {DeviceInfo.Current.Platform} \n";
                DisplayAlert("Info o zřízení", deviceInfo, "OK");
            }
            else if(volbaInfo == "Konektivita")
            {
                NetworkAccess accessType = Connectivity.Current.NetworkAccess;
                if(accessType == NetworkAccess.Internet)
                {
                    DisplayAlert("Info o konekntivitě", "Připojen", "OK");
                }
                else
                {
                    DisplayAlert("Info o konektivitě", "Neaktivní připojení", "OK");
                }
            }
        }
    }

    private void btnVibrujUwU_Clicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.Android || DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            Vibration.Default.Vibrate();
        }
        else 
        {
            DisplayAlert("Chyba", "Nepodporovaná platforma", "OK");
        }
    }

    private void Battery_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Baterie", (Battery.Default.ChargeLevel * 100).ToString() +  "%", "OK");
    }
}

