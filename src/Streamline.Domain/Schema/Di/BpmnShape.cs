#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("BPMNShape", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("BPMNShape", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
public partial class BpmnShape : LabeledShape
{
        
    [System.Xml.Serialization.XmlElementAttribute("BPMNLabel")]
    public BpmnLabel BpmnLabel { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("bpmnElement")]
    public System.Xml.XmlQualifiedName BpmnElement { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isHorizontal")]
    public bool IsHorizontal { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsHorizontal property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsHorizontalSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isExpanded")]
    public bool IsExpanded { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsExpanded property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsExpandedSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isMarkerVisible")]
    public bool IsMarkerVisible { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsMarkerVisible property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsMarkerVisibleSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isMessageVisible")]
    public bool IsMessageVisible { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsMessageVisible property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsMessageVisibleSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("participantBandKind")]
    public ParticipantBandKind ParticipantBandKind { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the ParticipantBandKind property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ParticipantBandKindSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("choreographyActivityShape")]
    public System.Xml.XmlQualifiedName ChoreographyActivityShape { get; set; }
}