using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RRLauncher.Services;
using RRLauncherAPI.Models;

namespace RRLauncher.ViewModel
{
    public class InputViewModel: ViewModelBase
    {
        private readonly CommandsManager _commandsManager;

        public RelayCommand ExecuteCommand { get; set; }
        public RelayCommand PreviousCommand { get; set; }
        public RelayCommand NextCommand { get; set; }
        public RelayCommand SubCommand { get; set; }
        
        public InputViewModel(CommandsManager _commandsManager)
        {
            this._commandsManager = _commandsManager;
            _commandsManager.LoadCommands();
            _input = String.Empty;
            ExecuteCommand = new RelayCommand(Execute);
            PreviousCommand = new RelayCommand(() => SelectedIndex = SelectedIndex == 0 ? 0 : SelectedIndex - 1);
            NextCommand = new RelayCommand(() => SelectedIndex = SelectedIndex == Results.Count - 1 ? Results.Count - 1 : SelectedIndex + 1);
            SubCommand = new RelayCommand(FindSubCommands);
        }

        private void FindSubCommands()
        {
            if (FindingSubCommands)
                return;
            MasterCommand = SelectedCommand;
            Input = String.Empty;
            Results = _commandsManager.FindSubCommands(MasterCommand, Input);
        }

        private void Execute()
        {
            if (SelectedCommand != null)
                _commandsManager.ExecuteCommand(SelectedCommand);
            MasterCommand = null;
            Input = String.Empty;
        }


        private string _input;
        public String Input
        {
            get { return _input; }
            set
            {
                Set(ref _input, value);
                PopulateResults();
            }
        }

        private void PopulateResults()
        {
            if (FindingSubCommands)
            {
                Results = _commandsManager.FindSubCommands(MasterCommand, Input);
            }
            else
            {
                Results = _commandsManager.FindCommands(Input);
            }
            if (Results.Count > 0)
                SelectedCommand = Results[0];
        }

        private List<Command> _results;
        public List<Command> Results
        {
            get { return _results; }
            set { Set(ref _results, value); }
        }

        private Command _selectedCommand;
        public Command SelectedCommand
        {
            get { return _selectedCommand; }
            set{ Set(ref _selectedCommand, value); }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { Set(ref _selectedIndex, value); }
        }

        public bool FindingSubCommands => MasterCommand != null;

        private Command _masterCommand;

        public Command MasterCommand
        {
            get { return _masterCommand; }
            set { Set(ref _masterCommand, value); }
        }
    }
}
