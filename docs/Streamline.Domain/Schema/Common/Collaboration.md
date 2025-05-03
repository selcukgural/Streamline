# Collaboration

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** İki veya daha fazla bağımsız iş ortağının (`Participant` - genellikle Havuz/Pool olarak gösterilir) belirli bir iş hedefine ulaşmak için nasıl etkileşimde bulunduğunu tanımlayan bir diyagram türünün ana kapsayıcısıdır. Katılımcıları ve aralarındaki mesaj akışlarını (`MessageFlow`) içerir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `Definitions` elemanının altında bir `RootElement` olarak tanımlanır ve bir işbirliği diyagramını temsil eder.
    *   İşbirliğine katılan farklı rolleri veya sistemleri temsil eden `Participant` elemanlarını içerir.
    *   `Participant`'lar arasındaki mesaj alışverişini gösteren `MessageFlow` elemanlarını içerir.
    *   İsteğe bağlı olarak `Artifact`'lar (Açıklamalar, Gruplar), `ConversationNode`'lar (İletişim Diyagramı elemanları), `CorrelationKey`'ler (Mesaj Korelasyonu için) gibi ek detayları barındırabilir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): İşbirliğinin adı.
    *   `IsClosed` (bool, `XmlAttribute`, Varsayılan: `false`): İşbirliğinin "kapalı" olup olmadığını belirtir. `true` ise, genellikle katılımcıların iç süreç detayları (`Process` tanımları) bu diyagramda gösterilmez, sadece katılımcılar ve aralarındaki etkileşim (mesaj akışları) odak noktasıdır. `false` ise (varsayılan), katılımcıların iç süreçleri de gösterilebilir (White-box görünüm).
    *   `Participant` (Collection<`Participant`>, `XmlElement`): İşbirliğine katılan tarafların (havuzların) koleksiyonu.
    *   `MessageFlow` (Collection<`MessageFlow`>, `XmlElement`): Katılımcılar arasındaki mesaj akışlarının koleksiyonu.
    *   `Artifact` (Collection<`Artifact`>, `XmlElement`): İşbirliği diyagramına eklenmiş `Association`, `Group`, `TextAnnotation` gibi artifact'ların koleksiyonu.
    *   `ConversationNode` (Collection<`ConversationNode`>, `XmlElement`): İşbirliği içindeki iletişim yapılarını modellemek için kullanılan `Conversation`, `SubConversation`, `CallConversation` elemanlarının koleksiyonu.
    *   `ConversationAssociation` (Collection<`ConversationAssociation`>, `XmlElement`): `ConversationNode`'ları `Participant`'lara bağlayan ilişkiler.
    *   `ParticipantAssociation` (Collection<`ParticipantAssociation`>, `XmlElement`): Farklı `Participant`'ları birbirine bağlayan ilişkiler (nadiren kullanılır).
    *   `MessageFlowAssociation` (Collection<`MessageFlowAssociation`>, `XmlElement`): `MessageFlow`'ları ilişkilendiren yapılar (nadiren kullanılır).
    *   `CorrelationKey` (Collection<`CorrelationKey`>, `XmlElement`): Katılımcılar arasındaki mesajlaşmalarda, doğru süreç örneğini veya konuşmayı tanımlamak ve ilişkilendirmek için kullanılan korelasyon anahtarları.
    *   `ChoreographyRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu işbirliğiyle ilgili olan `Choreography` tanımlarına referanslar.
    *   `ConversationLink` (Collection<`ConversationLink`>, `XmlElement`): `ConversationNode`'ları birbirine bağlayan linkler.
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Participant`, `MessageFlow`, `Artifact`, `ConversationNode`, `ConversationAssociation`, `ParticipantAssociation`, `MessageFlowAssociation`, `CorrelationKey`, `ConversationLink` gibi birçok şema elemanı.
    *   `Choreography` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   B2B (İşletmeler Arası) veya sistemler arası etkileşimleri, farklı organizasyonel birimler arasındaki işbirliğini modellemek için kullanılır.
    *   Her `Participant` genellikle bir `Process` tanımına referans verir (`processRef` özelliği ile) ve bu katılımcının işbirliği içindeki davranışını gösterir.
    *   `MessageFlow`'lar yalnızca farklı `Participant`'lar (Havuzlar) arasında çizilebilir, aynı havuz içinde kullanılamaz.

## XML Örneği

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:tns="http://example.com/order"
             targetNamespace="http://example.com/order">

  <!-- Gerekli diğer tanımlar: Process, Message, CorrelationProperty vb. -->
  <message id="Msg_OrderRequest" name="Order Request" itemRef="tns:OrderData"/>
  <message id="Msg_OrderConfirmation" name="Order Confirmation" itemRef="tns:ConfData"/>
  <correlationProperty id="Prop_OrderID" name="OrderID"/>

  <process id="Proc_Customer" name="Customer Process" isExecutable="false"/>
  <process id="Proc_Supplier" name="Supplier Process" isExecutable="false"/>

  <collaboration id="Collab_OrderHandling" name="Order Handling Collaboration">
    <documentation>Collaboration between customer and supplier for order processing.</documentation>

    <participant id="Part_Customer" name="Customer" processRef="Proc_Customer"/>
    <participant id="Part_Supplier" name="Supplier" processRef="Proc_Supplier"/>

    <messageFlow id="MFlow_Request" name="Send Order" sourceRef="Part_Customer" targetRef="Part_Supplier" messageRef="Msg_OrderRequest"/>
    <messageFlow id="MFlow_Confirm" name="Send Confirmation" sourceRef="Part_Supplier" targetRef="Part_Customer" messageRef="Msg_OrderConfirmation"/>

    <correlationKey id="CKey_Order" name="Order Correlation Key">
      <correlationPropertyRef>Prop_OrderID</correlationPropertyRef>
    </correlationKey>

    <textAnnotation id="Annot_1">
      <text>Order must be confirmed within 24 hours.</text>
    </textAnnotation>
    <association id="Assoc_1" sourceRef="MFlow_Confirm" targetRef="Annot_1"/>

  </collaboration>

</definitions>
``` 