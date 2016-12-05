using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WordCloudCalculator.WPF
{
	class CommandProxy : FrameworkElement, ICommand
	{
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandProxy));

		public ICommand Command
		{
			get { return GetValue(CommandProperty) as ICommand; }
			set { SetValue(CommandProperty, value); }
		}

		public bool CanExecute(object parameter)
		{
			return Command?.CanExecute(parameter) ?? false;
		}

		public void Execute(object parameter)
		{
			Command?.Execute(parameter);
		}

		public event EventHandler CanExecuteChanged;
	}
}
