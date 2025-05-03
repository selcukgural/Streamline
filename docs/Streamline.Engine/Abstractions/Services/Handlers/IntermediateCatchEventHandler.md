# IntermediateCatchEventHandler

*   **BPMN Elementi:** `Intermediate Catch Event` (Orta Yakalama Olayı - Timer, Message, Signal, Link, Conditional vb.)
*   **BPMN Amacı:** Süreç akışı sırasında belirli bir olayın (zamanın dolması, mesajın gelmesi, sinyalin alınması vb.) gerçekleşmesini bekler. Olay gerçekleştiğinde akış devam eder. `Link` tipi beklemez, sadece akışı bağlar.
*   **Uygulama & Davranış:**
    *   Bu handler bir `IntermediateCatchEvent`\'e ulaşıldığında çalışır.
    *   Event\'in `EventDefinition`\'ını kontrol eder (Timer, Message, Signal, Link vb.).
    *   **Timer/Message/Signal/Conditional:**
        *   İlgili olay türü ve adı/koşulu ile bir `EventSubscription` oluşturur.
        *   Eğer Timer ise, `ITimerJobScheduler` kullanarak zamanlayıcıyı kurar ve JobId\'yi `EventSubscription`\'a kaydeder.
        *   Mevcut `Execution`\'ı bu olay düğümünde **beklemeye alır**. Olay dışarıdan tetiklendiğinde (timer job, mesaj/sinyal alımı), ilgili `EventSubscription` bulunur ve `ExecutionFlowManager` aracılığıyla bekleyen `Execution` devam ettirilir.
    *   **Link:**
        *   Bekleme yapmaz.
        *   Giden *tek* `SequenceFlow`\'u bulur.
        *   `ExecutionFlowManager.ContinueExecutionAsync` ile akışı devam ettirir. (Not: Link Event\'ler genellikle çiftler halinde kullanılır - Throw ve Catch. Bu handler Catch kısmıdır). 