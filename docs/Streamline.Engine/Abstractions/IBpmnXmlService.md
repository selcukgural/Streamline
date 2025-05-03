# IBpmnXmlService

*   **Arayüz Amacı:** BPMN 2.0.2 standardındaki XML dosyalarını Streamline uygulamasının anlayabileceği `Definitions` nesne modeline dönüştürmek (import) ve tersi işlemi (export) yapmak için gerekli sözleşmeyi tanımlar. İş akışı tanımlarının okunması ve yazılması için temel bir bileşendir.
*   **Konum:** `Streamline.Engine.Abstractions`
*   **Uygulama & Davranış:**
    *   Bu arayüzü implemente eden sınıflar (örneğin, `Streamline.Infrastructure` içindeki `BpmnXmlService`), verilen bir BPMN XML metnini ayrıştırarak (deserialize) bir `Definitions` nesnesi oluşturmalıdır (`Import` metodu).
    *   Ayrıca, mevcut bir `Definitions` nesnesini alıp bunu geçerli bir BPMN 2.0.2 XML metnine dönüştürmelidir (`Export` metodu).
    *   Hata durumlarında (geçersiz XML, boş içerik vb.) uygun exception'lar fırlatmalıdır.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Events.Definitions`: İş akışı tanımının kök nesnesi.
*   **Metotlar:**
    *   `Definitions Import(string xmlContent)`: XML içeriğini `Definitions` nesnesine dönüştürür.
    *   `string Export(Definitions definitions)`: `Definitions` nesnesini XML içeriğine dönüştürür.
*   **Önemli Noktalar:**
    *   Bu arayüz, iş akışı motorunun belirli bir XML ayrıştırma kütüphanesine doğrudan bağımlı olmasını engeller. Farklı implementasyonlar kullanılabilir.
    *   XML işlemleri potansiyel olarak zaman alıcı veya hataya açık olabileceğinden, implementasyonların verimli ve dayanıklı olması önemlidir. 