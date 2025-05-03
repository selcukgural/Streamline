# Incident

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`Incident`, bir süreç örneğinin yürütülmesi sırasında meydana gelen bir çalışma zamanı olayını veya problemini temsil eder. Genellikle başarısız olan bir iş (Job), yakalanamayan bir hata (Error) veya motorun ilerlemesini engelleyen başka bir beklenmedik durum sonucunda oluşturulur.

Incident'lar, operasyonel izleme ve problem çözme için önemli bilgiler sağlar.

`EntityBase` sınıfından türediği için bir `Id` ve `DomainEvents` koleksiyonuna sahiptir.

## Özellikler

*   **`IncidentType` { get; init; } : `string`**
    *   Olayın türünü kategorize eder. Örnekler: `"failedJob"` (bir iş başarısız oldu), `"configurationError"` (hatalı yapılandırma), `"unhandledError"` (yakalanamayan hata), `"unhandledEscalation"` (yakalanamayan yükseltme).
*   **`IncidentTimestamp` { get; init; } : `DateTime`**
    *   Olayın meydana geldiği zaman damgası (UTC).
*   **`ExecutionId` { get; init; } : `Guid`**
    *   Olayın meydana geldiği `Execution`'ın ID'si.
*   **`Execution` { get; init; } : `Execution`**
    *   Olayın meydana geldiği `Execution` nesnesine navigasyon özelliği.
*   **`ProcessInstanceId` { get; init; } : `Guid`**
    *   Olayın meydana geldiği `ProcessInstance`'ın ID'si.
*   **`ActivityId` { get; init; } : `string`**
    *   Olayın doğrudan ilişkili olduğu veya kök saldığı BPMN akış elemanının (Activity, Event, Gateway) ID'si.
*   **`JobId` { get; init; } : `Guid?`**
    *   Eğer olay başarısız bir iş (Job) ile ilgiliyse, o işin ID'si.
*   **`Job` { get; init; } : `Job?`**
    *   İlgili `Job` nesnesine navigasyon özelliği (varsa).
*   **`ProcessDefinitionId` { get; init; } : `string`**
    *   Olayın meydana geldiği süreç tanımının ID'si.
*   **`Message` { get; init; } : `string`**
    *   Olayı açıklayan kısa bir mesaj (genellikle hata mesajının özeti).
*   **`Details` { get; init; } : `string?`**
    *   Olay hakkında daha detaylı bilgi. Örneğin, bir iş başarısız olduysa yığın izi (stack trace) veya konfigürasyon detayları olabilir.
*   **`TenantId` { get; init; } : `string?`**
    *   Çoklu kiracılık kullanılıyorsa, olayın ait olduğu kiracının ID'si (opsiyonel).

## Kullanım

`Incident` nesneleri genellikle süreç motoru tarafından otomatik olarak oluşturulur:
*   Bir `Job` (örn. Zamanlayıcı, Zamanuyumsuz Devamlılık) yürütülürken bir istisna (exception) oluştuğunda.
*   Bir `ActivityInstance` `Fail()` metodu çağrıldığında (bazı implementasyonlarda `ActivityFailedEvent` yerine veya ek olarak `Incident` oluşturulabilir).
*   `ExecutionFlowManager` içinde yakalanamayan bir Hata (Error) veya Yükseltme (Escalation) olduğunda.

Bu olay kayıtları, operasyonel ekiplerin veya sistem yöneticilerinin süreçlerdeki problemleri tespit etmesi ve çözmesi için `IRepository<Incident>` üzerinden sorgulanabilir. 