#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("Diagram", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("Diagram", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnDiagram))]
public abstract partial class Diagram
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("documentation")]
    public string Documentation { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("resolution")]
    public double Resolution { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the Resolution property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ResolutionSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("id")]
    public string Id { get; set; }
}