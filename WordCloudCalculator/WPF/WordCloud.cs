using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;

namespace terradbtag.WordCloud
{
	public class WordCloud : Control
	{
		public static readonly DependencyProperty WordsProperty = DependencyProperty.Register("Words", typeof(ObservableCollection<IWeightedWord>), typeof(WordCloud));
		public static readonly DependencyProperty AppearenceArgumentsProperty = DependencyProperty.Register("AppearenceArguments", typeof(IWordCloudAppearenceArguments), typeof(WordCloud));
		public static readonly DependencyProperty WordSelectedCommandProperty = DependencyProperty.Register("WordSelectedCommand", typeof(ICommand), typeof(WordCloud));
		public static readonly DependencyProperty WordAppearenceCalculationMethodTypeProperty = DependencyProperty.Register("WordAppearenceCalculationMethodType", typeof(Type), typeof(WordCloud));

		public ObservableCollection<IWeightedWord> Words
		{
			get { return GetValue(WordsProperty) as ObservableCollection<IWeightedWord>;}
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
	}
}
