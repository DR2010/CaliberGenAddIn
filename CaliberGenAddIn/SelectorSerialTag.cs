using System;
using System.Collections.Generic;
using EAAddIn.Interfaces;
using Starbase.CaliberRM.Interop;

namespace EAAddIn
{
    public class SelectorSerialTag : IRequirementSelector
    {
        private readonly HashSet<String> selectedRequirements = new HashSet<string>();

        public int CountSelected
        {
            get { return selectedRequirements.Count; }
        }

        #region IRequirementSelector Members

        public bool IsSelected(IRequirementTreeNode node)
        {
            return selectedRequirements.Contains(node.SerialNumberTag);
        }

        #endregion

        public void AddSerialTag(String tag)
        {
            selectedRequirements.Add(tag);
        }
    }
}