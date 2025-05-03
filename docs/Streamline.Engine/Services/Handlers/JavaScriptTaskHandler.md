# JavaScriptTaskHandler

*   **BPMN Elementi:** `Script Task` (Betik Görevi - `scriptFormat="text/javascript"` veya benzeri)
*   **BPMN Amacı:** Süreç içinde JavaScript kod parçacıklarını çalıştırmak için kullanılır.
*   **Uygulama & Davranış:**
    *   Bu handler JavaScript betiği içeren bir `ScriptTask`\'a ulaşıldığında çalışır.
    *   `ActivityInstance` oluşturur/bulur.
    *   BPMN modelinden `script` içeriğini okur.
    *   Jint, V8 (Microsoft.ClearScript üzerinden) gibi bir JavaScript motorunu kullanarak:
        *   Gerekli süreç değişkenlerini betik motoruna girdi olarak aktarır.
        *   Betiği çalıştırır.
        *   Betikten dönen sonuçları (varsa) süreç değişkenlerine yazar.
    *   `ActivityInstance`\'ı `Completed` olarak işaretler.
    *   Giden `SequenceFlow`\'u bulur ve `ExecutionFlowManager.ContinueExecutionAsync` ile akışı devam ettirir.
    *   Hata durumunda (`script` çalışmazsa), `ActivityInstance`\'ı `Failed` yapar. 