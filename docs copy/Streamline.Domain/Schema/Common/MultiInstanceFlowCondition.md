# MultiInstanceFlowCondition

*   **BPMN Tipi:** Enum (Enumeration)
*   **Amaç:** `MultiInstanceLoopCharacteristics` elemanının `behavior` özniteliği (standartta tanımlı ancak bu kod tabanında doğrudan görünmeyebilir) `FlowCondition` olarak ayarlandığında, çoklu örnek aktivitesinin örneklerinin tamamlanma veya iptal koşulunu belirtir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Değerler:**
    *   `None`: Akış koşulu uygulanmaz veya davranış başka bir mekanizma (örn. `completionCondition`) ile belirlenir.
    *   `One`: Aktif örneklerden *biri* `completionCondition`'ı sağladığında, diğer tüm aktif örnekler sonlandırılır ve çoklu örnek aktivitesi tamamlanır. (Tek bir başarılı sonuç yeterli olduğunda kullanılır).
    *   `All`: Tüm aktif örnekler tamamlanana kadar beklenir (`completionCondition`'a bağlı olarak). Bu genellikle varsayılan davranıştır eğer `behavior` belirtilmemişse.
    *   `Complex`: Tamamlanma veya iptal koşulunun `ComplexBehaviorDefinition` elemanları ile tanımlanan daha karmaşık bir mantığa dayandığını belirtir.
*   **Kullanım:**
    *   `MultiInstanceLoopCharacteristics` elemanının (bu kodda doğrudan görünmeyen) `behavior` özniteliği ile birlikte kullanılır.
*   **Önemli Noktalar:**
    *   Çoklu örnek aktivitelerinin davranışını özelleştirmek için kullanılır.
    *   `behavior` özniteliği ve `FlowCondition` kullanımı standart BPMN 2.0'da tanımlıdır, ancak implementasyon detayları motorlara göre değişebilir. 