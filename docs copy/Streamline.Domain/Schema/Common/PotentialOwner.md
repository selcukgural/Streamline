# PotentialOwner

*   **BPMN Tipi:** Somut (Concrete) - `HumanPerformer`
*   **Amaç:** Bir `UserTask`'ı gerçekleştirmek için *potansiyel* olarak sorumlu olan insan kullanıcıları veya gruplarını tanımlar. Görev bu kişilere veya gruplara atanabilir veya bu kişiler/gruplar görevi talep edebilir (claim).
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `HumanPerformer` -> `Performer` -> `ResourceRole` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Genellikle bir `UserTask`'ın `ResourceRole` koleksiyonu içinde bulunur.
    *   `HumanPerformer`'dan miras aldığı için, bir `Resource`'a (`ResourceRef` ile) veya bir `ResourceAssignmentExpression`'a bağlanır.
    *   `ResourceAssignmentExpression` genellikle potansiyel sahipleri dinamik olarak belirleyen bir ifade içerir (örn. "role:Yönetici", "users:ahmet,mehmet", "group:SatışEkibi").
    *   Bir `UserTask`'ın birden fazla `PotentialOwner`'ı olabilir.
*   **Özellikler:**
    *   (`HumanPerformer`, `Performer`, `ResourceRole`, `BaseElement`'ten ilgili tüm özellikleri miras alır. En önemlileri `ResourceAssignmentExpression` veya `ResourceRef`'tir.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.HumanPerformer` (Miras aldığı sınıf)
    *   `Streamline.Domain.Schema.Activities.UserTask` (Genellikle içinde bulunur)
    *   `Streamline.Domain.Schema.Common.Resource` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.ResourceAssignmentExpression`
*   **Önemli Noktalar:**
    *   `UserTask` atamalarını modellemenin standart yoludur.
    *   Görevlerin belirli rollere, gruplara veya dinamik olarak belirlenen kullanıcılara yönlendirilmesini sağlar. 