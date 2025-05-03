# DataInput & DataOutput

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:**
    *   `DataInput`: Bir Aktivitenin çalışması için *gereken* veriyi temsil eder.
    *   `DataOutput`: Bir Aktivitenin çalışması sonucunda *üretilen* veriyi temsil eder.
    *   Bu elemanlar, aktivitelerin veri gereksinimlerini ve sonuçlarını açıkça tanımlamak için `InputOutputSpecification` içinde kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Data`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Genellikle bir Aktivitenin `ioSpecification` elemanı altında tanımlanırlar.
    *   Bir `ItemDefinition`'a (`itemSubjectRef`) referans vererek veri tipini belirtirler.
    *   Verinin bir koleksiyon olup olmadığını (`IsCollection`) belirtirler.
    *   İsteğe bağlı olarak verinin durumunu (`DataState`) belirtebilirler.
    *   `DataInputAssociation` ve `DataOutputAssociation` aracılığıyla süreçteki `DataObjectReference` veya `DataStoreReference` gibi elemanlardan/elemanlara veri akışını sağlarlar.
*   **Özellikler (Her ikisi için de benzer):**
    *   `Name` (string, `XmlAttribute`, İsteğe Bağlı): Veri giriş/çıkışının adı (aktivite içindeki yerel adı).
    *   `ItemSubjectRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Verinin tipini tanımlayan `ItemDefinition`'a referans.
    *   `IsCollection` (bool, `XmlAttribute`, Varsayılan: `false`): Verinin bir koleksiyon olup olmadığını belirtir.
    *   `DataState` (`DataState`, `XmlElement`, İsteğe Bağlı): Verinin bu giriş/çıkış anındaki durumunu belirten referans.
*   **Bağımlılıklar (Her ikisi için de):**
    *   `Streamline.Domain.Schema.Data.DataState`: Veri durumunu tutan sınıf.
    *   `Streamline.Domain.Schema.Events.ItemDefinition` (dolaylı): `ItemSubjectRef` ile ilişkilidir.
    *   `System.Xml.XmlQualifiedName`: `ItemSubjectRef`'i tutmak için.
*   **Önemli Noktalar:**
    *   Aktivitelerin veri arayüzünü tanımlamak için kullanılır.
    *   Veri eşleştirme (data mapping) ve süreç değişkenlerinin yönetimi için önemlidir.
    *   Diyagram üzerinde doğrudan görünmezler, ancak `DataInputAssociation` ve `DataOutputAssociation` aracılığıyla etkilerini gösterirler. 