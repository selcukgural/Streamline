# Extension

*   **BPMN Tipi:** Yardımcı Eleman
*   **Amaç:** Bir BPMN `Definitions` dosyasında kullanılan belirli bir standart dışı uzantıyı (`ExtensionDefinition`) deklare etmek için kullanılır. Bu, modelin belirli bir araç veya platforma özgü ek özellikler içerdiğini belirtir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** Yok (Doğrudan `Definitions` elemanının bir koleksiyon özelliği içinde kullanılır)
*   **Uygulama & Davranış:**
    *   Bir `Definitions` elemanının `<extension>` alt etiketi olarak XML'de yer alır.
    *   Belirli bir `ExtensionDefinition`'a referans verir.
    *   `mustUnderstand` özniteliği, bu uzantının modeli işleyen araç tarafından anlaşılmasının zorunlu olup olmadığını belirtir.
*   **Özellikler:**
    *   `Definition` (`XmlQualifiedName`, `XmlAttribute`): Kullanılan uzantının tanımına (`ExtensionDefinition` elemanının ID'sine) bir referans. Bu alan boş olabilir, ancak genellikle uzantının ne olduğunu belirtmek için kullanılır.
    *   `MustUnderstand` (bool, `XmlAttribute`, Varsayılan: `false`): Eğer `true` olarak ayarlanırsa, bu BPMN dosyasını işleyen herhangi bir yazılımın bu uzantıyı tanıması ve doğru şekilde işlemesi gerektiğini belirtir. Eğer yazılım uzantıyı anlamıyorsa, genellikle modeli işlemeyi reddetmelidir. `false` ise, anlaşılmayan uzantılar göz ardı edilebilir.
    *   `Documentation` (Collection<`Documentation`>, `XmlElement`): Bu uzantı kullanımıyla ilgili ek açıklamalar.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.ExtensionDefinition` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.Definitions` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.Documentation`
*   **Önemli Noktalar:**
    *   BPMN standardının genişletilebilirliğini sağlar.
    *   Farklı araçlar veya platformlar arasında özel veri veya davranışların nasıl ele alınacağını yönetmeye yardımcı olur.
    *   `mustUnderstand="true"` kullanımı, modelin belirli bir uzantıya kritik olarak bağımlı olduğunu gösterir. 