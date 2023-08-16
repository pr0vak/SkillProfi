using SkillProfi.Desktop.Data;
using SkillProfi.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SkillProfi.Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Page
    {
        private MainWindow _window;
        private RequestDataApi _dataApi;

        public UserPanel(MainWindow window)
        {
            InitializeComponent();
            _window = window;
            _dataApi = new RequestDataApi();
        }

        private void ToSendRequest(object sender, RoutedEventArgs e)
        {
            if (IsChecked())
            {
                var request = new Request()
                {
                    Name = tbName.Text,
                    Email = tbContact.Text,
                    Message = tbMessage.Text
                };

                _dataApi.Create(request);
                MessageBox.Show("Ваша заявка успешно отправлена.", "Заявка",
                     MessageBoxButton.OK, MessageBoxImage.Information);

                tbContact.Text = string.Empty;
                tbMessage.Text = string.Empty;
                tbName.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Не корректно оформлена заявка!", "Заявка",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool IsChecked()
        {
            if (tbName.Text.Length <= 2 || tbContact.Text.Length <= 2 ||
                tbMessage.Text.Length <= 10 || !tbContact.Text.Contains("@"))
            {
                return false;
            }

            return true;
        }

        private void ToCloseWindow(object sender, RoutedEventArgs e)
        {
            _window.Close();
        }

        private void Authorize(object sender, RoutedEventArgs e)
        {
            _window.frame.Content = new AuthPage(_window);
        }
    }
}
