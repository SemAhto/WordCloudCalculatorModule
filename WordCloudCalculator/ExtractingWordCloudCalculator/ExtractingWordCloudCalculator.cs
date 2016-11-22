using System;
using System.Collections.Generic;
using System.Linq;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;

namespace WordCloudCalculator.ExtractingWordCloudCalculator
{
	/// <summary>
	/// Implements an host for a WordCloudCalculationMethod which can extract an IWeightedWord from an another DataType Instance
	/// </summary>
	/// <typeparam name="TWordAppearenceCalculationMethod"></typeparam>
	public class ExtractingWordCloudCalculator<TWordAppearenceCalculationMethod> : IWordCloudCalculator 
		where TWordAppearenceCalculationMethod: IWordAppearenceCalculationMethod, new()
	{ 
		public VisualizedWord[] Calculate<TWord>(IWordCloudAppearenceArguments args, IEnumerable<TWord> wordSource, Func<TWord, IWeightedWord> extractWord)
		{
			var appearenceCalculationMethod = new TWordAppearenceCalculationMethod()
			{
				Arguments = args
			};
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