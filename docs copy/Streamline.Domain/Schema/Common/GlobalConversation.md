# GlobalConversation

*   **BPMN Tipi:** Somut (Concrete) - `Collaboration`'dan türemiştir (Standarda göre `Collaboration`'ın özel bir türüdür).
*   **Amaç:** Katılımcılar (`Participant`) arasındaki üst düzey mesaj alışverişlerini gösteren bir İletişim Diyagramını (`Conversation Diagram`) temsil eder. Bir `Collaboration`'dan farklı olarak, genellikle katılımcıların iç süreç detaylarını (`Process`) göstermez, sadece aralarındaki konuşmalara odaklanır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `Collaboration` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   `Collaboration`'dan miras aldığı için `Participant`, `MessageFlow`, `ConversationNode` (özellikle `Conversation`, `SubConversation`), `ConversationLink`, `Artifact` gibi elemanları içerebilir.
    *   Temel amacı, `Collaboration`'ın basitleştirilmiş bir görünümünü sunarak, farklı katılımcılar arasındaki temel etkileşimleri ve konuşma gruplarını (`Conversation` elemanları) vurgulamaktır.
    *   `CallConversation` aktivitesi, global olarak tanımlanmış bir `GlobalConversation`'ı referans alabilir.
*   **Özellikler:**
    *   (`Collaboration`, `RootElement`, `BaseElement`'ten ilgili tüm özellikleri miras alır: `Name`, `IsClosed`, `Participant`, `MessageFlow`, `ConversationNode`, `ConversationLink`, `Artifact`, `CorrelationKey` vb.)
*   **Bağımlılıklar:**
    *   `Collaboration` (Miras aldığı sınıf)
    *   `Participant`, `MessageFlow`, `ConversationNode`, `ConversationLink`, `Artifact`, `CorrelationKey` vb. (İçerdiği elemanlar)
*   **Önemli Noktalar:**
    *   Karmaşık işbirliklerinin üst düzey bir özetini sunmak için kullanılır.
    *   Odak noktası katılımcılar arasındaki mesajlaşma etkileşimleridir.
    *   Bu C# sınıfı `Collaboration`'dan türese de, BPMN kavramsal modelinde genellikle farklı bir diyagram türü olarak ele alınır. 