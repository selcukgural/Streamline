using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Events;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tEvent", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("event", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BoundaryEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CatchEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EndEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ImplicitThrowEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(IntermediateCatchEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(IntermediateThrowEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(StartEvent))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ThrowEvent))]
public abstract partial class Event : FlowNode
{
        
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
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Event" /> class.</para>
    /// </summary>
    protected Event()
    {
        _property = [];
    }
}