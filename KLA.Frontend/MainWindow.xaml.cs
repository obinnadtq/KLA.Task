using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KLA.Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using var client = new HttpClient();

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7242/api/v1/convertDollar")
            {
                Content = JsonContent.Create(numberTextBox.Text)
            };

            var responseMessage = await client.SendAsync(requestMessage);
            

            if (responseMessage.IsSuccessStatusCode)
            {
                message.Content = await responseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                message.Content = $"Server error code {responseMessage.StatusCode}";
            }
        }
    }
}
