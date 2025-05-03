# ProcessInstance (Aggregate Root)

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`ProcessInstance`, bir BPMN süreç tanımının tek bir çalıştırılmasını (yürütülmesini) temsil eden temel Aggregate Root varlığıdır. Bir süreç başlatıldığında, bu süreç tanımı için yeni bir `ProcessInstance` nesnesi oluşturulur.

Süreç örneği, sürecin genel durumunu, ilişkili verileri (değişkenler) ve süreç boyunca gerçekleşen tüm aktivite ve olayların izini tutar.

`EntityBase` sınıfından türediği için bir `Id` ve `DomainEvents` koleksiyonuna sahiptir.

## Özellikler

*   **`ProcessDefinitionId` { get; init; } : `string`**
    *   Bu süreç örneğinin ait olduğu BPMN süreç tanımının benzersiz kimliği (genellikle BPMN XML'indeki `process` elemanının `id`'si).
*   **`BusinessKey` { get; init; } : `string?`**
    *   Bu süreç örneğiyle ilişkilendirilmiş, iş alanına özgü bir anahtar (opsiyonel). Örneğin, bir sipariş ID'si veya müşteri numarası olabilir. Süreç örneklerini iş verilerine göre bulmak için kullanılır.
*   **`State` { get; set; } : `ProcessInstanceState`**
    *   Süreç örneğinin mevcut durumunu gösterir (`Running`, `Suspended`, `Completed`, `Terminated`, `Aborted`). Bkz: `Enums.cs`.
*   **`StartTime` { get; init; } : `DateTime`**
    *   Süreç örneğinin başlatıldığı zaman damgası (UTC).
*   **`EndTime` { get; set; } : `DateTime?`**
    *   Süreç örneğinin sona erdiği (tamamlandığı, iptal edildiği veya sonlandırıldığı) zaman damgası (UTC). Örnek hala çalışıyorsa veya askıdaysa `null` değerindedir.

## İlişkili Koleksiyonlar (Navigation Properties)

Bu koleksiyonlar, süreç örneğiyle ilişkili diğer çalışma zamanı varlıklarını içerir:

*   **`Variables` { get; } : `IReadOnlyCollection<Variable>`**
    *   Bu süreç örneğine ait değişkenlerin (process variables) salt okunur koleksiyonu.
*   **`ActivityInstances` { get; } : `IReadOnlyCollection<ActivityInstance>`**
    *   Bu süreç örneği kapsamında yürütülen veya yürütülmüş aktivite örneklerinin (task, subprocess vb.) salt okunur koleksiyonu.
*   **`Executions` { get; } : `IReadOnlyCollection<Execution>`**
    *   Bu süreç örneği içinde aktif olan yürütme token'larının (genellikle paralel akışları temsil eder) salt okunur koleksiyonu.
*   **`EventSubscriptions` { get; } : `IReadOnlyCollection<EventSubscription>`**
    *   Bu süreç örneğiyle ilişkili olay aboneliklerinin (örn. bekleyen mesajlar, sinyaller, zamanlayıcılar için) salt okunur koleksiyonu.
*   **`Jobs` { get; } : `IReadOnlyCollection<Job>`**
    *   Bu süreç örneğiyle ilişkili zamanlanmış işlerin (örn. timer olayları, zamanuyumsuz devamlar) salt okunur koleksiyonu.
*   **`Incidents` { get; } : `IReadOnlyCollection<Incident>`**
    *   Bu süreç örneği sırasında meydana gelen olay kayıtlarının (hatalar, teknik problemler) salt okunur koleksiyonu.

## Metotlar

`ProcessInstance` sınıfı, yaşam döngüsünü yönetmek için aşağıdaki metotları sağlar:

*   **`AddVariable(Variable variable)`**, **`AddActivityInstance(ActivityInstance activityInstance)`**, **`AddExecution(Execution execution)`**, **`AddIncident(Incident incident)`**: İlgili koleksiyonlara yeni öğeler eklemek için kullanılır (genellikle motorun iç mantığı tarafından çağrılır).
*   **`Complete()`**: Süreç örneğini başarıyla tamamlanmış (`Completed`) durumuna geçirir ve `EndTime` değerini ayarlar.
*   **`Suspend()`**: Çalışan (`Running`) bir süreç örneğini askıya alınmış (`Suspended`) duruma geçirir.
*   **`Activate()`**: Askıya alınmış (`Suspended`) bir süreç örneğini tekrar çalışır (`Running`) duruma getirir.
*   **`Terminate(bool force = false)`**: Süreç örneğini sonlandırılmış (`Terminated`) durumuna geçirir. Genellikle dış bir müdahale sonucu kullanılır. İlişkili yürütmelerin ve işlerin sonlandırılması beklenir.
*   **`Abort(string? reason = null)`**: Süreç örneğini iptal edilmiş (`Aborted`) durumuna geçirir. Genellikle beklenmedik bir hata veya iş kuralı ihlali nedeniyle kullanılır.

## Kullanım

Bir süreç başlatıldığında (`IProcessEngineService.StartProcessInstanceAsync` gibi bir metotla) yeni bir `ProcessInstance` oluşturulur. Motor, süreç ilerledikçe bu nesnenin durumunu ve ilişkili koleksiyonlarını günceller. Uygulama katmanı servisleri veya sorgular, süreç örneklerinin durumunu takip etmek veya yönetmek için bu nesneye erişir (genellikle `IRepository<ProcessInstance>` aracılığıyla). 