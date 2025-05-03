# ParticipantAssociation

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir İşbirliği (`Collaboration`) içinde iki farklı `Participant`'ı (Havuzu) birbirine bağlamak için kullanılan bir ilişki elemanıdır. Standartta tanımlı olmasına rağmen pratikte kullanımı oldukça nadirdir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Collaboration` elemanının `ParticipantAssociation` koleksiyonu içinde bulunur.
    *   İki `Participant` arasında bir ilişki olduğunu belirtir.
    *   `MessageFlow`'lar katılımcılar arası etkileşimi zaten gösterdiği için bu elemanın ek bir anlam katması genellikle belirsizdir ve çoğu modelleme aracı tarafından aktif olarak kullanılmaz veya desteklenmez.
*   **Özellikler:**
    *   `InnerParticipantRef` (`XmlQualifiedName`, `XmlElement`, Zorunlu): İlişkilendirilen "iç" `Participant`'ın ID'sine referans.
    *   `OuterParticipantRef` (`XmlQualifiedName`, `XmlElement`, Zorunlu): İlişkilendirilen "dış" `Participant`'ın ID'sine referans.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Participant` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.Collaboration` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Standartta var olan ancak pratikte nadiren kullanılan bir BPMN elemanıdır.
    *   Genellikle `MessageFlow` kullanmak katılımcılar arası ilişkiyi göstermek için yeterlidir. 