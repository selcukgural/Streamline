# IFlowNodeHandlerFactory

*   **Arayüz Amacı:** Belirli bir `FlowNode` (akış düğümü) örneği veya türü için uygun `IFlowNodeHandler` (veya `IFlowNodeEventHandler`) implementasyonunu sağlamaktan sorumlu fabrika (factory) deseninin sözleşmesini tanımlar. İş akışı motorunun, farklı BPMN elemanları için doğru işleyiciyi dinamik olarak seçmesini sağlar.
*   **Konum:** `Streamline.Engine.Services` (veya Abstractions, konumu tartışmalı olabilir)
*   **Uygulama & Davranış:**
    *   Bu arayüzü implemente eden sınıf (örneğin, `FlowNodeHandlerFactory`), genellikle dependency injection container'ı kullanarak mevcut tüm `IFlowNodeHandler`/`IFlowNodeEventHandler` implementasyonlarına erişir.
    *   `GetHandler<TNode>(TNode node)` metodu çağrıldığında, verilen `node`'un türüne (`TNode`) karşılık gelen kaydedilmiş handler örneğini döndürür.
    *   Eğer belirli bir `TNode` tipi için bir handler kayıtlı değilse, `null` döndürebilir (veya bir exception fırlatabilir, implementasyona bağlı).
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.FlowNode`: Handler'ı alınacak akış düğümünün temel tipi.
    *   `Streamline.Engine.Abstractions.IFlowNodeHandler<TNode>`: Döndürülecek handler'ın arayüz tipi.
*   **Metotlar:**
    *   `IFlowNodeHandler<TNode>? GetHandler<TNode>(TNode node) where TNode : FlowNode`: Verilen `node` için uygun handler'ı döndürür.
*   **Önemli Noktalar:**
    *   Fabrika deseni, `ExecutionFlowManager` gibi servislerin belirli handler implementasyonlarına doğrudan bağımlı olmasını engeller, sistemi daha esnek ve genişletilebilir hale getirir.
    *   Handler'ların kaydedilmesi ve bulunması genellikle başlangıçta (startup) DI container konfigürasyonu veya yansıtma (reflection) tabanlı tarama ile yapılır. 