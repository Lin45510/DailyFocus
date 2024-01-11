using DailyFocus.DataBase.DAO;

namespace DailyFocus
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        readonly CommitmentsDAO commitmentsDAO = new();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var teste = await commitmentsDAO.SaveItemAsync(new() { Name = "teste" });
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {teste} time";
            else
                CounterBtn.Text = $"Clicked {teste} times";
        }
    }
}