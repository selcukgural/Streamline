# ItemKind

*   **BPMN Tipi:** Enum (Enumeration)
*   **Amaç:** Bir `ItemDefinition` tarafından tanımlanan öğenin türünü belirtir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Değerler:**
    *   `Information`: Öğenin bilgi tabanlı olduğunu gösterir. Bu, en yaygın kullanılan ve varsayılan değerdir. Süreç değişkenleri, mesaj içerikleri, veri nesneleri genellikle bu türdedir.
    *   `Physical`: Öğenin fiziksel bir varlığı temsil ettiğini gösterir. Bu daha az kullanılır ve genellikle fiziksel üretim veya lojistik süreçlerinde anlamlı olabilir.
*   **Kullanım:**
    *   `ItemDefinition` elemanının `itemKind` özniteliğinde kullanılır.
*   **Önemli Noktalar:**
    *   Modeldeki verinin doğası hakkında ek bilgi sağlar. 