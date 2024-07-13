using System.Globalization;

namespace MultilingualMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedLanguage = picker.SelectedItem.ToString();
            SetLanguage(selectedLanguage);
        }

        private void SetLanguage(string languageCode)
        {
            CultureInfo culture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Refresh the page to apply the new culture
            MainPage newPage = new MainPage();
            Application.Current.MainPage = newPage;

            if (culture.TextInfo.IsRightToLeft)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}
