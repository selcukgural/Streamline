# EndPoint

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Bir `Interface` tarafından tanımlanan bir servise veya hizmete erişmek için kullanılan fiziksel veya mantıksal bir uç noktayı (genellikle bir adres veya bağlantı noktası) temsil eder. Özellikle Web Servisleri ile etkileşimde kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   Bir `Participant` (özellikle bir servis sağlayıcıyı temsil ediyorsa) veya bir `Interface` ile ilişkilendirilebilir.
    *   Bir `Operation`'ın (`Interface` içinde tanımlanan) belirli bir `EndPoint` üzerinden çağrılacağını belirtmek için kullanılabilir.
    *   Genellikle WSDL (Web Services Description Language) gibi teknolojilerle birlikte düşünülür; WSDL'deki bir port tanımına karşılık gelebilir.
*   **Özellikler:**
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
    *   Kendine özgü standart özellikleri yoktur, ancak `ExtensionElements` aracılığıyla adres, protokol gibi bilgiler eklenebilir.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Interface` (İlişkilendirilebilir)
    *   `Streamline.Domain.Schema.Common.Operation` (İlişkilendirilebilir)
    *   `Streamline.Domain.Schema.Common.Participant` (İlişkilendirilebilir)
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Servis odaklı mimarilerde (SOA) veya sistem entegrasyonlarında harici servislerin nasıl çağrılacağını modellemek için önemlidir.
    *   Soyut `Interface` tanımını somut bir erişim noktasıyla bağlar. 