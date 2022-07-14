using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAAddIn.Applications.ProcessDocumentationTemplate
{
    class Template
    {
        public string templateMenuName
        {
            get;
            set;
        }

        public string templateGuid
        {
            get;
            set;
        }

        public string replaceStringWhat
        {
            get;
            set;
        }

        public string destinationType
        {
            get;
            set;
        }

     public static List<Template> setTemplateValues()
     {
         List<Template> templateList = new List<Template>();


         // Set up all the values for new document templates
         Template template = new Template();

         template.templateMenuName = "Process Documentation";
         template.templateGuid = @"{DF413F22-F4C4-4c30-9BF1-99E0A000A91B}";
         template.replaceStringWhat = @"<<Batch Name>>";
         template.destinationType = "Class";

         templateList.Add(template);
         return templateList;
     }

  
    }

    
}
