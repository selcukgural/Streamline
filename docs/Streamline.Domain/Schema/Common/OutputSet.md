# OutputSet

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `InputOutputSpecification` içinde tanımlanan `DataOutput` elemanlarını mantıksal bir grup olarak tanımlar. Bir aktivitenin veya çağrılabilir elemanın yürütülmesi sonucunda üretilen belirli bir çıktı kombinasyonunu temsil eder.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `InputOutputSpecification`'ın `OutputSet` koleksiyonu içinde bulunur.
    *   Bir aktivite tamamlandığında hangi `DataOutput`'ların bir arada değer alacağını belirtir.
    *   Bir `InputOutputSpecification` birden fazla `OutputSet` içerebilir; bu, aktivitenin farklı sonuçlar üretebileceği anlamına gelir.
    *   Genellikle, bir aktivite tamamlandığında, aktif olan `InputSet` ile ilişkili (`inputSetRefs` ile) veya varsayılan `OutputSet`'teki `dataOutputRefs` ile listelenen ve `optionalOutputRefs`'te olmayan tüm `DataOutput`'lar bir değer alır.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Çıkış setinin adı (isteğe bağlı).
    *   `DataOutputRefs` (Collection<string>, `XmlElement`): Bu çıkış setini oluşturan `DataOutput` elemanlarının ID'lerine referanslar. Bu referanslar, aynı `InputOutputSpecification` içinde tanımlanmış `DataOutput`'lara işaret etmelidir.
    *   `OptionalOutputRefs` (Collection<string>, `XmlElement`): `DataOutputRefs`'te listelenenlerden hangilerinin aktivite tamamlandığında bir değere sahip olmasının *zorunlu olmadığını* belirten `DataOutput` ID'lerine referanslar.
    *   `WhileExecutingOutputRefs` (Collection<string>, `XmlElement`): Aktivite çalışırken (`EXECUTING` durumundayken) değeri ayarlanabilen veya erişilebilen `DataOutput` ID'lerine referanslar.
    *   `InputSetRefs` (Collection<string>, `XmlElement`): Bu çıkış setinin üretilmesine neden olan `InputSet`'lerin ID'lerine referanslar (isteğe bağlı ilişkilendirme).
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Data.DataOutput` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.InputSet` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.InputOutputSpecification` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Aktivite tamamlama sonrası veri durumunu tanımlamada önemlidir.
    *   Bir `InputOutputSpecification`'ın en az bir `OutputSet` içermesi zorunludur. 