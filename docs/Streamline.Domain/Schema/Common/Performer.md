# Performer

*   **BPMN Tipi:** Somut (Concrete) / Soyut (Abstract) (Genellikle alt sınıfları kullanılır) - `ResourceRole`
*   **Amaç:** Bir `Activity`'yi (genellikle `UserTask`) gerçekleştiren genel bir kaynağı temsil eder. Bu, insan (`HumanPerformer`) veya potansiyel olarak başka tür bir kaynak olabilir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `ResourceRole` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `ResourceRole`'den türediği için, bir aktivitenin kaynak gereksinimlerini tanımlamak amacıyla kullanılır.
    *   `HumanPerformer` ve `PotentialOwner` sınıfları bu sınıftan türemiştir.
    *   Genellikle doğrudan kullanılmak yerine, daha spesifik olan alt sınıfları (`HumanPerformer`, `PotentialOwner`) tercih edilir.
    *   Bir `Resource` tanımına (`ResourceRef` ile) veya bir `ResourceAssignmentExpression`'a bağlanarak kaynağın kimliği belirlenir.
*   **Özellikler:**
    *   (`ResourceRole` ve `BaseElement`'ten ilgili özellikleri miras alır: `Name`, `ResourceRef`, `ResourceParameterBinding`, `ResourceAssignmentExpression`, `Id`, `Documentation` vb.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.ResourceRole` (Miras aldığı sınıf)
    *   `Streamline.Domain.Schema.Common.HumanPerformer` (Alt sınıfı)
    *   `Streamline.Domain.Schema.Common.PotentialOwner` (Alt sınıfı)
    *   `Streamline.Domain.Schema.Activities.Activity` (İlişkilendirilebilir)
    *   `Streamline.Domain.Schema.Common.Resource` (dolaylı, referans yoluyla)
*   **Önemli Noktalar:**
    *   Aktivitelerin kaynak atamalarını modellemek için kullanılan hiyerarşinin bir parçasıdır.
    *   Pratikte genellikle `HumanPerformer` veya `PotentialOwner` olarak kullanılır. 