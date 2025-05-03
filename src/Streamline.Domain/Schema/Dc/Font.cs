#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Dc;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("Font", Namespace="http://www.omg.org/spec/DD/20100524/DC")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("Font", Namespace="http://www.omg.org/spec/DD/20100524/DC")]
public partial class Font
{
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("size")]
    public double Size { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the Size property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool SizeSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isBold")]
    public bool IsBold { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsBold property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsBoldSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isItalic")]
    public bool IsItalic { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsItalic property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsItalicSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isUnderline")]
    public bool IsUnderline { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsUnderline property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsUnderlineSpecified { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("isStrikeThrough")]
    public bool IsStrikeThrough { get; set; }
        
    /// <summary>
    /// <para xml:lang="en">Gets or sets a value indicating whether the IsStrikeThrough property is specified.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IsStrikeThroughSpecified { get; set; }
}