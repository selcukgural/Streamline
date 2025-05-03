#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tLinkEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("linkEventDefinition", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class LinkEventDefinition : EventDefinition
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _source;
        
    [System.Xml.Serialization.XmlElementAttribute("source")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> Source
    {
        get => _source;
        private set => _source = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Source collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool SourceSpecified => Source.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="LinkEventDefinition" /> class.</para>
    /// </summary>
    public LinkEventDefinition()
    {
        _source = [];
    }
        
    [System.Xml.Serialization.XmlElementAttribute("target")]
    public System.Xml.XmlQualifiedName Target { get; set; }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}