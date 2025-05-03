# CorrelationPropertyBinding

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `CorrelationSubscription` (mesaj aboneliği) kapsamında, belirli bir `CorrelationProperty`'nin değerinin gelen mesajdan nasıl çıkarılacağını tanımlar. Korelasyon için gerekli olan verinin mesaj içindeki yerini belirtir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `CorrelationSubscription` elemanının içinde bulunur.
    *   Bir `CorrelationProperty`'ye referans verir ve bu özelliğin değerini mesajdan almak için bir `FormalExpression` (genellikle XPath) içerir.
    *   Motor, bir mesaj aldığında ve bir `CorrelationSubscription`'ı değerlendirdiğinde, bu binding'leri kullanarak mesajdan korelasyon anahtarını oluşturan değerleri çıkarır.
*   **Özellikler:**
    *   `CorrelationPropertyRef` (`XmlQualifiedName`, `XmlAttribute`, Zorunlu): Değeri mesajdan çıkarılacak olan `CorrelationProperty`'nin ID'sine referans.
    *   `DataPath` (`FormalExpression`, `XmlElement`, Zorunlu): `CorrelationPropertyRef` ile belirtilen özelliğin değerinin, abone olunan mesajın (`Message`) içeriğinden nasıl (hangi ifadeyle, örn. XPath `/order/id`) çıkarılacağını tanımlayan ifade.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.CorrelationProperty` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.FormalExpression`
    *   `Streamline.Domain.Schema.Common.CorrelationSubscription` (Bu elemanın içinde yer alır)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Mesaj korelasyon mekanizmasının temel bir parçasıdır.
    *   Soyut korelasyon özelliklerini (`CorrelationProperty`) somut mesaj verileriyle (`Message` içeriği) ilişkilendirir. 