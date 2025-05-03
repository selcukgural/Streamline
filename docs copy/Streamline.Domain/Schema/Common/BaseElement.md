# BaseElement

*   **BPMN Tipi:** Soyut (Abstract)
*   **Amaç:** BPMN 2.0 standardındaki neredeyse tüm elemanların miras aldığı temel soyut sınıftır. Her BPMN elemanının sahip olması gereken ortak özellikleri (ID, dokümantasyon, uzantı elemanları) tanımlar.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** Yok (En temel sınıf)
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, BPMN XML şemasındaki hemen hemen tüm elemanlar (örneğin `FlowElement`, `RootElement`, `Artifact`, `EventDefinition`, `Participant`, `MessageFlow` vb.) tarafından doğrudan veya dolaylı olarak miras alınır.
    *   Doğrudan bir örneği oluşturulamaz, ancak diğer tüm BPMN elemanları bu sınıftan türetildiği için her eleman bu özelliklere sahiptir.
*   **Özellikler:**
    *   `Id` (string, `XmlAttribute`): Her BPMN elemanını benzersiz şekilde tanımlayan zorunlu kimlik. Diyagram içinde referans vermek için kullanılır.
    *   `Documentation` (Collection<`Documentation`>, `XmlElement`): Eleman hakkında metinsel açıklamalar içeren bir koleksiyon. Modelleyiciler veya araçlar tarafından elemanın amacını veya detaylarını açıklamak için kullanılır.
    *   `ExtensionElements` (`ExtensionElements`, `XmlElement`): Standart BPMN kapsamı dışındaki özel veri veya meta verileri eklemek için kullanılan bir mekanizma. Üçüncü parti araçlar veya özel uygulamalar tarafından kullanılabilir.
    *   `AnyAttribute` (Collection<`XmlAttribute`>, `XmlAnyAttribute`): Şemada açıkça tanımlanmamış olan ek XML özniteliklerini kabul etmek için kullanılır. Genellikle uzantı mekanizmasıyla birlikte değerlendirilir.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Documentation`: Dokümantasyon öğeleri için.
    *   `Streamline.Domain.Schema.Common.ExtensionElements`: Uzantı öğeleri için.
*   **Önemli Noktalar:**
    *   BPMN eleman hiyerarşisinin köküdür.
    *   Tüm BPMN elemanlarının ortak temelini oluşturur.
    *   XML serileştirme/deserileştirme (`System.Xml.Serialization`) için gerekli attribuutları (`XmlType`, `XmlInclude`, `XmlElement`, `XmlAttribute` vb.) içerir. `XmlInclude` attribuutları, `BaseElement` tipinde bir değişkenin aslında hangi somut alt tipi (örneğin `Task`, `StartEvent`) içerdiğini serileştiricinin bilmesini sağlar.
    *   `AnyAttribute` (Collection<`XmlAttribute`>, `XmlAnyAttribute`): Şemada açıkça tanımlanmamış herhangi bir XML özelliğini (attribute) yakalamak için kullanılır. Bu, standardın gelecekteki sürümleri veya özel uzantılarla uyumluluğu artırır.
*   **Önemli Noktalar:**
    *   Bu sınıf, BPMN nesne modelinin temelini oluşturur ve ortak özellikleri tek bir yerde toplar.
    *   Soyut olduğu için doğrudan örneği oluşturulamaz.

## XML Örneği

`BaseElement` soyut olduğundan doğrudan XML'de `<baseElement>` olarak bulunmaz. Ancak, ondan türeyen *herhangi bir* somut BPMN elemanı bu temel özellikleri içerebilir:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:custom="http://mycompany.com/bpmn-extensions"
             targetNamespace="http://example.com/process">

  <process id="Process_1" name="My Process">
    <documentation>This is a description of the process.</documentation>
    <documentation>Another documentation entry.</documentation>
    <extensionElements>
      <custom:priority>High</custom:priority>
      <custom:reviewCycle>Weekly</custom:reviewCycle>
    </extensionElements>
    <!-- Sürecin diğer elemanları -->
  </process>

  <message id="Msg_1" name="Order Message">
    <documentation>Contains order details.</documentation>
    <!-- itemRef vb. -->
  </message>

  <userTask id="Task_1" name="Review Task" custom:assigneeGroup="reviewers">
     <!-- Diğer UserTask elemanları -->
     <!-- custom:assigneeGroup bir 'AnyAttribute' örneğidir -->
  </userTask>

</definitions>
```

Yukarıdaki örnekte:
*   `id`, `name` gibi temel öznitelikler (Id, Name gibi BaseElement özelliklerine karşılık gelir, Name FlowElement'ten gelir).
*   `documentation` elemanları (`Documentation` koleksiyonu).
*   `extensionElements` içindeki özel elemanlar (`ExtensionElements` özelliği).
*   `userTask` üzerindeki `custom:assigneeGroup` özniteliği (`AnyAttribute` koleksiyonu tarafından yakalanır).
hepsi `BaseElement` tarafından tanımlanan yapının somut elemanlardaki yansımalarıdır. 