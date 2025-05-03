# Association

*   **BPMN Tipi:** Somut (Concrete) - `Artifact`
*   **Amaç:** Bir `Artifact` (örn. `TextAnnotation`, `DataObjectReference`) ile bir `FlowElement` (örn. `Activity`, `Event`, `Gateway`) arasında yönlü olmayan bir ilişki kurmak için kullanılır. Genellikle bir açıklamanın hangi elemana ait olduğunu veya bir veri nesnesinin hangi aktivite tarafından kullanıldığını/üretildiğini göstermek için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Flow` (Ancak `Artifact`'tan miras aldığı için Artifacts altında da düşünülebilir, spesifikasyon bunu Flow altında gruplar)
*   **Miras:** `Artifacts.Artifact` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir kaynak (`SourceRef`) ve bir hedef (`TargetRef`) elemanı birbirine bağlar. Bu elemanlardan biri genellikle bir `Artifact` olur.
    *   İsteğe bağlı olarak ilişkinin yönünü (`AssociationDirection`) belirtebilir, ancak genellikle yönsüzdür (`None`).
*   **Özellikler:**
    *   `SourceRef` (`XmlQualifiedName`, `XmlAttribute`, **Zorunlu**): İlişkinin başladığı elemanın ID'si.
    *   `TargetRef` (`XmlQualifiedName`, `XmlAttribute`, **Zorunlu**): İlişkinin bağlandığı elemanın ID'si.
    *   `AssociationDirection` (`AssociationDirection` enum, `XmlAttribute`, Varsayılan: `None`): İlişkinin yönünü belirtir (`None`, `One`, `Both`). `One` genellikle kaynaktan hedefe doğru bir ok ucuyla gösterilir. `Both` nadiren kullanılır.
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Kaynak ve hedef referanslarını tutmak için.
    *   `Streamline.Domain.Schema.Flow.AssociationDirection`: İlişki yönünü belirten enum.
*   **Önemli Noktalar:**
    *   `SequenceFlow` ve `MessageFlow`'dan farklı olarak süreç akışını *etkilemez*, sadece elemanlar arasındaki ilişkiyi görsel olarak veya anlamsal olarak belirtir.
    *   Diyagramlarda noktalı çizgi ile gösterilir.
    *   `Data Association` (Veri İlişkisi), `DataInputAssociation` ve `DataOutputAssociation` sınıfları ile modellenen daha spesifik bir türdür ve veri akışını gösterir. 