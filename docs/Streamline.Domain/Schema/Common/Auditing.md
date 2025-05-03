# Auditing

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir `FlowElement` ile ilişkilendirilmiş denetim (audit) bilgilerini ve yapılandırmasını tutmak için kullanılır. Süreç yürütme sırasında hangi bilgilerin kaydedileceğini veya izleneceğini belirtmek amacıyla kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Genellikle `FlowElement`'lerin (örn. `Task`, `Event`, `Gateway`) bir alt elemanı olarak bulunur.
    *   Spesifik denetim mekanizması ve içeriği genellikle BPMN motoru veya platform tarafından tanımlanır ve bu elemanın içeriği veya özellikleri buna göre yorumlanır.
*   **Özellikler:**
    *   (Genellikle standart BPMN şemasında doğrudan tanımlı özellikleri yoktur, `ExtensionElements` ile genişletilebilir veya motor tarafından özel olarak yorumlanabilir.)
*   **Önemli Noktalar:**
    *   Süreçlerin uyumluluk (compliance) ve performans analizi için önemlidir.
    *   Bu elemanın detayı ve kullanımı, kullanılan BPMN aracına ve motoruna göre değişiklik gösterebilir. 