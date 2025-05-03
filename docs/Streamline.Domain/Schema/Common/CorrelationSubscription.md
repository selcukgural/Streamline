# CorrelationSubscription

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `Process`'in veya belirli bir aktivitenin (`ReceiveTask`, mesaj olayları) hangi `CorrelationKey`'e abone olduğunu ve bu anahtarı oluşturan özelliklerin (`CorrelationProperty`) değerlerini gelen mesajlardan nasıl çıkaracağını tanımlar. Mesajları belirli süreç örnekleriyle ilişkilendirme mekanizmasını yapılandırır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Genellikle bir `Process` elemanının içinde veya mesaj alabilen `FlowNode`'lar (`ReceiveTask`, Mesaj Başlangıç/Ara Yakalama Olayları) ile ilişkilendirilir.
    *   Bir `CorrelationKey`'e (`correlationKeyRef`) referans verir.
    *   Referans verilen `CorrelationKey`'deki her bir `CorrelationProperty` için, değerin mesajdan nasıl çıkarılacağını (`DataPath`) belirten bir `CorrelationPropertyBinding` içerir.
    *   Motor, bu aboneliği kullanarak, belirtilen `CorrelationKey`'e uyan ve `CorrelationPropertyBinding`'ler aracılığıyla değerleri çıkarılabilen mesajları bekler ve ilgili süreç örneğine yönlendirir.
*   **Özellikler:**
    *   `CorrelationKeyRef` (`XmlQualifiedName`, `XmlAttribute`, Zorunlu): Abone olunan `CorrelationKey`'in ID'sine referans.
    *   `CorrelationPropertyBinding` (Collection<`CorrelationPropertyBinding`>, `XmlElement`): `CorrelationKeyRef` ile belirtilen anahtardaki her bir özellik için, değerin gelen mesajdan nasıl çıkarılacağını tanımlayan `CorrelationPropertyBinding` elemanlarının koleksiyonu.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.CorrelationKey` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.CorrelationPropertyBinding`
    *   `Streamline.Domain.Schema.Common.Process` (Genellikle içinde bulunur)
    *   Mesaj alabilen FlowNode'lar (örn. `ReceiveTask`) (İlişkilendirilebilir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Mesaj tabanlı süreç başlatma ve devam ettirme için kritik bir yapıdır.
    *   Gelen mesajların doğru süreç örneğine yönlendirilmesini sağlar. 