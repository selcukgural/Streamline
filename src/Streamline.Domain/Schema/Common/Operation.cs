#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tOperation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("operation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Operation : BaseElement
{
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("inMessageRef")]
    public System.Xml.XmlQualifiedName InMessageRef { get; set; }
        
    [System.Xml.Serialization.XmlElementAttribute("outMessageRef")]
    public System.Xml.XmlQualifiedName OutMessageRef { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _errorRef;
        
    [System.Xml.Serialization.XmlElementAttribute("errorRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> ErrorRef
    {
        get => _errorRef;
        private set => _errorRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ErrorRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ErrorRefSpecified => ErrorRef.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Operation" /> class.</para>
    /// </summary>
    public Operation()
    {
        _errorRef = [];
    }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("implementationRef")]
    public System.Xml.XmlQualifiedName ImplementationRef { get; set; }
}