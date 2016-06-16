//
//  Hex.cs
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
using System.Text;

namespace GrapeCity.Codec
{
	public static class Hex
	{
		private static readonly char [] DIGITS = {
			'0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
		};

		public static string EncodeToString (byte [] bytes)
		{
			var encodedChars = Encode (bytes);
			return new string (encodedChars);
		}

		public static char [] Encode (byte [] data)
		{

			var len = data.Length;

			var output = new char [len << 1];

			// two characters form the hex value.
			for (int i = 0, j = 0; i < len; i++) {
				output [j++] = DIGITS [(0xF0 & data [i]) >> 4];
				output [j++] = DIGITS [0x0F & data [i]];
			}

			return output;
		}

		public static byte [] Decode (byte [] array)
		{
			var str = Encoding.UTF8.GetString (array);
			return Decode (str);
		}

		public static byte [] Decode (string hex)
		{
			return Decode (hex.ToCharArray ());
		}

		public static byte [] Decode (char [] data)
		{
			int len = data.Length;

			if ((len & 0x01) != 0) {
				throw new ArgumentException ("Odd number of characters.");
			}

			byte [] output = new byte [len >> 1];

			// two characters form the hex value.
			for (int i = 0, j = 0; j < len; i++) {
				int f = ToDigit (data [j], j) << 4;
				j++;
				f = f | ToDigit (data [j], j);
				j++;
				output [i] = (byte)(f & 0xFF);
			}

			return output;
		}

		static int ToDigit (char ch, int index)
		{

			int digit = (int)char.GetNumericValue (ch);
			if (digit == -1) {
				throw new ArgumentException ("Illegal hexadecimal charcter " + ch + " at index " + index);
			}
			return digit;
		}
	}
}

