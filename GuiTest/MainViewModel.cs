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

		const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public ObservableCollection<IWeightedWord> Words { get; set; }

		public ICommand WordSelectedCommand
		{
			get { return _wordSelectedCommand; }
			set { _wordSelectedCommand = value; OnPropertyChanged();}
		}

		public Random Random { get; set; } = new Random();

		private string GenerateWord(int length)
		{
			var result = "";
			for (int i = 0; i < length; i++)
			{
				result += Alphabet[Random.Next(0, 26)];
			}
			return result.ToLowerInvariant();
		}

		public MainViewModel()
		{
			var words = new List<IWeightedWord>();
			for (int i = 0; i < 200; i++)
			{
				words.Add(new WeightedWord() {Text = GenerateWord(Random.Next(3,11)), Weight = Random.Next(1,100)});
			}

			Words = new ObservableCollection<IWeightedWord>(words.OrderByDescending(weightedWord => weightedWord.Weight));

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
