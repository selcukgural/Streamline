# Participant

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir İşbirliği (`Collaboration`) içindeki bağımsız bir katılımcıyı temsil eder. Bu, bir iş ortağı, belirli bir rol (örn. "Müşteri", "Satıcı"), bir organizasyonel birim veya bir sistem olabilir. Diyagramlarda genellikle Havuz (Pool) olarak görselleştirilir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Collaboration` elemanının `Participant` koleksiyonu içinde bulunur.
    *   `MessageFlow`'lar, farklı `Participant`'lar arasındaki iletişimi gösterir.
    *   Eğer bir iç süreci varsa, `processRef` özniteliği ile ilgili `Process` elemanına referans verir. Bu durumda havuz "white-box" olarak kabul edilir ve içindeki süreç diyagramı gösterilebilir.
    *   Eğer `processRef` belirtilmemişse, katılımcı "black-box" olarak kabul edilir ve iç detayları gösterilmez, sadece diğer katılımcılarla olan etkileşimleri (mesaj akışları) önemlidir.
    *   `participantMultiplicity` özelliği ile bu katılımcıdan birden fazla örnek olabileceği belirtilebilir (diyagramda genellikle havuz üzerinde özel bir simge ile gösterilir).
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Katılımcının (Havuzun) adı.
    *   `ProcessRef` (`XmlQualifiedName`, `XmlAttribute`): Katılımcının davranışını tanımlayan `Process` elemanının ID'sine referans (isteğe bağlı).
    *   `InterfaceRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu katılımcının sunduğu veya kullandığı `Interface`'lerin ID'lerine referanslar.
    *   `EndPointRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu katılımcıyla ilişkili `EndPoint`'lerin ID'lerine referanslar.
    *   `ParticipantMultiplicity` (`ParticipantMultiplicity`, `XmlElement`): Katılımcının çoklu örnek (multi-instance) olup olmadığını ve minimum/maksimum örnek sayısını belirtir (isteğe bağlı).
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Collaboration` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.Process` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.Interface` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.EndPoint` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.ParticipantMultiplicity`
    *   `Streamline.Domain.Schema.Flow.MessageFlow` (Bağlantı için)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   İşbirliği diyagramlarının temel yapı taşıdır.
    *   Farklı sorumluluk alanlarını veya sistem sınırlarını ayırmak için kullanılır.
    *   `Lane` (Kulvar) ile karıştırılmamalıdır. Kulvarlar tek bir süreç (`Participant`'ın içi) içindeki bölümlemelerdir.

## XML Örneği

`<participant>` elemanları bir `<collaboration>` içinde tanımlanır:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL" ...>

  <!-- Katılımcıların iç süreç tanımları (varsa) -->
  <process id="Proc_Buyer" name="Buyer Process" isExecutable="true"> ... </process>
  <process id="Proc_Seller" name="Seller Process" isExecutable="true"> ... </process>

  <collaboration id="Collab_BuySell" name="Buying and Selling Collaboration">

    <!-- 'White-box' Katılımcı (İç süreci var) -->
    <participant id="Part_Buyer" name="Buyer" processRef="Proc_Buyer">
      <documentation>Represents the customer initiating the purchase.</documentation>
    </participant>

    <!-- 'Black-box' Katılımcı (İç süreci belirtilmemiş) -->
    <participant id="Part_Shipper" name="Shipping Company"/>

    <!-- Çoklu Örnekli Katılımcı -->
    <participant id="Part_Seller" name="Seller" processRef="Proc_Seller">
      <participantMultiplicity minimum="1" maximum="10"/>
    </participant>

    <!-- Mesaj Akışları -->
    <messageFlow id="MFlow1" sourceRef="Part_Buyer" targetRef="Part_Seller" ... />
    <messageFlow id="MFlow2" sourceRef="Part_Seller" targetRef="Part_Shipper" ... />

  </collaboration>

</definitions>
``` 