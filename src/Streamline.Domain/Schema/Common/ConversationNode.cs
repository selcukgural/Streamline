#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tConversationNode", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("conversationNode", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CallConversation))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Conversation))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SubConversation))]
public abstract partial class ConversationNode : BaseElement
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
    /// <para xml:lang="en">Initializes a new instance of the <see cref="ConversationNode" /> class.</para>
    /// </summary>
    protected ConversationNode()
    {
        _participantRef = new System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName>();
        _messageFlowRef = new System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName>();
        _correlationKey = new System.Collections.ObjectModel.Collection<CorrelationKey>();
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _messageFlowRef;
        
    [System.Xml.Serialization.XmlElementAttribute("messageFlowRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> MessageFlowRef
    {
        get => _messageFlowRef;
        private set => _messageFlowRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the MessageFlowRef collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool MessageFlowRefSpecified => MessageFlowRef.Count != 0;

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

    [System.Xml.Serialization.XmlAttributeAttribute("name")]
    public string Name { get; set; }
}