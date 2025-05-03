# ActivityFailedEvent

**Namespace:** `Streamline.Domain.Events`

Bu Domain Event, bir `ActivityInstance`\'ın başarısız (Faulted) duruma geçtiğini belirtir.

## Özellikler

*   **`ActivityInstance FailedActivity` { get; }**: Başarısız olan `ActivityInstance` nesnesinin referansını içerir. Bu referans üzerinden aktivitenin ID\'si, FlowNode ID\'si, hata mesajı gibi detaylara erişilebilir.

## Kullanım

Genellikle `ActivityInstance.Fail()` metodu içinde bu olay tetiklenir (`AddDomainEvent`).

Bu olayı dinleyen Handler\'lar (genellikle Application katmanında) şunları yapabilir:
*   Bir Incident (olay kaydı) oluşturabilir.
*   Yöneticilere bildirim gönderebilir.
*   Başarısız olan aktiviteyle ilgili özel loglama yapabilir.
*   Süreç motorunun hata yayma (`FindAndHandleErrorAsync`) mekanizmasından *ayrı olarak* ek işlemler gerçekleştirebilir. 