namespace Streamline.Domain.Schema.Common;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tSubConversation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("subConversation", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class SubConversation : ConversationNode
{
        
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

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="SubConversation" /> class.</para>
    /// </summary>
    public SubConversation()
    {
        _conversationNode = [];
    }
}