# Lane

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir süreç diyagramındaki `FlowNode`'ları (Aktiviteler, Olaylar, Geçitler) gruplandırmak ve sınıflandırmak için kullanılan bir bölümdür (kulvar). Genellikle rolleri, katılımcıları, organizasyonel birimleri veya sistemleri temsil etmek için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `LaneSet` içinde bulunur.
    *   Hangi `FlowNode`'ların bu kulvara ait olduğunu `flowNodeRef` özelliği ile belirtir. Bir `FlowNode` en fazla bir `Lane`'e ait olabilir.
    *   Kulvarlar hiyerarşik olabilir; bir `Lane`, kendi içinde alt kulvarları gruplayan bir `childLaneSet` içerebilir.
    *   `partitionElementRef` ile, bu kulvarın mantıksal olarak hangi elemanı (örn. bir `Participant` veya `ResourceRole`) temsil ettiğini belirtebilir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Kulvarın diyagramda görünen adı.
    *   `FlowNodeRef` (Collection<string>, `XmlElement`): Bu kulvar tarafından içerilen `FlowNode` elemanlarının ID'lerine referanslar.
    *   `ChildLaneSet` (`LaneSet`, `XmlElement`): Bu kulvarın alt kulvarlarını içeren `LaneSet` (eğer varsa).
    *   `PartitionElementRef` (`XmlQualifiedName`, `XmlAttribute`): Bu kulvarın temsil ettiği (partition ettiği) elemana (örn. `Participant`, `ResourceRole`) bir referans (isteğe bağlı).
    *   `PartitionElement` (`BaseElement`, `XmlElement`): Bölümlenen elemanı doğrudan `Lane` içine gömmek için alternatif bir yol (isteğe bağlı, `partitionElementRef` tercih edilir).
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.LaneSet` (İçinde bulunur veya içerebilir)
    *   `Streamline.Domain.Schema.Common.FlowNode` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.Participant` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.ResourceRole` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Süreç diyagramlarının okunabilirliğini ve organizasyonunu artırır.
    *   Aktivitelerin sorumluluklarını veya yerlerini görselleştirmek için kullanılır.
    *   `Participant` (Havuz/Pool) ile karıştırılmamalıdır. Havuzlar farklı süreç katılımcılarını temsil ederken, kulvarlar tek bir süreç içindeki bölümlemelerdir. 

## XML Örneği

`<lane>` elemanları bir `<laneSet>` içinde tanımlanır:

```xml
<process id="Process_OrderFulfillment" name="Order Fulfillment">
  <!-- Süreçteki FlowNode tanımları (Tasklar, Eventler vb.) -->
  <userTask id="Task_ReceiveOrder" name="Receive Order"/>
  <userTask id="Task_CheckInventory" name="Check Inventory"/>
  <userTask id="Task_ShipOrder" name="Ship Order"/>
  <serviceTask id="Task_UpdateSystem" name="Update System"/>
  <endEvent id="End_OrderComplete" name="Order Complete"/>
  <!-- SequenceFlow tanımları ... -->

  <laneSet id="LaneSet_Main">
    <lane id="Lane_Sales" name="Sales Department">
      <documentation>Handles receiving the order.</documentation>
      <flowNodeRef>Task_ReceiveOrder</flowNodeRef>
    </lane>
    <lane id="Lane_Warehouse" name="Warehouse">
      <flowNodeRef>Task_CheckInventory</flowNodeRef>
      <!-- İç içe kulvar seti -->
      <childLaneSet id="LaneSet_Shipping">
         <lane id="Lane_ShippingClerk" name="Shipping Clerk">
           <flowNodeRef>Task_ShipOrder</flowNodeRef>
         </lane>
      </childLaneSet>
    </lane>
    <lane id="Lane_System" name="System">
        <flowNodeRef>Task_UpdateSystem</flowNodeRef>
        <flowNodeRef>End_OrderComplete</flowNodeRef> <!-- Bir FlowNode birden fazla Lane'de olamaz, ama bu örnekte farklı kulvarlara dağılmış -->
    </lane>
  </laneSet>
</process>
```

**Not:** Yukarıdaki örnekte `End_OrderComplete` elemanının `Lane_System` içine konulması sadece bir örnektir. Bir `FlowNode` normalde sadece bir `Lane`'e aittir. Gerçek bir modelde, bitiş olayının belirli bir role atanması gerekmeyebilir. 