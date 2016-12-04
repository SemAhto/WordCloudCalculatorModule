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

            var list = new List<IDataRow>();
            //{
            //    new DataRow {Text = "Tag1", Weight = 100},
            //    new DataRow {Text = "Tag2", Weight = 80},
            //    new DataRow {Text = "Tag3", Weight = 70},
            //    new DataRow {Text = "Tag4", Weight = 69},
            //    new DataRow {Text = "Tag5", Weight = 66},
            //    new DataRow {Text = "Tag6", Weight = 63},
            //    new DataRow {Text = "Tag7", Weight = 30},
            //    new DataRow {Text = "Tag8", Weight = 30},
            //    new DataRow {Text = "Tag9", Weight = 15},
            //    new DataRow {Text = "Tag14", Weight = 10},
            //    new DataRow {Text = "Tag10", Weight = 1},
            //    new DataRow {Text = "Tag11", Weight = 0}
            //};

            int max = 3332;
            Random r = new Random();
            for (int i = 0; i < max; ++i) {
                list.Add( new DataRow { Text = "Tag" + i, Weight = (100 / ( max % 123 ) * i + 1 ) } );
            }

            //var calc = new ExtractingWordCloudCalculator<SimpleAppearenceCalculationMethod>();
            var calc = new ExtractingWordCloudCalculator<CircleAppearenceCalculationMethod>();

            var appearenaceArgs = new WordCloudAppearenceArguments()
			{
				PanelSize = new Size(Console.WindowWidth, Console.WindowHeight),
				FontSizeRange = new Range(10, 72.0),
				OpacityRange = new Range(0.24, 1.0),
				WordMargin = new Margin(0,0,0,0),
				WordSizeCalculator = GetTextMetrics
			};

			var ret = calc.Calculate(appearenaceArgs, list, row => new WeightedWord {Text = row.Text, Weight = row.Weight});

            foreach (var c in ret)
			{ 
                var lleft = Convert.ToInt32(c.Position.Left + appearenaceArgs.PanelSize.Width / 2);
                var ltop = Convert.ToInt32(c.Position.Top + appearenaceArgs.PanelSize.Height / 2);
                var lwidth = lleft + c.Size.Width;
                var lheight = ltop + c.Size.Height;
                if (!(lleft < 0 || lleft > appearenaceArgs.PanelSize.Width || lwidth > appearenaceArgs.PanelSize.Width
                    || ltop < 0 || ltop > appearenaceArgs.PanelSize.Height || lheight > appearenaceArgs.PanelSize.Height)) { 
                    Console.CursorLeft = lleft;
                    Console.CursorTop = ltop;

				    Console.ForegroundColor = (ConsoleColor.Black + 1 + (int)(15  * c.Opacity % 15));
				    Console.Write(c.Text);
                }
            }
			Console.ReadLine();
		}
	}
}
