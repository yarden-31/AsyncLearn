
using System.Windows.Input;

namespace ViewModels
{
    public class TaskItemViewModel : BaseViewModel
    {
        private CancellationTokenSource cts = new();

        public ICommand CancelCommand { get; }

        public TaskItemViewModel()
        {
            CancelCommand = new Command(Cancel);
        }

        private void Cancel()
        {
            cts.Cancel();
        }

        private int taskId;
        public int TaskId
        {
            get => taskId;
            set
            {
                taskId = value;
                OnPropertyChanged();
            }
        }

        private double _progress;
        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        private string _statusText = "Waiting...";
        public string StatusText
        {
            get => _statusText;
            set
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }

        private bool _isCancelEnabled = true;
        public bool IsCancelEnabled
        {
            get => _isCancelEnabled;
            set
            {
                _isCancelEnabled = value;
                OnPropertyChanged();
            }
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                StatusText = "Running...";

                for (int i = 1; i <= 100; i++)
                {
                    await Task.Delay(50, cts.Token);
                    Progress = i / 100.0;
                }

                StatusText = "Completed";
                IsCancelEnabled = false;

                return true;
            }
            catch (OperationCanceledException)
            {
                StatusText = "Cancelled";
                IsCancelEnabled = false;

                return false;
            }
            finally
            {
                cts.Dispose();
            }
        }
    }
}
