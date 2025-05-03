# ManualTaskHandler

*   **BPMN Elementi:** `Manual Task` (Manuel Görev)
*   **BPMN Amacı:** Süreç motoru dışında, bir insan tarafından otomasyon olmaksızın gerçekleştirilen bir görevi modeller. Süreç motoru bu görevin başladığını bilir ancak tamamlanmasını aktif olarak takip etmez veya zorlamaz.
*   **Uygulama & Davranış:**
    *   Bu handler bir `ManualTask`\'a ulaşıldığında çalışır.
    *   İlişkili `ActivityInstance`\'ı oluşturur/bulur ve hemen `Completed` olarak işaretler (çünkü motor görevin kendisini beklemez).
    *   Giden `SequenceFlow`\'u bulur.
    *   `ExecutionFlowManager.ContinueExecutionAsync` ile akışı hemen devam ettirir. Yani, motor için bu adım genellikle bir "pass-through" (geçiş) adımıdır. 