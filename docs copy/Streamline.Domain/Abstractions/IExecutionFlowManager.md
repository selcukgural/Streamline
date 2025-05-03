# IExecutionFlowManager

**Namespace:** `Streamline.Domain.Abstractions`

Bu arayüz, bir süreç örneği (Process Instance) içindeki yürütme (Execution) akışını BPMN tanımına göre yöneten servisin kontratını tanımlar. Süreç motorunun temel mantığını soyutlar.

## Metotlar

*   **`Task ContinueExecutionAsync(Guid executionId, CancellationToken cancellationToken)`:**
    *   Belirtilen ID\'ye sahip yürütmeyi (token) mevcut durumundan devam ettirir.
    *   Bir sonraki düğümü veya düğümleri belirler ve yürütmeyi oraya taşır.

*   **`Task ForkAndContinueExecutionAsync(Execution originalExecution, Definitions definitions, SequenceFlow flowToFollow, CancellationToken cancellationToken)`:**
    *   Paralel bir ağ geçidi (Parallel Gateway) ayrımından (fork) çıkan belirli bir sıra akışı (`flowToFollow`) için yeni bir yürütme oluşturur.
    *   Yeni yürütmeyi hedef düğüme yerleştirir ve devam etmesini tetikler.
    *   Bu metodu çağırmadan önce orijinal yürütmenin (`originalExecution`) sonlandırılması beklenir.

*   **`Task<Execution> StartExecutionAtNodeAsync(Guid processInstanceId, string targetFlowNodeId, Guid? initiatingExecutionId, CancellationToken cancellationToken)`:**
    *   Bir süreç örneği içinde, belirtilen bir akış düğümünden (`targetFlowNodeId`) doğrudan yeni bir yürütme oluşturur.
    *   Oluşturulan yürütmenin hemen devam etmesini tetikler.
    *   Özellikle kesintili olmayan sınır olayları (non-interrupting boundary events) gibi senaryolarda veya belirli bir noktadan süreç başlatmak için kullanılır.
    *   `initiatingExecutionId`, bu başlatmayı tetikleyen (varsa) yürütmenin ID\'sini belirtir.

*   **`Task TriggerBoundaryTimerEventAsync(EventSubscription subscription, bool cancelActivity, CancellationToken cancellationToken)`:**
    *   Bir Sınır Zamanlayıcı Olayı (Boundary Timer Event) tetiklendiğinde çalışacak mantığı başlatır.
    *   Tetiklenen olayın abonelik bilgisini (`subscription`) alır.
    *   `cancelActivity` parametresi, sınır olayının kesintili (interrupting) olup olmadığını belirtir.

*   **`Task<bool> FindAndHandleErrorAsync(Execution failedExecution, Definitions definitions, string errorCode, CancellationToken cancellationToken)`:**
    *   Başarısız olan bir yürütme (`failedExecution`) tarafından tetiklenen bir hata durumunda, uygun bir Hata Sınır Olayı (Error Boundary Event) veya Hata Başlangıç Olayı (Error Start Event - Event Subprocess içinde) arar.
    *   Aramayı mevcut kapsamdan (scope) başlayarak yukarı doğru (parent scope\'lara) yapar.
    *   Uygun bir handler bulursa onu çalıştırır (genellikle yeni bir execution başlatır ve kapsamı sonlandırır) ve `true` döner.
    *   Handler bulunamazsa `false` döner.

*   **`Task<bool> FindAndHandleEscalationAsync(Execution originatingExecution, Definitions definitions, string escalationCode, string? escalatedAtNodeId, CancellationToken cancellationToken)`:**
    *   Bir Yükseltme Olayı (Escalation Event) tetiklendiğinde, uygun bir Yükseltme Sınır Olayı (Escalation Boundary Event) veya Yükseltme Başlangıç Olayı (Escalation Start Event - Event Subprocess içinde) arar.
    *   Aramayı mevcut kapsamdan başlayarak yukarı doğru yapar.
    *   Uygun bir handler bulursa onu çalıştırır (yeni bir execution başlatabilir, kesintiliyse kapsamı sonlandırabilir) ve `true` döner.
    *   Handler bulunamazsa `false` döner.

*   **`Task TerminateScopeContentsAsync(Execution scopeExecution, bool terminateScopeItself, string reason, CancellationToken cancellationToken)`:**
    *   Belirtilen bir kapsam yürütmesi (`scopeExecution` - örn. bir Alt Sürecin scope execution\'ı) tarafından tanımlanan kapsam içindeki *tüm* aktif yürütmeleri ve ilişkili aktif `ActivityInstance`\'ları sonlandırır/iptal eder.
    *   Genellikle kesintili (interrupting) bir sınır olayı veya olay alt süreci (event subprocess) tetiklendiğinde kullanılır.
    *   `terminateScopeItself` parametresi `true` ise, kapsamı tanımlayan `scopeExecution`\'ın kendisi de sonlandırılır. 