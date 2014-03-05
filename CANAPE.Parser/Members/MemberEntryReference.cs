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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CANAPE.Parser
{
    /// <summary>
    /// Class to hold a reference to another data member
    /// </summary>
    [Serializable]
    public class MemberEntryReference
    {
        private MemberEntry _container;
        private Guid[] _reference;
        private Type[] _validTypes;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">The member entry which 
        /// contains this reference</param>
        /// <param name="validTypes">Valid types which terminate the reference</param>
        public MemberEntryReference(MemberEntry container, params Type[] validTypes)
        {
            _container = container;
            _reference = new Guid[0];
            _validTypes = validTypes;
        }

        /// <summary>
        /// Constructor, uses default list of valid members for reference
        /// </summary>
        /// <param name="container">The member entry which 
        /// contains this reference</param>      
        public MemberEntryReference(MemberEntry container) : this(container, 
            typeof(IntegerPrimitiveMemberEntry))
        {

        }

        public IEnumerable<Type> ValidTypes
        {
            get { return _validTypes; }
        }

        private static Guid[] GetChainFromStrings(SequenceParserType parent, string[] strings)
        {
            List<Guid> reference = new List<Guid>();

            if (parent != null)
            {
                if (strings.Length == 0)
                {
                    throw new ArgumentException(CANAPE.Parser.Properties.Resources.MemberEntryReference_InvalidChain);
                }

                foreach (string s in strings)
                {
                    MemberEntry entry = parent.FindMember(s);

                    if (entry != null)
                    {
                        reference.Add(entry.Uuid);
                    }
                    else
                    {
                        // Could not find entry
                        throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.MemberEntryReference_CouldNotFindMember, s));
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("parent");
            }

            return reference.ToArray();
        }

        /// <summary>
        /// Is this member entry valid to end the chain?
        /// </summary>
        /// <param name="entry">The entry to test</param>
        /// <returns>True if a valid type (i.e. an integer type of some kind)</returns>
        public bool IsValidType(MemberEntry entry)
        {
            Type currType = entry.GetType();
            foreach (Type t in _validTypes)
            {
                if (t.IsAssignableFrom(currType))
                {
                    return true;
                }
            }

            return false;
        }

        private MemberEntry[] GetEntries(SequenceParserType parent, Guid[] reference)
        {            
            List<MemberEntry> entries = new List<MemberEntry>();

            if (parent != null)
            {
                if (reference.Length == 0)
                {
                    throw new ArgumentException(CANAPE.Parser.Properties.Resources.MemberEntryReference_InvalidChain);
                }

                for (int i = 0; i < reference.Length; ++i)
                {
                    MemberEntry entry = parent.FindMember(reference[i]);

                    if (entry != null)
                    {
                        entries.Add(entry);

                        if (entry.BaseType is SequenceParserType)
                        {
                            if (i < (reference.Length - 1))
                            {
                                parent = (SequenceParserType)((SequenceMemberEntry)entry).BaseType;
                            }
                            else
                            {
                                throw new ArgumentException(CANAPE.Parser.Properties.Resources.MemberEntryReference_InvalidChainMustStartWithSequences);
                            }
                        }
                        else if ((i != reference.Length - 1) || !IsValidType(entry))
                        {
                            throw new ArgumentException(CANAPE.Parser.Properties.Resources.MemberEntryReference_InvalidChainMustEndWithTypes);
                        }
                    }
                    else
                    {
                        // Could not find entry
                        throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.MemberEntryReference_CouldNotFindMember, reference[i]));
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("parent");
            }

            return entries.ToArray();
        }

        public MemberEntry GetTargetMember()
        {
            MemberEntry[] entries = null;

            try
            {
                entries = GetEntries(_container.Parent, _reference);
            }
            finally
            {
                if (entries == null)
                {
                    _reference = new Guid[0];
                }                                       
            }

            return entries.Last();
        }

        private bool IsValidChain(Guid[] reference)
        {
            bool ret = false;

            if (reference.Length > 0)
            {
                GetEntries(_container.Parent, reference);

                ret = true;
            }
           
            return ret;
        }

        public bool IsValid()
        {
            try
            {
                return IsValidChain(_reference);
            }
            catch (ArgumentException)
            {
                _reference = new Guid[0];
            }

            return false;
        }

        /// <summary>
        /// Set the reference chain of guids
        /// </summary>
        /// <param name="reference">The array of references</param>
        /// <exception cref="System.ArgumentException">Indicates an invalid reference list</exception>
        public void SetReferenceChain(Guid[] reference)
        {           
            if (IsValidChain(reference))
            {
                _reference = (Guid[])reference.Clone();
            }
        }

        /// <summary>
        /// Set reference chain from a string
        /// </summary>
        /// <param name="reference">The reference chain in dotted form</param>
        public void SetReferenceChain(string reference)
        {            
            SetReferenceChain(GetChainFromStrings(_container.Parent, reference.Split('.')));
        }

        /// <summary>
        /// Get list of names for the reference
        /// </summary>
        /// <returns>The list of fields</returns>
        public IEnumerable<string> GetNames()
        {
            List<string> strings = new List<string>();

            if (_reference.Length > 0)
            {
                try
                {
                    MemberEntry[] entries = GetEntries(_container.Parent, _reference);

                    foreach (MemberEntry entry in entries)
                    {
                        strings.Add(entry.Name);
                    }
                }
                catch (ArgumentException)
                {
                    _reference = new Guid[0];
                }
            }

            return strings;
        }

        /// <summary>
        /// Convert chain to a string
        /// </summary>
        /// <returns>The reference chain, in dotted form</returns>
        public override string ToString()
        {
            string ret = null;

            try
            {                
                ret = String.Join(".", GetNames());
            }
            catch (ArgumentException)
            {
                _reference = new Guid[0];
                ret = "";
            }

            return ret;
        }

        public override bool Equals(object obj)
        {
            MemberEntryReference reference = obj as MemberEntryReference;
            bool ret = false;

            if (reference != null)
            {
                if ((reference._container == _container) && (reference._reference.Length == _reference.Length))
                {
                    int i = 0;
                    for(i = 0; i < _reference.Length; ++i)
                    {
                        if(reference._reference[i] != _reference[i])
                        {
                            break;
                        }
                    }

                    ret = i == _reference.Length;
                }
            }

            return ret;
        }

        public override int GetHashCode()
        {
            int currCode = 0;

            foreach (Guid g in _reference)
            {
                currCode ^= g.GetHashCode();
            }

            return currCode;
        }
    }
}
