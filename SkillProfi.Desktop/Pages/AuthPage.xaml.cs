using SkillProfi.Desktop.Data;
using SkillProfi.Desktop.View;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private MainWindow _window;

        public AuthPage(MainWindow window)
        {
            InitializeComponent();
            _window = window;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var password = pbPassword.Password;
            var isAuthorize = Task.Run(() => new AccountDataApi()
                .Athorization(login, password))
                .Result;

            if (isAuthorize)
            {
                _window.frame.Content = new AdminPanel(_window);
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            _window.frame.Content = new UserPanel(_window);
        }
    }
}
