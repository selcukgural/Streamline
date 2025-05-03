# Relationship

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** BPMN elemanları arasında, standart akış (`SequenceFlow`), mesajlaşma (`MessageFlow`) veya artifact (`Association`) ilişkileri dışında kalan, genellikle daha soyut veya özel anlam taşıyan bir ilişkiyi tanımlamak için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   `Definitions` elemanının altında tanımlanır.
    *   İlişkinin türünü (`type`), yönünü (`direction`), kaynağını (`source`) ve hedefini (`target`) belirtir.
    *   `type` özniteliği genellikle özel bir URI veya tanımlayıcı içerir ve ilişkinin anlamını belirtir (örn. "isSupportedBy", "refines", "uses"). Standart BPMN'de önceden tanımlanmış `type` değerleri yoktur.
    *   Kaynak ve hedef, herhangi bir `BaseElement` olabilir.
*   **Özellikler:**
    *   `Type` (string, `XmlAttribute`, Zorunlu): İlişkinin türünü tanımlayan bir URI veya metin.
    *   `Direction` (`RelationshipDirection` enum, `XmlAttribute`, İsteğe Bağlı): İlişkinin yönünü belirtir (`None`, `Forward`, `Backward`, `Both`).
    *   `Source` (Collection<`XmlQualifiedName`>, `XmlElement`, Zorunlu): İlişkinin kaynak tarafındaki bir veya daha fazla elemanın ID'sine referans.
    *   `Target` (Collection<`XmlQualifiedName`>, `XmlElement`, Zorunlu): İlişkinin hedef tarafındaki bir veya daha fazla elemanın ID'sine referans.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.RelationshipDirection` (Enum)
    *   Herhangi bir `BaseElement` alt türü (dolaylı, `Source` ve `Target` referansları yoluyla)
    *   `Streamline.Domain.Schema.Common.Definitions` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Standart dışı veya modele özgü ilişkileri belgelemek için bir mekanizma sunar.
    *   Kullanımı yaygın değildir ve anlamı genellikle modele veya kullanılan araca özeldir. 