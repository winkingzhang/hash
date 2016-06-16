//
//  Sha384Hash.cs
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

namespace GrapeCity.Crypto.Hash
{
	public class Sha384Hash : SimpleHash
	{
		public const string ALGORITHM_NAME = "SHA384";

		public Sha384Hash ()
			: base (ALGORITHM_NAME)
		{
		}

		public Sha384Hash (object source)
			: base (ALGORITHM_NAME, source)
		{
		}

		public Sha384Hash (object source, object salt)
			: base (ALGORITHM_NAME, source, salt)
		{
		}

		public Sha384Hash (object source, object salt, int hashIterations)
			: base (ALGORITHM_NAME, source, salt, hashIterations)
		{
		}
	}
}
