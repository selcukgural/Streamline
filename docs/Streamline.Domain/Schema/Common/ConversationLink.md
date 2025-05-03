# ConversationLink

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** İletişim Diyagramlarında (`Collaboration` içinde tanımlanır), iki farklı `Participant`'ı veya bir `Participant` ile bir `ConversationNode`'u birbirine bağlayan bir bağlantı elemanıdır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Collaboration` elemanının içinde tanımlanır.
    *   İletişim Diyagramında iki eleman (Katılımcı veya İletişim Düğümü) arasındaki bağlantıyı gösterir.
    *   Görsel olarak genellikle düz bir çizgi ile temsil edilir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Bağlantının isteğe bağlı adı.
    *   `SourceRef` (`XmlQualifiedName`, `XmlAttribute`, Zorunlu): Bağlantının başlangıç noktasındaki `Participant` veya `ConversationNode`'un ID'sine referans.
    *   `TargetRef` (`XmlQualifiedName`, `XmlAttribute`, Zorunlu): Bağlantının bitiş noktasındaki `Participant` veya `ConversationNode`'un ID'sine referans.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Participant` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.ConversationNode` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   İletişim Diyagramlarının temel bağlantı mekanizmasıdır.
    *   `SequenceFlow` (Süreç Diyagramları) ve `MessageFlow` (İşbirliği Diyagramları) ile karıştırılmamalıdır; bu sadece İletişim Diyagramlarına özgüdür. 