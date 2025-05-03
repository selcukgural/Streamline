# BusinessRuleTaskHandler

*   **BPMN Elementi:** `Business Rule Task` (İş Kuralı Görevi)
*   **BPMN Amacı:** Bir iş kuralı motorunu (örneğin DMN - Decision Model and Notation) veya belirli bir iş kuralı setini çalıştırmak için kullanılır. Girdi verilerini alır, kuralları uygular ve çıktı üretir.
*   **Uygulama & Davranış:**
    *   Bu handler bir `BusinessRuleTask`\'a ulaşıldığında çalışır.
    *   İlişkili `ActivityInstance`\'ı oluşturur/bulur.
    *   BPMN modelinde tanımlanan kural referansını (örn. DMN tablosu adı) alır.
    *   Gerekli girdi değişkenlerini `Execution` veya `ProcessInstance` üzerinden toplar.
    *   Enjekte edilen bir kural motoru servisini (henüz implemente edilmemiş olabilir) çağırarak kuralları çalıştırır.
    *   Kural motorundan dönen sonuçları süreç değişkenlerine yazar.
    *   `ActivityInstance`\'ı `Completed` olarak işaretler.
    *   Giden `SequenceFlow`\'u bulur ve `ExecutionFlowManager.ContinueExecutionAsync` ile akışı devam ettirir. 