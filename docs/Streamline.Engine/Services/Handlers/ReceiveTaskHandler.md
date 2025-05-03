# ReceiveTaskHandler

*   **BPMN Elementi:** `Receive Task` (Mesaj Alma Görevi)
*   **BPMN Amacı:** Süreç akışını, belirli bir mesaj gelene kadar durduran bir bekleme noktasıdır. Mesaj geldiğinde görev tamamlanır ve akış devam eder. `IntermediateCatchEvent` (Message) ile benzerdir ancak aktivite (task) formundadır.
*   **Uygulama & Davranış:**
    *   Bu handler bir `ReceiveTask`\'a ulaşıldığında çalışır.
    *   İlişkili `ActivityInstance`\'ı oluşturur/bulur ve `Active` durumda bırakır.
    *   BPMN modelinden beklenen mesajın adını (`messageRef`) alır.
    *   `IntermediateCatchEventHandler` (Message) gibi, ilgili mesaj adı için bir `EventSubscription` oluşturur.
    *   Mevcut `Execution`\'ı bu görev düğümünde **beklemeye alır**.
    *   Dışarıdan ilgili mesaj alındığında, `EventSubscription` bulunur, `ActivityInstance` `Completed` yapılır ve `ExecutionFlowManager` aracılığıyla bekleyen `Execution` devam ettirilir. 