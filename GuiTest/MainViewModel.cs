using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WordCloudCalculator.Contract.Word;

namespace GuiTest
{
	class MainViewModel : INotifyPropertyChanged
	{
		private ICommand _wordSelectedCommand;

		public ObservableCollection<IWeightedWord> Words { get; set; }

		public ICommand WordSelectedCommand
		{
			get { return _wordSelectedCommand; }
			set { _wordSelectedCommand = value; OnPropertyChanged();}
		}

		public MainViewModel()
		{
			Words = new ObservableCollection<IWeightedWord>
			{
				new WeightedWord {Text = "Foo", Weight = 100},
				new WeightedWord {Text = "Foo2", Weight = 80},
				new WeightedWord {Text = "Foo3", Weight = 60},
				new WeightedWord {Text = "Foo4", Weight = 30},
				new WeightedWord {Text = "Foo5", Weight = 20},
			};

			WordSelectedCommand = new RelayCommand(ExecuteTagSelectedCommand);
		}

		private void ExecuteTagSelectedCommand(object o)
		{
			var tag = o as IWord;
			MessageBox.Show(tag?.Text);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
