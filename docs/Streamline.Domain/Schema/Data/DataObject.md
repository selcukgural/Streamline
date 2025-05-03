# DataObject

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Süreç içinde kullanılan veya üretilen belirli bir bilgiyi veya belgeyi temsil eder. Sürecin *içinde* var olan veriyi gösterir (`DataStore`'dan farklı olarak).
*   **Konum:** `Streamline.Domain.Schema.Data`
*   **Miras:** `FlowElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Process` veya `SubProcess` içinde tanımlanır.
    *   Genellikle bir `ItemDefinition`'a (`itemSubjectRef`) referans vererek veri tipini belirtir.
    *   Verinin bir koleksiyon olup olmadığını (`IsCollection`) belirtir.
    *   İsteğe bağlı olarak verinin durumunu (`DataState`) belirtebilir.
    *   Doğrudan diyagram üzerinde `DataObjectReference` ile temsil edilir ve `DataAssociation` ile aktivitelere bağlanır.
*   **Özellikler:**
    *   `ItemSubjectRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Bu veri nesnesinin tipini ve yapısını tanımlayan `ItemDefinition`'a referans.
    *   `IsCollection` (bool, `XmlAttribute`, Varsayılan: `false`): Bu veri nesnesinin bir koleksiyonu mu temsil ettiğini belirtir.
    *   `DataState` (`DataState`, `XmlElement`, İsteğe Bağlı): Veri nesnesinin içinde bulunduğu durumu (örn. "Yeni", "İncelendi", "Onaylandı") belirten bir referans.
    *   `Name` (string, `XmlAttribute`, Miras): Veri nesnesinin adı.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Data.DataState`: Veri durumunu tutan sınıf.
    *   `Streamline.Domain.Schema.Events.ItemDefinition` (dolaylı): `ItemSubjectRef` ile ilişkilidir.
    *   `System.Xml.XmlQualifiedName`: `ItemSubjectRef`'i tutmak için.
*   **Önemli Noktalar:**
    *   Süreç içindeki veri akışını ve bağımlılıkları modellemek için kullanılır.
    *   `DataObjectReference`, diyagram üzerinde `DataObject`'un belirli bir noktadaki kullanımını temsil eder. 