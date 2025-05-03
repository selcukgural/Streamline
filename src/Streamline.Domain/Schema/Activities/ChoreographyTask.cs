using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tChoreographyTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("choreographyTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class ChoreographyTask : ChoreographyActivity
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> _messageFlowRef;
        
    [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
    [System.Xml.Serialization.XmlElementAttribute("messageFlowRef")]
    public System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName> MessageFlowRef
    {
        get => _messageFlowRef;
        private set => _messageFlowRef = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="ChoreographyTask" /> class.</para>
    /// </summary>
    public ChoreographyTask()
    {
        _messageFlowRef = new System.Collections.ObjectModel.Collection<System.Xml.XmlQualifiedName>();
    }
}