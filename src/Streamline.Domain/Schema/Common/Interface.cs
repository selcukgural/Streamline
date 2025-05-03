#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tInterface", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("interface", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Interface : RootElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Operation> _operation;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("operation")]
    public System.Collections.ObjectModel.Collection<Operation> Operation
    {
        get => _operation;
        private set => _operation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Interface" /> class.</para>
    /// </summary>
    public Interface()
    {
        _operation = [];
    }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("implementationRef")]
    public System.Xml.XmlQualifiedName ImplementationRef { get; set; }
}