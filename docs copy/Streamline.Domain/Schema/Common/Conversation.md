# Conversation

*   **BPMN Tipi:** Somut (Concrete) - `ConversationNode`
*   **Amaç:** İletişim Diyagramlarında (Conversation Diagrams), bir veya daha fazla katılımcı (`Participant`) arasında gerçekleşen mantıksal bir mesaj alışverişi setini veya iletişim bağlamını temsil eder.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `ConversationNode` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   İletişim Diyagramının bir parçası olarak kullanılır.
    *   Genellikle `ConversationLink` aracılığıyla diğer `ConversationNode`'lara veya `Participant`'lara bağlanır.
    *   Bir grup ilgili mesaj alışverişini (`MessageFlow`) mantıksal olarak gruplandırır.
*   **Özellikler:**
    *   (`ConversationNode` ve `BaseElement`'ten ilgili özellikleri miras alır. `ConversationNode`'dan `Name`, `ParticipantRef`, `MessageFlowRef`, `CorrelationKey` gibi özellikler gelir.)
    *   Kendine özgü ek standart özellikleri yoktur.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.ConversationNode` (Miras aldığı sınıf)
    *   `Streamline.Domain.Schema.Common.Participant` (dolaylı, `participantRef` yoluyla)
    *   `Streamline.Domain.Schema.Flow.MessageFlow` (dolaylı, `messageFlowRef` yoluyla)
    *   `Streamline.Domain.Schema.Common.CorrelationKey` (dolaylı)
    *   `Streamline.Domain.Schema.Common.ConversationLink` (Bağlantı için)
*   **Önemli Noktalar:**
    *   İletişim Diyagramları, İşbirliği (`Collaboration`) diyagramlarının üst düzey bir görünümünü sağlamak için kullanılır ve `Conversation` elemanı bu diyagramların merkezindedir.
    *   Karmaşık işbirliklerindeki ana iletişim akışlarını basitleştirerek göstermeye yardımcı olur. 