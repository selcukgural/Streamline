# ITimerJobScheduler

**Namespace:** `Streamline.Domain.Abstractions`

Bu arayüz, zamanlanmış görevlerin (özellikle BPMN Timer Event\'leri için) oluşturulması, güncellenmesi ve iptal edilmesi işlemlerini soyutlar. Motorun belirli bir zamanlama kütüphanesine (örn. Hangfire, Quartz.NET) doğrudan bağımlı olmasını engeller.

## Metotlar

*   **`Task<string> ScheduleTimerJobAsync(Guid subscriptionId, DateTime dueTime)`:**
    *   Belirtilen `subscriptionId` (genellikle bir `EventSubscription` ID\'si) ile ilişkili bir zamanlanmış görevi, belirtilen `dueTime` (UTC) zamanında tetiklenecek şekilde kurar.
    *   Zamanlama sistemi tarafından atanan benzersiz görev ID\'sini (`jobId`) string olarak döndürür. Bu ID, daha sonra görevi güncellemek veya iptal etmek için kullanılabilir.

*   **`Task CancelTimerJobAsync(string jobId)`:**
    *   Belirtilen `jobId`\'ye sahip zamanlanmış görevi iptal eder.
    *   Görev zaten çalıştıysa veya bulunamazsa genellikle bir hata fırlatmaz, işlemi sessizce tamamlar.

*   **`Task RescheduleTimerJobAsync(string oldJobId, Guid newSubscriptionId, DateTime newDueTime)`:**
    *   Mevcut bir zamanlanmış görevi (`oldJobId`) iptal edip, yeni bir görev zamanlar.
    *   Yeni görev, `newSubscriptionId` ile ilişkilendirilir ve `newDueTime` zamanında tetiklenir.
    *   Özellikle tekrarlayan (Time Cycle) timer\'ların bir sonraki tetiklenmesini kurmak için kullanılır.
    *   Yeni görevin ID\'sini string olarak döndürür. 