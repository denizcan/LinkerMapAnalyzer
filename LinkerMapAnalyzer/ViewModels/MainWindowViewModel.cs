using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace LinkerMapAnalyzer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        private int _count;

        public int Count
        {
            get => _count;
            set => this.RaiseAndSetIfChanged(ref _count, value);
        }

        public ICommand StepCommand => ReactiveCommand.Create(() => { Count = Count + 1; });
    }
}
