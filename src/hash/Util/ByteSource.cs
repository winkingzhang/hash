//
//  ByteSource.cs
//
//  Author:
//       winkingzhang <winking.zhang@grapecity.com>
//
//  Copyright (c) 2016 copyright (c) 2014, winkingzhang
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
namespace GrapeCity.Util
{
	/// <summary>
	///  A <see cref="IByteSource"/> wraps a byte array and provides additional encoding operations. 
	/// </summary>
	public interface IByteSource
	{
		/// <summary>
		///  Returns the wrapped byte array.
		/// </summary>
		/// <returns>
		///  the wrapped byte array.
		/// </returns>
		byte [] GetBytes ();

		/// <summary>
		///  Returns the <a href="http://en.wikipedia.org/wiki/Hexadecimal">Hex</a>-formatted string representation of the
		///  underlying wrapped byte array.
		/// </summary>
		/// <returns>
		///  the <a href="http://en.wikipedia.org/wiki/Hexadecimal">Hex</a>-formatted string representation of the
		///  underlying wrapped byte array.
		/// </returns>
		string ToHex ();

		string ToBase64 ();

		bool IsEmpty ();
	}

	public static class ByteSourceExtension
	{
		public static IByteSource ToByteSource(this byte[] bytes)
		{
			return new SimpleByteSource (bytes);
		}
	}
}

