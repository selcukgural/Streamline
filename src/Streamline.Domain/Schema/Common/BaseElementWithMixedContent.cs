#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tBaseElementWithMixedContent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("baseElementWithMixedContent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Expression))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FormalExpression))]
public abstract partial class BaseElementWithMixedContent
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
    /// <para xml:lang="en">Initializes a new instance of the <see cref="BaseElementWithMixedContent" /> class.</para>
    /// </summary>
    protected BaseElementWithMixedContent()
    {
        _documentation = [];
        _anyAttribute = [];
    }
        
    [System.Xml.Serialization.XmlElementAttribute("extensionElements")]
    public ExtensionElements ExtensionElements { get; set; }
        
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

    [System.Xml.Serialization.XmlTextAttribute]
    public string[] Text { get; set; }
}