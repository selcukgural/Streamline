# ActivityInstance

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`ActivityInstance`, çalışan bir `ProcessInstance` içindeki belirli bir BPMN akış elemanının (örneğin bir Task, SubProcess, Gateway veya Event) tek bir yürütülmesini veya örneğini temsil eder. Bir `Execution` (yürütme token'ı) bir akış elemanına ulaştığında, motor genellikle bu eleman için bir `ActivityInstance` oluşturur.

Bu sınıf, aktivitenin durumunu, başlangıç ve bitiş zamanlarını ve aktivite türüne bağlı olarak ek bilgileri (örn. User Task için atama bilgileri) takip eder.

`EntityBase` sınıfından türediği için bir `Id` ve `DomainEvents` koleksiyonuna sahiptir.

## Özellikler

*   **`ProcessInstanceId` { get; init; } : `Guid`**
    *   Bu aktivite örneğinin ait olduğu `ProcessInstance`'ın ID'si.
*   **`ProcessInstance` { get; set; } : `ProcessInstance`**
    *   Bu aktivite örneğinin ait olduğu `ProcessInstance` nesnesine navigasyon özelliği.
*   **`ExecutionId` { get; init; } : `Guid?`**
    *   Bu aktivite örneğini oluşturan veya ilişkilendirilen `Execution`'ın ID'si.
*   **`Execution` { get; init; } : `Execution?`**
    *   İlişkili `Execution` nesnesine navigasyon özelliği.
*   **`FlowNodeId` { get; init; } : `string`**
    *   BPMN süreç tanımındaki ilgili akış elemanının (Activity, Gateway, Event) ID'si.
*   **`FlowNodeName` { get; init; } : `string?`**
    *   BPMN tanımındaki ilgili akış elemanının adı (opsiyonel, loglama/izleme için kullanışlıdır).
*   **`State` { get; set; } : `ActivityInstanceState`**
    *   Aktivite örneğinin mevcut durumunu gösterir (`Active`, `Completed`, `Faulted`, `Cancelled`, vb.). Bkz: `Enums.cs`.
*   **`StartTime` { get; init; } : `DateTime`**
    *   Aktivite örneğinin aktif hale geldiği zaman damgası (UTC).
*   **`EndTime` { get; set; } : `DateTime?`**
    *   Aktivite örneğinin sona erdiği (tamamlandığı, hata verdiği, iptal edildiği) zaman damgası (UTC). Hala aktifse `null` değerindedir.

### Göreve Özgü Özellikler (Task Specific)

Bu özellikler genellikle Kullanıcı Görevleri (User Tasks) için anlamlıdır:

*   **`AssigneeId` { get; set; } : `string?`**
    *   Görevin atandığı kullanıcının ID'si.
*   **`CandidateUserIds` { get; set; } : `string?`**
    *   Görevi alabilecek aday kullanıcıların ID'lerinin virgülle ayrılmış listesi.
*   **`CandidateGroupIds` { get; set; } : `string?`**
    *   Görevi alabilecek aday grupların ID'lerinin virgülle ayrılmış listesi.
*   **`DueDate` { get; set; } : `DateTime?`**
    *   Görevin son teslim tarihi (opsiyonel).
*   **`FollowUpDate` { get; set; } : `DateTime?`**
    *   Görevin takip tarihi (opsiyonel).
*   **`Priority` { get; set; } : `int?`**
    *   Görevin önceliği (opsiyonel).

### Çağrı Aktivitesine Özgü Özellikler (Call Activity Specific)

*   **`CalledProcessInstanceId` { get; set; } : `Guid?`**
    *   Eğer bu bir Çağrı Aktivitesi (Call Activity) ise, başlattığı alt süreç örneğinin `ProcessInstanceId`'si.

### Hataya Özgü Özellikler (Error/Fault Specific)

*   **`ErrorMessage` { get; set; } : `string?`**
    *   Aktivite örneği `Faulted` durumundaysa, ilgili hata mesajı.
*   **`ErrorDetails` { get; set; } : `string?`**
    *   Aktivite örneği `Faulted` durumundaysa, hatanın detayları veya yığın izi (stack trace).

## Metotlar (Davranışlar)

`ActivityInstance` sınıfı, durumunu yönetmek için temel metotları içerir:

*   **`Complete()`**: Aktif (`Active`) durumdaki bir aktivite örneğini tamamlanmış (`Completed`) durumuna geçirir.
*   **`Fail(string errorMessage, string? errorDetails = null)`**: Aktif (`Active`) durumdaki bir aktivite örneğini hatalı (`Faulted`) durumuna geçirir, hata bilgilerini kaydeder ve `ActivityFailedEvent` domain olayını tetikler.
*   **`Cancel()`**: Aktif (veya duruma göre diğer) bir aktivite örneğini iptal edilmiş (`Cancelled`) durumuna geçirir.
*   **`Assign(string assigneeId)`**: Genellikle aktif bir Kullanıcı Görevini belirli bir kullanıcıya atar.

## Kullanım

`ActivityInstance` nesneleri, süreç motoru tarafından bir `Execution` bir akış elemanına girdiğinde oluşturulur ve aktivite tamamlandığında veya sonlandırıldığında güncellenir. Genellikle süreç örneklerinin geçmişini (audit log) veya mevcut durumunu sorgulamak için kullanılırlar. `ProcessInstance` üzerindeki `ActivityInstances` koleksiyonu veya `IRepository<ActivityInstance>` aracılığıyla erişilebilirler. 