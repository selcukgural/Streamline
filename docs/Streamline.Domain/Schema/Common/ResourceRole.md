# ResourceRole

*   **BPMN Tipi:** Somut (Concrete) / Soyut (Abstract) (Genellikle alt sınıfları kullanılır) - `BaseElement`
*   **Amaç:** Bir `Process` veya `Activity` ile ilişkili olan ve genellikle o aktiviteyi gerçekleştirmek için gereken bir kaynağın (`Resource`) rolünü veya türünü tanımlar. `Performer`, `HumanPerformer`, `PotentialOwner` gibi daha spesifik kaynak rolleri için temel oluşturur.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Process` veya `Activity`'nin `ResourceRole` koleksiyonu içinde bulunur.
    *   Bir aktiviteyi gerçekleştirecek kaynağı belirtmek için kullanılır.
    *   Kaynak, ya doğrudan `resourceRef` ile belirli bir `Resource`'a bağlanır ya da `resourceAssignmentExpression` ile dinamik olarak belirlenir.
    *   `resourceParameterBinding`'ler, kaynağın parametrelerinin bu rol bağlamında nasıl değerleneceğini belirtir.
    *   `Performer`, `HumanPerformer`, `PotentialOwner` bu sınıftan türemiştir ve genellikle `ResourceRole` yerine bu daha spesifik alt sınıflar kullanılır.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Kaynak rolünün adı (örn. "Onaylayan", "Sistem Yöneticisi", "Yazıcı").
    *   `ResourceRef` (`XmlQualifiedName`, `XmlElement`): Bu rolü üstlenen belirli `Resource` elemanının ID'sine referans (isteğe bağlı).
    *   `ResourceAssignmentExpression` (`ResourceAssignmentExpression`, `XmlElement`): Rolü üstlenecek kaynağı dinamik olarak belirleyen ifade (isteğe bağlı). `ResourceRef` ile birlikte kullanılamaz.
    *   `ResourceParameterBinding` (Collection<`ResourceParameterBinding`>, `XmlElement`): `ResourceRef` ile referans verilen kaynağın `ResourceParameter`'larının bu rol bağlamında nasıl değer alacağını tanımlayan bağlamalar.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Resource` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.ResourceAssignmentExpression`
    *   `Streamline.Domain.Schema.Common.ResourceParameterBinding`
    *   `Streamline.Domain.Schema.Common.Performer` (Alt sınıfı)
    *   `Streamline.Domain.Schema.Common.Process` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Activities.Activity` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Aktivitelerin kaynak gereksinimlerini ve atamalarını modellemek için merkezi bir elemandır.
    *   Genellikle doğrudan `ResourceRole` yerine `Performer`, `HumanPerformer` veya `PotentialOwner` kullanılır. 