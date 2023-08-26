using SkillProfi.Desktop.Core;
using SkillProfi.Desktop.Data;
using SkillProfi.Desktop.Models;
using SkillProfi.Desktop.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SkillProfi.Desktop.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Провайдер для работы с заявками.
        /// </summary>
        private RequestDataApi requestDataApi;

        /// <summary>
        /// Провайдер для работы с аккаунтами (Авторазация).
        /// </summary>
        private AccountDataApi accountDataApi;

        private string login;           // Имя пользователя
        private string password;        // Пароль пользователя
        private string name;            // Полное имя гостя
        private string email;           // Почта гостя
        private string message;         // Сообщение о проблеме, описанное гостем

        private bool isEnable = true;   // Вкл/откл полей для ввода данных и кнопок в окне авторизации

        private Visibility authoziationVisibility = Visibility.Visible; // отображение окна авторизации
        private Visibility userPanelVisibility = Visibility.Hidden;     // отображение окна гостя
        private Visibility adminPanelVisibility = Visibility.Hidden;    // отображение окна пользователя

        private ObservableCollection<Request> requests;                 // список заявок
        private Request currentRequest;                                 // выбранная заявка для обработки


        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Login 
        { 
            get => login; 
            set => Set(ref login, value); 
        }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }

        /// <summary>
        /// Полное имя гостя.
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        /// <summary>
        /// Почта гостя.
        /// </summary>
        public string Email
        {
            get => email;
            set => Set(ref email, value);
        }

        /// <summary>
        /// Сообщение о проблеме от гостя.
        /// </summary>
        public string Message
        {
            get => message;
            set => Set(ref message, value);
        }

        /// <summary>
        /// Состояние отображение окна авторизации.
        /// </summary>
        public Visibility AuthoziationVisibility
        {
            get => authoziationVisibility;
            set => Set(ref authoziationVisibility, value);
        }

        /// <summary>
        /// Состояние отображение окна гостя.
        /// </summary>
        public Visibility UserPanelVisibility
        {
            get => userPanelVisibility;
            set => Set(ref userPanelVisibility, value);
        }

        /// <summary>
        /// Состояние отображение окна пользователя.
        /// </summary>
        public Visibility AdminPanelVisibility
        {
            get => adminPanelVisibility;
            set => Set(ref adminPanelVisibility, value);
        }

        /// <summary>
        /// Список заявок.
        /// </summary>
        public ObservableCollection<Request> Requests
        {
            get => requests;
            set => Set(ref requests, value);
        }

        /// <summary>
        /// Выбранная заявка.
        /// </summary>
        public Request CurrentRequest
        {
            get => currentRequest;
            set => Set(ref currentRequest, value);
        }

        /// <summary>
        /// Включены ли редактирование полей данных и взаимодействие с кнопками в окне авторизации.
        /// </summary>
        public bool IsEnable
        {
            get => isEnable;
            set => Set(ref isEnable, value);
        }

        
        // Команды

        /// <summary>
        /// Команда для проверки данных и авторизации пользователя.
        /// </summary>
        public RelayCommand AuthorizationCommand { get; set; }

        /// <summary>
        /// Команда для перехода в окно гостя.
        /// </summary>
        public RelayCommand GuestCommand { get; set; }

        /// <summary>
        /// Команда для перехода в окно авторизации.
        /// </summary>
        public RelayCommand GetAuthorizationViewCommand { get; set; }

        /// <summary>
        /// Команда, проверяющая корректность ввода полей для отправки заявки и отрпавка заявки.
        /// </summary>
        public RelayCommand SendRequestCommand { get; set; }    

        /// <summary>
        /// Команда для откртия окна подачи заявки.
        /// </summary>
        public RelayCommand CreateRequestCommand { get; set; }  

        /// <summary>
        /// Команда для обновления состояния заявки.
        /// </summary>
        public RelayCommand UpdateCommand { get; set; }  

        /// <summary>
        /// Команда, открывающее окно с описанием заявки.
        /// </summary>
        public RelayCommand GetInfoCommand { get; set; }  

        public MainViewModel()
        {
            Task.Run(Initialize);
        }

        /// <summary>
        /// Инициализация полей.
        /// </summary>
        private async Task Initialize()
        {
            accountDataApi = new AccountDataApi();
            requestDataApi = new RequestDataApi();
            name = string.Empty;
            email = string.Empty;
            message = string.Empty;

            AuthorizationCommand = new RelayCommand(async o =>
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) 
                {
                    MessageBox.Show("Введите логин и пароль.\nВ логине и пароле не должно быть пробелов!", "Авторизация");
                    return;
                }
                IsEnable = false;
                var _login = login;
                var _password = password;
                Login = "Идет проверка подключения...";

                if (await accountDataApi.Authorization(_login, _password))
                {
                    AuthoziationVisibility = Visibility.Hidden;
                    AdminPanelVisibility = Visibility.Visible;
                    Requests = new ObservableCollection<Request>((await requestDataApi.GetAll()).Reverse());
                }
                else
                {
                    Login = _login;
                    IsEnable = true;
                }

            });

            GuestCommand = new RelayCommand(o =>
            {
                AuthoziationVisibility = Visibility.Hidden;
                UserPanelVisibility = Visibility.Visible;
            });

            GetAuthorizationViewCommand = new RelayCommand(o =>
            {
                UserPanelVisibility = Visibility.Hidden;
                AuthoziationVisibility = Visibility.Visible;
            });

            SendRequestCommand = new RelayCommand(o =>
            {
                if (name.Split(' ').Length == 3 && email.Contains('@') && message.Length > 10)
                {
                    var request = new Request
                    {
                        Name = name,
                        Email = email,
                        Message = message
                    };

                    Task.Run(() => requestDataApi.Create(request));

                    Name = string.Empty;
                    Email = string.Empty;
                    Message = string.Empty;
                    MessageBox.Show("Ваша заявка успешно отправлена!", "Заявка", 
                        MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Некорректно введены данные!", "Заявка", 
                        MessageBoxButton.OK, 
                        MessageBoxImage.Error);   
                }
            });

            CreateRequestCommand = new RelayCommand(o =>
            {
                new CreateRequestWindow(requestDataApi).ShowDialog();
            });

            UpdateCommand = new RelayCommand(o =>
            {
                Task.Run(UpdateRequests);
            });

            GetInfoCommand = new RelayCommand(o =>
            {
                if (currentRequest != null)
                {
                    new DetailsRequestWindow(requestDataApi, currentRequest).ShowDialog();
                }
            });
        }

        /// <summary>
        /// Обновление списка заявок.
        /// </summary>
        /// <returns></returns>
        private async Task UpdateRequests()
        {
            Requests = new ObservableCollection<Request>((await requestDataApi.GetAll()).Reverse());
        }
    }
}
