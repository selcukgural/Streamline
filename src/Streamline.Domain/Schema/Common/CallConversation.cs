#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCallConversation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("callConversation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class CallConversation : ConversationNode
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ParticipantAssociation> _participantAssociation;
        
    [System.Xml.Serialization.XmlElementAttribute("participantAssociation")]
    public System.Collections.ObjectModel.Collection<ParticipantAssociation> ParticipantAssociation
    {
        get => _participantAssociation;
        private set => _participantAssociation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ParticipantAssociation collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ParticipantAssociationSpecified => ParticipantAssociation.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="CallConversation" /> class.</para>
    /// </summary>
    public CallConversation()
    {
        _participantAssociation = [];
    }
        
    [System.Xml.Serialization.XmlAttributeAttribute("calledCollaborationRef")]
    public System.Xml.XmlQualifiedName CalledCollaborationRef { get; set; }
}