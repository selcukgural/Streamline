using Streamline.Domain.Schema.Dc;

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("Edge", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("Edge", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnEdge))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LabeledEdge))]
public abstract partial class Edge : DiagramElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Point> _waypoint;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("waypoint")]
    public System.Collections.ObjectModel.Collection<Point> Waypoint
    {
        get => _waypoint;
        private set => _waypoint = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Edge" /> class.</para>
    /// </summary>
    protected Edge()
    {
        _waypoint = [];
    }
}