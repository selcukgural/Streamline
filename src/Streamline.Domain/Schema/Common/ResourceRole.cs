#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tResourceRole", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("resourceRole", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(HumanPerformer))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Performer))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PotentialOwner))]
public partial class ResourceRole : BaseElement
{
        
    [System.Xml.Serialization.XmlElementAttribute("resourceRef")]
    public System.Xml.XmlQualifiedName ResourceRef { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ResourceParameterBinding> _resourceParameterBinding;
        
    [System.Xml.Serialization.XmlElementAttribute("resourceParameterBinding")]
    public System.Collections.ObjectModel.Collection<ResourceParameterBinding> ResourceParameterBinding
    {
        get => _resourceParameterBinding;
        private set => _resourceParameterBinding = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ResourceParameterBinding collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ResourceParameterBindingSpecified => ResourceParameterBinding.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="ResourceRole" /> class.</para>
    /// </summary>
    public ResourceRole()
    {
        _resourceParameterBinding = [];
    }
        
    [System.Xml.Serialization.XmlElementAttribute("resourceAssignmentExpression")]
    public ResourceAssignmentExpression ResourceAssignmentExpression { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}