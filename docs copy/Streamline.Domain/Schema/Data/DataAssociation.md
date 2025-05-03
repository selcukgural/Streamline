# DataAssociation (DataInputAssociation & DataOutputAssociation)

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Aktiviteler ile veri nesneleri (`DataObjectReference`), veri depoları (`DataStoreReference`) veya diğer kaynaklar arasındaki veri akışını ve eşleştirmesini tanımlar.
    *   `DataInputAssociation`: Verinin bir Aktiviteye *nasıl girdiğini* tanımlar. Kaynak genellikle bir `DataObjectReference` veya `DataStoreReference`, hedef ise Aktivitenin bir `DataInput`'udur.
    *   `DataOutputAssociation`: Verinin bir Aktiviteden *nasıl çıktığını* tanımlar. Kaynak genellikle Aktivitenin bir `DataOutput`'u, hedef ise bir `DataObjectReference` veya `DataStoreReference`'tır.
*   **Konum:** `Streamline.Domain.Schema.Data`
*   **Miras:** `BaseElement` (Her ikisi de `DataAssociation`'dan türese de, BPMN şemasında `DataAssociation` kendisi de somut bir tip olarak tanımlanır, ancak genellikle alt tipleri kullanılır).
*   **Uygulama & Davranış:**
    *   Bir veya daha fazla kaynak (`SourceRef`) ile bir hedef (`TargetRef`) arasında bağlantı kurar.
    *   İsteğe bağlı olarak veri dönüşümü (`Transformation`) veya atama (`Assignment`) kuralları içerebilir.
    *   Diyagram üzerinde genellikle noktalı ok çizgileri ile gösterilir.
*   **Özellikler (`DataAssociation` temelinde):**
    *   `SourceRef` (Collection<string>, `XmlElement`): Verinin geldiği eleman(lar)ın ID'leri.
    *   `TargetRef` (string, `XmlElement`, **Zorunlu**): Verinin gittiği elemanın ID'si.
    *   `Transformation` (`FormalExpression`, `XmlElement`, İsteğe Bağlı): Kaynaktan hedefe veri aktarılırken uygulanacak dönüşüm kuralını (örn. XPath, XQuery) tanımlayan ifade.
    *   `Assignment` (Collection<`Assignment`>, `XmlElement`, İsteğe Bağlı): Kaynak ve hedef veri yapıları arasında belirli alanların nasıl eşleştirileceğini tanımlayan atama kuralları.
*   **Özellikler (Alt Tipler):**
    *   `DataInputAssociation` ve `DataOutputAssociation` genellikle `DataAssociation`'ın özelliklerini kullanır ve hangi yönde veri akışı olduğunu belirtmek için kullanılırlar.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.FormalExpression`: Dönüşüm ifadeleri için.
    *   `Streamline.Domain.Schema.Common.Assignment`: Atama kuralları için.
*   **Önemli Noktalar:**
    *   Süreç içindeki veri akışının detaylarını modellemek için kritik öneme sahiptir.
    *   Veri eşleştirme ve dönüşüm mantığını tanımlayarak motorun veriyi doğru şekilde işlemesini sağlar.
    *   `Association`'dan farklı olarak, veri akışını temsil eder ve genellikle `Activity`'lerin `ioSpecification`'ı ile ilişkilidir. 