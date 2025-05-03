#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tResource", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("resource", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Resource : RootElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ResourceParameter> _resourceParameter;
        
    [System.Xml.Serialization.XmlElementAttribute("resourceParameter")]
    public System.Collections.ObjectModel.Collection<ResourceParameter> ResourceParameter
    {
        get => _resourceParameter;
        private set => _resourceParameter = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ResourceParameter collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ResourceParameterSpecified => ResourceParameter.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Resource" /> class.</para>
    /// </summary>
    public Resource()
    {
        _resourceParameter = [];
    }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}