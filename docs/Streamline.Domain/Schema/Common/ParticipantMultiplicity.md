# ParticipantMultiplicity

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `Participant`'ın (Havuzun) çoklu örnek (multi-instance) olup olmadığını ve kaç tane örneğinin olabileceğini (minimum ve maksimum) belirtir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Participant` elemanının `<participantMultiplicity>` alt etiketi olarak XML'de yer alır.
    *   `maximum` özniteliği 1'den büyükse, bu katılımcının birden fazla örneği temsil ettiğini gösterir (örn. birden fazla müşteri, tedarikçi).
    *   Diyagramlarda genellikle katılımcı havuzunun alt orta kısmında üç dikey çizgi simgesi ile gösterilir.
*   **Özellikler:**
    *   `Minimum` (int, `XmlAttribute`, Varsayılan: `0`): Bu katılımcıdan olması gereken minimum örnek sayısı.
    *   `Maximum` (int, `XmlAttribute`, Varsayılan: `1`): Bu katılımcıdan olabilecek maksimum örnek sayısı. 1'den büyük bir değer veya sınırsız anlamına gelen özel bir gösterim (örn. `*` - kodda `int.MaxValue` olabilir) kullanılabilir.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Participant` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Bir rolün veya sistemin birden fazla bağımsız örneğiyle etkileşimde bulunulduğunu modellemek için kullanılır.
    *   Bu sadece bir gösterimdir; çalışma zamanında örneklerin nasıl yönetileceği genellikle süreç motorunun veya uygulamanın sorumluluğundadır. 