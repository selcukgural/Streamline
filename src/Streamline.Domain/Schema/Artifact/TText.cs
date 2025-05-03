#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Artifact;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tText", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("text", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
// ReSharper disable once InconsistentNaming
public partial class TText
{
        
    [System.Xml.Serialization.XmlAnyElementAttribute]
    public System.Xml.XmlElement Any { get; set; }
        
    [System.Xml.Serialization.XmlTextAttribute]
    public string[] Text { get; set; }
}