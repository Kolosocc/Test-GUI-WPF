using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserNameInput.Text))
            {
                StatusMessage.Text = $"Welcome, {UserNameInput.Text}!";
            }
        }
    }
}