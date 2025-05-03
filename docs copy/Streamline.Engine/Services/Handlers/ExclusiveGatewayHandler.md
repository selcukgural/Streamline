# ExclusiveGatewayHandler

*   **BPMN Elementi:** `Exclusive Gateway` (Dışlayıcı Ağ Geçidi)
*   **BPMN Amacı:** Akış yolunu, tanımlanan koşullara göre *sadece bir* yöne yönlendirir.
    *   **Ayırma (Split):** Giden `SequenceFlow`\'lar üzerindeki koşulları sırayla değerlendirir. Koşulu sağlayan *ilk* akış yolunu seçer ve akışı oradan devam ettirir. Genellikle bir "default flow" (varsayılan akış) tanımlanır, eğer hiçbir koşul sağlanmazsa bu yol kullanılır.
    *   **Birleştirme (Merge):** Herhangi bir gelen akış yolundan gelen akışı alır ve doğrudan tek giden yola birleştirir. Bekleme yapmaz.
*   **Uygulama & Davranış:**
    *   **Ayırma (Splitting):**
        *   Giden `SequenceFlow`\'ları (genellikle tanımlı bir sıra ile) alır.
        *   Her bir akışın koşulunu (`conditionExpression`) süreç değişkenleri üzerinden değerlendirir (örn. C# script, JS script veya başka bir ifade dili ile).
        *   Koşulu `true` olan ilk akışı bulur.
        *   Eğer hiçbir koşul sağlanmazsa, tanımlı "default" akışı arar.
        *   Seçilen *tek bir* akış üzerinden `ExecutionFlowManager.ContinueExecutionAsync` ile mevcut `Execution`\'ı devam ettirir.
    *   **Birleştirme (Merging):**
        *   Gelen `Execution`\'ı alır.
        *   Tek bir giden `SequenceFlow`\'u bulur.
        *   `ExecutionFlowManager.ContinueExecutionAsync` ile mevcut `Execution`\'ı giden yoldan devam ettirir. Bekleme yapmaz. 