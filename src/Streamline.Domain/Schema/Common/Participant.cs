#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tParticipant", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("participant", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class Participant : BaseElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _interfaceRef;
        
    [System.Xml.Serialization.XmlElementAttribute("interfaceRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> InterfaceRef
    {
        get => _interfaceRef;
        private set => _interfaceRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the InterfaceRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool InterfaceRefSpecified => InterfaceRef.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Participant" /> class.</para>
    /// </summary>
    public Participant()
    {
        _interfaceRef = [];
        _endPointRef = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _endPointRef;
        
    [System.Xml.Serialization.XmlElementAttribute("endPointRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> EndPointRef
    {
        get => _endPointRef;
        private set => _endPointRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the EndPointRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool EndPointRefSpecified => EndPointRef.Count != 0;

    [System.Xml.Serialization.XmlElementAttribute("participantMultiplicity")]
    public ParticipantMultiplicity ParticipantMultiplicity { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlAttributeAttribute("processRef")]
    public System.Xml.XmlQualifiedName ProcessRef { get; set; }
}