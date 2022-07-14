using System;
using System.Collections.Generic;
using EAAddIn.Interfaces;
using Starbase.CaliberRM.Interop;

namespace EAAddIn
{
    public class SelectorSerialTag : IRequirementSelector
    {
        private readonly HashSet<String> _selectedRequirements = new HashSet<string>();

        public int CountSelected
        {
            get { return _selectedRequirements.Count; }
        }

        #region IRequirementSelector Members

        public bool IsSelected(IRequirementTreeNode node)
        {
            return _selectedRequirements.Contains(node.SerialNumberTag);
        }

        #endregion

        public void AddSerialTag(String tag)
        {
            _selectedRequirements.Add(tag);
        }
    }
}