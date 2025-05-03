# Artifact

*   **BPMN Tipi:** Soyut (Abstract)
*   **Amaç:** Süreç akışını doğrudan etkilemeyen ancak süreç hakkında ek bilgi sağlayan veya görsel gruplama yapan grafiksel elemanlar için temel sınıftır. `FlowElement`'lerden farklı olarak `SequenceFlow` ile bağlanamazlar (ancak `Association` ile ilişkilendirilebilirler).
*   **Konum:** `Streamline.Domain.Schema.Artifacts`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, `TextAnnotation`, `Group` ve `Association` gibi somut artifact türleri tarafından miras alınır.
*   **Özellikler:**
    *   (Genellikle `BaseElement`'ten gelen ID dışındaki standart özellikleri yoktur.)
*   **Önemli Noktalar:**
    *   Süreç diyagramının okunabilirliğini ve anlaşılırlığını artırmak için kullanılırlar.
    *   Soyut olduğu için doğrudan örneği oluşturulamaz. 