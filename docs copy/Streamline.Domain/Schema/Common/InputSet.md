# InputSet

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `InputOutputSpecification` içinde tanımlanan `DataInput` elemanlarını mantıksal bir grup olarak tanımlar. Bir aktivitenin veya çağrılabilir elemanın yürütülmesi için gerekli olan belirli bir girdi kombinasyonunu temsil eder.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `InputOutputSpecification`'ın `InputSet` koleksiyonu içinde bulunur.
    *   Bir aktivitenin başlayabilmesi için hangi `DataInput`'ların bir arada mevcut olması gerektiğini belirtir.
    *   Bir `InputOutputSpecification` birden fazla `InputSet` içerebilir; bu, aktivitenin farklı girdi kombinasyonlarıyla başlayabileceği anlamına gelir.
    *   Genellikle, bir `InputSet`'teki `dataInputRefs` ile listelenen ve `optionalInputRefs`'te olmayan tüm `DataInput`'lar bir değer aldığında, aktivite o `InputSet` için hazır kabul edilir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Giriş setinin adı (isteğe bağlı).
    *   `DataInputRefs` (Collection<string>, `XmlElement`): Bu giriş setini oluşturan `DataInput` elemanlarının ID'lerine referanslar. Bu referanslar, aynı `InputOutputSpecification` içinde tanımlanmış `DataInput`'lara işaret etmelidir.
    *   `OptionalInputRefs` (Collection<string>, `XmlElement`): `DataInputRefs`'te listelenenlerden hangilerinin aktivitenin başlaması için *zorunlu olmadığını* belirten `DataInput` ID'lerine referanslar.
    *   `WhileExecutingInputRefs` (Collection<string>, `XmlElement`): Aktivite çalışırken (`EXECUTING` durumundayken) erişilebilen veya güncellenebilen `DataInput` ID'lerine referanslar.
    *   `OutputSetRefs` (Collection<string>, `XmlElement`): Bu giriş seti sağlandığında üretilmesi beklenen `OutputSet`'lerin ID'lerine referanslar (isteğe bağlı ilişkilendirme).
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Data.DataInput` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.OutputSet` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.InputOutputSpecification` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Aktivite başlatma koşullarının veri gereksinimleri açısından tanımlanmasında önemlidir.
    *   Bir `InputOutputSpecification`'ın en az bir `InputSet` içermesi zorunludur. 