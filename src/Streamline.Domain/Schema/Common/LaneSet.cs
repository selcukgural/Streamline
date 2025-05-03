#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tLaneSet", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("laneSet", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class LaneSet : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Lane> _lane;
        
    [System.Xml.Serialization.XmlElementAttribute("lane")]
    public System.Collections.ObjectModel.Collection<Lane> Lane
    {
        get => _lane;
        private set => _lane = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Lane collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool LaneSpecified => Lane.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="LaneSet" /> class.</para>
    /// </summary>
    public LaneSet()
    {
        _lane = [];
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}