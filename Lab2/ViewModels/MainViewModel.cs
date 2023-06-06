using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Text.Json;
using Lab2.Services;

namespace Lab2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _currentDateTime;
        private string _currentDeviceInfo;
        private string _currentSource = "Resources/Images/adachi.webp";
        public string Title
        {
            get => "Welcome to.NET MAUI";
        }

        public string ImageSource
        {
            get => _currentSource;
            set
            {
                _currentSource = value;
                OnPropertyChanged();
            }
        }

        public string CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                _currentDateTime = value;
                OnPropertyChanged();
            }
        }
        public ICommand UpdateTimeCommand { get; }
        public ICommand UpdateImageCommand { get; }

        public string CurrentDeviceinfo
        {
            get => new StringBuilder()
            .AppendLine($"Model: {DeviceInfo.Model}")
            .AppendLine($"Manufacturer: {DeviceInfo.Manufacturer}")
            .AppendLine($"Platform: {DeviceInfo.Platform}")
            .AppendLine($"OS Version: {DeviceInfo.VersionString}").ToString();
            set
            {
                _currentDeviceInfo = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            UpdateTimeCommand = new Command(UpdateTime);
            CurrentDateTime = DateTime.Now.ToString("F");
            FireAndForget();
        }

        private static async void FireAndForget()
        {
            await DatabaseService.Init();
            await FetchDataFromApiAsync();
        }
        private void UpdateTime()
        {
            CurrentDateTime = DateTime.Now.ToString("F");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private static async Task FetchDataFromApiAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var toDoItem = JsonSerializer.Deserialize<ToDoItem>(json);
                await DatabaseService.SaveItemAsync(toDoItem);
                Console.WriteLine("added1");
            }
        }
    }
}
