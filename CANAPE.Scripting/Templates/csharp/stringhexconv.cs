//    CANAPE Network Testing Tool
//    Copyright (C) 2014 Context Information Security
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.

// Simple example of a string + hex converter in C#

using CANAPE.Scripting;
using CANAPE.Utils;

class SimpleStringHexConverter
{
	private static byte[] DoConvert(byte[] ba)
	{
		for(int i = 0; i < ba.Length; ++i)
		{
			ba[i] ^= 42;
		}
		
		return ba;
	}
	
	public static string ConvertString(long startPos, string data)
	{
		return BinaryEncoding.Instance.GetString(
				DoConvert(BinaryEncoding.Instance.GetBytes(data))
			);
	}

	// Do the actual work	
	public static byte[] ConvertHex(long startPos, byte[] data)
	{
		return DoConvert(data);		
	}
}
