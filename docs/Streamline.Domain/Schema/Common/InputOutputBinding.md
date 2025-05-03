# InputOutputBinding

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `CallableElement`'in (örn. `Process`, `GlobalTask`) desteklediği bir `Interface` üzerindeki belirli bir `Operation`'ın, `CallableElement`'in kendi giriş/çıkış spesifikasyonu (`IoSpecification`) ile nasıl eşleştiğini tanımlar. Bu, `CallActivity` gibi çağıran elemanların, çağrılan elemanın hangi veri girişlerini operasyonun giriş mesajına, hangi veri çıkışlarını operasyonun çıkış mesajına bağlayacağını belirlemesini sağlar.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `CallableElement`'in `IoBinding` koleksiyonu içinde bulunur.
    *   `CallableElement` tarafından desteklenen bir `Interface`'deki (`supportedInterfaceRef` ile belirtilen) bir `Operation`'a (`operationRef`) referans verir.
    *   Bu operasyonun giriş mesajı için hangi `DataInput`'un (`inputDataRef`) kullanılacağını belirtir.
    *   Bu operasyonun çıkış mesajı için hangi `DataOutput`'un (`outputDataRef`) kullanılacağını belirtir.
    *   `CallActivity` bu bilgiyi kullanarak, kendi `DataInputAssociation` ve `DataOutputAssociation`'larını çağrılan `CallableElement`'in `DataInput` ve `DataOutput`'ları ile doğru şekilde eşleştirir.
*   **Özellikler:**
    *   `OperationRef` (`XmlQualifiedName`, `XmlAttribute`, Zorunlu): Bağlamanın ilişkili olduğu `Operation`'ın ID'sine referans. Bu operasyon, `CallableElement`'in desteklediği bir `Interface` içinde tanımlı olmalıdır.
    *   `InputDataRef` (string, `XmlAttribute`, Zorunlu): `OperationRef` ile belirtilen operasyonun giriş mesajına karşılık gelen, `CallableElement`'in `IoSpecification`'ında tanımlı `DataInput` elemanının ID'si.
    *   `OutputDataRef` (string, `XmlAttribute`, Zorunlu): `OperationRef` ile belirtilen operasyonun çıkış mesajına karşılık gelen, `CallableElement`'in `IoSpecification`'ında tanımlı `DataOutput` elemanının ID'si.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.CallableElement` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.Operation` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Data.DataInput` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Data.DataOutput` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   `CallActivity` ile `CallableElement` arasındaki veri akışını tanımlamanın önemli bir parçasıdır, özellikle `CallableElement` birden fazla operasyon sunan bir `Interface` uyguluyorsa.
    *   Soyut operasyonları somut veri giriş/çıkışlarıyla eşleştirir. 