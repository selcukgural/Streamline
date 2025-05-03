# MessageReceivedNotification

**Namespace:** `Streamline.Domain.Events`

Bu sınıf, belirli bir Mesajın (Message) sistem tarafından alındığını ve potansiyel olarak bir veya daha fazla bekleyen süreci tetikleyebileceğini belirten bir bildirimdir (Notification). Genellikle MediatR `INotification` arayüzünü uygular.

## Özellikler

*   **`string MessageName` { get; }**: Alınan mesajın adı.
*   **`Guid? CorrelationId` { get; }**: Mesajla ilişkili olabilecek korelasyon ID\'si (opsiyonel).
*   **`object? Payload` { get; }**: Mesajla birlikte alınan veri (opsiyonel).

## Kullanım

Dış bir kaynaktan (örn. API endpoint, mesaj kuyruğu) bir mesaj alındığında, Application katmanındaki ilgili servis veya controller tarafından `IMediator.Publish()` ile bu bildirim yayınlanabilir.

Bu bildirimi dinleyen Handler\'lar (Notification Handler\'lar):
*   `IEventSubscriptionRepository` kullanarak bu mesaj adına (`MessageName`) ve potansiyel olarak korelasyon anahtarına (`Payload` içindeki verilere göre) abone olmuş aktif `EventSubscription`\'ları bulur.
*   Bulunan her abonelik için ilgili `Execution`\'ı `ExecutionFlowManager` aracılığıyla devam ettirir (veya `ReceiveTask` ise `ActivityInstance`\'ı tamamlar).
*   Mesajın alındığına dair loglama yapar. 