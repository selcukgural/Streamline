# IntermediateThrowEventHandler

*   **BPMN Elementi:** `Intermediate Throw Event` (Orta Fırlatma Olayı - None, Message, Signal, Escalation, Link, Compensate vb.)
*   **BPMN Amacı:** Süreç akışı sırasında belirli bir olayı *aktif olarak* fırlatır veya tetikler.
    *   **None:** Genellikle bir akışın belirli bir noktaya ulaştığını belirtmek veya görsel bir işaretleyici olarak kullanılır; özel bir aksiyonu yoktur.
    *   **Message/Signal:** Belirtilen mesajı veya sinyali yayınlar/gönderir.
    *   **Escalation:** Belirtilen yükseltme kodunu fırlatır.
    *   **Link:** Akışı, aynı isimdeki karşılık gelen `IntermediateCatchEvent` (Link) elemanına yönlendirir.
    *   **Compensate:** Belirtilen bir aktivite veya mevcut kapsam için telafi (compensation) mantığını tetikler.
*   **Uygulama & Davranış:**
    *   Bu handler bir `IntermediateThrowEvent`\'e ulaşıldığında çalışır.
    *   Event\'in `EventDefinition`\'ını kontrol eder.
    *   **None:** Özel bir işlem yapmaz.
    *   **Message/Signal:** İlgili mesaj/sinyal adını alır. Bir event bus (örn. MediatR) veya özel bir servis aracılığıyla bu olayı yayınlar/gönderir.
    *   **Escalation:** `EscalationEventDefinition`\'dan kodu alır. Mevcut `Execution`\'da `Escalate` metodunu çağırır. `ExecutionFlowManager.FindAndHandleEscalationAsync`\'i çağırır.
    *   **Link:** `LinkEventDefinition`\'dan hedef link adını alır. Eşleşen Catch Link Event\'ini bulur (bu genellikle doğrudan olmaz, `ExecutionFlowManager`\'ın akışı yönlendirmesi gerekebilir veya özel bir Link yönetimi gerekir).
    *   **Compensate:** `CompensateEventDefinition`\'dan telafi edilecek aktivitenin referansını alır. `ExecutionFlowManager` içinde ilgili telafi mantığını tetikler (bu karmaşık olabilir).
    *   *Tüm* Throw Event türleri için, olayı fırlattıktan/işlemi yaptıktan sonra, giden *tek* `SequenceFlow`\'u bulur ve `ExecutionFlowManager.ContinueExecutionAsync` ile akışı **devam ettirir**. 