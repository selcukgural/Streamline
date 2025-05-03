# PartnerEntity

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Bir İşbirliği (`Collaboration`) veya Koreografi (`Choreography`) bağlamında yer alan belirli bir iş ortağını, kuruluşu veya sistemi temsil eder. Genellikle `PartnerRole` ile birlikte kullanılarak bir katılımcının kim olduğunu (`PartnerEntity`) ve işbirliğindeki rolünün ne olduğunu (`PartnerRole`) ayırır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   Bir veya daha fazla `Participant` (`participantRef` ile) bu `PartnerEntity` tarafından temsil edilebilir.
    *   `PartnerRole`, hangi `PartnerEntity`'nin o rolü üstlendiğini belirtmek için bu elemana referans verebilir.
    *   Özellikle Koreografi diyagramlarında, etkileşimdeki tarafları daha soyut bir şekilde tanımlamak için kullanılır.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): İş ortağı varlığının adı (örn. "Acme Corporation", "Müşteri Yönetim Sistemi").
    *   `ParticipantRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu iş ortağı varlığını temsil eden `Participant` elemanlarının ID'lerine referanslar.
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Participant` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.PartnerRole` (İlişkilendirilebilir)
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   İşbirliği içindeki katılımcıları daha yapısal bir şekilde tanımlamaya yardımcı olur.
    *   `PartnerRole` ile birlikte kullanıldığında, bir kuruluşun farklı işbirliklerinde farklı roller üstlenebileceğini modellemeyi kolaylaştırır. 