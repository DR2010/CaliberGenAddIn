using Starbase.CaliberRM.Interop;

namespace EAAddIn.Interfaces
{
    public interface IRequirementSelector
    {
        /// <summary>
        /// Returns true if the requirements associated with the node is to
        /// be included as part of some selection of requirements
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool IsSelected(IRequirementTreeNode node);
    }
}