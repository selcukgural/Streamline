# ResourceParameter

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `Resource`'un sahip olduğu belirli bir parametreyi veya özelliği tanımlar. Kaynağın niteliklerini detaylandırmak için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Resource` elemanının `ResourceParameter` koleksiyonu içinde bulunur (`<resourceParameter>` alt etiketi).
    *   Parametrenin adını (`name`), veri tipini (`type` - `ItemDefinition` referansı) ve zorunlu olup olmadığını (`isRequired`) belirtir.
    *   Örneğin, bir "Danışman" kaynağının "uzmanlıkAlanı" (string) veya "saatlikÜcret" (decimal) gibi parametreleri olabilir.
    *   `ResourceParameterBinding` elemanı, bir aktivitenin bu parametreye nasıl bir değer atayacağını veya ondan nasıl bir değer alacağını tanımlar.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Parametrenin adı.
    *   `Type` (`XmlQualifiedName`, `XmlAttribute`): Parametrenin veri tipini tanımlayan `ItemDefinition` elemanının ID'sine referans (isteğe bağlı).
    *   `IsRequired` (bool, `XmlAttribute`): Bu parametrenin kaynak için zorunlu bir özellik olup olmadığını belirtir (isteğe bağlı).
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Resource` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Events.ItemDefinition` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.ResourceParameterBinding` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Kaynakların daha detaylı tanımlanmasını sağlar.
    *   Kaynak seçimi veya ataması sırasında bu parametreler kullanılabilir. 