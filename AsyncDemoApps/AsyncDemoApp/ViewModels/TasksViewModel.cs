using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        public ObservableCollection<TaskItemViewModel> Tasks { get; }
            = new();

        private int _completedTasksCount;

        public int CompletedTasksCount
        {
            get => _completedTasksCount;
            set
            {
                _completedTasksCount = value;
                OnPropertyChanged();
            }
        }

        private int taskCounter = 0;

        public ICommand StartTaskCommand { get; }
        public ICommand ClearAllCommand { get; }

        public TasksViewModel()
        {
            StartTaskCommand = new Command(async () => await StartNewTaskAsync());

            ClearAllCommand = new Command(async () => await ClearAllAsync());
        }

        private async Task StartNewTaskAsync()
        {
            taskCounter++;

            TaskItemViewModel newTask = new TaskItemViewModel
            {
                TaskId = taskCounter
            };

            Tasks.Add(newTask);

            bool result = await newTask.RunAsync();

            if (result)
            {
                CompletedTasksCount++;
            }
        }

        private async Task ClearAllAsync()
        {
            bool answer = await ShowConfirmAsync(
                "Clear All",
                "Are you sure you want to clear all tasks?",
                "Yes",
                "No");

            if (answer)
            {
                Tasks.Clear();
                CompletedTasksCount = 0;
            }
        }
    }
}
