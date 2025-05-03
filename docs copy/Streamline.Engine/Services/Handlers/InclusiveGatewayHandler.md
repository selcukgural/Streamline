# InclusiveGatewayHandler

*   **BPMN Elementi:** `Inclusive Gateway` (Kapsayıcı Ağ Geçidi)
*   **BPMN Amacı:** Koşullara bağlı olarak akışı bir veya *daha fazla* yola ayırır ve bu ayrılan yolların *tümünün* tamamlanmasını bekleyerek birleştirir.
    *   **Ayırma (Fork):** Giden *tüm* `SequenceFlow`\'ların koşullarını değerlendirir. Koşulu sağlayan *her bir* akış için paralel bir yol başlatır. En az bir yolun koşulu sağlaması beklenir (veya default akış).
    *   **Birleştirme (Join):** Kendisine karşılık gelen ayırıcı kapsayıcı ağ geçidinden çıkan ve bu birleştirici ağ geçidine gelen *tüm* aktif akışların ulaşmasını bekler. Tüm beklenen akışlar geldiğinde, tek bir giden yoldan devam eder.
*   **Uygulama & Davranış:** (Paralel ve Dışlayıcı\'nın karması, daha karmaşık)
    *   **Ayırma (Forking):**
        *   Gelen `Execution`\'ı sonlandırır.
        *   *Tüm* giden `SequenceFlow`\'ların koşullarını (`conditionExpression`) değerlendirir.
        *   Koşulu `true` olan *her bir* akış için *yeni bir Execution* oluşturur.
        *   Her yeni execution için `ExecutionFlowManager.ForkAndContinueExecutionAsync` çağırarak paralel yolları başlatır.
        *   (Eğer hiçbir koşul sağlanmazsa, default akış varsa onu kullanır veya hata verir).
    *   **Birleştirme (Joining):** (Bu kısım genellikle state management gerektirir)
        *   Gelen `Execution`\'ı ağ geçidinde beklemeye alır.
        *   Bu ağ geçidine hangi execution\'ların beklendiğini takip eder (genellikle ayırıcı ağ geçidinde başlatılan execution\'ların sayısına ve kimliklerine göre).
        *   Beklenen *tüm* execution\'lar bu ağ geçidine ulaştığında:
            *   Bekleyen diğer execution\'ları sonlandırır.
            *   Mevcut (son gelen) execution\'ı yeniden aktif eder.
            *   Tek bir giden `SequenceFlow`\'u bulur.
            *   `ExecutionFlowManager.ContinueExecutionAsync` ile bu execution\'ı giden yoldan devam ettirir. 