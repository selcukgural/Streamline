#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("BPMNEdge", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("BPMNEdge", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
public partial class BpmnEdge : LabeledEdge
{
        
    [System.Xml.Serialization.XmlElementAttribute("BPMNLabel")]
    public BpmnLabel BpmnLabel { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("bpmnElement")]
    public System.Xml.XmlQualifiedName BpmnElement { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("sourceElement")]
    public System.Xml.XmlQualifiedName SourceElement { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("targetElement")]
    public System.Xml.XmlQualifiedName TargetElement { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("messageVisibleKind")]
    public MessageVisibleKind MessageVisibleKind { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the MessageVisibleKind property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool MessageVisibleKindSpecified { get; set; }
}