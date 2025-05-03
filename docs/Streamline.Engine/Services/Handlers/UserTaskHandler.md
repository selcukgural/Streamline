# UserTaskHandler

*   **BPMN Elementi:** `User Task` (Kullanıcı Görevi)
*   **BPMN Amacı:** Bir insanın gerçekleştirmesi gereken bir görevi modeller. Genellikle bir kullanıcı arayüzünde görünür ve bir kullanıcı tarafından tamamlanması beklenir. Atama (assignee), aday kullanıcılar/gruplar (candidate users/groups) gibi özelliklere sahip olabilir.
*   **Uygulama & Davranış:**
    *   Bu handler bir `UserTask`\'a ulaşıldığında çalışır.
    *   İlişkili bir `ActivityInstance` oluşturur veya mevcut aktif olanı bulur. Durumunu `Active` olarak ayarlar/korur.
    *   BPMN modelinden okunan `assignee`, `candidateUsers`, `candidateGroups`, `dueDate` gibi bilgileri `ActivityInstance` üzerine kaydeder.
    *   Mevcut `Execution`\'ı bu `UserTask` düğümünde **beklemeye alır** (`ArriveAtConvergingGateway` benzeri bir metotla `CurrentFlowNodeId`\'yi ayarlar ve `IsActive` durumunu korur, ancak otomatik devam etmez).
    *   Giden akışları **tetiklemez**. Akışın devam etmesi için dışarıdan bir tetikleyici gerekir (örneğin, bir kullanıcının görevi tamamladığını belirten bir API çağrısı, bu çağrı sonucunda ilgili `ActivityInstance` bulunur, `Complete` edilir ve ardından `ExecutionFlowManager.ContinueExecutionAsync` çağrılır). 