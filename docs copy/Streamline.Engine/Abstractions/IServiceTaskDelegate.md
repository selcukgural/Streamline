# IServiceTaskDelegate

*   **Arayüz Amacı:** BPMN `Service Task` elemanının `implementation` özelliğinde belirtilen ve dış sistemlerle veya özel kod mantığıyla etkileşim kuracak olan sınıflar için bir sözleşme tanımlar. `ServiceTaskHandler` tarafından çağrılacak olan kodun uyması gereken standardı belirler.
*   **Konum:** `Streamline.Engine.Abstractions`
*   **Uygulama & Davranış:**
    *   Bu arayüzü implemente eden sınıflar, genellikle belirli bir işlevi yerine getiren servislerdir (örneğin, e-posta gönderme, API çağırma, veritabanı güncelleme).
    *   `ExecuteAsync(FlowNodeHandlerContext context, CancellationToken cancellationToken)` metodu, `ServiceTaskHandler` tarafından çağrıldığında asıl iş mantığını yürütür.
    *   `context` parametresi aracılığıyla mevcut yürütme (`Execution`), süreç değişkenleri (varsa), ve diğer motor servislerine (`IUnitOfWork` gibi) erişim sağlar.
    *   İşlemler genellikle asenkrondur.
*   **Bağımlılıklar:**
    *   `Streamline.Engine.Abstractions.FlowNodeHandlerContext`: Yürütme bağlamını ve motor servislerine erişimi sağlar.
*   **Önemli Noktalar:**
    *   `Service Task`'ın `implementation` özelliğinde genellikle bu arayüzü implemente eden sınıfın tam adı veya DI container tarafından bilinen bir anahtar (key) bulunur.
    *   Bu arayüz, iş akışı motorunun çekirdek mantığı ile harici veya özel servis mantığını birbirinden ayırır, böylece motorun farklı servislerle entegrasyonu kolaylaşır.
    *   Implementasyonlar DI container'a kaydedilmelidir ki `ServiceTaskHandler` bunları çözümleyebilsin. 