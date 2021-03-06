﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;

namespace WordCloudCalculator.WPF
{
	class WeightedWordToVisualizedWordConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var tags = values[0] as IEnumerable<IWeightedWord>;
			var height = System.Convert.ToDouble(values[1]);
			var width = System.Convert.ToDouble(values[2]);
			var visArgs = values[3] as IWordCloudAppearenceArguments;
			var methodType = values[4] as Type;

			if (tags != null && visArgs != null && methodType != null && height > 0 && width > 0)
			{
				visArgs.PanelSize = new Size(width, height);
				var calculator = new ExtractingWordCloudCalculator.ExtractingWordCloudCalculator(methodType);
				var result = calculator.Calculate(visArgs, tags);
				return result;
			}
			return null;
		}

		public NotifyCollectionChangedEventHandler WordsChangedEventHandler { get; set; }

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
