using SkillProfi.Desktop.Data;
using SkillProfi.Desktop.Models;
using SkillProfi.Desktop.Pages;
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
using System.Windows.Shapes;

namespace SkillProfi.Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для CreateRequestWindow.xaml
    /// </summary>
    public partial class CreateRequestWindow : Window
    {
        private RequestDataApi _dataApi;
        private AdminPanel _panel;

        public CreateRequestWindow(AdminPanel panel, RequestDataApi dataApi)
        {
            InitializeComponent();
            _dataApi = dataApi;
            _panel = panel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsChecked()) 
            {
                var request = new Request()
                {
                    Name = tbName.Text,
                    Email = tbEmail.Text,
                    Message = tbMessage.Text
                };

                _dataApi.Create(request);
                MessageBox.Show("Заявка успешно отправлена!", "Заявка", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректно заполнены поля ввода!", "Заявка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }            
        }

        private bool IsChecked()
        {
            if (tbEmail.Text.Trim().Length <= 1 ||
                tbName.Text.Trim().Length <= 1 ||
                tbMessage.Text.Trim().Length <= 1
                || !tbEmail.Text.Contains("@"))
            {
                return false;
            }

            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
