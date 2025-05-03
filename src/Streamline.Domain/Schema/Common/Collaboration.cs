#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Streamline.Domain.Schema.Activities;
using Streamline.Domain.Schema.Artifact;
using Streamline.Domain.Schema.Artifacts;
using Streamline.Domain.Schema.Flow;

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tCollaboration", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("collaboration", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Choreography))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalChoreographyTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalConversation))]
public partial class Collaboration : RootElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Participant> _participant;
        
    [System.Xml.Serialization.XmlElementAttribute("participant")]
    public System.Collections.ObjectModel.Collection<Participant> Participant
    {
        get => _participant;
        private set => _participant = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Participant collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ParticipantSpecified => Participant.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="Collaboration" /> class.</para>
    /// </summary>
    public Collaboration()
    {
        _participant = [];
        _messageFlow = [];
        _artifact = [];
        _conversationNode = [];
        _conversationAssociation = [];
        _participantAssociation = [];
        _messageFlowAssociation = [];
        _correlationKey = [];
        _choreographyRef = [];
        _conversationLink = [];
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<MessageFlow> _messageFlow;
        
    [System.Xml.Serialization.XmlElementAttribute("messageFlow")]
    public System.Collections.ObjectModel.Collection<MessageFlow> MessageFlow
    {
        get => _messageFlow;
        private set => _messageFlow = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the MessageFlow collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool MessageFlowSpecified => MessageFlow.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Artifacts.Artifact> _artifact;
        
    [System.Xml.Serialization.XmlElementAttribute("association", Type=typeof(Association), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("group", Type=typeof(Group), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("textAnnotation", Type=typeof(TextAnnotation), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("artifact")]
    public System.Collections.ObjectModel.Collection<Artifacts.Artifact> Artifact
    {
        get => _artifact;
        private set => _artifact = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Artifact collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ArtifactSpecified => Artifact.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ConversationNode> _conversationNode;
        
    [System.Xml.Serialization.XmlElementAttribute("callConversation", Type=typeof(CallConversation), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("conversation", Type=typeof(Conversation), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("subConversation", Type=typeof(SubConversation), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("conversationNode")]
    public System.Collections.ObjectModel.Collection<ConversationNode> ConversationNode
    {
        get => _conversationNode;
        private set => _conversationNode = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ConversationNode collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ConversationNodeSpecified => ConversationNode.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ConversationAssociation> _conversationAssociation;
        
    [System.Xml.Serialization.XmlElementAttribute("conversationAssociation")]
    public System.Collections.ObjectModel.Collection<ConversationAssociation> ConversationAssociation
    {
        get => _conversationAssociation;
        private set => _conversationAssociation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ConversationAssociation collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ConversationAssociationSpecified => ConversationAssociation.Count != 0;

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

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<MessageFlowAssociation> _messageFlowAssociation;
        
    [System.Xml.Serialization.XmlElementAttribute("messageFlowAssociation")]
    public System.Collections.ObjectModel.Collection<MessageFlowAssociation> MessageFlowAssociation
    {
        get => _messageFlowAssociation;
        private set => _messageFlowAssociation = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the MessageFlowAssociation collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool MessageFlowAssociationSpecified => MessageFlowAssociation.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<CorrelationKey> _correlationKey;
        
    [System.Xml.Serialization.XmlElementAttribute("correlationKey")]
    public System.Collections.ObjectModel.Collection<CorrelationKey> CorrelationKey
    {
        get => _correlationKey;
        private set => _correlationKey = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the CorrelationKey collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool CorrelationKeySpecified => CorrelationKey.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _choreographyRef;
        
    [System.Xml.Serialization.XmlElementAttribute("choreographyRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> ChoreographyRef
    {
        get => _choreographyRef;
        private set => _choreographyRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ChoreographyRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ChoreographyRefSpecified => ChoreographyRef.Count != 0;

    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ConversationLink> _conversationLink;
        
    [System.Xml.Serialization.XmlElementAttribute("conversationLink")]
    public System.Collections.ObjectModel.Collection<ConversationLink> ConversationLink
    {
        get => _conversationLink;
        private set => _conversationLink = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ConversationLink collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ConversationLinkSpecified => ConversationLink.Count != 0;

    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private bool _isClosed;
        
    [System.ComponentModel.DefaultValueAttribute(false)]
    [System.Xml.Serialization.XmlAttributeAttribute("isClosed")]
    public bool IsClosed
    {
        get => _isClosed;
        set => _isClosed = value;
    }
}