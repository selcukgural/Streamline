# LinkEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Aynı süreç seviyesindeki (aynı `Process` veya `SubProcess` içinde) iki `Intermediate Event` arasında mantıksal bir bağlantı (bir tür "goto" veya "off-page connector") oluşturmak için kullanılır. Diyagramı görsel olarak daha temiz tutmak veya uzun `SequenceFlow` çizgilerinden kaçınmak için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Her zaman bir çift olarak kullanılır: Bir `IntermediateThrowEvent` (Link fırlatan - kaynağı belirtir) ve bir `IntermediateCatchEvent` (Link yakalayan - hedefi belirtir).
    *   Her iki olay da aynı `name` özelliğine sahip bir `LinkEventDefinition` içerir.
    *   Fırlatan olay (`ThrowEvent`) `target` özelliğini, yakalayan olay (`CatchEvent`) ise `source` özelliğini *kullanmaz*. Bunun yerine, isim (`name`) eşleşmesi ile bağlantı kurulur. Ancak şemada `source` ve `target` referansları da bulunur (muhtemelen alternatif veya eski bir kullanım için).
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`, **Zorunlu**): Link olay çiftini tanımlayan benzersiz isim. Fırlatan ve yakalayan olaydaki isimler eşleşmelidir.
    *   `Source` (Collection<`XmlQualifiedName`>, `XmlElement`, İsteğe Bağlı): Bu link olayını tetikleyen (fırlatan) diğer link olaylarının ID'lerine referanslar (genellikle yakalayan olayda kullanılır).
    *   `Target` (`XmlQualifiedName`, `XmlElement`, İsteğe Bağlı): Bu link olayının tetiklediği (yakalayan) diğer link olayının ID'sine referans (genellikle fırlatan olayda kullanılır).
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Source ve Target referanslarını tutmak için.
*   **Önemli Noktalar:**
    *   Süreç akışını etkilemez, sadece `SequenceFlow` yerine kullanılır.
    *   Farklı süreç seviyeleri (ana süreç ve alt süreç) arasında kullanılamaz.
    *   Motorun, aynı `name`'e sahip fırlatan ve yakalayan link olaylarını eşleştirmesi gerekir. 