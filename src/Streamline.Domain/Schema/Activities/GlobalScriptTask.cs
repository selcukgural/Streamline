#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tGlobalScriptTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("globalScriptTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class GlobalScriptTask : GlobalTask
{
        
    [System.Xml.Serialization.XmlElementAttribute("script")]
    public Script Script { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("scriptLanguage")]
    public string ScriptLanguage { get; set; }
}