# Property

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `Process`, `SubProcess` veya başka bir kapsam içindeki (örn. `LoopCharacteristics`) bir veri değişkenini temsil eder. Süreç boyunca değer tutmak ve aktarmak için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Genellikle bir `Process` veya `SubProcess` elemanının `<property>` alt etiketi olarak tanımlanır.
    *   Süreç içindeki aktiviteler tarafından `DataInputAssociation` (okuma) ve `DataOutputAssociation` (yazma) aracılığıyla erişilir.
    *   `itemSubjectRef` özniteliği ile veri tipini (`ItemDefinition`) belirtir.
    *   `DataState` elemanı ile içinde bulunabileceği durumu (state) belirtebilir (daha az yaygın kullanılır).
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Özelliğin (değişkenin) adı. Kapsamı içinde benzersiz olmalıdır.
    *   `ItemSubjectRef` (`XmlQualifiedName`, `XmlAttribute`): Bu özelliğin veri tipini ve yapısını tanımlayan `ItemDefinition` elemanının ID'sine referans (isteğe bağlı). Tip belirtmek iyi bir pratiktir.
    *   `DataState` (`DataState`, `XmlElement`): Bu özelliğin içinde bulunabileceği belirli bir durumu gösteren `DataState` elemanına referans (isteğe bağlı).
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Events.ItemDefinition` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Data.DataState` (İlişkilendirilebilir)
    *   `Streamline.Domain.Schema.Common.Process` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Activities.SubProcess` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Data.DataInputAssociation` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Data.DataOutputAssociation` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Süreç değişkenlerinin BPMN modelinde tanımlanmasını sağlar.
    *   `DataObject`'lardan farklı olarak, `Property`'ler genellikle diyagram üzerinde doğrudan görselleştirilmez, ancak sürecin veri bağlamının bir parçasıdır. 

## XML Örneği

`<property>` elemanları genellikle bir `<process>` veya `<subProcess>` içinde tanımlanır:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:xsd="http://www.w3.org/2001/XMLSchema"
             xmlns:order="http://example.com/orderdata"
             targetNamespace="http://example.com/process">

  <!-- Gerekli ItemDefinition'lar -->
  <itemDefinition id="ItemDef_String" structureRef="xsd:string"/>
  <itemDefinition id="ItemDef_Int" structureRef="xsd:int"/>
  <itemDefinition id="ItemDef_Order" structureRef="order:OrderType"/>
  <import importType="http://www.w3.org/2001/XMLSchema" location="OrderData.xsd" namespace="http://example.com/orderdata"/>

  <process id="Process_DataHandling" name="Data Handling Process">

    <!-- Property Tanımları -->
    <property id="Prop_CustomerName" name="customerName" itemSubjectRef="ItemDef_String"/>
    <property id="Prop_OrderCount" name="orderCount" itemSubjectRef="ItemDef_Int"/>
    <property id="Prop_CurrentOrder" name="currentOrder" itemSubjectRef="ItemDef_Order"/>
    <property id="Prop_Status" name="status" /> <!-- Tip belirtilmemiş -->

    <!-- Bu property'leri kullanan aktiviteler -->
    <userTask id="Task_GetData" name="Get Data">
      <ioSpecification>
        <dataOutput id="Output_Name" name="customerName_out" itemSubjectRef="ItemDef_String"/>
        <outputSet><dataOutputRefs>Output_Name</dataOutputRefs></outputSet>
      </ioSpecification>
      <dataOutputAssociation>
        <sourceRef>Output_Name</sourceRef>
        <targetRef>Prop_CustomerName</targetRef>
      </dataOutputAssociation>
    </userTask>

    <scriptTask id="Task_ProcessData" name="Process Data">
      <ioSpecification>
        <dataInput id="Input_Name" name="customerName_in" itemSubjectRef="ItemDef_String"/>
        <inputSet><dataInputRefs>Input_Name</dataInputRefs></inputSet>
      </ioSpecification>
      <dataInputAssociation>
        <sourceRef>Prop_CustomerName</sourceRef>
        <targetRef>Input_Name</targetRef>
      </dataInputAssociation>
      <!-- script... -->
    </scriptTask>

    <!-- Diğer FlowElement'ler -->

  </process>
</definitions>
``` 