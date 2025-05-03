# ExecutionFlowManager

*   **Sınıf Amacı:** Streamline iş akışı motorunun kalbidir. BPMN 2.0.2 süreç tanımları üzerinde `Execution` (yürütme) token'larının ilerlemesini yönetir. Bir akış düğümünden diğerine geçişi, handler'ları çağırmayı, çatallanma (fork) ve birleşme (join) mantığını, hata (error) ve yükseltme (escalation) olaylarını ele almayı ve süreç örneklerinin genel yaşam döngüsünü koordine etmeyi sağlar.
*   **Konum:** `Streamline.Engine.Services`
*   **Arayüz:** `Streamline.Domain.Abstractions.IExecutionFlowManager` (Domain katmanındaki arayüzü implemente eder)
*   **Temel Davranışlar:**
    *   **`ContinueExecutionAsync(Guid executionId, ...)`:**
        *   Verilen `executionId`'ye sahip aktif bir `Execution`'ı yükler.
        *   Execution'ın ait olduğu `ProcessInstance`'ı ve ilgili BPMN `Definitions`'ı yükler.
        *   Execution'ın bulunduğu mevcut `FlowNode`'u (`CurrentFlowNodeId` ile) tanımlar.
        *   Eğer mevcut düğüm bir `Activity` ise, ona bağlı `BoundaryEvent` aboneliklerini kaydeder (`RegisterBoundaryEventSubscriptionsAsync`).
        *   `IFlowNodeHandlerFactory` kullanarak mevcut düğüm için uygun handler'ı alır.
        *   Bir `FlowNodeHandlerContext` oluşturur (Execution, Definitions, UnitOfWork, kendisi gibi bilgileri içerir).
        *   Handler'ın `ExecuteAsync` metodunu çağırır.
        *   Handler çalıştıktan sonra hata (`ErrorCode`) veya yükseltme (`LastEscalationCode`) olup olmadığını kontrol eder ve varsa ilgili `FindAndHandle...` metotlarını çağırır.
        *   Hata durumlarında veya tanım bulunamazsa Execution'ı `Fail` veya `Terminate` durumuna getirebilir.
    *   **`ForkAndContinueExecutionAsync(Execution originalExecution, ...)`:**
        *   Paralel veya kapsayıcı gateway'ler gibi durumlarda, mevcut bir `Execution`'dan yeni bir `Execution` oluşturarak akışı çatallandırır.
        *   Yeni Execution'ı belirtilen `SequenceFlow`'un hedef düğümüne konumlandırır.
        *   Yeni Execution için `ContinueExecutionAsync`'ı tetikler.
    *   **`FindAndHandleErrorAsync(...)`:**
        *   Bir `Execution` başarısız olduğunda (`ErrorCode` set edildiğinde), süreç tanımında bu hatayı yakalayacak uygun bir `Error Boundary Event` veya `Error Event Sub-Process` arar.
        *   Bulursa, hatayı yakalayan olaya yeni bir Execution başlatır veya mevcut Execution'ı oraya yönlendirir.
        *   Hata yakalanmazsa, genellikle süreç örneği başarısız olur.
    *   **`FindAndHandleEscalationAsync(...)`:**
        *   Bir `Execution` bir yükseltme tetiklediğinde (`LastEscalationCode` set edildiğinde), bu kodu yakalayacak uygun bir `Escalation Boundary Event` veya `Escalation Event Sub-Process` arar.
        *   Bulursa, ilgili handler'ı tetikler (bu genellikle ana akışı kesintiye uğratmaz).
    *   **Diğer Metotlar:** Sınır olaylarını kaydetme, süreç tanımını yükleme, belirli bir düğümde yeni bir Execution başlatma (`StartExecutionAtNodeAsync`), kapsam içindeki Execution'ları sonlandırma (`TerminateScopeContentsAsync`), zamanlayıcı olaylarını tetikleme (`TriggerBoundaryTimerEventAsync`) gibi yardımcı işlevleri içerir.
*   **Bağımlılıklar:**
    *   `IUnitOfWork`: Veritabanı işlemleri için (Execution, ProcessInstance, ActivityInstance gibi varlıkları okuma/yazma).
    *   `IBpmnXmlService`: Süreç tanımlarını (XML) yüklemek ve ayrıştırmak için.
    *   `ILogger`: Loglama için.
    *   `IFlowNodeHandlerFactory`: Düğümler için uygun handler'ları almak için.
    *   `ITimerJobScheduler`: Zamanlayıcı olaylarını programlamak ve yönetmek için (örneğin, Timer Boundary Events).
*   **Önemli Noktalar:**
    *   Bu sınıf, motorun durum makinesi gibi çalışır ve akışın ilerlemesinden sorumludur.
    *   İşlemler genellikle asenkrondur (`async Task`).
    *   Hata yönetimi ve işlemlerin atomikliği (Unit of Work kullanımı) kritiktir.
    *   BPMN standardının karmaşık yönlerini (paralel akışlar, olaylar, alt süreçler) ele alır. 