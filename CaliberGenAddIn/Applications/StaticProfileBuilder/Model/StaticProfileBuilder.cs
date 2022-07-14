using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using EA;

namespace EAAddIn.Applications.StaticProfileBuilder
{
    public class StaticProfileBuilder
    {
        private StaticProfileBuilder()
        {
            
        }
        public StaticProfileBuilder(Repository repository)
        {
            EARepository = repository;
            
            RefreshDiagram();
        }
        public string Diagram
        {
            get { return diagram == null ? string.Empty : diagram.Name; }
        }
        public Repository EARepository { get; set; }

        public XDocument Xml { get; set; }

        private Diagram diagram;

        public void RefreshDiagram()
        {
            diagram = EARepository.GetCurrentDiagram();

            if (diagram != null)
            {
                
            }
        }

        public bool CreateXMLFromDiagram()
        {
            if (string.IsNullOrEmpty(Diagram))
            {
                return false;
            }
            
            return true;
        }
    }
}
