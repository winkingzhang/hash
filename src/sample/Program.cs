//
//  Program.cs
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
using GrapeCity.Crypto.Hash;
using CommandLineParser.Arguments;

namespace GrapeCity.Crypto.Hash.Sample
{
	class MainClass
	{
		public static void Main (string [] args)
		{
			var parser = new CommandLineParser.CommandLineParser ();
			var algorithm = new EnumeratedValueArgument<string> (
					'a',
					"algorithm",
					"available algorithms for computing hash, default to SHA1",
					new []{Md2Hash.ALGORITHM_NAME,
						   Md5Hash.ALGORITHM_NAME,
						   Sha1Hash.ALGORITHM_NAME,
						   Sha256Hash.ALGORITHM_NAME,
						   Sha384Hash.ALGORITHM_NAME,
						   Sha512Hash.ALGORITHM_NAME});
			algorithm.DefaultValue = Sha1Hash.ALGORITHM_NAME;
			var source = new ValueArgument<string> (
					's',
					"source",
					"source to compute hash");
			source.Optional = false;
			var salt = new ValueArgument<string> (
					't',
					"salt",
					"salt for computing hash");
			var iterations = new ValueArgument<int> (
					'i',
					"iterations",
					"iterations when compute hash");
			iterations.DefaultValue = 1;
			parser.Arguments.AddRange (
				new Argument [] { algorithm, source, salt, iterations });

			parser.ParseCommandLine (args);

			if(!source.Parsed || string.IsNullOrEmpty(source.Value)){
				parser.ShowUsage();
				return;
			}

			SimpleHash hash = null;
			if(algorithm.Parsed){
				hash = new SimpleHash (algorithm.Value, source.Value, salt.Value, iterations.Value);
			}
			if(hash == null){
				hash = new Sha1Hash (source.Value, salt.Value, iterations.Value);
			}

			Console.WriteLine ("algorithms  :" + hash.AlgorithmName);
			Console.WriteLine ("source      :" + source.Value);
			Console.WriteLine ("salt        :" + salt.Value);
			Console.WriteLine ("iterations  :" + iterations.Value);
			Console.WriteLine ("hash(hex)   :" + hash.ToHex ());
			Console.WriteLine ("hash(base64):" + hash.ToBase64 ());
		}
	}
}
