using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WordCloudCalculator.Contract;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;


namespace WordCloudCalculator.ExtractingWordCloudCalculator
{
	public class SpiralAppearenceCalculationMethod : IWordAppearenceCalculationMethod
	{
		public IWordCloudAppearenceArguments Arguments { get; set; }
		public double MaxWeight { get; set; }
		public bool CanAddWords { get; private set; } = true;
		public List<Rect> Taken { get; private set; } = new List<Rect>();
		public double Phi { get; set; } = 0;

		private double CalculateRelativeValue(Range range, double current)
		{
			return range.CalculateRelativeValue(new Range(0, MaxWeight), current);
		}

		public double MaxExtension => PanelWidth > PanelHeight ? PanelWidth : PanelHeight;

		private double Rad(double degree)
		{
			return degree / 180 * Math.PI;
		}

		private const int PhiIncreaseDegree = 20;

		private double DegreeIncreaseCorrection
		{
			get
			{
				var radiusRange = new Range(0, MaxExtension);
				var partRange = new Range(0, 1);
				var part = partRange.CalculateRelativeValue(radiusRange, Radius);
				return 1 - ((Math.Log(1 - part) + 1) / (Math.E * Math.E));
			}
		}

		private Point CalculateSpiralPoint(double radius, double phi, Point basePoint)
			=> new Point(radius * Math.Cos(phi) + basePoint.X, radius * Math.Sin(phi) + basePoint.Y);

		private double PanelWidth => Arguments.PanelSize.Width;
		private double PanelHeight => Arguments.PanelSize.Height;

		private double Radius => A * Phi;

		public double A { get; set; } = 0.1;

		public bool IsRadiusOutOfBounds => Radius > PanelWidth / 2 && Radius > PanelHeight / 2;

		private bool IsRectOutOfBounds(Rect rect)
		{
			return rect.Left < 0 || rect.Top < 0 || rect.Left + rect.Width > Arguments.PanelSize.Width || rect.Top + rect.Height > Arguments.PanelSize.Height;
		}

		public VisualizedWord CalculateWordAppearence(IWeightedWord word, int itemIndex)
		{
			var fontSize = CalculateRelativeValue(Arguments.FontSizeRange, word.Weight);
			var opacity = CalculateRelativeValue(Arguments.OpacityRange, word.Weight);
			var size = Arguments.WordSizeCalculator(word.Text, fontSize);
			var visWord = new VisualizedWord(word)
			{
				FontSize = fontSize,
				Opacity = opacity,
				Size = size
			};

			//Startpunkt in der Mitte ermitteln
			var basePoint = new Point
			{
				X = Arguments.PanelSize.Width / 2 - size.Width / 2,
				Y = Arguments.PanelSize.Height / 2 - size.Height / 2
			};
			Rect rect;
			Point desiredPoint;

			do
			{
				desiredPoint = CalculateSpiralPoint(Radius, Phi, basePoint);
				rect = new Rect(desiredPoint, size);
				Phi += Rad(PhiIncreaseDegree * DegreeIncreaseCorrection);
			} while (!IsRadiusOutOfBounds && (IsRectOutOfBounds(rect) || Taken.Any(rect1 => rect.IntersectsWith(rect1))));
			// Radius Out Of Bounds is kill criterial

			Taken.Add(rect);

			visWord.Position = desiredPoint;

			if (!IsRadiusOutOfBounds) return visWord;
			CanAddWords = false;
			return null;
		}
	}
}