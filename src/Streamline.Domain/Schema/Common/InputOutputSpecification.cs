using Streamline.Domain.Schema.Data;

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tInputOutputSpecification", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("ioSpecification", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class InputOutputSpecification : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DataInput> _dataInput;
        
    [System.Xml.Serialization.XmlElementAttribute("dataInput")]
    public System.Collections.ObjectModel.Collection<DataInput> DataInput
    {
        get => _dataInput;
        private set => _dataInput = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataInput collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataInputSpecified => DataInput.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="InputOutputSpecification" /> class.</para>
    /// </summary>
    public InputOutputSpecification()
    {
        _dataInput = [];
        _dataOutput = [];
        _inputSet = [];
        _outputSet = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DataOutput> _dataOutput;
        
    [System.Xml.Serialization.XmlElementAttribute("dataOutput")]
    public System.Collections.ObjectModel.Collection<DataOutput> DataOutput
    {
        get => _dataOutput;
        private set => _dataOutput = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataOutput collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataOutputSpecified => DataOutput.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<InputSet> _inputSet;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("inputSet")]
    public System.Collections.ObjectModel.Collection<InputSet> InputSet
    {
        get => _inputSet;
        private set => _inputSet = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<OutputSet> _outputSet;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("outputSet")]
    public System.Collections.ObjectModel.Collection<OutputSet> OutputSet
    {
        get => _outputSet;
        private set => _outputSet = value;
    }
}