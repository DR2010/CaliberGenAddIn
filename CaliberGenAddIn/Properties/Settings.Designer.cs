﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EAAddIn.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LastCaliberProject {
            get {
                return ((string)(this["LastCaliberProject"]));
            }
            set {
                this["LastCaliberProject"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=DRPSQL007\\SQL07;Initial Catalog=EA_CaliberCoolgen;Integrated Security" +
            "=True")]
        public string EA_CaliberCoolgenConnectionString {
            get {
                return ((string)(this["EA_CaliberCoolgenConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=DRPSQL007\\SQL07;Initial Catalog=EA_Release1;Integrated Security=True")]
        public string EA_Release1ConnectionString {
            get {
                return ((string)(this["EA_Release1ConnectionString"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<None>")]
        public string HierarchyChildView {
            get {
                return ((string)(this["HierarchyChildView"]));
            }
            set {
                this["HierarchyChildView"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SQLServerScriptGeneratorConnectionString {
            get {
                return ((string)(this["SQLServerScriptGeneratorConnectionString"]));
            }
            set {
                this["SQLServerScriptGeneratorConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LastAddIn1 {
            get {
                return ((string)(this["LastAddIn1"]));
            }
            set {
                this["LastAddIn1"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LastAddIn2 {
            get {
                return ((string)(this["LastAddIn2"]));
            }
            set {
                this["LastAddIn2"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LastAddIn3 {
            get {
                return ((string)(this["LastAddIn3"]));
            }
            set {
                this["LastAddIn3"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DEWR, DEWR, DD-ARN-20-D000496, cc4967777c49a3ff")]
        public string DataDynamicsARLic {
            get {
                return ((string)(this["DataDynamicsARLic"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Dsn=ea local;uid=WL0035;app=Microsoft® Visual Studio® 2010;wsid=WDE307669;databas" +
            "e=EA_LOCAL;trusted_connection=Yes")]
        public string EA_LOCALConnectionString {
            get {
                return ((string)(this["EA_LOCALConnectionString"]));
            }
        }
    }
}
