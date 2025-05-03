# FlowNode

*   **BPMN Tipi:** Soyut (Abstract)
*   **Amaç:** Bir süreç akışında `SequenceFlow`'lar aracılığıyla birbirine bağlanabilen grafiksel elemanlar için temel sınıftır. Bu elemanlar, sürecin yürütme mantığını oluşturan düğüm noktalarıdır (Aktiviteler, Olaylar, Geçitler).
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `FlowElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, `Activity` (ve `Task`, `SubProcess`, `CallActivity` gibi alt türleri), `Event` (ve `StartEvent`, `EndEvent`, `IntermediateCatchEvent`, `IntermediateThrowEvent`, `BoundaryEvent` gibi alt türleri) ve `Gateway` (ve `ExclusiveGateway`, `ParallelGateway`, `InclusiveGateway`, `EventBasedGateway`, `ComplexGateway` gibi alt türleri) tarafından miras alınır.
    *   Bir `FlowNode`, gelen (`Incoming`) ve giden (`Outgoing`) `SequenceFlow`'lar aracılığıyla diğer `FlowNode`'lara bağlanır.
    *   `Lane`'ler içinde gruplanabilirler.
*   **Özellikler:**
    *   `Incoming` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu düğüme yönlendirilmiş olan `SequenceFlow` elemanlarının ID'lerine referanslar içeren bir koleksiyon.
    *   `Outgoing` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu düğümden çıkan `SequenceFlow` elemanlarının ID'lerine referanslar içeren bir koleksiyon.
    *   (`FlowElement`'ten `Name`, `Auditing`, `Monitoring`, `CategoryValueRef` ve `BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Flow.SequenceFlow` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.FlowElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Süreç akışının 'düğüm' noktalarını temsil eder.
    *   Süreç mantığının adımlarını (Aktiviteler), durum değişikliklerini (Olaylar) ve akış kontrolünü (Geçitler) modellemek için kullanılır.
    *   Soyut olduğu için doğrudan örneği oluşturulamaz.

## XML Örneği

`FlowNode` soyut olduğundan doğrudan XML'de bulunmaz. Ancak, ondan türeyen somut elemanlar (Aktiviteler, Olaylar, Geçitler) `incoming` ve `outgoing` özelliklerini kullanarak `SequenceFlow`'lar ile bağlanır.

```xml
<definitions ...>
  <process id="Proc_NodeExample" name="Flow Node Example">

    <startEvent id="StartNode" name="Start"/>

    <!-- FlowNode'dan türeyen Task -->
    <userTask id="TaskNode" name="Do Something">
      <incoming>Flow1</incoming> <!-- Gelen akış -->
      <outgoing>Flow2</outgoing> <!-- Giden akış -->
    </userTask>

    <!-- FlowNode'dan türeyen Gateway -->
    <exclusiveGateway id="GatewayNode" name="Decision">
      <incoming>Flow2</incoming> <!-- Gelen akış -->
      <outgoing>Flow3_Yes</outgoing> <!-- Giden akış 1 -->
      <outgoing>Flow3_No</outgoing> <!-- Giden akış 2 -->
    </exclusiveGateway>

    <!-- FlowNode'dan türeyen EndEvent -->
    <endEvent id="EndNode_Yes" name="End Yes">
      <incoming>Flow3_Yes</incoming> <!-- Gelen akış -->
    </endEvent>

    <endEvent id="EndNode_No" name="End No">
      <incoming>Flow3_No</incoming> <!-- Gelen akış -->
    </endEvent>

    <!-- FlowNode'ları bağlayan SequenceFlow'lar -->
    <sequenceFlow id="Flow1" sourceRef="StartNode" targetRef="TaskNode"/>
    <sequenceFlow id="Flow2" sourceRef="TaskNode" targetRef="GatewayNode"/>
    <sequenceFlow id="Flow3_Yes" sourceRef="GatewayNode" targetRef="EndNode_Yes">
      <conditionExpression xsi:type="tFormalExpression">${approved}</conditionExpression>
    </sequenceFlow>
    <sequenceFlow id="Flow3_No" sourceRef="GatewayNode" targetRef="EndNode_No">
       <conditionExpression xsi:type="tFormalExpression">${!approved}</conditionExpression>
    </sequenceFlow>

  </process>
</definitions>
```

Yukarıdaki örnekte `startEvent`, `userTask`, `exclusiveGateway` ve `endEvent` elemanları `FlowNode`'dan türemiştir ve `incoming`/`outgoing` elemanları aracılığıyla `sequenceFlow`'ların ID'lerini referans alarak akış içindeki bağlantılarını tanımlarlar. 