using System;
using System.Drawing;

namespace MeshViewer
{
	public class Gradient
	{
		public Color startColor;
		public Color endColor;
		public Gradient(Color startColor, Color endColor)
		{
			this.startColor = startColor;
			this.endColor   = endColor;
		}

		public Color GetColorAtPercentage(float percentage)
		{
			var c1 = this.startColor;
			var c2 = this.endColor;
			percentage = percentage > 1 ? 1 : percentage < 0 ? 0 : percentage;
			var rAverage = c1.R + (int)((c2.R - c1.R) * percentage);
			var gAverage = c1.G + (int)((c2.G - c1.G) * percentage);
			var bAverage = c1.B + (int)((c2.B - c1.B) * percentage);
			return Color.FromArgb(rAverage, gAverage, bAverage);
		}
	}
}