﻿// NClass - Free class diagram editor
// Copyright (C) 2006-2007 Balazs Tihanyi
// 
// This program is free software; you can redistribute it and/or modify it under 
// the terms of the GNU General Public License as published by the Free Software 
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT 
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// this program; if not, write to the Free Software Foundation, Inc., 
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Text.RegularExpressions;
using NClass.Core;
using NClass.Translations;

namespace NClass.CSharp
{
    [Serializable]
	internal class CSharpArgumentList : ArgumentList
	{
		// [<modifiers>] <type> <name> [,]
		const string ParameterPattern =
			@"\s*(?<modifier>out|ref|params)?(?(modifier)\s+|)" +
			@"(?<type>" + CSharpLanguage.GenericTypePattern2 + @")\s+" +
			@"(?<name>" + CSharpLanguage.NamePattern + @")\s*(,|$)";
		const string ParameterStringPattern = @"^\s*(" + ParameterPattern + ")*$";

		static Regex parameterRegex =
			new Regex(ParameterPattern, RegexOptions.ExplicitCapture);
		static Regex singleParamterRegex =
			new Regex("^" + ParameterPattern + "$", RegexOptions.ExplicitCapture);
		static Regex parameterStringRegex =
			new Regex(ParameterStringPattern, RegexOptions.ExplicitCapture);

		internal CSharpArgumentList()
		{
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is null.
		/// </exception>
		private CSharpArgumentList(CSharpArgumentList collection)
			: base(collection)
		{
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The parameter name is already exists.
		/// </exception>
		public override Parameter Add(string declaration)
		{
			Match match = singleParamterRegex.Match(declaration);

			if (match.Success) {
				Group nameGroup = match.Groups["name"];
				Group typeGroup = match.Groups["type"];
				Group modifierGroup = match.Groups["modifier"];

				if (IsReservedName(nameGroup.Value))
					throw new ReservedNameException(nameGroup.Value);

				Parameter parameter = new CSharpParameter(nameGroup.Value, typeGroup.Value,
					ParseParameterModifier(modifierGroup.Value));
				InnerList.Add(parameter);

				return parameter;
			}
			else {
				throw new BadSyntaxException(
					Strings.GetString("error_invalid_parameter_declaration"));
			}
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The parameter name is already exists.
		/// </exception>
		public override Parameter ModifyParameter(Parameter parameter, string declaration)
		{
			Match match = singleParamterRegex.Match(declaration);
			int index = InnerList.IndexOf(parameter);

			if (index < 0)
				return parameter;

			if (match.Success) {
				Group nameGroup = match.Groups["name"];
				Group typeGroup = match.Groups["type"];
				Group modifierGroup = match.Groups["modifier"];

				if (IsReservedName(nameGroup.Value, index))
					throw new ReservedNameException(nameGroup.Value);

				Parameter newParameter = new CSharpParameter(nameGroup.Value, typeGroup.Value,
					ParseParameterModifier(modifierGroup.Value));
				InnerList[index] = newParameter;
				return newParameter;
			}
			else {
				throw new BadSyntaxException(
					Strings.GetString("error_invalid_parameter_declaration"));
			}
		}

		private ParameterModifier ParseParameterModifier(string modifierString)
		{
			switch (modifierString) {
				case "ref":
					return ParameterModifier.Inout;

				case "out":
					return ParameterModifier.Out;

				case "params":
					return ParameterModifier.Params;

				case "in":
				default:
					return ParameterModifier.In;
			}
		}

		public override object Clone()
		{
			return new CSharpArgumentList(this);
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public override void InitFromString(string declaration)
		{
			if (parameterStringRegex.IsMatch(declaration)) {
				Clear();
				foreach (Match match in parameterRegex.Matches(declaration)) {
					Group nameGroup = match.Groups["name"];
					Group typeGroup = match.Groups["type"];
					Group modifierGroup = match.Groups["modifier"];

					InnerList.Add(new CSharpParameter(nameGroup.Value, typeGroup.Value,
						ParseParameterModifier(modifierGroup.Value)));
				}
			}
			else {
				throw new BadSyntaxException(
					Strings.GetString("error_invalid_parameter_declaration"));
			}
		}
	}
}
