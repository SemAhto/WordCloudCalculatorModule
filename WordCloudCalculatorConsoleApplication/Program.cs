using System;
using System.Collections.Generic;
using WordCloudCalculator.Contract;
using WordCloudCalculator.Contract.Visualization;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.ExtractingWordCloudCalculator;
using WordCloudCalculator.WordCloudCalculator;

namespace WordCloudCalculatorConsoleApplication
{
	class Program
	{
		public static Size GetTextMetrics(string text, double size) => new Size(text.Length, 1);

		static void Main(string[] args)
		{
			var list = new List<IDataRow>
			{
				new DataRow {Text = "Tag1", Weight = 100},
				new DataRow {Text = "Tag2", Weight = 80},
				new DataRow {Text = "Tag3", Weight = 70},
				new DataRow {Text = "Tag4", Weight = 69},
				new DataRow {Text = "Tag5", Weight = 66},
				new DataRow {Text = "Tag6", Weight = 63},
				new DataRow {Text = "Tag7", Weight = 30},
				new DataRow {Text = "Tag8", Weight = 30},
				new DataRow {Text = "Tag9", Weight = 15},
				new DataRow {Text = "Tag14", Weight = 10},
				new DataRow {Text = "Tag10", Weight = 1},
				new DataRow {Text = "Tag11", Weight = 0}
			};

			var calc = new ExtractingWordCloudCalculator<SimpleAppearenceCalculationMethod>();

			var appearenaceArgs = new WordCloudAppearenceArguments()
			{
				PanelSize = new Size(Console.WindowWidth, Console.WindowHeight),
				FontSizeRange = new Range(0.0, 15.0),
				OpacityRange = new Range(0.5, 1.0),
				WordMargin = Margin.None,
				WordSizeCalculator = GetTextMetrics
			};

			var ret = calc.Calculate(appearenaceArgs, list, row => new WeightedWord {Text = row.Text, Weight = row.Weight});

			foreach (var c in ret)
			{
				Console.CursorLeft = Convert.ToInt32(c.Position.Left);
				Console.CursorTop = Convert.ToInt32(c.Position.Top);
				Console.ForegroundColor = ConsoleColor.Black + Convert.ToInt32(c.Opacity * 15);
				Console.Write(c.Text);
			}
			Console.ReadLine();
		}
	}
}
