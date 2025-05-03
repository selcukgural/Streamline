# DataStoreReference

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Sürecin *dışında* bulunan bir veri depolama mekanizmasına (veritabanı, dosya sistemi, harici servis vb.) yapılan bir referansı temsil eder. Sürecin bu dış veri kaynağından veri okuduğunu veya yazdığını gösterir.
*   **Konum:** `Streamline.Domain.Schema.Data`
*   **Miras:** `FlowElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Diyagram üzerinde genellikle silindir şeklinde bir simge ile gösterilir.
    *   Hangi mantıksal `DataStore` elemanına (`dataStoreRef`) referans verdiğini belirtir (eğer `DataStore` tanımlanmışsa).
    *   Ayrıca, içerdiği verinin tipini (`itemSubjectRef`) ve durumunu (`DataState`) belirtebilir.
    *   `DataAssociation` ile aktivitelere bağlanarak veri okuma/yazma işlemini gösterir.
*   **Özellikler:**
    *   `DataStoreRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Referans verilen `DataStore` elemanının ID'si.
    *   `ItemSubjectRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Veri deposundaki verinin tipini tanımlayan `ItemDefinition`'a referans.
    *   `DataState` (`DataState`, `XmlElement`, İsteğe Bağlı): Verinin bu noktadaki durumunu belirten referans.
    *   `Name` (string, `XmlAttribute`, Miras): Diyagramda görünen adı (genellikle `DataStore`'un adını alır).
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Data.DataStore` (dolaylı): `DataStoreRef` ile ilişkilidir.
    *   `Streamline.Domain.Schema.Data.DataState`: Veri durumunu tutan sınıf.
    *   `Streamline.Domain.Schema.Events.ItemDefinition` (dolaylı): `ItemSubjectRef` ile ilişkilidir.
    *   `System.Xml.XmlQualifiedName`: `DataStoreRef` ve `ItemSubjectRef`'i tutmak için.
*   **Önemli Noktalar:**
    *   `DataObject`'tan farklı olarak, sürecin kendisinin sahip olmadığı, dışarıdaki kalıcı veriyi temsil eder.
    *   Sürecin dış sistemlerle veya veritabanlarıyla olan etkileşimini modellemek için kullanılır. 