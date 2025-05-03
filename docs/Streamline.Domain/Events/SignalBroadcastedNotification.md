# SignalBroadcastedNotification

**Namespace:** `Streamline.Domain.Events`

Bu sınıf, bir Sinyalin (Signal) sistem içinde genel olarak yayınlandığını (broadcast) belirten bir bildirimdir (Notification). MediatR `INotification` arayüzünü uygular.

Mesajlardan farklı olarak sinyallerin genellikle belirli bir hedefi yoktur, tüm ilgilenen alıcılara gönderilir.

## Özellikler

*   **`string SignalName` { get; }**: Yayınlanan sinyalin adı.
*   **`object? Payload` { get; }**: Sinyalle birlikte gönderilen veri (opsiyonel).

## Kullanım

`IntermediateThrowEventHandler` (Signal) içinde, sinyali yayınlama işlemi gerçekleştirildikten sonra `IMediator.Publish()` ile bu bildirim yayınlanabilir.

Bu bildirimi dinleyen Handler\'lar (Notification Handler\'lar):
*   Bu sinyal adına (`SignalName`) abone olmuş *tüm* aktif `EventSubscription`\'ları (`IEventSubscriptionRepository` kullanarak) bulur.
*   Bulunan *her* abonelik için ilgili `Execution`\'ı `ExecutionFlowManager` aracılığıyla devam ettirir.
*   Sinyalin yayınlandığına dair loglama yapar. 