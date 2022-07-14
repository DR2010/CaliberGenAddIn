using System;
using Starbase.CaliberRM.Interop;
using starbase = Starbase.CaliberRM.Interop;

namespace EAAddIn
{
    internal class CaliberUtils
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}