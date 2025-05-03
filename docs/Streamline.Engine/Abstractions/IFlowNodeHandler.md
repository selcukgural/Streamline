# IFlowNodeHandler

*   **Arayüz Amacı:** Bu arayüz, `IFlowNodeEventHandler`'dan biraz farklı bir soyutlama seviyesi sunar. Genellikle, bir akış düğümünün tüm yaşam döngüsünü veya ana işlevini tek bir metotla yönetmek için kullanılır. `IFlowNodeEventHandler` daha çok düğüme *giriş* olayına odaklanırken, `IFlowNodeHandler` düğümün genel işleyişini kapsayabilir.
*   **Konum:** `Streamline.Engine.Abstractions`
*   **Yapı:**
    *   **Generic Arayüz (`IFlowNodeHandler<TNode>`):**
        *   `TNode` tipi, hangi `FlowNode` türü için geçerli olduğunu belirtir.
        *   Genellikle spesifik metotlar içermez, temel işlevsellik non-generic arayüzdeki `ExecuteAsync` ile sağlanır. Ancak gerekirse türe özgü metotlar eklenebilir.
    *   **Non-Generic Arayüz (`IFlowNodeHandler`):**
        *   `ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)`: İlgili akış düğümünün ana mantığını yürüten asenkron metottur. Parametreler `IFlowNodeEventHandler`'daki `HandleEnterAsync` ile benzerdir, ancak burada `node` parametresi temel `FlowNode` tipindedir (implementasyon içinde cast edilebilir).
        *   Bağımlılık ekleme ve fabrika (`FlowNodeHandlerFactory`) kullanımı için temel tiptir.
*   **Uygulama & Davranış:**
    *   Bu arayüz, `IFlowNodeEventHandler` ile birlikte veya onun yerine kullanılabilir. Seçim, düğüm mantığının karmaşıklığına ve nasıl organize edilmek istendiğine bağlıdır.
    *   Bazı durumlarda, bir düğümün tüm işlevi tek bir `ExecuteAsync` metodunda daha temiz ifade edilebilir.
    *   Fabrika (`FlowNodeHandlerFactory`), bir `FlowNode` aldığında, uygun `IFlowNodeHandler`'ı bulup `ExecuteAsync` metodunu çağırabilir.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.FlowNode`: Tüm BPMN akış düğümlerinin temel sınıfı.
    *   `FlowNodeHandlerContext`: Yürütme bağlamını içeren kayıt.
*   **Önemli Noktalar:**
    *   `IFlowNodeHandler` ve `IFlowNodeEventHandler` arasındaki ayrım biraz belirsiz olabilir ve projenin tasarım tercihlerine göre değişebilir. Bazen aynı handler sınıfı her iki arayüzü de implemente edebilir.
    *   Genellikle, `IFlowNodeEventHandler` daha olay odaklı (giriş/çıkış) iken, `IFlowNodeHandler` daha çok düğümün bütünsel işlevine odaklanır.
    *   Mevcut kodda `IFlowNodeHandler`'ın `ExecuteAsync` metodu birincil olarak kullanılırken, `IFlowNodeEventHandler` daha çok tip güvenliği ve olay bazlı yaklaşım için arayüz tanımı sağlıyor gibi görünmektedir. (Bu nokta kodun kullanımına göre netleştirilmeli). 