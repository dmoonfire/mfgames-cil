#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;
using System.Xml;

#endregion

namespace MfGames.Numerics
{
	/// <summary>
	/// Implements a fractial browning motion generator.
	/// </summary>
	public class FractalBrownianMotion
	{
		#region Constructors

		public FractalBrownianMotion()
			: this(new PerlinNoise())
		{
		}

		public FractalBrownianMotion(INoise2 noise)
		{
			this.noise = noise;
			Frequency = 0.015;
			Persistence = 0.65;
			Octaves = 8;
			Amplitude = 1;
			Density = 1;
			Coverage = 0;
		}

		#endregion

		#region Noise

		private INoise2 noise;

		/// <summary>
		/// Creates a noise value between 0 and 1 for a given X and Y coordinate.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public double GetNoise(int x, int y)
		{
			// Build up a running total and keep the frequency and amplitude
			// since those will be changing with octavtes.
			double total = 0;
			double dx = x;
			double dy = y;
			double frequency = Frequency;
			double amplitude = Amplitude;

			// Process each octave
			for (int octave = 0; octave < Octaves; octave++)
			{
				// Calculate the total for this octave
				total += Smooth(dx * frequency, dy * frequency) * amplitude;
				frequency *= 2;
				amplitude *= Persistence;
			}

			// Adjust for coverage and density
			total = (total + Coverage) * Density;

			// Clamp the values from -1 to 1
			total = Math.Max(-1, Math.Min(1, total));

			// Return the results
			return total;
		}

		#endregion

		#region Motion

		/// <summary>
		/// Uses cosine interplotion between two values with an angle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		private double Interpolate(double x, double y, double a)
		{
			double val = (1 - Math.Cos(a * Math.PI)) * 0.5;
			return x * (1 - val) + y * val;
		}

		/// <summary>
		/// Smooths out the points around a given X and Y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private double Smooth(double x, double y)
		{
			// Cast to make things easier
			var ix = (int) x;
			var iy = (int) y;

			// Get the four square points
			double n1 = noise.GetNoise(ix, iy);
			double n2 = noise.GetNoise(ix + 1, iy);
			double n3 = noise.GetNoise(ix, iy + 1);
			double n4 = noise.GetNoise(ix + 1, iy + 1);

			// Interpolate the values
			double i1 = Interpolate(n1, n2, x - ix);
			double i2 = Interpolate(n3, n4, x - ix);

			// Interpolate the final numbers
			return Interpolate(i1, i2, y - iy);
		}

		#endregion

		#region Properties

		public double Amplitude { get; set; }

		public double Coverage { get; set; }

		public double Density { get; set; }

		public double Frequency { get; set; }

		public int Octaves { get; set; }

		public double Persistence { get; set; }

		#endregion

		#region XML I/O

		/// <summary>
		/// Reads in a perlin noise generator's settings from the given stream, using tagName
		/// as the enclosing tag.
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="tagName"></param>
		public void Read(XmlReader xml, string tagName)
		{
			// Make sure we have the right tag
			if (xml.LocalName != tagName)
				throw new Exception("Expected tag " + tagName + " but got " + xml.LocalName);

			// Read in the properties
			Frequency = XmlConvert.ToDouble(xml["f"]);
			Persistence = XmlConvert.ToDouble(xml["p"]);
			Octaves = XmlConvert.ToInt32(xml["o"]);
			Amplitude = XmlConvert.ToDouble(xml["a"]);
			Density = XmlConvert.ToDouble(xml["d"]);
			Coverage = XmlConvert.ToDouble(xml["c"]);
		}

		/// <summary>
		/// Writes out the settings of the perlin noise generator to the given XML stream.
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="tagName"></param>
		public void Write(XmlWriter xml, string tagName)
		{
			if (tagName != null)
				xml.WriteStartElement(tagName);

			xml.WriteAttributeString("f", XmlConvert.ToString(Frequency));
			xml.WriteAttributeString("p", XmlConvert.ToString(Persistence));
			xml.WriteAttributeString("o", XmlConvert.ToString(Octaves));
			xml.WriteAttributeString("a", XmlConvert.ToString(Amplitude));
			xml.WriteAttributeString("d", XmlConvert.ToString(Density));
			xml.WriteAttributeString("c", XmlConvert.ToString(Coverage));

			//noise.Write(xml, "noise");

			xml.WriteEndElement();
		}

		#endregion
	}
}