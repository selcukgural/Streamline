using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Events;
using Streamline.Engine.Abstractions;
using Streamline.Infrastructure.Services;

namespace Streamline.Engine.Tests;

public class BpmnXmlServiceTests
{
    private readonly IBpmnXmlService _bpmnXmlService = new BpmnXmlService();

    // A minimal valid BPMN 2.0 XML example
    private const string ValidBpmnXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<bpmn:definitions xmlns:bpmn=""http://www.omg.org/spec/BPMN/20100524/MODEL"" 
                  xmlns:bpmndi=""http://www.omg.org/spec/BPMN/20100524/DI"" 
                  xmlns:dc=""http://www.omg.org/spec/DD/20100524/DC"" 
                  xmlns:di=""http://www.omg.org/spec/DD/20100524/DI"" 
                  id=""Definitions_1"" 
                  targetNamespace=""http://bpmn.io/schema/bpmn"">
  <bpmn:process id=""Process_1"" isExecutable=""false"">
    <bpmn:startEvent id=""StartEvent_1""/>
  </bpmn:process>
  <bpmndi:BPMNDiagram id=""BPMNDiagram_1"">
    <bpmndi:BPMNPlane id=""BPMNPlane_1"" bpmnElement=""Process_1"">
      <bpmndi:BPMNShape id=""_BPMNShape_StartEvent_2"" bpmnElement=""StartEvent_1"">
        <dc:Bounds x=""173"" y=""102"" width=""36"" height=""36""/>
      </bpmndi:BPMNShape>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</bpmn:definitions>";

    // An invalid XML example
    private const string InvalidXml = @"<definitions><process></process><definitions>"; // Missing closing tag

    // Instantiate the service to be tested

    [Fact]
    public void Import_ValidXml_ReturnsDefinitionsObject()
    {
        // Act
        var definitions = _bpmnXmlService.Import(ValidBpmnXml);

        // Assert
        Assert.NotNull(definitions);
        Assert.Equal("Definitions_1", definitions.Id); // Property names might be PascalCase now
        // Check RootElement collection (assuming generated classes use this pattern)
        Assert.NotNull(definitions.RootElement);
        Assert.Single(definitions.RootElement);
        Assert.IsType<Process>(definitions.RootElement.First()); // Use actual class name (Process? tProcess?)
        var process = (Process)definitions.RootElement.First(); 
        Assert.Equal("Process_1", process.Id);
        // Check Items or specific properties within Process
        // Assuming Process class has a FlowElement collection or similar
        Assert.NotNull(process.FlowElement); // Adjust based on actual generated class structure
        Assert.Single(process.FlowElement.OfType<StartEvent>()); // Use actual class name (StartEvent? tStartEvent?)
        var startEvent = process.FlowElement.OfType<StartEvent>().First();
        Assert.Equal("StartEvent_1", startEvent.Id);
    }

    [Fact]
    public void Import_InvalidXml_ThrowsInvalidOperationException()
    {
        // Act & Assert
        // Cast to Action to avoid obsolete warning for sync methods
        var exception = Assert.Throws<InvalidOperationException>((Action)(() => _bpmnXmlService.Import(InvalidXml)));
        Assert.Contains("XML Deserialization failed", exception.Message);
        Assert.NotNull(exception.InnerException);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Import_NullOrWhiteSpaceXml_ThrowsArgumentException(string? invalidContent)
    {
        // Act & Assert
        // Cast to Action to avoid obsolete warning for sync methods
        Assert.Throws<ArgumentException>((Action)(() => _bpmnXmlService.Import(invalidContent!)));
    }

    [Fact]
    public void Export_ValidDefinitions_ReturnsCorrectXmlString()
    {
        // Arrange
        var definitions = new Definitions
        {
            Id = "Definitions_1",
            TargetNamespace = "http://bpmn.io/schema/bpmn"
            // RootElement collection is initialized in constructor, access via getter and use Add
        };

        var process = new Process
        {
            Id = "Process_1",
            IsExecutable = false,
            IsExecutableSpecified = true
            // FlowElement collection is initialized in constructor, access via getter and use Add
        };

        var startEvent = new StartEvent { Id = "StartEvent_1" };

        // Correctly add elements to the collections initialized by the constructor
        process.FlowElement.Add(startEvent);
        definitions.RootElement.Add(process);

        // Act
        var exportedXml = _bpmnXmlService.Export(definitions);

        // Assert
        Assert.NotNull(exportedXml);
        Assert.Contains("<bpmn:definitions", exportedXml);
        Assert.Contains(@"id=""Definitions_1""", exportedXml);
        Assert.Contains("<bpmn:process", exportedXml);
        Assert.Contains(@"id=""Process_1""", exportedXml);
        Assert.Contains("<bpmn:startEvent", exportedXml);
        Assert.Contains(@"id=""StartEvent_1""", exportedXml);
    }
         
    [Fact]
    public void Export_RoundTrip_ValidXml_ProducesEquivalentObject()
    {
        // Arrange: Import known valid XML first
        var initialDefinitions = _bpmnXmlService.Import(ValidBpmnXml);
        Assert.NotNull(initialDefinitions);

        // Act: Export the imported object
        var exportedXml = _bpmnXmlService.Export(initialDefinitions);

        // Assert: Import the exported XML again and compare key elements
        var roundTrippedDefinitions = _bpmnXmlService.Import(exportedXml);
        Assert.NotNull(roundTrippedDefinitions);

        // Compare key properties (adjust based on actual generated class structure)
        Assert.Equal(initialDefinitions.Id, roundTrippedDefinitions.Id);
        Assert.Equal(initialDefinitions.TargetNamespace, roundTrippedDefinitions.TargetNamespace);
        Assert.Equal(initialDefinitions.RootElement?.Count, roundTrippedDefinitions.RootElement?.Count);

        if (initialDefinitions.RootElement?.Count > 0 && roundTrippedDefinitions.RootElement?.Count > 0)
        {
            var initialProcess = initialDefinitions.RootElement.OfType<Process>().FirstOrDefault();
            var roundTrippedProcess = roundTrippedDefinitions.RootElement.OfType<Process>().FirstOrDefault();
            Assert.NotNull(initialProcess);
            Assert.NotNull(roundTrippedProcess);
            Assert.Equal(initialProcess.Id, roundTrippedProcess.Id);
                 
            // Compare process items (adjust based on actual structure)
            var initialStartEvent = initialProcess.FlowElement?.OfType<StartEvent>().FirstOrDefault();
            var roundTrippedStartEvent = roundTrippedProcess.FlowElement?.OfType<StartEvent>().FirstOrDefault();
            Assert.NotNull(initialStartEvent);
            Assert.NotNull(roundTrippedStartEvent);
            Assert.Equal(initialStartEvent.Id, roundTrippedStartEvent.Id);
        }
    }

    [Fact]
    public void Export_NullDefinitions_ThrowsArgumentNullException()
    { 
        // Act & Assert
        // Cast to Action to avoid obsolete warning for sync methods
        Assert.Throws<ArgumentNullException>((Action)(() => _bpmnXmlService.Export(null!)));
    }
}