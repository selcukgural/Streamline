# ExecutionEscalatedEvent

**Namespace:** `Streamline.Domain.Events`

Bu Domain Event, bir `Execution` (yürütme) içinde bir Escalation (Yükseltme) olayının tetiklendiğini belirtir.

## Özellikler

*   **`Execution EscalatedExecution` { get; }**: Yükseltmeyi tetikleyen `Execution` nesnesinin referansını içerir.
*   **`string EscalationCode` { get; }**: Tetiklenen yükseltmenin kodunu (BPMN modelinden gelen) içerir.
*   **`string? EscalatedAtNodeId` { get; }**: Yükseltmenin hangi akış düğümünde (`FlowNode`) tetiklendiğinin ID\'sini içerir (opsiyonel olabilir).

## Kullanım

Genellikle `Execution.Escalate()` metodu içinde veya bir `IntermediateThrowEvent` (Escalation) ya da `EndEvent` (Escalation) handle edilirken tetiklenir.

Bu olayı dinleyen Handler\'lar şunları yapabilir:
*   Yükseltme durumuyla ilgili özel loglama yapabilir.
*   Belirli yükseltme kodlarına göre dış sistemlere bildirim gönderebilir.
*   Süreç motorunun yükseltme yayma (`FindAndHandleEscalationAsync`) mekanizmasından *ayrı olarak* ek iş mantığı çalıştırabilir. 