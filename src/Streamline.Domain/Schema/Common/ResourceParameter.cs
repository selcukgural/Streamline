#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tResourceParameter", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("resourceParameter", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ResourceParameter : BaseElement
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("type")]
    public System.Xml.XmlQualifiedName Type { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isRequired")]
    public bool IsRequired { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsRequired property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsRequiredSpecified { get; set; }
}