# ConversationNode

*   **BPMN Tipi:** Soyut (Abstract) - `BaseElement`
*   **Amaç:** İletişim Diyagramlarında kullanılan elemanlar (`Conversation`, `SubConversation`, `CallConversation`) için temel soyut sınıftır. Bu elemanlar, katılımcılar arasındaki mesaj alışverişlerini gruplandırır ve yönetir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, `Conversation`, `SubConversation` ve `CallConversation` tarafından miras alınır.
    *   Bir İletişim Diyagramının (`Collaboration` içinde) parçasıdır.
    *   İlişkili olduğu katılımcıları (`Participant`), mesaj akışlarını (`MessageFlow`) ve korelasyon anahtarlarını (`CorrelationKey`) tanımlar.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): İletişim düğümünün adı.
    *   `ParticipantRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu iletişim düğümüne katılan `Participant` elemanlarının ID'lerine referanslar.
    *   `MessageFlowRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu iletişim düğümü kapsamında yer alan `MessageFlow` elemanlarının ID'lerine referanslar.
    *   `CorrelationKey` (Collection<`CorrelationKey`>, `XmlElement`): Bu iletişim bağlamındaki mesajları ilişkilendirmek için tanımlanan `CorrelationKey` elemanlarının koleksiyonu.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Participant` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Flow.MessageFlow` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.CorrelationKey`
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   İletişim Diyagramlarındaki temel yapı taşlarının ortak özelliklerini tanımlar.
    *   Soyut olduğu için doğrudan örneği oluşturulamaz. 