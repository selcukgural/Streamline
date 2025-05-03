# MessageBroadcastedNotification

**Namespace:** `Streamline.Domain.Events`

Bu sınıf, bir Mesajın (Message) sistem içinde yayınlandığını belirten bir bildirimdir (Notification). Genellikle MediatR `INotification` arayüzünü uygular.

Domain Event yerine Notification olarak adlandırılması, bunun genellikle Application katmanında, bir mesajın *fırlatılması* (örn. `IntermediateThrowEvent` (Message) veya `SendTask` tarafından) sonucunda tetiklendiğini ve diğer Application veya Infrastructure bileşenlerini bilgilendirme amacı taşıdığını düşündürür.

## Özellikler

*   **`string MessageName` { get; }**: Yayınlanan mesajın adını içerir.
*   **`Guid? CorrelationId` { get; }**: Mesajla ilişkili olabilecek bir korelasyon ID\'si (opsiyonel).
*   **`object? Payload` { get; }**: Mesajla birlikte gönderilen veri (opsiyonel).

## Kullanım

`IntermediateThrowEventHandler` (Message) veya `SendTaskHandler` içinde, mesajı yayınlama işlemi gerçekleştirildikten sonra `IMediator.Publish()` ile bu bildirim yayınlanabilir.

Bu bildirimi dinleyen Handler\'lar (Notification Handler\'lar):
*   Mesajı bir dış sisteme (örn. mesaj kuyruğu) gönderebilir.
*   İlgili loglamayı yapabilir.
*   Mesaj gönderimiyle ilgili istatistikleri güncelleyebilir. 