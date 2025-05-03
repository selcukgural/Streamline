# CompensateEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Daha önce başarıyla tamamlanmış bir aktivitenin etkilerini geri almak veya telafi etmek için kullanılan mekanizmayla ilgilidir. Genellikle uzun süren işlemlerde (transaction) veya hata durumlarında kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `IntermediateThrowEvent` (telafi işlemini tetikler), `EndEvent` (telafi işlemini tetikleyerek biter), `BoundaryEvent` (bir aktiviteye bağlı telafi işlemini tanımlar - bu olay yakalanmaz, sadece telafi aktivitesine bağlanır), `StartEvent` (yalnızca Event Sub-Process içinde, telafi işlemini başlatır) ile kullanılır.
    *   Bir `IntermediateThrowEvent` veya `EndEvent` içindeyse, telafi edilecek aktiviteyi (`ActivityRef`) belirtir. Eğer belirtmezse, içinde bulunduğu kapsamdaki (scope) tüm tamamlanmış ve telafi edilebilir aktiviteleri tetikler.
    *   Bir `BoundaryEvent` içindeyse, telafi işlemi tetiklendiğinde *çağrılacak* olan telafi aktivitesine (`ActivityRef`) işaret eder (genellikle `isForCompensation=true` olan bir `Task` veya `SubProcess`).
*   **Özellikler:**
    *   `WaitForCompletion` (bool, `XmlAttribute`, İsteğe Bağlı): Telafi işlemi başlatan olay için, telafi aktivitesinin tamamlanmasını bekleyip beklemeyeceğini belirtir (varsayılan genellikle true).
    *   `ActivityRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Telafi edilecek veya telafi işlemini gerçekleştirecek olan aktivitenin ID'sine referans.
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Aktivite referansını tutmak için.
*   **Önemli Noktalar:**
    *   Telafi (Compensation) karmaşık bir BPMN konusudur ve genellikle SAGA paterni gibi dağıtık işlem senaryolarında kullanılır.
    *   Telafi akışı normal süreç akışının dışındadır.
    *   Bir aktivitenin telafi edilebilir olması için genellikle ona bir `Compensate Boundary Event` bağlanır ve bu olay da telafi işlemini yapan aktiviteye (`isForCompensation=true`) bir `Association` ile bağlanır. 