#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("BPMNDiagram", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("BPMNDiagram", Namespace="http://www.omg.org/spec/BPMN/20100524/DI")]
public partial class BpmnDiagram : Diagram
{
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("BPMNPlane")]
    public BpmnPlane BpmnPlane { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<BpmnLabelStyle> _bpmnLabelStyle;
        
    [System.Xml.Serialization.XmlElementAttribute("BPMNLabelStyle")]
    public System.Collections.ObjectModel.Collection<BpmnLabelStyle> BpmnLabelStyle
    {
        get => _bpmnLabelStyle;
        private set => _bpmnLabelStyle = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the BpmnLabelStyle collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool BpmnLabelStyleSpecified => BpmnLabelStyle.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="BpmnDiagram" /> class.</para>
    /// </summary>
    public BpmnDiagram()
    {
        _bpmnLabelStyle = [];
    }
}