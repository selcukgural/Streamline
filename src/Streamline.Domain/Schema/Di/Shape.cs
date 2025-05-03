#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Dc;

namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("Shape", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("Shape", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnShape))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LabeledShape))]
public abstract partial class Shape : Node
{
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("Bounds", Namespace="http://www.omg.org/spec/DD/20100524/DC")]
    public Bounds Bounds { get; set; }
}