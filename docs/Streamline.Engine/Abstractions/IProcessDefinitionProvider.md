# IProcessDefinitionProvider

*   **Arayüz Amacı:** İş akışı motorunun, yürütme sırasında ihtiyaç duyduğu BPMN süreç tanımlarını (`Definitions` nesnesi olarak) elde etmek için kullanacağı mekanizmanın sözleşmesini tanımlar. Süreç tanımlarının nereden (veritabanı, dosya sistemi, vb.) ve nasıl yükleneceğini soyutlar.
*   **Konum:** `Streamline.Engine.Abstractions`
*   **Uygulama & Davranış:**
    *   Bu arayüzü implemente eden sınıflar, belirli bir `definitionId` (süreç anahtarı, ID, versiyon bilgisi vb. olabilir) kullanarak ilgili BPMN XML'ini bulmalı, `IBpmnXmlService` kullanarak ayrıştırmalı ve sonuçta ortaya çıkan `Definitions` nesnesini döndürmelidir (`GetDefinitionByIdAsync`).
    *   Implementasyonlar, tanımların depolandığı yere göre değişir (örneğin, veritabanındaki bir tablodan okuma, belirli bir klasördeki `.bpmn` dosyalarını okuma).
    *   Tanım bulunamazsa `DefinitionNotFoundException`, ayrıştırma sırasında hata olursa `DefinitionParseException` fırlatmalıdır.
    *   Gelecekte yeni süreç tanımlarını sisteme yüklemek (deploy) veya versiyonları sorgulamak için ek metotlar eklenebilir.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Events.Definitions`: Döndürülecek olan ayrıştırılmış süreç tanımı nesnesi.
*   **Metotlar:**
    *   `Task<Definitions> GetDefinitionByIdAsync(string definitionId, CancellationToken cancellationToken)`: Verilen ID'ye sahip süreç tanımını getirir.
*   **Önemli Noktalar:**
    *   Bu arayüz, motorun tanım depolama mekanizmasından bağımsız olmasını sağlar.
    *   Performans için önbellekleme (caching) mekanizmaları implementasyonlarda düşünülebilir, çünkü süreç tanımları genellikle sık sık değişmez ama sıkça okunur.
    *   `ExecutionFlowManager` gibi servisler, bir süreci yürütmeye başlamadan önce tanımı almak için bu arayüzü kullanır. 