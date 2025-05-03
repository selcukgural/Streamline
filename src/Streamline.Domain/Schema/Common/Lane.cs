#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tLane", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("lane", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Lane : BaseElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("partitionElement")]
    public BaseElement PartitionElement { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _flowNodeRef;
        
    [System.Xml.Serialization.XmlElementAttribute("flowNodeRef")]
    public System.Collections.ObjectModel.Collection<string> FlowNodeRef
    {
        get => _flowNodeRef;
        private set => _flowNodeRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the FlowNodeRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool FlowNodeRefSpecified => FlowNodeRef.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Lane" /> class.</para>
    /// </summary>
    public Lane()
    {
        _flowNodeRef = [];
    }
        
    [System.Xml.Serialization.XmlElementAttribute("childLaneSet")]
    public LaneSet ChildLaneSet { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("partitionElementRef")]
    public System.Xml.XmlQualifiedName PartitionElementRef { get; set; }
}