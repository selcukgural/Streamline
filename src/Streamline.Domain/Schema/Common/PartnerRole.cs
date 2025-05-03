#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tPartnerRole", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("partnerRole", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class PartnerRole : RootElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _participantRef;
        
    [System.Xml.Serialization.XmlElementAttribute("participantRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> ParticipantRef
    {
        get => _participantRef;
        private set => _participantRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ParticipantRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ParticipantRefSpecified => ParticipantRef.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="PartnerRole" /> class.</para>
    /// </summary>
    public PartnerRole()
    {
        _participantRef = [];
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}