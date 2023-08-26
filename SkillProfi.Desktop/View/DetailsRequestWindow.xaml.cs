using SkillProfi.Desktop.Models;
using SkillProfi.Desktop.Data;
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
    /// Логика взаимодействия для DetailsRequestWindow.xaml
    /// </summary>
    public partial class DetailsRequestWindow : Window
    {
        public RequestDataApi _dataApi;
        private Request _request;

        public DetailsRequestWindow(RequestDataApi dataApi, Request request)
        {
            InitializeComponent();
            _request = request;
            _dataApi = dataApi;
            InitiliizeDetails();
            cbStatus.SelectionChanged += CbStatus_SelectionChanged;
            btnSave.IsEnabled = false;
        }

        /// <summary>
        /// Инизиализирование полей данными.
        /// </summary>
        private void InitiliizeDetails()
        {
            tbId.Text = _request.Id.ToString();
            tbCreated.Text = _request.Created.ToString("dd.MM.yy HH:mm");
            tbName.Text = _request.Name;
            tbMessage.Text = _request.Message;
            tbEmail.Text = _request.Email;
            cbStatus.Text = _request.Status;
        }

        /// <summary>
        /// Обработка данных при изменении статуса.
        /// </summary>
        private void CbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (sender as ComboBox).SelectedValue.ToString();

            if (selected == _request.Status)
            {
                btnSave.IsEnabled = false;
            }
            else
            {
                btnSave.IsEnabled = true;
            }
        }

        /// <summary>
        /// Обновление состояния завяки.
        /// </summary>
        private void Update(object sender, RoutedEventArgs e)
        {
            _request.Status = cbStatus.SelectedValue.ToString();
            Task.Run(() => _dataApi.Update(_request));
            this.Close();
        }

        /// <summary>
        /// Закрыть данной окно.
        /// </summary>
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
