# CSharpScriptTaskHandler

*   **BPMN Elementi:** `Script Task` (Betik Görevi - `scriptFormat="text/csharp"` veya benzeri)
*   **BPMN Amacı:** Süreç içinde C# kod parçacıklarını çalıştırmak için kullanılır.
*   **Uygulama & Davranış:**
    *   Bu handler C# betiği içeren bir `ScriptTask`\'a ulaşıldığında çalışır.
    *   `ActivityInstance` oluşturur/bulur.
    *   BPMN modelinden `script` içeriğini okur.
    *   Roslyn Scripting API gibi bir C# betik motorunu kullanarak:
        *   Gerekli süreç değişkenlerini betik motoruna girdi olarak aktarır (genellikle bir `globals` nesnesi üzerinden).
        *   Betiği çalıştırır.
        *   Betikten dönen sonuçları (varsa) süreç değişkenlerine yazar.
    *   `ActivityInstance`\'ı `Completed` olarak işaretler.
    *   Giden `SequenceFlow`\'u bulur ve `ExecutionFlowManager.ContinueExecutionAsync` ile akışı devam ettirir.
    *   Hata durumunda (`script` çalışmazsa), `ActivityInstance`\'ı `Failed` yapar. 