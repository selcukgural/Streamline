# FlowNodeHandlerFactory

*   **Sınıf Amacı:** `IFlowNodeHandlerFactory` arayüzünü implemente eder. Dependency Injection (DI) container'ını kullanarak, belirli bir `FlowNode` (akış düğümü) türü için önceden kaydedilmiş olan `IFlowNodeHandler` implementasyonunu bulup döndürmekten sorumludur.
*   **Konum:** `Streamline.Engine.Services`
*   **Uygulama & Davranış:**
    *   Constructor aracılığıyla `IServiceProvider` (DI container erişimi için) ve `ILogger` alır.
    *   `GetHandler<TNode>(TNode node)` metodu:
        *   Verilen `node`'un `null` olup olmadığını kontrol eder.
        *   `node`'un çalışma zamanı tipini (`node.GetType()`) kullanarak ilgili handler arayüz tipini oluşturur (örneğin, `node` bir `StartEvent` ise `IFlowNodeHandler<StartEvent>` tipini oluşturur).
        *   DI container (`serviceProvider`) üzerinden `GetRequiredService` metodunu kullanarak bu handler arayüz tipine karşılık gelen servisi (yani handler implementasyonunu) çözümler.
        *   Çözümlenen servisin beklenen `IFlowNodeHandler<TNode>` tipine atanabilir olup olmadığını kontrol eder.
        *   Başarılı olursa handler'ı döndürür, aksi takdirde veya DI çözümlemesi sırasında bir hata oluşursa uygun bir exception fırlatır (örneğin, `InvalidOperationException`, `NotSupportedException`).
        *   İşlemler sırasında loglama yapar (`ILogger`).
*   **Bağımlılıklar:**
    *   `Microsoft.Extensions.DependencyInjection.IServiceProvider`: DI container'dan servisleri çözümlemek için.
    *   `Microsoft.Extensions.Logging.ILogger`: Loglama için.
    *   `Streamline.Engine.Abstractions.IFlowNodeHandler<TNode>`: Döndürülen handler'ların tipi.
    *   `Streamline.Domain.Schema.Common.FlowNode`: Handler'ı alınacak düğümün tipi.
*   **Önemli Noktalar:**
    *   Bu sınıf, motorun handler bulma mantığını merkezileştirir.
    *   Handler'ların DI container'a doğru şekilde kaydedilmiş olması (genellikle `Startup.cs` veya benzeri bir yerde) bu fabrikanın çalışması için kritiktir.
    *   `GetRequiredService` kullandığı için, eğer bir `FlowNode` tipi için handler kaydedilmemişse, uygulama başlatılırken veya handler ilk kez istenirken bir hata fırlatılacaktır. 