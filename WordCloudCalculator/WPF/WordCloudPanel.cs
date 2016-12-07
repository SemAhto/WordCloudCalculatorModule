using System.Windows;
using System.Windows.Controls;
using WordCloudCalculator.Contract.Word;

namespace WordCloudCalculator.WPF
{
    /// <summary>
    ///     INFO: http://ikeptwalking.com/wpf-measureoverride-arrangeoverride-explained/
    ///     Responsible for arranging the given UIChildren in desired cloud shape
    /// </summary>
    public class WordCloudPanel:Panel
    {
		protected override Size MeasureOverride(Size availableSize)
		{
			var childWidth = 0d;
			var childHeight = 0d;


			// Calculate WorstCase
			foreach (ContentPresenter child in InternalChildren)
			{
				var visWord = child.Content as VisualizedWord;
				if(visWord == null) continue;
				childHeight += visWord.Size.Height;
				childWidth += visWord.Size.Width;
			}

			return new Size
			{
				Width = double.IsPositiveInfinity(availableSize.Width) ? childWidth : availableSize.Width,
				Height = double.IsPositiveInfinity(availableSize.Height) ? childHeight : availableSize.Height
			};
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (ContentPresenter child in InternalChildren)
            {
	            var visWord = child.Content as VisualizedWord;
	            if (visWord == null) continue;
				var rec = new Rect(new Point(visWord.Position.X,visWord.Position.Y), new Size(visWord.Size.Width, visWord.Size.Height));
				child.Arrange(rec);
            }
            return finalSize;
        }
    }
}