namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("Plane", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("Plane", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnPlane))]
public abstract partial class Plane : Node
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DiagramElement> _diagramElement;
        
    [System.Xml.Serialization.XmlElementAttribute("BPMNShape", Type=typeof(BpmnShape), Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
    [System.Xml.Serialization.XmlElementAttribute("BPMNEdge", Type=typeof(BpmnEdge), Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
    [System.Xml.Serialization.XmlElementAttribute("DiagramElement")]
    public System.Collections.ObjectModel.Collection<DiagramElement> DiagramElement
    {
        get => _diagramElement;
        private set => _diagramElement = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DiagramElement collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DiagramElementSpecified => DiagramElement.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Plane" /> class.</para>
    /// </summary>
    protected Plane()
    {
        _diagramElement = [];
    }
}