# EscalationEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir olayın belirli bir yükseltme (`Escalation`) durumuyla ilgili olduğunu belirtir. Yükseltmeler, hatalardan farklı olarak, normal akışı kesintiye uğratmadan bir üst seviyeye (genellikle başka bir departman veya yönetici) bildirimde bulunmak için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `EndEvent` (yükseltme yayınlayarak biten - ama akışı kesmez), `BoundaryEvent` (bir aktivite sırasında yükseltme yakalayan - kesintili veya kesintisiz olabilir), `IntermediateThrowEvent` (yükseltme yayınlayan), `StartEvent` (yalnızca Event Sub-Process içinde, yükseltme yakalayarak alt süreci başlatan) ile kullanılır.
    *   Genellikle hangi `Escalation` elemanının yayınlandığını veya yakalandığını referans gösterir.
*   **Özellikler:**
    *   `EscalationRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): İlişkili `Escalation` elemanının ID'sine referans.
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Yükseltme referansını tutmak için.
    *   `Streamline.Domain.Schema.Common.Escalation` (dolaylı): `EscalationRef` ile ilişkilidir.
*   **Önemli Noktalar:**
    *   Hatalardan temel farkı, varsayılan olarak akışı kesintiye uğratmamasıdır (`BoundaryEvent`'in `cancelActivity` özelliğine bağlıdır).
    *   Bir sorun hakkında bilgilendirme yapmak veya paralel bir telafi akışı başlatmak için kullanılır.
    *   `EscalationRef` genellikle yükseltmenin adını ve kodunu (`escalationCode` özelliği `Escalation` elemanındadır) tanımlayan `Escalation` elemanına işaret eder. 