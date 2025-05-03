using Streamline.Domain.Schema.Common;

namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tGlobalUserTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("globalUserTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
public partial class GlobalUserTask : GlobalTask
{
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private System.Collections.ObjectModel.Collection<Rendering> _rendering;
        
    [System.Xml.Serialization.XmlElementAttribute("rendering")]
    public System.Collections.ObjectModel.Collection<Rendering> Rendering
    {
        get => _rendering;
        private set => _rendering = value;
    }
        
    /// <summary>
    /// <para xml:lang="en">Gets a value indicating whether the Rendering collection is empty.</para>
    /// </summary>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool RenderingSpecified => Rendering.Count != 0;

    /// <summary>
    /// <para xml:lang="en">Initializes a new instance of the <see cref="GlobalUserTask" /> class.</para>
    /// </summary>
    public GlobalUserTask()
    {
        _rendering = new System.Collections.ObjectModel.Collection<Rendering>();
    }
        
    [System.Xml.Serialization.XmlIgnoreAttribute]
    private string _implementation = "##unspecified";
        
    [System.ComponentModel.DefaultValueAttribute("##unspecified")]
    [System.Xml.Serialization.XmlAttributeAttribute("implementation")]
    public string Implementation
    {
        get => _implementation;
        set => _implementation = value;
    }
}