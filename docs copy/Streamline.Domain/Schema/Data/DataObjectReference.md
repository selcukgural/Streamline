# DataObjectReference

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir süreç diyagramı üzerinde belirli bir `DataObject`'un kullanımını veya görünümünü temsil eder. `DataObject` mantıksal veri tanımını yaparken, `DataObjectReference` bu verinin süreç akışının belirli bir noktasındaki halini (ve potansiyel olarak durumunu) gösterir.
*   **Konum:** `Streamline.Domain.Schema.Data`
*   **Miras:** `FlowElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Diyagram üzerinde görünen veri nesnesi simgesidir.
    *   Hangi `DataObject`'a (`dataObjectRef`) referans verdiğini belirtir.
    *   Ayrıca, `DataObject`'ta olduğu gibi, verinin tipini (`itemSubjectRef`) ve durumunu (`DataState`) da içerebilir (genellikle `DataObject`'tan miras alınır veya orada tanımlanır).
    *   `DataAssociation` ile aktivitelere bağlanarak veri giriş/çıkışını gösterir.
*   **Özellikler:**
    *   `DataObjectRef` (string, `XmlAttribute`, İsteğe Bağlı): Referans verilen `DataObject` elemanının ID'si.
    *   `ItemSubjectRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Veri tipini tanımlayan `ItemDefinition`'a referans (genellikle `DataObject`'tan alınır).
    *   `DataState` (`DataState`, `XmlElement`, İsteğe Bağlı): Verinin bu noktadaki durumunu belirten referans.
    *   `Name` (string, `XmlAttribute`, Miras): Diyagramda görünen adı (genellikle `DataObject`'un adını alır).
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Data.DataObject` (dolaylı): `DataObjectRef` ile ilişkilidir.
    *   `Streamline.Domain.Schema.Data.DataState`: Veri durumunu tutan sınıf.
    *   `Streamline.Domain.Schema.Events.ItemDefinition` (dolaylı): `ItemSubjectRef` ile ilişkilidir.
    *   `System.Xml.XmlQualifiedName`: `ItemSubjectRef`'i tutmak için.
*   **Önemli Noktalar:**
    *   Aynı `DataObject` birden fazla `DataObjectReference` ile diyagramın farklı yerlerinde temsil edilebilir (farklı durumları göstermek için).
    *   Süreç verisinin akış içindeki kullanımını görselleştirmek için kullanılır. 