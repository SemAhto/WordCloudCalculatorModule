namespace WordCloudCalculator.Contract.Visualization
{
	public struct Size
	{

		public Size(double width, double height)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; set; }
		public double Height { get; set; }

		public override string ToString()
		{
			return $"Width = {Width:N1}, Height = {Height:N1}";
		}
	}
}