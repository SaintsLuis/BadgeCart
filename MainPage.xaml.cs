namespace BadgeCart
{
    public partial class MainPage : ContentPage, IDisposable
    {
        public MainPage()
        {
            InitializeComponent();
            BadgeCounterService.CountChanged += OnCountChanged;
        }

        private void OnCountChanged(object? sender, int newCount)
        {
            //counterLabel.Text = $"Counter: {newCount}";
        }

        private void IncreaseCounterClicked(object sender, EventArgs e)
        {
            BadgeCounterService.SetCount(BadgeCounterService.Count + 1);
        }

        private void DecreaseCounterClicked(object sender, EventArgs e)
        {
            BadgeCounterService.SetCount(BadgeCounterService.Count - 1);
        }

        public void Dispose()
        {
            BadgeCounterService.CountChanged -= OnCountChanged;
        }

    }

}
