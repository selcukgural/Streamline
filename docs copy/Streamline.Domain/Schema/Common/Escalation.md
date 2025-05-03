# Escalation

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Süreç içinde normal akışı kesintiye uğratmadan, genellikle daha üst bir seviyeye veya farklı bir role bildirilmesi gereken belirli bir iş durumunu (yükseltme) global olarak tanımlamak için kullanılır. `Error`'dan farklı olarak, genellikle süreci sonlandırmaz.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   Bir `Escalation Event` (Başlangıç, Bitiş, Ara Fırlatma, Sınır) ile ilişkilendirilir.
    *   Fırlatılan bir yükseltme (`Escalation End Event`, `Escalation Intermediate Throw Event`), belirli bir `Escalation` tanımına referans verir.
    *   Yakalanan bir yükseltme (`Escalation Boundary Event`, `Escalation Start Event`), genellikle `escalationCode` üzerinden eşleşen bir `Escalation` tanımını yakalar ve genellikle süreci kesintiye uğratmadan alternatif bir akış (örn. bildirim gönderme) başlatır.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Yükseltmenin okunabilir adı (isteğe bağlı).
    *   `EscalationCode` (string, `XmlAttribute`): Yükseltmeyi teknik olarak tanımlayan benzersiz bir kod. Yükseltme yakalama mekanizmalarında eşleştirme için kullanılır.
    *   `StructureRef` (`XmlQualifiedName`, `XmlAttribute`): Yükseltmeyle birlikte taşınabilecek verinin yapısını tanımlayan bir `ItemDefinition`'a referans (isteğe bağlı).
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Events.EscalationEventDefinition` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Common.ItemDefinition` (dolaylı, `structureRef` yoluyla)
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   `Error` ile karıştırılmamalıdır. `Escalation` genellikle normal akışı kesmez, sadece ek bir işlem (genellikle bir bildirim veya farklı bir yola dallanma) tetikler.
    *   Örneğin, bir görevin belirli bir sürede tamamlanmaması durumunda yöneticinin bilgilendirilmesi gibi durumları modellemek için kullanılır.
    *   Global olarak tanımlandığı için aynı yükseltme tanımı birden fazla süreçte veya aynı süreçteki farklı yerlerde kullanılabilir. 