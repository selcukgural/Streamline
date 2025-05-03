#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tBoundaryEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("boundaryEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class BoundaryEvent : CatchEvent
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _cancelActivity = true;
        
    [System.ComponentModel.DefaultValueAttribute(true)]
    [System.Xml.Serialization.XmlAttributeAttribute("cancelActivity")]
    public bool CancelActivity
    {
        get => _cancelActivity;
        set => _cancelActivity = value;
    }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("attachedToRef")]
    public System.Xml.XmlQualifiedName AttachedToRef { get; set; }
}