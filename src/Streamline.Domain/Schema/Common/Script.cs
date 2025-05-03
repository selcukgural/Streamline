#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tScript", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("script", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Script
{
        
    [System.Xml.Serialization.XmlAnyElementAttribute]
    public System.Xml.XmlElement Any { get; set; }
        
    [System.Xml.Serialization.XmlTextAttribute]
    public string[] Text { get; set; }
}