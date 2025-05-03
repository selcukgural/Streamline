# InputOutputSpecification

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `Activity`'nin (örn. `Task`, `SubProcess`) veya bir `CallableElement`'in veri giriş ve çıkış gereksinimlerini yapılandırılmış bir şekilde tanımlar. Hangi verilerin girdi olarak beklendiğini (`DataInput`), hangi verilerin çıktı olarak üretileceğini (`DataOutput`) ve bunların nasıl setler halinde (`InputSet`, `OutputSet`) gruplandığını belirtir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Activity` veya `CallableElement`'in `<ioSpecification>` alt etiketi olarak XML'de yer alır.
    *   Aktivitenin veya çağrılabilir elemanın ihtiyaç duyduğu tüm `DataInput`'ları ve üreteceği tüm `DataOutput`'ları listeler.
    *   `InputSet`'ler, aktivitenin yürütülmesi için gerekli olan farklı girdi kombinasyonlarını tanımlar. Genellikle bir `InputSet`'teki tüm `DataInput`'lar sağlandığında aktivite başlayabilir.
    *   `OutputSet`'ler, aktivite tamamlandığında üretilecek olan farklı çıktı kombinasyonlarını tanımlar. Genellikle bir `OutputSet`'teki tüm `DataOutput`'lar aktivite bitiminde değer alır.
    *   `DataInputAssociation`'lar, süreç değişkenlerini veya `DataObjectReference`'ları buradaki `DataInput`'lara bağlar.
    *   `DataOutputAssociation`'lar, buradaki `DataOutput`'ları süreç değişkenlerine veya `DataObjectReference`'lara bağlar.
*   **Özellikler:**
    *   `DataInput` (Collection<`DataInput`>, `XmlElement`): Aktivite veya `CallableElement` için tanımlanan veri girişlerinin koleksiyonu.
    *   `DataOutput` (Collection<`DataOutput`>, `XmlElement`): Aktivite veya `CallableElement` için tanımlanan veri çıkışlarının koleksiyonu.
    *   `InputSet` (Collection<`InputSet`>, `XmlElement`, Zorunlu): Tanımlanan `DataInput`'ları gruplayan bir veya daha fazla giriş seti.
    *   `OutputSet` (Collection<`OutputSet`>, `XmlElement`, Zorunlu): Tanımlanan `DataOutput`'ları gruplayan bir veya daha fazla çıkış seti.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Data.DataInput`
    *   `Streamline.Domain.Schema.Data.DataOutput`
    *   `Streamline.Domain.Schema.Common.InputSet`
    *   `Streamline.Domain.Schema.Common.OutputSet`
    *   `Streamline.Domain.Schema.Activities.Activity` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Common.CallableElement` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Aktivitelerin ve çağrılabilir elemanların veri arayüzünü resmi olarak tanımlar.
    *   Veri akışının (`DataAssociation`'lar aracılığıyla) nasıl gerçekleşeceğini belirlemek için temel oluşturur.
    *   En az bir `InputSet` ve bir `OutputSet` içermesi zorunludur. 