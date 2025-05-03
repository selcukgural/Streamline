# ErrorEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir olayın belirli bir hata (`Error`) durumuyla ilgili olduğunu belirtir. Hatalar, süreç akışında beklenen veya beklenmeyen istisnai durumları temsil eder.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `EndEvent` (belirli bir hatayı fırlatarak süreci veya akışı sonlandıran), `BoundaryEvent` (bir aktivite sırasında oluşan belirli bir hatayı yakalayan), `StartEvent` (yalnızca Event Sub-Process içinde, belirli bir hatayı yakalayarak alt süreci başlatan) ile kullanılır.
    *   Genellikle hangi `Error` elemanının fırlatıldığını veya yakalandığını referans gösterir.
*   **Özellikler:**
    *   `ErrorRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): İlişkili `Error` elemanının ID'sine referans.
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Hata referansını tutmak için.
    *   `Streamline.Domain.Schema.Common.Error` (dolaylı): `ErrorRef` ile ilişkilidir.
*   **Önemli Noktalar:**
    *   Süreçlerde hata yönetimi (exception handling) mekanizmasının temelini oluşturur.
    *   Bir hata fırlatıldığında (`EndEvent` veya `Execution.Fail()`), motor uygun bir `Error Boundary Event` veya `Error Event Sub-Process Start Event` arar.
    *   `ErrorRef` genellikle hatanın adını ve hata kodunu (`errorCode` özelliği `Error` elemanındadır) tanımlayan `Error` elemanına işaret eder. 