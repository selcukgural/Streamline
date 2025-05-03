# ConversationAssociation

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `ConversationNode`'u (`Conversation`, `SubConversation`, `CallConversation`) bir `Participant`'a veya başka bir `ConversationNode`'a bağlamak için kullanılan bir ilişki elemanıdır. İletişim Diyagramlarındaki soyut iletişimleri İşbirliği Diyagramlarındaki somut katılımcılarla veya diğer iletişimlerle ilişkilendirir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Genellikle bir `Collaboration` elemanının içinde tanımlanır.
    *   İki `ConversationNode` arasında veya bir `ConversationNode` ile bir `Participant` arasında bir bağlantı kurar.
    *   Görsel olarak genellikle kesikli bir çizgi ile temsil edilir.
*   **Özellikler:**
    *   `InnerConversationNodeRef` (`XmlQualifiedName`, `XmlAttribute`, Zorunlu): İlişkinin "iç" tarafındaki `ConversationNode`'un veya `Participant`'ın ID'sine referans.
    *   `OuterConversationNodeRef` (`XmlQualifiedName`, `XmlAttribute`, Zorunlu): İlişkinin "dış" tarafındaki `ConversationNode`'un veya `Participant`'ın ID'sine referans.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.ConversationNode` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.Participant` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   İletişim Diyagramları ile İşbirliği Diyagramları arasındaki bağlantıyı kurmada önemli bir rol oynar.
    *   Hangi katılımcının hangi genel iletişimde yer aldığını belirtmek için kullanılır. 