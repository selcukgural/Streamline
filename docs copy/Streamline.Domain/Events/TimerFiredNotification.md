# TimerFiredNotification

**Namespace:** `Streamline.Domain.Events`

Bu sınıf, zamanlanmış bir görevin (Timer Job) tetiklendiğini (zamanının geldiğini) belirten bir bildirimdir (Notification). MediatR `INotification` arayüzünü uygular.

## Özellikler

*   **`Guid SubscriptionId` { get; }**: Tetiklenen zamanlayıcıya karşılık gelen `EventSubscription`\'ın ID\'si.

## Kullanım

Zamanlama sistemi (örn. Hangfire, Quartz.NET), zamanı gelen bir görevi çalıştırdığında, bu görevin iş mantığı (genellikle Infrastructure katmanında tanımlanan bir job metodu) `IMediator.Publish()` ile bu bildirimi yayınlar.

Bu bildirimi dinleyen Handler\'lar (Notification Handler\'lar, örn. `TimerFiredNotificationHandler`):
*   Verilen `SubscriptionId` ile ilgili `EventSubscription` nesnesini veritabanından yükler.
*   Abonelik bilgilerine göre (örn. Boundary Event mi, Intermediate Catch Event mi, kesintili mi) ilgili mantığı çalıştırır.
    *   Boundary Event ise: `IExecutionFlowManager.TriggerBoundaryTimerEventAsync` çağrılır.
    *   Intermediate Catch Event ise: İlgili `Execution` bulunur ve `IExecutionFlowManager.ContinueExecutionAsync` çağrılır.
*   Eğer timer tekrarlayan (Time Cycle) bir timersa, bir sonraki tetiklenme zamanını hesaplar ve `ITimerJobScheduler.RescheduleTimerJobAsync` ile görevi yeniden zamanlar, `EventSubscription`\'daki `JobId` ve `Configuration` (kalan tekrar sayısı) güncellenir.
*   Eğer son tekrarsa veya tek seferlik bir timersa, `EventSubscription` silinebilir veya pasif hale getirilebilir. 