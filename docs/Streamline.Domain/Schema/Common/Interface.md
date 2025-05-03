# Interface

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Bir dizi ilgili operasyonu (`Operation`) gruplayarak bir hizmet veya işlevsellik kontratını tanımlar. Genellikle dış sistemlerle veya servislerle (özellikle Web Servisleri) olan etkileşimleri modellemek için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   Bir veya daha fazla `Operation` içerir. Her operasyon genellikle bir giriş mesajı, bir çıkış mesajı ve potansiyel hata mesajları tanımlar.
    *   `ServiceTask`, `SendTask`, `ReceiveTask` gibi aktiviteler veya `MessageEventDefinition`, belirli bir `Interface`'deki bir `Operation`'ı referans alarak hangi dış servisin çağrılacağını veya bekleneceğini belirtir.
    *   `CallableElement`'ler (`Process`, `GlobalTask`), `supportedInterfaceRef` özelliği ile bu `Interface`'i desteklediklerini (yani bu operasyonları sunduklarını) belirtebilir.
    *   `implementationRef` özniteliği, bu soyut arayüz tanımının somut bir teknolojiyle (örn. WSDL portType, Java sınıfı) nasıl gerçekleştirildiğine dair bir ipucu verebilir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`, Zorunlu): Arayüzün adı.
    *   `Operation` (Collection<`Operation`>, `XmlElement`, Zorunlu): Bu arayüz tarafından tanımlanan operasyonların koleksiyonu. En az bir operasyon içermelidir.
    *   `ImplementationRef` (`XmlQualifiedName`, `XmlAttribute`): Arayüzün somut implementasyonuna bir referans (isteğe bağlı).
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Operation`
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Servis odaklı mimariler (SOA) veya sistem entegrasyonu senaryolarında süreçlerin dış dünyayla nasıl etkileşime girdiğini modellemek için temel bir yapıdır.
    *   WSDL (Web Services Description Language) kavramlarına benzer bir yapı sunar (`Interface` ~ `portType`, `Operation` ~ `operation`). 