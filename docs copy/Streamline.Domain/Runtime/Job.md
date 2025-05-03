# Job

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`Job`, süreç motoru tarafından daha sonra veya zamanuyumsuz olarak yürütülmesi gereken bir işi temsil eder. Tipik kullanım alanları şunlardır:
*   **Zamanlayıcı Olayları (Timer Events):** Belirli bir zamanda veya periyodik olarak tetiklenecek olaylar için.
*   **Zamanuyumsuz Devamlılıklar (Asynchronous Continuations):** Süreç akışında "async before" veya "async after" işaretlenmiş noktalarda, işlemi farklı bir thread'de veya zamanda devam ettirmek için.
*   **Harici Görevler (External Tasks):** İşin bir dış sistem tarafından alınmasını ve tamamlanmasını beklemek için (bu desene bağlı olarak).

`Job`'lar genellikle ayrı bir **Job Executor** bileşeni tarafından alınır, kilitlenir, yürütülür ve başarılı olursa silinir veya başarısız olursa yeniden denenir.

`EntityBase` sınıfından türediği için bir `Id` ve `DomainEvents` koleksiyonuna sahiptir (ancak genellikle domain event tetiklemez).

## Özellikler

*   **`ExecutionId` { get; init; } : `Guid`**
    *   Bu işin ilişkili olduğu `Execution`'ın ID'si.
*   **`Execution` { get; init; } : `Execution`**
    *   İlişkili `Execution` nesnesine navigasyon özelliği.
*   **`ProcessInstanceId` { get; init; } : `Guid`**
    *   Bu işin ait olduğu `ProcessInstance`'ın ID'si.
*   **`ProcessDefinitionId` { get; init; } : `string`**
    *   Bu işin ait olduğu süreç tanımının ID'si.
*   **`JobHandlerType` { get; init; } : `string`**
    *   Bu işi nasıl yürüteceğini bilen işleyicinin (handler) türünü belirtir. Motorun veya Job Executor'ın doğru mantığı çağırmasını sağlar. Örnekler: `"timer-intermediate-catch"`, `"async-continuation"`, `"timer-boundary-event"`, `"message-job"`.
*   **`JobHandlerConfiguration` { get; init; } : `string?`**
    *   İş işleyicisinin ihtiyaç duyduğu ek yapılandırma bilgilerini içerir. Örnekler:
        *   Zamanlayıcı işleri için: İlgili `EventSubscription` ID'si veya zamanlayıcı tanımı.
        *   Zamanuyumsuz devamlılıklar için: Devam edilecek aktivitenin ID'si.
*   **`DueDate` { get; set; } : `DateTime?`**
    *   İşin yürütülmeye uygun hale geleceği zaman damgası (UTC). Zamanlayıcı işleri için bu, tetiklenme zamanıdır. Zamanuyumsuz işler için genellikle hemen (`DateTime.UtcNow`) veya bir gecikmeden sonra olabilir.
*   **`Retries` { get; set; } : `int`**
    *   İş başarısız olursa, kaç kez daha yeniden deneneceğini gösterir. Her başarısız denemede bu sayı azalır.
*   **`LastFailureTime` { get; set; } : `DateTime?`**
    *   İşin en son başarısız olduğu zaman damgası (UTC).
*   **`LastFailureMessage` { get; set; } : `string?`**
    *   Son başarısızlığın hata mesajı.
*   **`LastFailureDetails` { get; set; } : `string?`**
    *   Son başarısızlığın detayları (örn. stack trace).
*   **`LockOwner` { get; set; } : `string?`**
    *   İşi şu anda yürütmek üzere kilitleyen Job Executor örneğinin benzersiz kimliği. Eğer `null` ise, iş kilitli değildir ve alınabilir.
*   **`LockExpirationTime` { get; set; } : `DateTime?`**
    *   `LockOwner` tarafından alınan kilidin ne zaman geçersiz olacağını belirten zaman damgası (UTC). Bu, bir executor çökerse işin başka bir executor tarafından alınabilmesini sağlar.
*   **`TenantId` { get; init; } : `string?`**
    *   Çoklu kiracılık kullanılıyorsa, işin ait olduğu kiracının ID'si (opsiyonel).

## Metotlar (Davranışlar)

*   **`RecordFailure(string message, string? details, int defaultRetries)`**: İşin başarısız olduğunu kaydeder, `Retries` sayısını azaltır, hata bilgilerini günceller ve kilidi sıfırlar. Kalan deneme hakkı varsa `DueDate`'i bir sonraki deneme için ayarlar, yoksa `DueDate`'i `null` yaparak işi pasif hale getirir (veya ölü harf kuyruğuna taşınmaya hazır hale getirir).
*   **`AcquireLock(string owner, TimeSpan lockDuration)`**: İşi belirtilen `owner` adına, belirtilen `lockDuration` süresi için kilitler.
*   **`ReleaseLock()`**: İş üzerindeki kilidi kaldırır.
*   **`MarkAsDeadLetter(string failureMessage, string? failureDetails = null)`**: İşi kalıcı olarak başarısız (`Retries = 0`, `DueDate = null`) olarak işaretler ve hata bilgilerini kaydeder. Bu işler genellikle manuel müdahale gerektirir.

## Kullanım

`Job` nesneleri süreç motoru tarafından ilgili BPMN yapılarında (zamanlayıcılar, async işaretleri) oluşturulur. Job Executor, periyodik olarak veritabanını sorgulayarak `DueDate`'i geçmiş ve kilitli olmayan işleri bulur, `AcquireLock` ile kilitler, `JobHandlerType`'a göre ilgili mantığı çalıştırır. Başarılı olursa işi siler, başarısız olursa `RecordFailure` çağırır. Eğer bir iş uzun süre başarısız olursa `MarkAsDeadLetter` ile işaretlenir. 