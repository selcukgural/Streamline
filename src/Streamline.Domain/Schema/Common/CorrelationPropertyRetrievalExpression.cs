#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCorrelationPropertyRetrievalExpression", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("correlationPropertyRetrievalExpression", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class CorrelationPropertyRetrievalExpression : BaseElement
{
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("messagePath")]
    public FormalExpression MessagePath { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("messageRef")]
    public System.Xml.XmlQualifiedName MessageRef { get; set; }
}