# SendTaskHandler

*   **BPMN Elementi:** `Send Task` (Mesaj Gönderme Görevi)
*   **BPMN Amacı:** Süreç içinden dışarıya veya başka bir sürece belirli bir mesajı gönderir. `IntermediateThrowEvent` (Message) ile benzerdir ancak aktivite formundadır.
*   **Uygulama & Davranış:**
    *   Bu handler bir `SendTask`\'a ulaşıldığında çalışır.
    *   `ActivityInstance` oluşturur/bulur.
    *   BPMN modelinden gönderilecek mesajın adını (`messageRef`) ve potansiyel içeriğini (veri eşleştirmeleri yoluyla) alır.
    *   `IntermediateThrowEventHandler` (Message) gibi, bir event bus veya mesajlaşma servisi aracılığıyla mesajı gönderir/yayınlar.
    *   `ActivityInstance`\'ı `Completed` olarak işaretler.
    *   Giden `SequenceFlow`\'u bulur ve `ExecutionFlowManager.ContinueExecutionAsync` ile akışı **devam ettirir**. 