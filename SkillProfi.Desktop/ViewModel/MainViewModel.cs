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
        private RequestDataApi requestDataApi;
        private AccountDataApi accountDataApi;
        private string login;
        private string password;
        private string name;
        private string email;
        private string message;
        private Visibility authoziationVisibility = Visibility.Visible;
        private Visibility userPanelVisibility = Visibility.Hidden;
        private Visibility adminPanelVisibility = Visibility.Hidden;
        private ObservableCollection<Request> requests;
        private Request currentRequest;


        public string Login 
        { 
            get => login; 
            set => Set(ref login, value); 
        }
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        public string Email
        {
            get => email;
            set => Set(ref email, value);
        }
        public string Message
        {
            get => message;
            set => Set(ref message, value);
        }

        public Visibility AuthoziationVisibility
        {
            get => authoziationVisibility;
            set => Set(ref authoziationVisibility, value);
        }

        public Visibility UserPanelVisibility
        {
            get => userPanelVisibility;
            set => Set(ref userPanelVisibility, value);
        }

        public Visibility AdminPanelVisibility
        {
            get => adminPanelVisibility;
            set => Set(ref adminPanelVisibility, value);
        }

        public ObservableCollection<Request> Requests
        {
            get => requests;
            set => Set(ref requests, value);
        }

        public Request CurrentRequest
        {
            get => currentRequest;
            set => Set(ref currentRequest, value);
        }

        
        public RelayCommand AuthorizationCommand { get; set; }
        public RelayCommand GuestCommand { get; set; }
        public RelayCommand GetAuthorizationViewCommand { get; set; }
        public RelayCommand SendRequestCommand { get; set; }    
        public RelayCommand CreateRequestCommand { get; set; }  
        public RelayCommand UpdateCommand { get; set; }  
        public RelayCommand DetailsCommand { get; set; }  
        public RelayCommand GetInfoCommand { get; set; }  

        public MainViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            accountDataApi = new AccountDataApi();
            requestDataApi = new RequestDataApi();
            name = string.Empty;
            email = string.Empty;
            message = string.Empty;

            AuthorizationCommand = new RelayCommand(o =>
            {
                if (accountDataApi.Athorization(login, password))
                {
                    AuthoziationVisibility = Visibility.Hidden;
                    AdminPanelVisibility = Visibility.Visible;
                    Requests = new ObservableCollection<Request>(requestDataApi.GetAll().Reverse());
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
                    requestDataApi.Create(new Request
                    {
                        Name = name,
                        Email = email,
                        Message = message
                    });

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
                Requests = new ObservableCollection<Request>(requestDataApi.GetAll());
            });

            DetailsCommand = new RelayCommand(o =>
            {
                if (currentRequest != null)
                {
                    new DetailsRequestWindow(requestDataApi, currentRequest).ShowDialog();
                }
            });

            GetInfoCommand = new RelayCommand(o =>
            {
                if (currentRequest != null)
                {
                    new DetailsRequestWindow(requestDataApi, currentRequest).ShowDialog();
                }
            });
        }
    }
}
