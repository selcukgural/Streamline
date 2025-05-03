# LaneSet

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir süreç (`Process`) içindeki `Lane`'leri (kulvarları) veya bir `Lane`'in alt kulvarlarını gruplayan bir koleksiyondur. Süreç diyagramındaki kulvar yapısını tanımlar.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Process` elemanının içinde (`<process>` etiketinin altında) veya bir `Lane` elemanının `childLaneSet` özelliği olarak bulunur.
    *   Bir veya daha fazla `Lane` elemanı içerir.
    *   Bir süreçte birden fazla `LaneSet` tanımlanabilir (ancak genellikle tek bir ana `LaneSet` kullanılır).
    *   Kulvarların hiyerarşik yapısını (`Lane` içindeki `childLaneSet`) oluşturmaya olanak tanır.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Kulvar setinin adı (isteğe bağlı).
    *   `Lane` (Collection<`Lane`>, `XmlElement`): Bu sete ait olan `Lane` elemanlarının koleksiyonu.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Lane`
    *   `Streamline.Domain.Schema.Common.Process` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Süreç diyagramlarındaki kulvarların organizasyonunu sağlar.
    *   Bir süreçteki tüm `FlowNode`'ların ait olduğu kulvarlar genellikle tek bir ana `LaneSet` altında tanımlanır.

## XML Örneği

`<laneSet>` genellikle bir `<process>` içinde bulunur veya hiyerarşik kulvarlar için bir `<lane>` içinde `childLaneSet` olarak kullanılır:

```xml
<process id="Process_Hiring" name="Hiring Process">
  <!-- FlowNode tanımları (Tasks, Events vb.) -->
  <userTask id="Task_SubmitApp" name="Submit Application"/>
  <userTask id="Task_ReviewApp" name="Review Application"/>
  <userTask id="Task_Interview" name="Interview Candidate"/>
  <userTask id="Task_MakeOffer" name="Make Offer"/>
  <endEvent id="End_Hired" name="Candidate Hired"/>
  <endEvent id="End_Rejected" name="Candidate Rejected"/>
  <!-- SequenceFlow tanımları ... -->

  <!-- Ana Kulvar Seti -->
  <laneSet id="LaneSet_HiringRoles">
    <lane id="Lane_Candidate" name="Candidate">
      <flowNodeRef>Task_SubmitApp</flowNodeRef>
    </lane>
    <lane id="Lane_HR" name="Human Resources">
      <flowNodeRef>Task_ReviewApp</flowNodeRef>
      <flowNodeRef>Task_Interview</flowNodeRef>
      <flowNodeRef>Task_MakeOffer</flowNodeRef>
      <flowNodeRef>End_Hired</flowNodeRef>
      <flowNodeRef>End_Rejected</flowNodeRef>
      <!-- İç içe Kulvar Seti Örneği (Lane içinde) -->
      <childLaneSet id="LaneSet_HR_Sub">
        <lane id="Lane_HR_Manager" name="HR Manager">
           <flowNodeRef>Task_MakeOffer</flowNodeRef> <!-- Örneğin, teklifi sadece müdür yapabilir -->
        </lane>
      </childLaneSet>
    </lane>
  </laneSet>

</process>
``` 