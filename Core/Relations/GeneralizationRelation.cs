// NClass - Free class diagram editor
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
using NClass.Translations;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NClass.Core
{
    [Serializable]
	public sealed class GeneralizationRelation : TypeRelation
	{
		/// <exception cref="RelationException">
		/// Cannot create generalization.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="derivedType"/> is null.-or-
		/// <paramref name="baseType"/> is null.
		/// </exception>
		internal GeneralizationRelation(CompositeType derivedType, CompositeType baseType)
			: base(derivedType, baseType)
		{
			Attach();
		}
        public GeneralizationRelation(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            Attach();
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }
		public override string Name
		{
			get { return "Generalization"; }
		}

		public override bool HasLabel
		{
			get { return false; }
		}

		private CompositeType DerivedType
		{
			get { return (CompositeType) First; }
		}

		private CompositeType BaseType
		{
			get { return (CompositeType) Second; }
		}

		/// <exception cref="RelationException">
		/// Cannot finalize relationship.
		/// </exception>
		protected override void OnAttaching(EventArgs e)
		{
			base.OnAttaching(e);

			if (!DerivedType.IsAllowedChild)
				throw new RelationException(Strings.GetString("error_not_allowed_child"));
			if (!BaseType.IsAllowedParent)
				throw new RelationException(Strings.GetString("error_not_allowed_parent"));
			if (First is SingleInharitanceType && ((SingleInharitanceType) First).HasExplicitBase)
				throw new RelationException(Strings.GetString("error_multiple_bases"));
			if (First is SingleInharitanceType ^ Second is SingleInharitanceType ||
				First is InterfaceType ^ Second is InterfaceType)
				throw new RelationException(Strings.GetString("error_invalid_base"));

			if (First is SingleInharitanceType && Second is SingleInharitanceType) {
				((SingleInharitanceType) First).Base = (SingleInharitanceType) Second;
			}
			else if (First is InterfaceType && Second is InterfaceType) {
				((InterfaceType) First).AddBase((InterfaceType) Second);
			}
		}

		protected override void OnDetaching(EventArgs e)
		{
			base.OnDetaching(e);

			if (First is SingleInharitanceType)
				((SingleInharitanceType) First).Base = null;
			else if (First is InterfaceType)
				((InterfaceType) First).RemoveBase(Second as InterfaceType);
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} --> {2}",
				Strings.GetString("generalization"), First.Name, Second.Name);
		}
	}
}
