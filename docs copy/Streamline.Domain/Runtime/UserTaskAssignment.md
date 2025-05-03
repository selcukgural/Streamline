# UserTaskAssignment

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`UserTaskAssignment`, bir süreç örneği içindeki Kullanıcı Görevinin (User Task) çalışma zamanı durumunu, atamasını ve ilgili bilgilerini temsil eder. Bir `Execution` bir User Task düğümüne ulaştığında, motor bu görevi insan etkileşimi için hazırlar ve genellikle bir `UserTaskAssignment` kaydı oluşturur.

Bu kayıtlar, kullanıcıların kendilerine atanmış veya talep edebilecekleri görevleri gördüğü bir **Görev Listesi (Tasklist)** uygulamasının temelini oluşturur.

`EntityBase` sınıfından türediği için bir `Id` ve `DomainEvents` koleksiyonuna sahiptir (ancak genellikle domain event tetiklemez).

## Özellikler

*   **`ProcessInstanceId` { get; init; } : `Guid`**
    *   Bu görevin ait olduğu `ProcessInstance`'ın ID'si.
*   **`ExecutionId` { get; init; } : `Guid`**
    *   Bu Kullanıcı Görevinde bekleyen `Execution`'ın ID'si.
*   **`TaskDefinitionId` { get; init; } : `string`**
    *   BPMN süreç tanımındaki ilgili `userTask` elemanının ID'si.
*   **`TaskName` { get; set; } : `string?`**
    *   BPMN tanımından kopyalanan görevin adı.
*   **`Description` { get; set; } : `string?`**
    *   Görev için sağlanan açıklama veya talimatlar (opsiyonel).
*   **`Assignee` { get; set; } : `string?`**
    *   Görevin şu anda atandığı kullanıcının ID'si. Eğer görev henüz kimseye atanmadıysa (aday durumundaysa) veya bir gruba atanmışsa `null` olabilir.
*   **`CandidateGroups` { get; set; } : `string?`**
    *   Görevi talep edebilecek (claim) aday grupların ID'lerinin virgülle ayrılmış listesi.
*   **`CandidateUsers` { get; set; } : `string?`**
    *   Görevi talep edebilecek aday kullanıcıların ID'lerinin virgülle ayrılmış listesi.
*   **`Status` { get; set; } : `UserTaskStatus`**
    *   Görevin mevcut durumunu gösterir: `Candidate` (aday, talep edilebilir), `Assigned` (bir kullanıcıya atanmış), `Completed` (tamamlanmış).
*   **`CreatedTime` { get; init; } : `DateTime`**
    *   Görevin oluşturulduğu zaman damgası (UTC).
*   **`ClaimedTime` { get; set; } : `DateTime?`**
    *   Görevin bir kullanıcı tarafından talep edildiği veya doğrudan atandığı zaman damgası (UTC).
*   **`CompletedTime` { get; set; } : `DateTime?`**
    *   Görevin tamamlandığı zaman damgası (UTC).
*   **`DueDate` { get; set; } : `DateTime?`**
    *   Görevin son teslim tarihi (opsiyonel).
*   **`Priority` { get; set; } : `int`**
    *   Görevin önceliği (genellikle sayısal bir değer, örn. 0-100).

## Metotlar (Davranışlar)

*   **`Claim(string userId)`**: Aday (`Candidate`) durumundaki bir görevi belirtilen `userId` adına atar (`Assigned` durumuna geçirir). Görevin bu kullanıcı tarafından talep edilip edilemeyeceğini kontrol etmek ek mantık gerektirebilir.
*   **`Unclaim()`**: Atanmış (`Assigned`) durumdaki bir görevin atamasını kaldırır ve tekrar aday (`Candidate`) durumuna getirir.
*   **`Complete()`**: Atanmış (`Assigned`) durumdaki bir görevi tamamlanmış (`Completed`) durumuna geçirir. Bu işlem genellikle ilgili `Execution`'ı süreç akışında devam ettirir.

## Kullanım

`UserTaskAssignment` kayıtları, motor bir User Task'e ulaştığında oluşturulur (veya Tasklist uygulaması tarafından senkronize edilir). Kullanıcılar, Tasklist arayüzü üzerinden:
*   Kendilerine atanmış (`Assignee = currentUserId` ve `Status = Assigned`) görevleri görür.
*   Üyesi oldukları gruplara atanmış veya doğrudan kendilerine aday gösterilmiş (`CandidateGroups` veya `CandidateUsers` içinde ve `Status = Candidate`) görevleri görür ve talep edebilir (`Claim`).
*   Atanmış görevleri tamamlayabilir (`Complete`).

Bu kayıtlar, genellikle `IRepository<UserTaskAssignment>` aracılığıyla Tasklist uygulaması tarafından sorgulanır ve yönetilir.

## UserTaskStatus Enum

```csharp
public enum UserTaskStatus
{
    /// <summary>Task is created and available for candidates to claim.</summary>
    Candidate = 0,
    /// <summary>Task is assigned to a specific user.</summary>
    Assigned = 1,
    /// <summary>Task has been completed.</summary>
    Completed = 2
}
```