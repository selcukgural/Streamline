#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Activities;

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCallableElement", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("callableElement", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalBusinessRuleTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalManualTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalScriptTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalUserTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Process))]
public partial class CallableElement : RootElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _supportedInterfaceRef;
        
    [System.Xml.Serialization.XmlElementAttribute("supportedInterfaceRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> SupportedInterfaceRef
    {
        get => _supportedInterfaceRef;
        private set => _supportedInterfaceRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the SupportedInterfaceRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool SupportedInterfaceRefSpecified => SupportedInterfaceRef.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="CallableElement" /> class.</para>
    /// </summary>
    public CallableElement()
    {
        _supportedInterfaceRef = new System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName>();
        _ioBinding = new System.Collections.ObjectModel.Collection<InputOutputBinding>();
    }
        
    [System.Xml.Serialization.XmlElementAttribute("ioSpecification")]
    public InputOutputSpecification IoSpecification { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<InputOutputBinding> _ioBinding;
        
    [System.Xml.Serialization.XmlElementAttribute("ioBinding")]
    public System.Collections.ObjectModel.Collection<InputOutputBinding> IoBinding
    {
        get => _ioBinding;
        private set => _ioBinding = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the IoBinding collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IoBindingSpecified => IoBinding.Count != 0;

    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}