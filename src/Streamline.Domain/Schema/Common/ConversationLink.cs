#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tConversationLink", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("conversationLink", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ConversationLink : BaseElement
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("sourceRef")]
    public System.Xml.XmlQualifiedName SourceRef { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("targetRef")]
    public System.Xml.XmlQualifiedName TargetRef { get; set; }
}