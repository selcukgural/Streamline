#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCategory", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("category", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Category : RootElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<CategoryValue> _categoryValue;
        
    [System.Xml.Serialization.XmlElementAttribute("categoryValue")]
    public System.Collections.ObjectModel.Collection<CategoryValue> CategoryValue
    {
        get => _categoryValue;
        private set => _categoryValue = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the CategoryValue collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool CategoryValueSpecified => CategoryValue.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Category" /> class.</para>
    /// </summary>
    public Category()
    {
        _categoryValue = [];
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}