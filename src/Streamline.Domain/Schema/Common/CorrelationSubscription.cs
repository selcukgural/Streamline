#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCorrelationSubscription", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("correlationSubscription", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class CorrelationSubscription : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<CorrelationPropertyBinding> _correlationPropertyBinding;
        
    [System.Xml.Serialization.XmlElementAttribute("correlationPropertyBinding")]
    public System.Collections.ObjectModel.Collection<CorrelationPropertyBinding> CorrelationPropertyBinding
    {
        get => _correlationPropertyBinding;
        private set => _correlationPropertyBinding = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the CorrelationPropertyBinding collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool CorrelationPropertyBindingSpecified => CorrelationPropertyBinding.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="CorrelationSubscription" /> class.</para>
    /// </summary>
    public CorrelationSubscription()
    {
        _correlationPropertyBinding = [];
    }
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlAttributeAttribute("correlationKeyRef")]
    public System.Xml.XmlQualifiedName CorrelationKeyRef { get; set; }
}