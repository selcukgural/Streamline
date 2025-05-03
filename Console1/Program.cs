using Streamline.DSL.CodeAnalysis.Bpmn;

const string input = @"
Process(name=""Example"", id=""1"")
{
    StartEvent(name=""Start"", id=""S1"")
    {
        Task(name=""Task1"", id=""T1"")
        Task(name=""Task2"", id=""T2"")
    }
    Task(name=""Task1"", id=""T1"")
    Task(name=""Task2"", id=""T2"")
    EndEvent(name=""End"", id=""E1"")
}";

try
{
    var parser = new Parser(input);
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
