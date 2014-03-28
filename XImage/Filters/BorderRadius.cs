﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using XImage.Utilities;

namespace XImage.Filters
{
	public class BorderRadius : IFilter
	{
		int _radius;

		public BorderRadius() : this(10) { }

		public BorderRadius(int radius)
		{
			_radius = radius;
		}

		public void ProcessImage(XImageRequest request, XImageResponse response)
		{
			int w = response.CanvasSize.Width - 1, h = response.CanvasSize.Height - 1;
			var diameter = Math.Min(_radius * 2, Math.Min(w, h));
			var path = new GraphicsPath();

			path.AddArc(0, 0, diameter, diameter, 180, 90);
			path.AddArc(w - diameter, 0, diameter, diameter, 270, 90);
			path.AddArc(w - diameter, h - diameter, diameter, diameter, 0, 90);
			path.AddArc(0, h - diameter, diameter, diameter, 90, 90);
			path.CloseAllFigures();

			response.OutputImage.ApplyMask(path, Brushes.White, !request.Output.SupportsTransparency);
		}
	}
}