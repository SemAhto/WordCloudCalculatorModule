using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;

namespace WordCloudCalculator.WPF
{
	public class WordCloud : Control, INotifyPropertyChanged
	{
		public static readonly DependencyProperty WordsProperty = DependencyProperty.Register("Words", typeof(IList<IWeightedWord>), typeof(WordCloud));

		public static readonly DependencyProperty AppearenceArgumentsProperty = DependencyProperty.Register("AppearenceArguments", typeof(IWordCloudAppearenceArguments), typeof(WordCloud));
		public static readonly DependencyProperty WordSelectedCommandProperty = DependencyProperty.Register("WordSelectedCommand", typeof(ICommand), typeof(WordCloud));
		public static readonly DependencyProperty WordAppearenceCalculationMethodTypeProperty = DependencyProperty.Register("WordAppearenceCalculationMethodType", typeof(Type), typeof(WordCloud));

		public IList<IWeightedWord> Words
		{
			get { return GetValue(WordsProperty) as IList<IWeightedWord>;}
			set { SetValue(WordsProperty, value);}
		}

		public IWordCloudAppearenceArguments AppearenceArguments
		{
			get { return GetValue(AppearenceArgumentsProperty) as IWordCloudAppearenceArguments;}
			set { SetValue(AppearenceArgumentsProperty, value);}
		}

		public ICommand WordSelectedCommand
		{
			get { return GetValue(WordSelectedCommandProperty) as ICommand;}
			set { SetValue(WordSelectedCommandProperty, value);}
		}

		public Type WordAppearenceCalculationMethodType
		{
			get { return GetValue(WordAppearenceCalculationMethodTypeProperty) as Type;}
			set { SetValue(WordAppearenceCalculationMethodTypeProperty, value);}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
