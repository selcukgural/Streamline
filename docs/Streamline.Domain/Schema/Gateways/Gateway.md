# Gateway

*   **BPMN Tipi:** Soyut (Abstract)
*   **Amaç:** Süreç akışının nasıl ayrıldığını (diverging) veya birleştiğini (converging) kontrol eden noktaları temsil eden tüm Geçit türleri için temel sınıftır.
*   **Konum:** `Streamline.Domain.Schema.Gateways`
*   **Miras:** `FlowNode` -> `FlowElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, belirli davranışlara sahip somut Geçit türleri (`ExclusiveGateway`, `InclusiveGateway`, `ParallelGateway`, `EventBasedGateway`, `ComplexGateway`) tarafından miras alınır.
    *   Geçidin akışı ayırmak mı (birden fazla giden akış), birleştirmek mi (birden fazla gelen akış) yoksa her ikisini birden mi yaptığını belirten bir `GatewayDirection` özelliği içerir.
*   **Özellikler:**
    *   `GatewayDirection` (`GatewayDirection` enum, `XmlAttribute`, Varsayılan: `Unspecified`): Geçidin amacını belirtir: `Converging` (birleştirme), `Diverging` (ayırma), `Mixed` (hem birleştirme hem ayırma), `Unspecified` (belirtilmemiş veya modelleme aracına bağlı).
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Gateways.GatewayDirection`: Geçit yönünü belirten enum.
*   **Önemli Noktalar:**
    *   Tüm geçitler bu sınıftan türer ve kendi özel davranışlarını eklerler.
    *   `GatewayDirection` genellikle modelleme aracı tarafından otomatik olarak ayarlanır, ancak motorun geçidin mantığını anlamasına yardımcı olabilir.
    *   Soyut olduğu için doğrudan örneği oluşturulamaz. 