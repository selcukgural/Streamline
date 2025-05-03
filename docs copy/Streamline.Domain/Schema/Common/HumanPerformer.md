# HumanPerformer

*   **BPMN Tipi:** Somut (Concrete) - `Performer`
*   **Amaç:** Bir `Activity`'yi, özellikle bir `UserTask`'ı gerçekleştiren insan kaynağını temsil eder. Bu, belirli bir kullanıcı, bir grup veya bir rol olabilir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `Performer` -> `ResourceRole` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Genellikle bir `Activity`'nin `ResourceRole` koleksiyonu içinde veya daha spesifik olarak bir `UserTask`'ın `ResourceAssignmentExpression`'ı içinde kullanılır.
    *   `PotentialOwner` sınıfı, bu sınıftan türemiştir ve bir `UserTask`'ın potansiyel sahiplerini (görevi alabilecek kişi veya grupları) tanımlar.
    *   `HumanPerformer` ise genellikle görevi fiilen yapan kişiyi veya atanmış kişiyi temsil eder.
    *   Bir `Resource` tanımına (`ResourceRef` ile) veya bir `FormalExpression`'a (`ResourceAssignmentExpression` içinde) bağlanarak hangi insan kaynağının kastedildiği belirlenir.
*   **Özellikler:**
    *   (`Performer`, `ResourceRole`, `BaseElement`'ten ilgili özellikleri miras alır. Bunlar arasında `Name`, `ResourceRef`, `ResourceParameterBinding`, `ResourceAssignmentExpression` gibi özellikler bulunur.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Performer` (Miras aldığı sınıf)
    *   `Streamline.Domain.Schema.Common.PotentialOwner` (Alt sınıfı)
    *   `Streamline.Domain.Schema.Activities.UserTask` (Genellikle ilişkili olduğu aktivite)
    *   `Streamline.Domain.Schema.Common.Resource` (dolaylı, `ResourceRef` yoluyla)
    *   `Streamline.Domain.Schema.Common.ResourceAssignmentExpression` (Genellikle içinde kullanılır)
*   **Önemli Noktalar:**
    *   İnsan etkileşimi gerektiren görevlerin (`UserTask`) kaynak atamalarını modellemek için temel bir elemandır.
    *   `PotentialOwner` ile birlikte kullanılarak görevlerin kimler tarafından yapılabileceği ve kimin yaptığı belirtilir. 