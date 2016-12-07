using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WordCloudCalculator.Contract;
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
			var u2 = U1Generator.NextDouble();

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

		public MainViewModel()
		{
			var words = new List<IWeightedWord>();
			for (int i = 0; i < 200; i++)
			{
				var wordLength = (int)Math.Floor(GenerateNormalDistributedValue(4, 0.1));
				if (wordLength < 0) wordLength = 0;
				var weight = GenerateExpotentialDistributedValue();
				words.Add(new WeightedWord() {Text = GenerateWord(wordLength), Weight = weight});
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
