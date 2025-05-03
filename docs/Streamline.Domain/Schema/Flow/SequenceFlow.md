# SequenceFlow

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir süreç veya alt süreç içindeki iki `FlowNode` (Olay, Aktivite, Gateway) arasındaki sıralı akış yönünü gösterir. Süreç mantığının hangi adımlarla ilerleyeceğini tanımlar.
*   **Konum:** `Streamline.Domain.Schema.Flow`
*   **Miras:** `FlowElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir kaynak (`SourceRef`) ve bir hedef (`TargetRef`) düğüme bağlanır.
    *   İsteğe bağlı olarak bir koşul (`ConditionExpression`) içerebilir. Bu koşul, akışın bu yol üzerinden devam edip etmeyeceğini belirler (genellikle Gateway'lerden sonra kullanılır).
*   **Özellikler:**
    *   `SourceRef` (string, `XmlAttribute`, **Zorunlu**): Akışın başladığı `FlowNode` elemanının ID'si.
    *   `TargetRef` (string, `XmlAttribute`, **Zorunlu**): Akışın ulaştığı `FlowNode` elemanının ID'si.
    *   `ConditionExpression` (`Expression`, `XmlElement`): Akışın gerçekleşmesi için değerlendirilmesi gereken koşul. Genellikle `FormalExpression` tipindedir. Koşulsuz akışlarda bu eleman bulunmaz.
    *   `IsImmediate` (bool, `XmlAttribute`, İsteğe Bağlı): Özellikle döngülerde veya karmaşık birleşmelerde kullanılan, akışın hemen gerçekleşip gerçekleşmeyeceğini belirten bir bayrak (genellikle XML'de bulunmaz veya motor tarafından özel yorumlanır).
    *   `Name` (string, `XmlAttribute`, Miras): Akışa bir isim vermek için kullanılır (genellikle koşullu akışlarda kullanılır).
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Expression`: Koşul ifadesini tutmak için.
*   **Önemli Noktalar:**
    *   Süreç akış mantığının temelini oluşturur.
    *   `ExclusiveGateway` ve `InclusiveGateway`'den çıkan `SequenceFlow`'lar genellikle bir `ConditionExpression` içerir.
    *   Bir `Task` veya `Event`'ten genellikle tek bir koşulsuz `SequenceFlow` çıkar (bazı özel durumlar hariç). 