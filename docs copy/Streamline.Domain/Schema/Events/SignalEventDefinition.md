# SignalEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir olayın belirli bir sinyalin yayınlanması (`ThrowEvent`) veya yakalanması (`CatchEvent`) ile ilgili olduğunu belirtir. Sinyaller, belirli bir hedefe gönderilmeyen, genel bir yayın (broadcast) mekanizmasıdır. Aynı süreç içinde veya farklı süreçler arasında iletişim kurmak için kullanılabilir.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `StartEvent` (sinyalle başlayan süreç), `IntermediateCatchEvent` (sinyal bekleyen), `IntermediateThrowEvent` (sinyal yayınlayan), `EndEvent` (sinyal yayınlayarak biten), `BoundaryEvent` (aktiviteye bağlı sinyal yakalayan) gibi elemanlarla birlikte kullanılır.
    *   Genellikle hangi `Signal` elemanının beklendiğini veya yayınlandığını referans gösterir.
*   **Özellikler:**
    *   `SignalRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): İlişkili `Signal` elemanının ID'sine referans.
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Sinyal referansını tutmak için.
    *   `Streamline.Domain.Schema.Common.Signal` (dolaylı): `SignalRef` ile ilişkilidir.
*   **Önemli Noktalar:**
    *   Mesajlardan farklı olarak, sinyallerin belirli bir alıcısı yoktur; yayınlandığında, o anda o sinyali bekleyen tüm aktif `CatchEvent`'ler tetiklenebilir.
    *   Genellikle süreçler arası koordinasyon veya belirli bir durumun birden fazla yere bildirilmesi için kullanılır.
    *   `SignalRef` genellikle sinyalin adını tanımlayan `Signal` elemanına işaret eder. 