namespace Streamline.Domain.Schema.Di;

[System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1182.0")]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute("Node", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Diagnostics.DebuggerStepThroughAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("Node", Namespace="http://www.omg.org/spec/DD/20100524/DI")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnLabel))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnPlane))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BpmnShape))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Label))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LabeledShape))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Plane))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Shape))]
public abstract partial class Node : DiagramElement;