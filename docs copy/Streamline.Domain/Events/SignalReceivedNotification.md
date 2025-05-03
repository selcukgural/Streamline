# SignalReceivedNotification

**Namespace:** `Streamline.Domain.Events`

Bu sınıf, sistemin dışarıdan bir Sinyal (Signal) aldığını belirten bir bildirimdir (Notification). Bu, `SignalBroadcastedNotification`\'dan farklı olarak, sinyalin *içeride* değil, *dışarıdan* geldiğini ima eder (veya doğrudan belirli abonelikleri hedeflemek için kullanılabilir).

## Özellikler

*   **`string SignalName` { get; }**: Alınan sinyalin adı.
*   **`object? Payload` { get; }**: Sinyalle birlikte alınan veri (opsiyonel).

## Kullanım

Dış bir kaynaktan (örn. API endpoint) bir sinyal alındığında, Application katmanındaki ilgili servis veya controller tarafından `IMediator.Publish()` ile bu bildirim yayınlanabilir.

Bu bildirimi dinleyen Handler\'lar (Notification Handler\'lar):
*   `IEventSubscriptionRepository` kullanarak bu sinyal adına (`SignalName`) abone olmuş *tüm* aktif `EventSubscription`\'ları bulur.
*   Bulunan *her* abonelik için ilgili `Execution`\'ı `ExecutionFlowManager` aracılığıyla devam ettirir.
*   Sinyalin alındığına dair loglama yapar.

**Not:** Eğer sistemde sinyaller sadece içeriden (`IntermediateThrowEvent`) tetikleniyorsa ve dışarıdan sinyal alımı yoksa, bu bildirim sınıfına ihtiyaç olmayabilir ve sadece `SignalBroadcastedNotification` yeterli olabilir. 