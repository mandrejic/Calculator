using Microsoft.Phone.Controls;

namespace XFormsRPNCalculator.WinPhone
{
    /// <summary>
    /// Main Page
    /// </summary>
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new XFormsRPNCalculator.App());
        }
    }
}
