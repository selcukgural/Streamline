using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tGlobalTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("globalTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalBusinessRuleTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalManualTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalScriptTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GlobalUserTask))]
public partial class GlobalTask : CallableElement
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<ResourceRole> _resourceRole;
        
    [System.Xml.Serialization.XmlElementAttribute("performer", Type=typeof(Performer), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("humanPerformer", Type=typeof(HumanPerformer), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("potentialOwner", Type=typeof(PotentialOwner), Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
    [System.Xml.Serialization.XmlElementAttribute("resourceRole")]
    public System.Collections.ObjectModel.Collection<ResourceRole> ResourceRole
    {
        get => _resourceRole;
        private set => _resourceRole = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the ResourceRole collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ResourceRoleSpecified => ResourceRole.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="GlobalTask" /> class.</para>
    /// </summary>
    public GlobalTask()
    {
        _resourceRole = [];
    }
}