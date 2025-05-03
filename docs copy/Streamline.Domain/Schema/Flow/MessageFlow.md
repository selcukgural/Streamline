# MessageFlow

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Farklı `Participant`'lar (havuzlar - Pool) arasında gönderilen mesajların akışını gösterir. Süreçler arası veya süreç ile dış katılımcı arasındaki iletişimi modeller.
*   **Konum:** `Streamline.Domain.Schema.Flow`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir mesaj kaynağını (`SourceRef`) ve bir mesaj hedefini (`TargetRef`) birbirine bağlar. Kaynak ve hedef farklı havuzlarda olmalıdır.
    *   Kaynak ve hedef, bir `Activity`, `Event` veya `Participant` olabilir.
    *   Genellikle hangi mesajın (`MessageRef`) gönderildiğini belirtir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`, İsteğe Bağlı): Mesaj akışına bir isim vermek için kullanılır.
    *   `SourceRef` (`XmlQualifiedName`, `XmlAttribute`, **Zorunlu**): Mesajın gönderildiği elemanın (veya havuzun) ID'si.
    *   `TargetRef` (`XmlQualifiedName`, `XmlAttribute`, **Zorunlu**): Mesajın alındığı elemanın (veya havuzun) ID'si.
    *   `MessageRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Gönderilen `Message` elemanının ID'sine referans.
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Kaynak, hedef ve mesaj referanslarını tutmak için.
    *   `Streamline.Domain.Schema.Common.Message` (dolaylı): `MessageRef` ile ilişkilidir.
*   **Önemli Noktalar:**
    *   `SequenceFlow`'dan farklı olarak, aynı havuz içindeki elemanları bağlamak için kullanılmaz.
    *   İki farklı organizasyon veya sistem arasındaki etkileşimi modellemek için kritiktir.
    *   Diyagramlarda kesikli çizgi ile gösterilir. 