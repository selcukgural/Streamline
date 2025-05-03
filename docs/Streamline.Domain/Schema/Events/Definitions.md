# Definitions

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir BPMN 2.0 XML dosyasının kök (root) elemanıdır. Bir veya daha fazla süreç (`Process`), işbirliği (`Collaboration`), koreografi (`Choreography`), mesaj (`Message`), hata (`Error`), veri tipi (`ItemDefinition`) gibi tüm global tanımları ve bu tanımlara ait diyagram bilgilerini (`BPMNDiagram`) içerir.
*   **Konum:** `Streamline.Domain.Schema.Events` (Spesifikasyonda kök eleman, bu projede Events altına konmuş olabilir)
*   **Miras:** Yok (Doğrudan `System.Object`)
*   **Uygulama & Davranış:**
    *   Bir BPMN dosyasındaki tüm tanımların ana kapsayıcısıdır.
    *   İsim alanları (namespace), dışa aktaran araç bilgisi (exporter), ifade ve tip dilleri gibi meta-verileri içerir.
    *   Global olarak tanımlanan ve farklı süreçler tarafından paylaşılabilen elemanları (Mesajlar, Hatalar, Sinyaller, Veri Tipleri vb.) barındırır.
    *   Süreçlerin ve diğer elemanların görsel yerleşim bilgilerini içeren `BPMNDiagram` elemanlarını içerir.
*   **Özellikler:**
    *   `Id` (string, `XmlAttribute`): Definitions elemanının kendi ID'si.
    *   `Name` (string, `XmlAttribute`): Definitions koleksiyonuna verilen isim.
    *   `TargetNamespace` (string, `XmlAttribute`, **Zorunlu**): Bu dosyadaki tanımların ait olduğu hedef isim alanı (namespace).
    *   `ExpressionLanguage` (string, `XmlAttribute`, Varsayılan: XPath): Dosya içinde kullanılan ifadelerin varsayılan dili.
    *   `TypeLanguage` (string, `XmlAttribute`, Varsayılan: XML Schema): Veri tiplerinin tanımlandığı varsayılan dil.
    *   `Exporter` (string, `XmlAttribute`): BPMN dosyasını oluşturan aracın adı.
    *   `ExporterVersion` (string, `XmlAttribute`): BPMN dosyasını oluşturan aracın sürümü.
    *   `Import` (Collection<`Import`>, `XmlElement`): Dış tanımları içeri aktarmak için kullanılan `Import` elemanları koleksiyonu.
    *   `Extension` (Collection<`Extension`>, `XmlElement`): Standardın eski sürümlerinden kalma veya özel uzantılar için.
    *   `RootElement` (Collection<`RootElement`>, `XmlElement`): `Process`, `Collaboration`, `Message`, `Error`, `Signal`, `ItemDefinition` gibi tüm üst seviye tanımları içeren koleksiyon (XML'de bu elemanların kendi etiketleriyle bulunur).
    *   `BpmnDiagram` (Collection<`BpmnDiagram`>, `XmlElement`): Süreçlerin ve elemanların görsel diyagram bilgilerini içeren `BPMNDiagram` elemanları koleksiyonu.
    *   `Relationship` (Collection<`Relationship`>, `XmlElement`): Farklı tanımlar arasındaki ilişkileri belirtmek için (nadiren kullanılır).
    *   `AnyAttribute` (Collection<`XmlAttribute`>, `XmlAnyAttribute`): Şemada tanımlanmamış ek özellikleri yakalamak için.
*   **Metotlar (Ekstra - Bu sınıfa eklenmiş olabilir):**
    *   `FindSequenceFlows(List<string> ids)`: Verilen ID'lere sahip `SequenceFlow`'ları bulur.
    *   `FindFlowElementById<TFlowElement>(string id)`: Belirli bir ID'ye sahip `FlowElement`'i bulur.
    *   `FindIncomingSequenceFlows(string targetFlowNodeId)`: Belirli bir düğüme gelen `SequenceFlow`'ları bulur.
*   **Bağımlılıklar:**
    *   Neredeyse tüm diğer BPMN Şema sınıfları (`Import`, `Extension`, `RootElement` alt tipleri, `BpmnDiagram`, `Relationship` vb.).
*   **Önemli Noktalar:**
    *   Her BPMN 2.0 XML dosyasının temelini oluşturur.
    *   `IBpmnXmlService` tarafından XML'den deserialize edilen ana nesnedir. 