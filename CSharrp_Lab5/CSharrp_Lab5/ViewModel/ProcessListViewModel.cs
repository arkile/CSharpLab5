using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CSharp_Lab5.Tools;
using CSharp_Lab5.Model;
using System.Threading;

namespace CSharp_Lab5.ViewModel
{
    class ProcessListViewModel : INotifyPropertyChanged
    {

        public MyProcess SelectedProcess { get; set; }

        private ObservableCollection<MyProcess> _users;

        public ObservableCollection<MyProcess> ProcessesList
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }



        private RelayCommand<object> _exitProcess;
        private RelayCommand<object> _sortCommand;
        private RelayCommand<object> _openFolderCommand;

        private string _sortBy;


        internal ProcessListViewModel()
        {


            Process[] processes = Process.GetProcesses();
            ProcessesList = new ObservableCollection<MyProcess>();
            foreach (var process in processes)
            {
                ProcessesList.Add(new MyProcess(process));
            }

            Thread refreshThread = new Thread(new ThreadStart(RefreshThreads));
            refreshThread.Start();
        }

        private void RefreshThreads()
        {

            lock (new Object())
            {
                while (true)
                {
                    Process[] processes = Process.GetProcesses();
                    ObservableCollection<MyProcess> newProcessesList = new ObservableCollection<MyProcess>();
                    foreach (var process in processes)
                    {
                        newProcessesList.Add(new MyProcess(process));
                    }

                    ProcessesList = newProcessesList;
                    Thread.Sleep(5000);
                }
            }

        }

        public string SortBy
        {
            set => _sortBy = value;
        }

        public RelayCommand<object> ExitProcessCommand
        {
            get
            {
                return _exitProcess ?? (_exitProcess = new RelayCommand<object>(
                           ExitProcess, o => true));
            }
        }

        private void ExitProcess(object obj)
        {
            try
            {
                SelectedProcess.Process.Kill();
                SelectedProcess.Process.WaitForExit();
                SelectedProcess.Process.Dispose();
                ProcessesList.Remove(SelectedProcess);
            }
            catch
            {

            }
        }

        public RelayCommand<object> OpenFolderCommand =>
            _openFolderCommand ?? (_openFolderCommand = new RelayCommand<object>(
                OpenFolder));

        private void OpenFolder(object obj)
        {
            try
            {

                var correctPath = SelectedProcess.Path;


                Process.Start(correctPath.Substring(0, correctPath.LastIndexOf('\\')));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public RelayCommand<object> SortCommand =>
            _sortCommand ?? (_sortCommand = new RelayCommand<object>(
                Sort));

        private void Sort(object obj)
        {

            var processes = ProcessesList.ToList();
            switch (_sortBy)
            {

                case "Name":

                    processes.Sort((process, process1) => String.Compare(process.Name, process1.Name, StringComparison.Ordinal));
                    break;
                case "Id":
                    processes.Sort((process, process1) => process.Id.CompareTo(process1.Id));
                    break;
                case "Active":
                    processes.Sort((process, process1) => process.IsActive.CompareTo(process1.IsActive));
                    break;
                case "CPU":
                    processes.Sort((process, process1) => process.Cpu.CompareTo(process1.Cpu));
                    break;
                case "RAM":
                    processes.Sort((process, process1) => process.Memory.CompareTo(process1.Memory));
                    break;
                case "Threads":
                    processes.Sort((process, process1) => process.NumOfThreads.CompareTo(process1.NumOfThreads));
                    break;
                case "User":
                    processes.Sort((process, process1) => process.UserName.CompareTo(process1.UserName));
                    break;
                case "Start date":
                    processes.Sort((process, process1) => process.LaunchDateTime.CompareTo(process1.LaunchDateTime));
                    break;

            }


            ProcessesList = new ObservableCollection<MyProcess>(processes);



        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
