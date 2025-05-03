# EventSubscription

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`EventSubscription`, bir `Execution`'ın (yürütme token'ının) belirli bir olayın (mesaj, sinyal, zamanlayıcı, koşul vb.) gerçekleşmesini beklediğini temsil eden bir varlıktır. Süreç motoru, bir `Execution` beklemeye neden olan bir BPMN elemanına (örn. Intermediate Catch Event, Receive Task, Boundary Event, Event Subprocess Start Event) ulaştığında bir `EventSubscription` oluşturur.

Motor, dışarıdan gelen olayları (örn. alınan bir mesaj, tetiklenen bir zamanlayıcı) ilgili `EventSubscription`'ları bularak eşleştirir ve bekleyen `Execution`'ı devam ettirir.

`EntityBase` sınıfından türediği için bir `Id` ve `DomainEvents` koleksiyonuna sahiptir.

## Özellikler

*   **`EventType` { get; init; } : `string`**
    *   Beklenen olayın türünü belirtir. Yaygın türler: `"message"`, `"signal"`, `"timer"`, `"conditional"`, `"compensation"`.
*   **`EventName` { get; init; } : `string`**
    *   Beklenen olayın adını belirtir (örn. mesaj adı, sinyal adı). Zamanlayıcı veya koşul gibi bazı olay türleri için bu alan kullanılmayabilir veya özel bir anlam taşıyabilir.
*   **`ExecutionId` { get; init; } : `Guid`**
    *   Bu olayı bekleyen `Execution`'ın ID'si.
*   **`Execution` { get; init; } : `Execution`**
    *   Bu olayı bekleyen `Execution` nesnesine navigasyon özelliği.
*   **`ProcessInstanceId` { get; init; } : `Guid`**
    *   Bu aboneliğin ait olduğu `ProcessInstance`'ın ID'si.
*   **`ActivityId` { get; init; } : `string`**
    *   Bu aboneliği oluşturan BPMN akış elemanının (Catch Event, Boundary Event, Receive Task vb.) ID'si.
*   **`AttachedToActivityId` { get; init; } : `string?`**
    *   Eğer bu abonelik bir Sınır Olayı (Boundary Event) tarafından oluşturulduysa, olayın eklendiği aktivitenin (`Activity`) ID'sini içerir. Diğer durumlar için `null`'dır.
*   **`CreatedTime` { get; init; } : `DateTime`**
    *   Aboneliğin oluşturulduğu zaman damgası (UTC).
*   **`Configuration` { get; private set; } : `string?`**
    *   Aboneliğe özgü ek yapılandırma bilgilerini içerir. Örnekler:
        *   Zamanlayıcılar için: ISO 8601 formatında zaman tanımı (`TimeDate`, `TimeDuration`, `TimeCycle`).
        *   Mesajlar için: Korelasyon anahtarlarını veya beklentilerini tanımlayan veriler (örn. JSON formatında).
        *   Koşullu olaylar için: Değerlendirilecek koşul ifadesi.
*   **`TenantId` { get; init; } : `string?`**
    *   Çoklu kiracılık (multi-tenancy) kullanılıyorsa, bu aboneliğin ait olduğu kiracının ID'si (opsiyonel).
*   **`JobId` { get; set; } : `string?`**
    *   Eğer bu abonelik bir zamanlayıcı (timer) ile ilişkiliyse ve bir zamanlama sistemi (örn. Hangfire) kullanılıyorsa, zamanlama sistemindeki ilgili işin (job) ID'sini tutar. Bu, işi iptal etmek veya yeniden zamanlamak için kullanılır.

## Metotlar

*   **`SetConfiguration(string? configuration)`**: Aboneliğin `Configuration` özelliğini günceller. Özellikle tekrarlayan zamanlayıcıların kalan tekrar sayısını güncellemek gibi senaryolarda kullanılabilir.

## Kullanım

`EventSubscription` nesneleri, süreç motoru tarafından `Execution`'lar olayları beklemeye başladığında oluşturulur ve olay gerçekleştiğinde (veya abonelik iptal olduğunda) silinir veya güncellenir. `IEventSubscriptionRepository` gibi bir depo aracılığıyla sorgulanarak, belirli bir olayın (örn. gelen bir mesaj) hangi `Execution`'ları tetiklemesi gerektiği belirlenir. 