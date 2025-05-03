#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Artifact;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tTextAnnotation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("textAnnotation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class TextAnnotation : Artifacts.Artifact
{
        
    [System.Xml.Serialization.XmlElementAttribute("text")]
    public TText Text { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _textFormat = "text/plain";
        
    [System.ComponentModel.DefaultValueAttribute("text/plain")]
    [System.Xml.Serialization.XmlAttributeAttribute("textFormat")]
    public string TextFormat
    {
        get => _textFormat;
        set => _textFormat = value;
    }
}