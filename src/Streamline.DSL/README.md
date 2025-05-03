# Streamline.DSL

Streamline.DSL is a domain-specific language (DSL) and parser for modeling BPMN (Business Process Model and Notation) diagrams in C#. It allows you to define, analyze, and process BPMN workflows using a custom, human-readable syntax.

## Features

- **BPMN Syntax Parsing:** Parse BPMN process definitions from custom DSL files.
- **Syntax Tree Generation:** Build strongly-typed C# syntax trees representing BPMN elements.
- **Extensible Architecture:** Easily add new BPMN elements or attributes.
- **Error Handling:** Detailed exceptions for invalid or malformed process definitions.
- **.NET 9.0 Support:** Modern C# features and nullable reference types.

## Example DSL

```bpmnsl
Proccess(name="TestProcess", id="TestProcess")
{
    StartEvent(name="Start", id="StartEvent_1")
    SequenceFlow(sourceRef="StartEvent_1", targetRef="EndEvent_1")
    UserTask(name="UserTask", id="UserTask_1")
    SequenceFlow(sourceRef="UserTask_1", targetRef="EndEvent_1")
    Gateway(name="ExclusiveGateway", id="ExclusiveGateway_1")
    EndEvent(name="End", id="EndEvent_1")
}
```

```csharp
try
{
    // Example: Loading and parsing a BPMN DSL file
    var dslText = File.ReadAllText("path/to/your/process.bpmnsl");
    var parser = new Parser(dslText);
    var syntaxTree = parser.Parse();
    Console.WriteLine(syntaxTree);
    
    foreach (var element in syntaxTree.Elements)
    {
        Console.WriteLine($"Element: {element.Kind}");
        foreach (var child in element.GetChildren())
        {
            Console.WriteLine($"  Child: {child.Kind}");
        }
    }
    
    Console.WriteLine("Done.");
    Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
```
