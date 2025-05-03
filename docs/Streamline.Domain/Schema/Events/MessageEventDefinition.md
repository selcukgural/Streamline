# MessageEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir olayın belirli bir mesajın alınması (`CatchEvent`) veya gönderilmesi (`ThrowEvent`) ile tetiklendiğini belirtir. Süreçler arası iletişim veya dış sistemlerle etkileşim için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `StartEvent` (mesajla başlayan süreç), `IntermediateCatchEvent` (devam etmek için mesaj bekleyen), `IntermediateThrowEvent` (mesaj gönderen), `EndEvent` (mesaj göndererek biten), `ReceiveTask`, `SendTask` gibi elemanlarla birlikte kullanılır.
    *   Genellikle hangi `Message` elemanının beklendiğini veya gönderildiğini referans gösterir.
*   **Özellikler:**
    *   `MessageRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): İlişkili `Message` elemanının ID'sine referans.
    *   `OperationRef` (`XmlQualifiedName`, `XmlElement`, İsteğe Bağlı): Eğer mesaj bir Web Servisi operasyonunun parçasıysa, ilgili `Operation` elemanının ID'sine referans.
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Mesaj ve operasyon referanslarını tutmak için.
    *   `Streamline.Domain.Schema.Common.Message` (dolaylı): `MessageRef` ile ilişkilidir.
    *   `Streamline.Domain.Schema.Common.Operation` (dolaylı): `OperationRef` ile ilişkilidir.
*   **Önemli Noktalar:**
    *   Mesajlaşma tabanlı süreç entegrasyonlarında temel bir bileşendir.
    *   `MessageRef` genellikle mesajın adını ve potansiyel olarak veri yapısını (`ItemDefinition` referansı) tanımlayan `Message` elemanına işaret eder. 