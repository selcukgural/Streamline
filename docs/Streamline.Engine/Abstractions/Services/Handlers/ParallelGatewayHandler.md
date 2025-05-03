# ParallelGatewayHandler

*   **BPMN Elementi:** `Parallel Gateway` (Paralel Ağ Geçidi)
*   **BPMN Amacı:** Akışları birleştirmek veya ayırmak için kullanılır.
    *   **Ayırma (Fork):** Gelen tek bir akış yolunu, tüm giden sıra akışları boyunca *eş zamanlı* olarak devam eden birden fazla paralel yola ayırır.
    *   **Birleştirme (Join):** Kendisine gelen *tüm* paralel akış yollarının tamamlanmasını bekler. Tüm yollar geldiğinde, tek bir giden akış yolundan devam eder.
*   **Uygulama & Davranış:**
    *   Gelen akış sayısını ve giden akış sayısını kontrol ederek ayırma mı yoksa birleştirme mi yapacağını anlar.
    *   **Ayırma (Forking):**
        *   Gelen `Execution`\'ı sonlandırır.
        *   *Tüm* giden `SequenceFlow`\'ları bulur.
        *   Her bir giden akış için *yeni bir Execution* oluşturur (aynı `ProcessInstance` altında, genellikle aynı `ParentExecution` ile).
        *   Her yeni execution için `ExecutionFlowManager.ForkAndContinueExecutionAsync` (veya benzeri) metodunu çağırarak paralel yolları başlatır.
    *   **Birleştirme (Joining):**
        *   Gelen `Execution`\'ı ağ geçidinde beklemeye alır (`ArriveAtConvergingGateway`).
        *   Aynı ağ geçidinde bekleyen *diğer* aktif `Execution`\'ları kontrol eder (genellikle `Execution.WaitingAtGatewayId` gibi bir alan üzerinden).
        *   Beklenen toplam execution sayısı (gelen sıra akışı sayısı kadar) bu ağ geçidine ulaştıysa:
            *   Bekleyen diğer execution\'ları sonlandırır.
            *   Mevcut (son gelen) execution\'ı yeniden aktif eder (`LeaveConvergingGateway`).
            *   Tek bir giden `SequenceFlow`\'u bulur.
            *   `ExecutionFlowManager.ContinueExecutionAsync` ile bu execution\'ı giden yoldan devam ettirir.
        *   Eğer beklenen sayıya henüz ulaşılmadıysa, mevcut execution beklemeye devam eder. 