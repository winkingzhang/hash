//
//  SimpleByteSource.cs
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
using System.Linq;
using System.Text;
using GrapeCity.Codec;

namespace GrapeCity.Util
{
	public class SimpleByteSource : IByteSource
	{
		private readonly byte [] _bytes;
		private string _cachedHex;
		private string _cachedBase64;

		public SimpleByteSource (byte [] bytes)
		{
			_bytes = bytes;
		}

		public SimpleByteSource (char [] chars)
		{
			_bytes = Encoding.UTF8.GetBytes (chars);
		}

		public SimpleByteSource (string @string)
		{
			_bytes = Encoding.UTF8.GetBytes (@string);
		}

		public SimpleByteSource (IByteSource source)
		{
			_bytes = source.GetBytes ();
		}

		public byte [] GetBytes ()
		{
			return _bytes;
		}

		public bool IsEmpty ()
		{
			return _bytes == null || _bytes.Length == 0;
		}

		public string ToBase64 ()
		{
			if (string.IsNullOrEmpty (_cachedBase64)) {
				_cachedBase64 = Base64.EncodeToString (GetBytes ());
			}
			return _cachedBase64;
		}

		public string ToHex ()
		{
			if (string.IsNullOrEmpty (_cachedHex)) {
				_cachedHex = Hex.EncodeToString (GetBytes ());
			}
			return _cachedHex;
		}

		public override string ToString ()
		{
			return ToBase64 ();
		}

		public override int GetHashCode ()
		{
			if (_bytes == null || _bytes.Length == 0) {
				return 0;
			}
			return _bytes.GetHashCode ();
		}

		public override bool Equals (object obj)
		{
			if (ReferenceEquals (obj, this)) {
				return true;
			}
			if (obj is IByteSource) {
				return GetBytes ().SequenceEqual ((obj as IByteSource).GetBytes ());
			}
			return false;
		}
	}
}

