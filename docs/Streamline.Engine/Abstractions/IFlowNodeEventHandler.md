# IFlowNodeEventHandler

*   **Arayüz Amacı:** Belirli bir BPMN akış düğümü (`FlowNode`) türüne (örneğin, `StartEvent`, `ServiceTask`, `ExclusiveGateway`) yürütme akışı *girdiğinde* (`enter`) çalıştırılacak mantığı tanımlamak için kullanılır. Bu, her bir BPMN elemanı türü için özel davranışlar eklemeyi sağlar.
*   **Konum:** `Streamline.Engine.Abstractions`
*   **Yapı:**
    *   **Generic Arayüz (`IFlowNodeEventHandler<TNode>`):**
        *   `TNode` tipi, hangi `FlowNode` türü için geçerli olduğunu belirtir (örneğin, `IFlowNodeEventHandler<StartEvent>`).
        *   `HandleEnterAsync(TNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)`: Yürütme akışı bu tipteki bir düğüme girdiğinde çağrılan asenkron metottur. Düğümün kendisi (`node`), yürütme bağlamı (`context`) ve iptal token'ı (`cancellationToken`) parametre olarak alınır.
    *   **Non-Generic Arayüz (`IFlowNodeEventHandler`):**
        *   Bu arayüz, somut `TNode` tipini bilmeden bağımlılık ekleme (dependency injection) ve tip keşfi (örneğin Scrutor kütüphanesi ile) için kullanılır.
        *   Fabrika desenlerinde (`FlowNodeHandlerFactory` gibi) handler'ları bulmak ve yönetmek için işaretleyici (marker) görevi görür.
*   **Uygulama & Davranış:**
    *   Her bir BPMN akış düğümü türü için (veya ortak bir davranış sergileyen bir grup için) bu arayüzü implemente eden bir sınıf (`handler`) oluşturulur.
    *   `ExecutionFlowManager`, bir düğüme ilerlediğinde, o düğümün tipine uygun `IFlowNodeEventHandler` implementasyonunu bulur ve `HandleEnterAsync` metodunu çağırır.
    *   `HandleEnterAsync` içinde, ilgili düğüme özgü işlemler gerçekleştirilir (örneğin, bir `ServiceTask` için harici bir servisi çağırmak, bir `UserTask` için görev oluşturmak, bir `Gateway` için koşulları değerlendirmek).
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.FlowNode`: Tüm BPMN akış düğümlerinin temel sınıfı.
    *   `FlowNodeHandlerContext`: Handler metoduna geçirilen, yürütme (`Execution`), tanımlar (`Definitions`), veritabanı işlemleri (`IUnitOfWork`), akış yöneticisi (`IExecutionFlowManager`) gibi önemli bilgileri ve servisleri içeren kayıt (record).
*   **Önemli Noktalar:**
    *   Bu arayüz, motorun çekirdek akış mantığını (düğümler arasında gezinme) ve düğümlere özgü iş mantığını birbirinden ayırır.
    *   Gelecekte bir düğümden *çıkarken* (`leave`) de mantık işletilmesi gerekirse, `HandleLeaveAsync` gibi bir metot eklenebilir.
    *   Handler'lar genellikle `Scoped` veya `Transient` ömürlü olarak kaydedilir. 