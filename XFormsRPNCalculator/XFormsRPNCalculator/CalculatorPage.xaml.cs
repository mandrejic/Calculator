using Xamarin.Forms;

namespace XFormsRPNCalculator
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
            this.BindingContext = ((App)Application.Current).GetCalculatorViewModel();

            this.SizeChanged += (s, e) =>
            {
                if (this.Width != this.Height)
                    ShowExtraButtons(this.Width > this.Height);
            };
        }

        private void ShowExtraButtons(bool visible)
        {
            foreach (View child in LayoutRoot.Children)
            {
                if (child is Button && (int)child.GetValue(Grid.ColumnProperty) > 3)
                {
                    child.IsVisible = visible;
                }
            }
        }
    }
}
