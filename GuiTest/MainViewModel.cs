using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WordCloudCalculator.Contract.Word;

namespace GuiTest
{
	class MainViewModel : INotifyPropertyChanged
	{
		private ICommand _wordSelectedCommand;
		private IList<IWeightedWord> _words = new List<IWeightedWord>();

		const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public IList<IWeightedWord> Words
		{
			get { return _words; }
			set { _words = value; OnPropertyChanged();}
		}

		public ICommand WordSelectedCommand
		{
			get { return _wordSelectedCommand; }
			set { _wordSelectedCommand = value; OnPropertyChanged();}
		}

		public ICommand RefreshWords { get; set; }

		public Random U1Generator { get; set; } = new Random();
		public Random U2Generator { get; set; } = new Random();

		private string GenerateWord(int length)
		{
			var result = "";
			for (int i = 0; i < length; i++)
			{
				result += Alphabet[U1Generator.Next(0, 26)];
			}
			return result.ToLowerInvariant();
		}

		private double GenerateStandardNormalDistributedValue()
		{
			var u1 = U1Generator.NextDouble();
			var u2 = U2Generator.NextDouble();

			return Math.Sqrt(-2*Math.Log(u1))*Math.Cos(2*Math.PI*u2);
		}

		private double GenerateNormalDistributedValue(double average = 0, double deviation = 1)
		{
			return average + deviation*GenerateStandardNormalDistributedValue();
		}

		private double GenerateExpotentialDistributedValue(double lambda = 1)
		{
			var u = U1Generator.NextDouble();
			return Math.Log(1 - u)/-lambda;
		}

		private void GenerateWords(int count = 200)
		{
			var words = new List<IWeightedWord>();
			for (var i = 0; i < count; i++)
			{
				var wordLength = (int)Math.Floor(GenerateNormalDistributedValue(8, 2));
				if (wordLength < 2) wordLength = 2;
				var weight = GenerateExpotentialDistributedValue();
				words.Add(new WeightedWord() { Text = GenerateWord(wordLength), Weight = weight });
			}
			Words = words;
		}

		public MainViewModel()
		{
			GenerateWords();
			WordSelectedCommand = new RelayCommand(ExecuteTagSelectedCommand);
			RefreshWords = new RelayCommand(ExecuteRefreshWords);
		}

		private void ExecuteRefreshWords(object o)
		{
			GenerateWords();
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
