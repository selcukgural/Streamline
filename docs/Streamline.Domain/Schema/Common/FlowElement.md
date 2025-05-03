# FlowElement

*   **BPMN Tipi:** Soyut (Abstract)
*   **Amaç:** Bir süreç (`Process`) veya koreografi (`Choreography`) akışı içinde yer alabilen temel elemanlar için soyut bir üst sınıftır. Bu elemanlar, sürecin nasıl ilerlediğini gösteren yapı taşlarıdır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, `FlowNode` (Aktiviteler, Olaylar, Geçitler), `SequenceFlow`, `DataObject`, `DataStoreReference`, `ChoreographyActivity` gibi süreç akışını oluşturan tüm görsel elemanlar tarafından miras alınır.
    *   Bir sürecin veya alt sürecin `FlowElement` koleksiyonu içinde bulunurlar.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Akış elemanının kullanıcı tarafından okunabilir adı (örn. Görev adı, Olay adı).
    *   `Auditing` (`Auditing`, `XmlElement`): Bu elemanla ilgili denetim kayıtları veya yapılandırmaları için bir alan sağlar.
    *   `Monitoring` (`Monitoring`, `XmlElement`): Süreç izleme araçları tarafından kullanılabilecek izleme ile ilgili yapılandırmalar için bir alan sağlar.
    *   `CategoryValueRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu akış elemanının ilişkili olduğu `CategoryValue` tanımlarına referanslar. Elemanları gruplamak veya sınıflandırmak için kullanılır.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Auditing`
    *   `Streamline.Domain.Schema.Common.Monitoring`
    *   `Streamline.Domain.Schema.Common.CategoryValue` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Süreç akışının temel yapısal elemanlarını gruplandırır.
    *   `Process` veya `SubProcess` gibi kapsayıcılar içinde yer alırlar.
    *   Soyut olduğu için doğrudan örneği oluşturulamaz.

## XML Örneği

`FlowElement` soyut olduğundan doğrudan XML'de bulunmaz. Ancak, ondan türeyen *tüm* somut elemanlar (örn. `<startEvent>`, `<userTask>`, `<exclusiveGateway>`, `<sequenceFlow>`, `<dataObjectReference>`) bir `<process>` veya `<subProcess>` içinde yer alır ve `FlowElement` özelliklerini (özellikle `name`) içerebilir.

```xml
<definitions ...>
  <category id="Cat_Finance" name="Financial"/>
    <categoryValue id="Val_Approval" value="Approval Step"/>
  </category>

  <process id="Proc_Example" name="Flow Element Example Process">
    <!-- FlowElement'ten türeyen eleman örnekleri -->

    <startEvent id="Start_1" name="Request Submitted">
      <documentation>Process starts when a request is submitted.</documentation>
      <monitoring/> <!-- Monitoring için özel uzantılar eklenebilir -->
      <categoryValueRef>Val_Approval</categoryValueRef>
    </startEvent>

    <sequenceFlow id="Flow_1" name="To Approval Task" sourceRef="Start_1" targetRef="Task_1"/>

    <userTask id="Task_1" name="Approve Request">
       <documentation>Requires manager approval.</documentation>
       <auditing/>
       <categoryValueRef>Val_Approval</categoryValueRef>
    </userTask>

    <dataObject id="DataObj_Request" name="Request Data" itemSubjectRef="tns:RequestType"/>

    <dataObjectReference id="DataRef_Request" name="Request Info" dataObjectRef="DataObj_Request"/>

    <exclusiveGateway id="Gateway_1" name="Decision Point"/>

    <endEvent id="End_1" name="Process Finished"/>

    <!-- Diğer SequenceFlow'lar ve elemanlar -->

  </process>
</definitions>
```

Yukarıdaki örnekte `<startEvent>`, `<sequenceFlow>`, `<userTask>`, `<dataObjectReference>`, `<exclusiveGateway>`, `<endEvent>` gibi elemanların hepsi `FlowElement`'ten türemiştir ve `name`, `documentation`, `auditing`, `monitoring`, `categoryValueRef` gibi ortak özellikleri (ve `BaseElement` özelliklerini) kullanabilirler. 