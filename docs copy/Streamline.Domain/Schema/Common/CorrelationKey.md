# CorrelationKey

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement` (Genellikle `Collaboration` veya `ConversationNode` içinde tanımlanır)
*   **Amaç:** Farklı süreç örnekleri (`ProcessInstance`) arasındaki mesajlaşmalarda, belirli bir konuşma veya işbirliği bağlamını tanımlamak ve gelen mesajları doğru süreç örneğiyle eşleştirmek için kullanılan bir mekanizmadır. Bir veya daha fazla `CorrelationProperty`'den oluşur.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Genellikle bir `Collaboration` veya `ConversationNode` elemanının içinde tanımlanır.
    *   Bir veya daha fazla `CorrelationProperty`'ye referans verir. Bu özellikler, mesajların içindeki verilerden (örneğin sipariş ID, müşteri ID) elde edilir.
    *   `MessageFlow`'lar ve mesajlaşma ile ilgili olaylar (`MessageEventDefinition`, `ReceiveTask` vb.) bu anahtara referans vererek mesajların nasıl korele edileceğini belirtir.
    *   Motor, gelen bir mesajı aldığında, mesajdaki verilere dayanarak korelasyon anahtarının değerini hesaplar ve bu değere sahip olan uygun süreç örneğini bulup mesajı ona iletir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Korelasyon anahtarının adı (isteğe bağlı).
    *   `CorrelationPropertyRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu korelasyon anahtarını oluşturan `CorrelationProperty` elemanlarının ID'lerine referanslar. Bir anahtar, birden fazla özelliğin birleşimi olabilir.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.CorrelationProperty` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Özellikle uzun süren veya birden fazla etkileşim içeren B2B süreçlerinde veya karmaşık işbirliklerinde mesajların doğru hedefe yönlendirilmesi için kritiktir.
    *   Bir `CorrelationSubscription` (genellikle bir mesaj olayı veya `ReceiveTask` ile ilişkilidir), belirli bir `CorrelationKey`'e nasıl abone olunacağını ve gelen mesajlardan anahtar değerlerinin nasıl çıkarılacağını tanımlar.

## XML Örneği

`correlationKey` genellikle bir `collaboration` içinde tanımlanır:

```xml
<definitions ...>
  <!-- CorrelationProperty tanımları -->
  <correlationProperty id="Prop_CaseID" name="CaseIdentifier"/>
  <correlationProperty id="Prop_CustomerID" name="CustomerIdentifier"/>

  <collaboration id="Collab_SupportProcess" name="Customer Support Collaboration">
    <!-- Participant, MessageFlow vb. -->
    ...

    <!-- Tek özellikli Correlation Key -->
    <correlationKey id="CKey_Case" name="Case Correlation Key">
      <documentation>Correlates messages based on the unique case ID.</documentation>
      <correlationPropertyRef>Prop_CaseID</correlationPropertyRef>
    </correlationKey>

    <!-- Çok özellikli Correlation Key -->
    <correlationKey id="CKey_CustomerCase" name="Customer Case Correlation">
       <documentation>Correlates messages based on both Customer and Case ID.</documentation>
      <correlationPropertyRef>Prop_CustomerID</correlationPropertyRef>
      <correlationPropertyRef>Prop_CaseID</correlationPropertyRef>
    </correlationKey>

    ...
  </collaboration>
</definitions>
``` 