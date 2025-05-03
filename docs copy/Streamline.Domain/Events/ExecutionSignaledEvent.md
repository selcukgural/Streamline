# ExecutionSignaledEvent

**Namespace:** `Streamline.Domain.Events`

Bu Domain Event, bir `Execution`\'ın belirli bir sinyal (Signal) aldığını veya bir sinyal tarafından tetiklendiğini belirtir.

## Özellikler

*   **`Execution TargetExecution` { get; }**: Sinyali alan veya sinyal tarafından etkilenen `Execution` nesnesinin referansını içerir.
*   **`string SignalName` { get; }**: Alınan veya tetiklenen sinyalin adını (BPMN modelinden gelen) içerir.
*   **`object? SignalPayload` { get; }**: Sinyal ile birlikte iletilen (varsa) veriyi içerir. Türü değişkendir.

## Kullanım

Bir `IntermediateCatchEvent` (Signal), `StartEvent` (Signal) veya `BoundaryEvent` (Signal) bir sinyal aldığında tetiklenir. Genellikle Application katmanında, gelen bir sinyal isteği işlendiğinde ve ilgili `EventSubscription` bulunduğunda oluşturulur.

Bu olayı dinleyen Handler\'lar:
*   Sinyal alındıktan sonra yapılması gereken ek iş mantığını yürütebilir.
*   Sinyal verisini (`SignalPayload`) işleyebilir.
*   Loglama veya izleme yapabilir. 