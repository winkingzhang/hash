//
//  AbstractHash.cs
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
using System.Security.Cryptography;
using System.Text;
using GrapeCity.Codec;
using GrapeCity.Util;

namespace GrapeCity.Crypto.Hash
{
	public class SimpleHash : IHash
	{
		private const int DEFAULT_ITERATIONS = 1;
		private readonly string _algorithmName;
		private byte [] _bytes;
		private IByteSource _salt;
		private int _iterations;
		private string _hexEncoded = null;
		private string _base64Encoded = null;

		public SimpleHash (string algorithmName)
		{
			_algorithmName = algorithmName;
			_iterations = DEFAULT_ITERATIONS;
		}

		public SimpleHash (string algorithmName, object source)
			: this (algorithmName, source, null, DEFAULT_ITERATIONS)
		{
		}

		public SimpleHash (string algorithmName, object source, object salt)
			: this (algorithmName, source, salt, DEFAULT_ITERATIONS)
		{
		}

		public SimpleHash (string algorithmName, object source, object salt, int hashIterations)
		{
			if (string.IsNullOrEmpty (algorithmName)) {
				throw new ArgumentNullException (nameof (algorithmName),
												"algorithmName argument cannot be null or empty.");
			}
			_algorithmName = algorithmName;
			_iterations = Math.Max (DEFAULT_ITERATIONS, hashIterations);
			IByteSource saltBytes = null;
			if (salt != null) {
				saltBytes = ConvertSaltToBytes (salt);
				_salt = saltBytes;
			}

			var sourceBytes = ConvertSourceToBytes (source);
			ComputeHash (sourceBytes, saltBytes, hashIterations);
		}

		public string AlgorithmName {
			get { return _algorithmName; }
		}

		public IByteSource Salt {
			get { return _salt; }
		}

		public int Iterations {
			get { return _iterations; }
		}

		public byte [] GetBytes ()
		{
			return _bytes;
		}

		public void SetBytes (byte [] alreadyHashedBytes)
		{
			_bytes = alreadyHashedBytes;
			_hexEncoded = null;
			_base64Encoded = null;
		}

		public bool IsEmpty ()
		{
			return _bytes == null || _bytes.Length == 0;
		}

		protected IByteSource ConvertSourceToBytes (object source)
		{
			return ToByteSource (source);
		}

		protected IByteSource ConvertSaltToBytes (object salt)
		{
			return ToByteSource (salt);
		}

		protected IByteSource ToByteSource (Object o)
		{
			if (o == null) {
				return null;
			}
			if (o is IByteSource) {
				return (IByteSource)o;
			}
			byte [] bytes = ToBytes (o);
			return new SimpleByteSource (bytes);
		}

		protected byte [] ToBytes (object o)
		{
			if (o == null) {
				throw new ArgumentNullException (nameof (o),
												 "Argument for byte conversion cannot be null.");
			}
			if (o is byte []) {
				return (byte [])o;
			}
			if (o is IByteSource) {
				return ((IByteSource)o).GetBytes ();
			}
			if (o is char []) {
				return Encoding.UTF8.GetBytes ((char [])o);
			}
			if (o is string) {
				return Encoding.UTF8.GetBytes ((string)o);
			}
			throw new NotSupportedException ();
		}

		protected HashAlgorithm GetHashAlgorithm (string algorithmName)
		{
			return HashAlgorithm.Create (algorithmName);
		}

		protected void ComputeHash (IByteSource source, IByteSource salt, int hashIterations)
		{
			byte [] saltBytes = salt != null ? salt.GetBytes () : null;
			byte [] hashedBytes = ComputeHash (source.GetBytes (), saltBytes, hashIterations);
			SetBytes (hashedBytes);
		}

		protected byte [] ComputeHash (byte [] bytes)
		{
			return ComputeHash (bytes, null, 1);
		}

		protected byte [] ComputeHash (byte [] bytes, byte [] salt)
		{
			return ComputeHash (bytes, salt, 1);
		}

		protected byte [] ComputeHash (byte [] bytes, byte [] salt, int hashIterations)
		{
			var digest = GetHashAlgorithm (AlgorithmName);
			if (salt != null) {
				digest.Initialize ();
				var buffer = new byte [salt.Length];
				digest.TransformBlock (salt, 0, salt.Length, buffer, 0);
			}
			digest.TransformFinalBlock (bytes, 0, bytes.Length);
			var hashed = digest.Hash;

			var iterations = hashIterations - 1; //already hashed once above

			//iterate remaining number:
			for (int i = 0; i < iterations; i++) {
				digest.Initialize ();
				hashed = digest.TransformFinalBlock (hashed, 0, hashed.Length);
			}
			return hashed;
		}

		public string ToHex ()
		{
			if (_hexEncoded == null) {
				_hexEncoded = Hex.EncodeToString (GetBytes ());
			}
			return _hexEncoded;
		}

		public string ToBase64 ()
		{
			if (_base64Encoded == null) {
				_base64Encoded = Base64.EncodeToString (GetBytes ());
			}
			return _base64Encoded;
		}

		public override string ToString ()
		{
			return ToHex ();
		}

		public override bool Equals (object obj)
		{
			if (obj is IHash) {
				var other = obj as IHash;
				return GetBytes ().SequenceEqual (other.GetBytes ());
			}
			return false;
		}

		public override int GetHashCode ()
		{
			if (_bytes == null || _bytes.Length == 0) {
				return 0;
			}
			return _bytes.GetHashCode ();
		}
	}
}

