using SkillProfi.Desktop.Models;
using SkillProfi.Desktop.Data;
using SkillProfi.Desktop.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        private MainWindow _window;
        public RequestDataApi _dataApi;

        public ObservableCollection<Request> Requests { get; set; } 

        public AdminPanel(MainWindow window)
        {
            InitializeComponent();
            _window = window;
            Initialize();
            listView.ItemsSource = Requests;
            var list = listView.Items.Count;
        }
        
        public void Initialize()
        {
            _dataApi = new RequestDataApi();
            Requests = new ObservableCollection<Request>(_dataApi.GetAll().Reverse());
            var list = listView.Items.Count;
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var request = listView.SelectedItem as Request;

            if (request == null)
                return;

            new DetailsRequestWindow(_dataApi, request).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this._window.Close();
        }

        private void listView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!listView.IsMouseCaptured)
            {
                listView.SelectedItem = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new CreateRequestWindow(this, _dataApi).Show();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            Requests = new ObservableCollection<Request>(_dataApi.GetAll().Reverse());
            listView.ItemsSource = Requests;
        }
    }
}
