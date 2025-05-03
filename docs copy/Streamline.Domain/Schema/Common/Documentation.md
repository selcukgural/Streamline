# Documentation

*   **BPMN Tipi:** Yardımcı Eleman
*   **Amaç:** Herhangi bir BPMN elemanına (`BaseElement`'ten türeyen) açıklayıcı metin veya dokümantasyon eklemek için kullanılır. Modelin anlaşılmasına yardımcı olur.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** Yok (Doğrudan `BaseElement`'in bir koleksiyon özelliği içinde kullanılır)
*   **Uygulama & Davranış:**
    *   Herhangi bir BPMN elemanının `<documentation>` alt etiketi olarak XML'de yer alır.
    *   Bir `BaseElement`, birden fazla `Documentation` elemanına sahip olabilir (farklı amaçlar veya diller için).
    *   İçeriği genellikle düz metin (`text/plain`) olmakla birlikte, `textFormat` özniteliği ile farklı formatlar (örn. `text/html`) belirtilebilir.
*   **Özellikler:**
    *   `Id` (string, `XmlAttribute`): Dokümantasyon elemanının kendi benzersiz ID'si (isteğe bağlı).
    *   `TextFormat` (string, `XmlAttribute`, Varsayılan: `"text/plain"`): Dokümantasyon metninin formatını belirten MIME tipi (örn. `text/plain`, `text/html`).
    *   `Text` (`string[]`, `XmlText`): Dokümantasyonun metin içeriği. `textFormat` `text/plain` ise bu alan kullanılır.
    *   `Any` (`XmlElement`, `XmlAnyElement`): `textFormat` `text/plain`'den farklıysa (örn. XHTML), yapılandırılmış dokümantasyon içeriğini tutmak için kullanılır.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.BaseElement` (Bu elemanı içerir)
*   **Önemli Noktalar:**
    *   Modelin okunabilirliğini ve anlaşılabilirliğini artırmak için kullanılır.
    *   Araçlar tarafından kullanıcıya bilgi göstermek amacıyla kullanılabilir. 