#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tExtension", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("extension", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Extension
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Documentation> _documentation;
        
    [System.Xml.Serialization.XmlElementAttribute("documentation")]
    public System.Collections.ObjectModel.Collection<Documentation> Documentation
    {
        get => _documentation;
        private set => _documentation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Documentation collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DocumentationSpecified => Documentation.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Extension" /> class.</para>
    /// </summary>
    public Extension()
    {
        _documentation = [];
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("definition")]
    public System.Xml.XmlQualifiedName Definition { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _mustUnderstand;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("mustUnderstand")]
    public bool MustUnderstand
    {
        get => _mustUnderstand;
        set => _mustUnderstand = value;
    }
}