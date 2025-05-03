#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Flow;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tAssociation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("association", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Association : Artifacts.Artifact
{
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("sourceRef")]
    public System.Xml.XmlQualifiedName SourceRef { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("targetRef")]
    public System.Xml.XmlQualifiedName TargetRef { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private AssociationDirection _associationDirection = AssociationDirection.None;
        
    [System.ComponentModel.DefaultValueAttribute(AssociationDirection.None)]
    [System.Xml.Serialization.XmlAttributeAttribute("associationDirection")]
    public AssociationDirection AssociationDirection
    {
        get => _associationDirection;
        set => _associationDirection = value;
    }
}