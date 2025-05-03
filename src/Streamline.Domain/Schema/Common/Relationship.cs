#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tRelationship", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("relationship", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Relationship : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _source;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("source")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> Source
    {
        get => _source;
        private set => _source = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Relationship" /> class.</para>
    /// </summary>
    public Relationship()
    {
        _source = [];
        _target = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _target;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("target")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> Target
    {
        get => _target;
        private set => _target = value;
    }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("type")]
    public string Type { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("direction")]
    public RelationshipDirection Direction { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the Direction property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DirectionSpecified { get; set; }
}