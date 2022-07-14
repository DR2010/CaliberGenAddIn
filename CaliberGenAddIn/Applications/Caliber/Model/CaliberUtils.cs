
using Starbase.CaliberRM.Interop;


namespace EAAddIn
{
    internal static class CaliberUtils
    {
        /// <summary>
        /// Comparision function used to sort projects in collection
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CompareProjects(IProject a, IProject b)
        {
            return a.Name.CompareTo(b.Name);
        }
    }
}