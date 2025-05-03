# TimerEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir olayın belirli bir zaman koşulu gerçekleştiğinde tetiklendiğini belirtir. Bu, belirli bir tarih/saat, belirli bir süre geçtikten sonra veya periyodik olarak tekrarlanan bir zaman döngüsü olabilir.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `StartEvent` (belirli bir zamanda başlayan süreç), `IntermediateCatchEvent` (belirli bir süre bekleyen veya belirli bir zamana kadar bekleyen), `BoundaryEvent` (bir aktivite belirli bir süreyi aşarsa tetiklenen) gibi elemanlarla birlikte kullanılır.
    *   Zaman koşulunu tanımlayan üç alt elemandan *yalnızca birini* içerebilir.
*   **Özellikler:**
    *   `TimeDate` (`Expression`, `XmlElement`): Belirli bir tarih ve saati tanımlayan bir ifade (genellikle ISO 8601 formatında, örn. `2024-12-31T23:59:59Z`).
    *   `TimeDuration` (`Expression`, `XmlElement`): Olayın referans alındığı noktadan ne kadar süre sonra tetikleneceğini belirten bir ifade (genellikle ISO 8601 süre formatında, örn. `PT5M` - 5 dakika, `P2D` - 2 gün).
    *   `TimeCycle` (`Expression`, `XmlElement`): Tekrarlayan bir zamanlamayı tanımlayan bir ifade (genellikle ISO 8601 tekrarlayan aralık formatı veya CRON ifadesi, örn. `R5/PT10M` - 10 dakikada bir 5 kez tekrarla, `0 0 * * *` - her gece yarısı).
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Expression`: Zamanlama ifadelerini tutmak için (genellikle `FormalExpression`).
*   **Önemli Noktalar:**
    *   Zaman bazlı otomasyon ve eskalasyonlar için kritiktir.
    *   İfadelerin formatı (ISO 8601, CRON) ve yorumlanması BPMN motoru tarafından desteklenmelidir.
    *   Streamline uygulamasında zamanlayıcı işleri (`Job`) oluşturmak için kullanılır. 