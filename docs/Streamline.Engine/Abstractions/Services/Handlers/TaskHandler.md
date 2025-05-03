# TaskHandler

*   **BPMN Elementi:** `Task` (Görev - Soyut/Temel Tip)
*   **BPMN Amacı:** Süreç içinde gerçekleştirilen atomik bir iş birimini temsil eder. Bu, `UserTask`, `ServiceTask` gibi daha spesifik görev türlerinin temelidir. Spesifik bir tür belirtilmemişse, genellikle motorun bekleme yapmadan geçtiği bir adım olarak yorumlanabilir veya özel bir implementasyon gerektirebilir.
*   **Uygulama & Davranış:**
    *   Bu handler, `UserTaskHandler`, `BusinessRuleTaskHandler` gibi daha spesifik bir handler bulunamadığında genel `Task` elemanları için çağrılır (Factory\'deki sıraya göre).
    *   Genellikle basit bir "pass-through" (geçiş) veya temel bekleme durumu mantığı uygular.
    *   İlişkili `ActivityInstance`\'ı bulur veya oluşturur ve durumunu `Completed` olarak günceller.
    *   Görevin giden `SequenceFlow`\'unu bulur.
    *   `ExecutionFlowManager.ContinueExecutionAsync` çağırarak execution\'ın bir sonraki düğüme ilerlemesini sağlar. 