# Error

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Süreç akışı sırasında meydana gelebilecek ve normal akışı kesintiye uğratabilecek belirli bir hata durumunu global olarak tanımlamak için kullanılır. Bu tanımlar, hata olayları (`ErrorEventDefinition`) tarafından referans alınır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   Bir `Error Event` (Başlangıç, Bitiş, Ara Fırlatma, Sınır) ile ilişkilendirilir.
    *   Fırlatılan bir hata (`Error End Event`, `Error Intermediate Throw Event`), belirli bir `Error` tanımına referans verir.
    *   Yakalanan bir hata (`Error Boundary Event`, `Error Start Event`), genellikle `errorCode` üzerinden eşleşen bir `Error` tanımını yakalar.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Hatanın okunabilir adı (isteğe bağlı).
    *   `ErrorCode` (string, `XmlAttribute`): Hatayı teknik olarak tanımlayan benzersiz bir kod. Hata yakalama mekanizmalarında eşleştirme için kullanılır.
    *   `StructureRef` (`XmlQualifiedName`, `XmlAttribute`): Hatayla birlikte taşınabilecek verinin yapısını tanımlayan bir `ItemDefinition`'a referans (isteğe bağlı).
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Events.ErrorEventDefinition` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Common.ItemDefinition` (dolaylı, `structureRef` yoluyla)
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Süreçlerdeki istisnai durumları modellemek için kullanılır.
    *   Hata kodları (`ErrorCode`), belirli hataların yakalanıp farklı şekilde ele alınmasını sağlar (örneğin, "Stok Yetersiz" hatası için farklı bir akış izlemek).
    *   Global olarak tanımlandığı için aynı hata tanımı birden fazla süreçte veya aynı süreçteki farklı yerlerde kullanılabilir. 