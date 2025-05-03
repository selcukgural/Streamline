namespace Streamline.Domain.Schema.Activities;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("tTask", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("task", Namespace="http://www.omg.org/spec/BPMN/20100524/MODEL")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BusinessRuleTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ManualTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReceiveTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ScriptTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SendTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ServiceTask))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(UserTask))]
public partial class Task : Activity;