# Group

*   **BPMN Tipi:** Somut (Concrete) - `Artifact`
*   **Amaç:** Süreç diyagramındaki elemanları görsel olarak gruplamak için kullanılır. Genellikle analiz veya dokümantasyon amacıyla, belirli bir kategoriye ait veya birlikte ele alınması gereken elemanları vurgulamak için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Artifacts` (Ancak `docs` altında `Artifact` dizinine koyduk)
*   **Miras:** `Artifact` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Diyagram üzerinde genellikle kesikli çizgiyle çizilmiş bir dikdörtgen ile gösterilir ve içine aldığı elemanları çevreler.
    *   Akış mantığını etkilemez.
    *   İsteğe bağlı olarak bir `CategoryValue`'ya referans vererek grubun amacını veya kategorisini belirtebilir.
*   **Özellikler:**
    *   `CategoryValueRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Bu grubun temsil ettiği `CategoryValue` elemanının ID'sine referans.
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Kategori referansını tutmak için.
    *   `Streamline.Domain.Schema.Common.CategoryValue` (dolaylı): `CategoryValueRef` ile ilişkilidir.
*   **Önemli Noktalar:**
    *   Tamamen görsel ve dokümantasyon amaçlıdır, süreç yürütmesini etkilemez.
    *   Diyagramın karmaşıklığını azaltmaya ve belirli bölümleri vurgulamaya yardımcı olur. 