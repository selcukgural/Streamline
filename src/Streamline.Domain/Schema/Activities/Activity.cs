#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Data;

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tActivity", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("activity", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AdHocSubProcess))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BusinessRuleTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CallActivity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ManualTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReceiveTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ScriptTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SendTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ServiceTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SubProcess))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Task))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Transaction))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(UserTask))]
public abstract partial class Activity : FlowNode
{
        
    [System.Xml.Serialization.XmlElementAttribute("ioSpecification")]
    public InputOutputSpecification IoSpecification { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Property> _property;
        
    [System.Xml.Serialization.XmlElementAttribute("property")]
    public System.Collections.ObjectModel.Collection<Property> Property
    {
        get => _property;
        private set => _property = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Property collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool PropertySpecified => Property.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Activity" /> class.</para>
    /// </summary>
    protected Activity()
    {
        _property = [];
        _dataInputAssociation = [];
        _dataOutputAssociation = [];
        _resourceRole = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DataInputAssociation> _dataInputAssociation;
        
    [System.Xml.Serialization.XmlElementAttribute("dataInputAssociation")]
    public System.Collections.ObjectModel.Collection<DataInputAssociation> DataInputAssociation
    {
        get => _dataInputAssociation;
        private set => _dataInputAssociation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataInputAssociation collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataInputAssociationSpecified => DataInputAssociation.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<DataOutputAssociation> _dataOutputAssociation;
        
    [System.Xml.Serialization.XmlElementAttribute("dataOutputAssociation")]
    public System.Collections.ObjectModel.Collection<DataOutputAssociation> DataOutputAssociation
    {
        get => _dataOutputAssociation;
        private set => _dataOutputAssociation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the DataOutputAssociation collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool DataOutputAssociationSpecified => DataOutputAssociation.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ResourceRole> _resourceRole;
        
    [System.Xml.Serialization.XmlElementAttribute("performer", Type=typeof(Performer), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("humanPerformer", Type=typeof(HumanPerformer), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("potentialOwner", Type=typeof(PotentialOwner), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("resourceRole")]
    public System.Collections.ObjectModel.Collection<ResourceRole> ResourceRole
    {
        get => _resourceRole;
        private set => _resourceRole = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ResourceRole collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ResourceRoleSpecified => ResourceRole.Count != 0;

    [System.Xml.Serialization.XmlElementAttribute("multiInstanceLoopCharacteristics", Type=typeof(MultiInstanceLoopCharacteristics), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("standardLoopCharacteristics", Type=typeof(StandardLoopCharacteristics), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("loopCharacteristics")]
    public LoopCharacteristics LoopCharacteristics { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _isForCompensation;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("isForCompensation")]
    public bool IsForCompensation
    {
        get => _isForCompensation;
        set => _isForCompensation = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _startQuantity = "1";
        
    [System.ComponentModel.DefaultValueAttribute("1")]
    [System.Xml.Serialization.XmlAttributeAttribute("startQuantity")]
    public string StartQuantity
    {
        get => _startQuantity;
        set => _startQuantity = value;
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _completionQuantity = "1";
        
    [System.ComponentModel.DefaultValueAttribute("1")]
    [System.Xml.Serialization.XmlAttributeAttribute("completionQuantity")]
    public string CompletionQuantity
    {
        get => _completionQuantity;
        set => _completionQuantity = value;
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("default")]
    public string Default { get; set; }
}