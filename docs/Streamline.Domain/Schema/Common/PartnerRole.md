# PartnerRole

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Bir İşbirliği (`Collaboration`) veya Koreografi (`Choreography`) bağlamında bir iş ortağının (`PartnerEntity`) üstlendiği rolü temsil eder (örn. "Alıcı", "Satıcı", "Taşıyıcı", "Servis Sağlayıcı").
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   Bir veya daha fazla `Participant` (`participantRef` ile) bu rolü üstlenebilir.
    *   Genellikle bir `PartnerEntity` ile ilişkilendirilir (ancak doğrudan bir referans özelliği yoktur; ilişki genellikle `Participant` üzerinden kurulur veya Koreografi tanımlarında belirtilir).
    *   Özellikle Koreografi diyagramlarında, etkileşimdeki tarafların rollerini tanımlamak için kullanılır.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): İş ortağı rolünün adı.
    *   `ParticipantRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu rolü üstlenen `Participant` elemanlarının ID'lerine referanslar.
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Participant` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.PartnerEntity` (İlişkilendirilebilir)
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   İşbirliği içindeki katılımcıların rollerini daha net bir şekilde tanımlamaya yardımcı olur.
    *   Aynı `PartnerEntity` (kuruluş) farklı işbirliklerinde farklı `PartnerRole`'ler üstlenebilir. 