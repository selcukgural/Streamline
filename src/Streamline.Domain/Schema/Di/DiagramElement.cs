#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("DiagramElement", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("DiagramElement", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnEdge))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnLabel))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnPlane))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnShape))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Edge))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Label))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LabeledEdge))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LabeledShape))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Node))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Plane))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Shape))]
public abstract partial class DiagramElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("extension")]
    public DiagramElementExtension Extension { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("id")]
    public string Id { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> _anyAttribute;
        
    [System.Xml.Serialization.XmlAnyAttributeAttribute]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> AnyAttribute
    {
        get => _anyAttribute;
        private set => _anyAttribute = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the AnyAttribute collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool AnyAttributeSpecified => AnyAttribute.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="DiagramElement" /> class.</para>
    /// </summary>
    protected DiagramElement()
    {
        _anyAttribute = [];
    }
}