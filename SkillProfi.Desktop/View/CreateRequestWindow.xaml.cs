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
using System.Windows.Shapes;

namespace SkillProfi.Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для CreateRequestWindow.xaml
    /// </summary>
    public partial class CreateRequestWindow : Window
    {
        private RequestDataApi _dataApi;

        public CreateRequestWindow(RequestDataApi dataApi)
        {
            InitializeComponent();
            _dataApi = dataApi;
        }

        /// <summary>
        /// Отправить заявку.
        /// </summary>
        private void SendRequest(object sender, RoutedEventArgs e)
        {
            // Если поля корректно заполнены
            if (IsChecked()) 
            {
                var request = new Request()
                {
                    Name = tbName.Text,
                    Email = tbEmail.Text,
                    Message = tbMessage.Text
                };

                Task.Run(() => _dataApi.Create(request));
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

        /// <summary>
        /// Проверка веденных данных в полях на корректность.
        /// </summary>
        /// <returns>Результат проверки.</returns>
        private bool IsChecked()
        {
            var emailLength = tbEmail.Text.Trim().Length;
            var isEmail = tbEmail.Text.Contains('@');
            var nameLength = tbName.Text.Trim().Length;
            var isFullName = tbName.Text.Trim().Split(' ').Length == 3;
            var messageLength = tbMessage.Text.Trim().Length;

            if (emailLength <= 6 || nameLength <= 10 ||
                messageLength <= 20 || !isEmail || !isFullName)
            {
                return false;
            }

            return true;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
