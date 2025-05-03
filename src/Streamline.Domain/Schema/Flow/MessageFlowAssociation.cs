using Streamline.Domain.Schema.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Flow;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tMessageFlowAssociation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("messageFlowAssociation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class MessageFlowAssociation : BaseElement
{
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("innerMessageFlowRef")]
    public System.Xml.XmlQualifiedName InnerMessageFlowRef { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("outerMessageFlowRef")]
    public System.Xml.XmlQualifiedName OuterMessageFlowRef { get; set; }
}