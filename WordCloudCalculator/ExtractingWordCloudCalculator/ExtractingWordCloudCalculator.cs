using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;

namespace WordCloudCalculator.ExtractingWordCloudCalculator
{
	/// <summary>
	/// Implements an host for a WordCloudCalculationMethod which can extract an IWeightedWord from an another DataType Instance
	/// </summary>
	public class ExtractingWordCloudCalculator : IWordCloudCalculator
	{
		private Type WordAppearenceCalculationMethodType { get; set; }
		public ExtractingWordCloudCalculator(Type wordAppearenceCalculationMethodType)
		{
			WordAppearenceCalculationMethodType = wordAppearenceCalculationMethodType;
		}
		public VisualizedWord[] Calculate<TWord>(IWordCloudAppearenceArguments args, IEnumerable<TWord> wordSource, Func<TWord, IWeightedWord> extractWord)
		{
			var appearenceCalculationMethod = Activator.CreateInstance(WordAppearenceCalculationMethodType) as IWordAppearenceCalculationMethod;
			if(appearenceCalculationMethod == null) return new VisualizedWord[0];
			appearenceCalculationMethod.Arguments = args;
			
			var result = wordSource
				.TakeWhile(word => appearenceCalculationMethod.CanAddWords)
				.Select((rawWord, index) =>
				{
					var word = extractWord(rawWord);
					if (index == 0)
						appearenceCalculationMethod.MaxWeight = word.Weight;
					return appearenceCalculationMethod.CalculateWordAppearence(word, index);
				}).ToArray();
			return result;
		}

		public VisualizedWord[] Calculate(IWordCloudAppearenceArguments args, IEnumerable<IWeightedWord> wordSource)
		{
			return Calculate(args, wordSource, word => word);
		}
	}
}