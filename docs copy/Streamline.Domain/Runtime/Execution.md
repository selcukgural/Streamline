# Execution

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`Execution`, bir `ProcessInstance` içindeki belirli bir yürütme yolunu veya "token"ı temsil eden bir varlıktır. BPMN'de akışın ilerlediği yolu gösterir. Bir süreç örneğinde, özellikle paralel veya kapsayıcı (sub-process) yapılar kullanıldığında, birden fazla `Execution` aynı anda aktif olabilir.

Her `Execution`, süreç akışında belirli bir noktada bulunur ve kendi yerel değişkenlerine, olay aboneliklerine ve ilişkili işlere sahip olabilir.

`EntityBase` sınıfından türediği için bir `Id` ve `DomainEvents` koleksiyonuna sahiptir.

## Özellikler

*   **`ProcessInstanceId` { get; init; } : `Guid`**
    *   Bu yürütmenin ait olduğu `ProcessInstance`'ın ID'si.
*   **`ProcessInstance` { get; set; } : `ProcessInstance`**
    *   Bu yürütmenin ait olduğu `ProcessInstance` nesnesine navigasyon özelliği.
*   **`ParentExecutionId` { get; init; } : `Guid?`**
    *   Eğer bu yürütme başka bir yürütmenin çocuğu ise (örn. paralel gateway'den sonra veya bir alt süreç içinde oluşturulduysa), ebeveyn `Execution`'ın ID'si. Ana süreç akışı için `null`'dır.
*   **`ParentExecution` { get; init; } : `Execution?`**
    *   Ebeveyn `Execution` nesnesine navigasyon özelliği.
*   **`CurrentFlowNodeId` { get; set; } : `string?`**
    *   Bu yürütme token'ının şu anda bulunduğu BPMN akış elemanının (Activity, Event, Gateway) ID'si. Yürütme aktif olarak bir düğümde beklemiyorsa (örn. bir sıra akışı üzerindeyse veya yeni oluşturulduysa) `null` olabilir.
*   **`WaitingAtGatewayId` { get; set; } : `string?`**
    *   Eğer yürütme bir birleştirici ağ geçidinde (converging gateway) diğer paralel yolların gelmesini bekliyorsa, bu ağ geçidinin ID'sini tutar. Aksi takdirde `null`'dır.
*   **`IsActive` { get; set; } : `bool`**
    *   Bu yürütme yolunun aktif olup olmadığını gösterir. Birleştiğinde veya sonlandığında `false` olur.
*   **`IsScope` { get; init; } : `bool`**
    *   Bu yürütmenin bir kapsam (scope) temsil edip etmediğini belirtir. Kapsamlar, değişkenlerin veya olayların (örn. hata, yükseltme) etki alanını sınırlar. Ana süreç örneği ve alt süreçler tipik olarak birer kapsamdır.
*   **`ScopeFlowNodeId` { get; init; } : `string?`**
    *   Eğer `IsScope` `true` ise ve bu bir alt süreç kapsamını temsil ediyorsa, ebeveyn kapsamdaki ilgili alt süreç düğümünün (`SubProcess`) ID'sini tutar. Ana süreç kapsamı için `null`'dır.
*   **`ErrorCode` { get; private set; } : `string?`**
    *   Yürütme bir Hata (Error) nedeniyle başarısız olduysa, ilgili BPMN Hata Kodunu içerir.
*   **`ErrorMessage` { get; private set; } : `string?`**
    *   Yürütme başarısız olduysa, hata mesajını içerir.
*   **`FailedAtNodeId` { get; private set; } : `string?`**
    *   Yürütme başarısız olduysa, hatanın meydana geldiği akış düğümünün ID'sini içerir.
*   **`LastEscalationCode` { get; private set; } : `string?`**
    *   Bir Yükseltme (Escalation) olayı bu yürütme tarafından tetiklendiyse veya işlendiyse, ilgili BPMN Yükseltme Kodunu içerir.

## İlişkili Koleksiyonlar

*   **`Variables` { get; } : `IReadOnlyCollection<Variable>`**
    *   Bu yürütme kapsamına özel (local) değişkenlerin salt okunur koleksiyonu.
*   **`EventSubscriptions` { get; } : `IReadOnlyCollection<EventSubscription>`**
    *   Bu yürütme yolunda bekleyen olay aboneliklerinin (örn. bir mesaj veya sinyal bekleyen Catch Event) salt okunur koleksiyonu.
*   **`Jobs` { get; } : `IReadOnlyCollection<Job>`**
    *   Bu yürütmeyle ilişkili işlerin (örn. zamanuyumsuz devamlılık (async continuation) için oluşturulan job) salt okunur koleksiyonu.
*   **`Incidents` { get; } : `IReadOnlyCollection<Incident>`**
    *   Bu yürütme yolu üzerinde meydana gelen olay kayıtlarının salt okunur koleksiyonu.

## Metotlar (Davranışlar)

`Execution` sınıfı, motor tarafından akışı yönetmek için kullanılan davranış metotları içerir:

*   **`EnterFlowNode(string flowNodeId)`**: Yürütmeyi belirtilen akış düğümüne taşır.
*   **`LeaveFlowNode()`**: Yürütmenin mevcut düğümden ayrıldığını işaret eder.
*   **`Terminate(string? reason = null)`**: Bu yürütme yolunu sonlandırır (`IsActive`'i `false` yapar).
*   **`CreateChildExecution(bool isScope = false, string? scopeFlowNodeId = null)`**: Mevcut yürütmenin bir alt yürütmesini oluşturur (paralel akış veya alt süreç için).
*   **`Signal(string? signalName = null, object? data = null)`**: Yürütmenin bir sinyal aldığını veya devam etmeye hazır olduğunu bildirir. Olayları (Catch Events) veya bekleme durumlarını (örn. Receive Task) tetiklemek için kullanılır. `ExecutionSignaledEvent` domain olayını tetikler.
*   **`ArriveAtConvergingGateway(string gatewayId)`**: Yürütmenin bir birleştirici ağ geçidine ulaştığını ve beklemeye geçtiğini işaret eder.
*   **`Reactivate()`**: Birleştirici ağ geçidinde bekleyen bir yürütmeyi tekrar aktif hale getirir (genellikle tüm yollar geldiğinde).
*   **`LeaveConvergingGateway()`**: Birleştirici ağ geçidinden ayrıldığını işaret eder.
*   **`Fail(string errorCode, string errorMessage, string? errorDetails = null)`**: Yürütmeyi hata durumuna geçirir, ilgili hata bilgilerini ayarlar ve `IsActive`'i `false` yapar.
*   **`Escalate(string escalationCode, string? escalationName = null)`**: Bir yükseltme olayını tetikler, `LastEscalationCode`'u ayarlar ve `ExecutionEscalatedEvent` domain olayını tetikler.

## Kullanım

`Execution` nesneleri genellikle doğrudan uygulama kodu tarafından oluşturulmaz veya yönetilmez. Süreç motoru (`ExecutionFlowManager`), süreç akışını ilerletirken `Execution` nesnelerini oluşturur, günceller ve sonlandırır. `ProcessInstance` üzerindeki `Executions` koleksiyonu aracılığıyla veya `IRepository<Execution>` kullanılarak sorgulanabilirler. 