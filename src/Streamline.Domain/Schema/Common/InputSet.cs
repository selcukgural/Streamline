#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tInputSet", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("inputSet", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class InputSet : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _dataInputRefs;
        
    [System.Xml.Serialization.XmlElementAttribute("dataInputRefs")]
    public System.Collections.ObjectModel.Collection<string> DataInputRefs
    {
        get => _dataInputRefs;
        private set => _dataInputRefs = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataInputRefs collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataInputRefsSpecified => DataInputRefs.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="InputSet" /> class.</para>
    /// </summary>
    public InputSet()
    {
        _dataInputRefs = [];
        _optionalInputRefs = [];
        _whileExecutingInputRefs = [];
        _outputSetRefs = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _optionalInputRefs;
        
    [System.Xml.Serialization.XmlElementAttribute("optionalInputRefs")]
    public System.Collections.ObjectModel.Collection<string> OptionalInputRefs
    {
        get => _optionalInputRefs;
        private set => _optionalInputRefs = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the OptionalInputRefs collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool OptionalInputRefsSpecified => OptionalInputRefs.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _whileExecutingInputRefs;
        
    [System.Xml.Serialization.XmlElementAttribute("whileExecutingInputRefs")]
    public System.Collections.ObjectModel.Collection<string> WhileExecutingInputRefs
    {
        get => _whileExecutingInputRefs;
        private set => _whileExecutingInputRefs = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the WhileExecutingInputRefs collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool WhileExecutingInputRefsSpecified => WhileExecutingInputRefs.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<string> _outputSetRefs;
        
    [System.Xml.Serialization.XmlElementAttribute("outputSetRefs")]
    public System.Collections.ObjectModel.Collection<string> OutputSetRefs
    {
        get => _outputSetRefs;
        private set => _outputSetRefs = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the OutputSetRefs collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool OutputSetRefsSpecified => OutputSetRefs.Count != 0;

    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}