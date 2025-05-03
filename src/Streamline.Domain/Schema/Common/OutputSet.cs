#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tOutputSet", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("outputSet", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class OutputSet : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _dataOutputRefs;
        
    [System.Xml.Serialization.XmlElementAttribute("dataOutputRefs")]
    public System.Collections.ObjectModel.Collection<string> DataOutputRefs
    {
        get => _dataOutputRefs;
        private set => _dataOutputRefs = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataOutputRefs collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataOutputRefsSpecified => DataOutputRefs.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="OutputSet" /> class.</para>
    /// </summary>
    public OutputSet()
    {
        _dataOutputRefs = [];
        _optionalOutputRefs = [];
        _whileExecutingOutputRefs = [];
        _inputSetRefs = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _optionalOutputRefs;
        
    [System.Xml.Serialization.XmlElementAttribute("optionalOutputRefs")]
    public System.Collections.ObjectModel.Collection<string> OptionalOutputRefs
    {
        get => _optionalOutputRefs;
        private set => _optionalOutputRefs = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the OptionalOutputRefs collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool OptionalOutputRefsSpecified => OptionalOutputRefs.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _whileExecutingOutputRefs;
        
    [System.Xml.Serialization.XmlElementAttribute("whileExecutingOutputRefs")]
    public System.Collections.ObjectModel.Collection<string> WhileExecutingOutputRefs
    {
        get => _whileExecutingOutputRefs;
        private set => _whileExecutingOutputRefs = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the WhileExecutingOutputRefs collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool WhileExecutingOutputRefsSpecified => WhileExecutingOutputRefs.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _inputSetRefs;
        
    [System.Xml.Serialization.XmlElementAttribute("inputSetRefs")]
    public System.Collections.ObjectModel.Collection<string> InputSetRefs
    {
        get => _inputSetRefs;
        private set => _inputSetRefs = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the InputSetRefs collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool InputSetRefsSpecified => InputSetRefs.Count != 0;

    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}