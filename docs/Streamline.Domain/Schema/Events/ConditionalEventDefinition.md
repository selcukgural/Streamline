# ConditionalEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir olayın belirli bir koşul (`condition`) doğru (true) olarak değerlendirildiğinde tetiklendiğini belirtir. Koşul genellikle süreç değişkenlerindeki değişikliklere veya belirli bir duruma bağlıdır.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `StartEvent` (yalnızca Event Sub-Process içinde veya kural tabanlı sistemlerde, belirli bir koşul sağlandığında süreci başlatan), `IntermediateCatchEvent` (belirli bir koşul sağlanana kadar bekleyen), `BoundaryEvent` (bir aktiviteye bağlı, koşul sağlandığında tetiklenen - kesintili veya kesintisiz) ile kullanılır.
    *   Tetiklenmesi için gerekli koşulu tanımlayan bir `Expression` içerir.
*   **Özellikler:**
    *   `Condition` (`Expression`, `XmlElement`, **Zorunlu**): Tetiklenme koşulunu tanımlayan ifade (genellikle `FormalExpression`). Bu ifadenin sonucu `true` olduğunda olay tetiklenir.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Expression`: Koşul ifadesini tutmak için.
*   **Önemli Noktalar:**
    *   Süreç değişkenlerindeki veya dış sistemlerdeki durum değişikliklerine reaktif olarak tepki vermek için kullanılır.
    *   Koşulun ne zaman ve nasıl değerlendirileceği (örn. her değişken değiştiğinde, periyodik olarak) BPMN motorunun implementasyonuna bağlıdır. 