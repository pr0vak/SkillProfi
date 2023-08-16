using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SkillProfi.Desktop.Models
{
    public class Request : INotifyPropertyChanged
    {
        private string status = "Получена";

        public int Id { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string Name { get; set; }

        public string Message { get; set; }

        public string Status 
        { 
            get => status; 
            set  
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            } 
        }

        public string Email { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
