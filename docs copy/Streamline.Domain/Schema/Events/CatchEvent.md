# CatchEvent (Soyut Sınıf)

**Namespace:** `Streamline.Domain.Schema.Events`

**Temel Sınıf:** `Streamline.Domain.Schema.Events.Event`

**BPMN Elemanı:** `tCatchEvent`

## Açıklama

`CatchEvent` (Yakalamalı Olay), BPMN'de bir sürecin ilerleyişini etkileyen bir tetikleyicinin (trigger) gerçekleşmesini bekleyen olaylar için soyut bir temel sınıftır. Bu olaylar, belirli bir koşul sağlandığında, bir mesaj alındığında, bir zamanlayıcı dolduğunda vb. tetiklenirler.

Süreç motoru bir Yakalamalı Olaya ulaştığında, genellikle ilgili `Execution`'ı beklemeye alır ve ilişkili `EventDefinition` tarafından belirtilen tetikleyicinin gerçekleşmesini bekler.

`Event` sınıfından miras alır.

Bu sınıf, BPMN XML şemasındaki `tCatchEvent` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`DataOutput` : `Collection<DataOutput>`**
    *   Olay tetiklendiğinde ortaya çıkan veriyi temsil eden `DataOutput` tanımlarının koleksiyonu. Örneğin, alınan bir mesajın içeriği bir `DataOutput` olarak tanımlanabilir.
*   **`DataOutputAssociation` : `Collection<DataOutputAssociation>`**
    *   `DataOutput`'larda yakalanan verinin süreçteki Veri Nesnelerine veya değişkenlere nasıl yazılacağını tanımlayan `DataOutputAssociation` ilişkilerinin koleksiyonu.
*   **`OutputSet` : `OutputSet?`**
    *   Birden fazla `DataOutput` olduğunda, bunların nasıl bir set oluşturduğunu tanımlar.
*   **`EventDefinition` : `Collection<EventDefinition>`**
    *   Bu Yakalamalı Olayın hangi tetikleyici(ler) tarafından aktif hale getirileceğini tanımlayan `EventDefinition` alt elemanlarının koleksiyonu. Bir `CatchEvent` birden fazla `EventDefinition` içerebilir (örn. hem mesaj hem de sinyal bekleyen bir olay), ancak genellikle tek bir tane içerir. Olası türler: `MessageEventDefinition`, `TimerEventDefinition`, `SignalEventDefinition`, `ConditionalEventDefinition`, `LinkEventDefinition` (IntermediateCatchEvent için), `ErrorEventDefinition` (BoundaryEvent, EventSubProcess Start için), `EscalationEventDefinition` (BoundaryEvent, EventSubProcess Start için), `CompensateEventDefinition` (BoundaryEvent, EventSubProcess Start için), `CancelEventDefinition` (BoundaryEvent için).
*   **`EventDefinitionRef` : `Collection<System.Xml.XmlQualifiedName>`**
    *   BPMN dosyasının kök seviyesinde tanımlanmış olan global `EventDefinition`'lara yapılan referansların (`QName`) koleksiyonu. Bu, olay tanımlarını yeniden kullanmayı sağlar.
*   **`ParallelMultiple` : `bool` (Öznitelik: `parallelMultiple`, Varsayılan: `false`)**
    *   Eğer olay birden fazla `EventDefinition` içeriyorsa (veya `eventDefinitionRef` ile birden fazla tanıma işaret ediyorsa) bu öznitelik davranışını belirler:
        *   `false` (Varsayılan): Tanımlanan olay tanımlarından *herhangi birinin* gerçekleşmesi, bu Yakalamalı Olayı tetikler ve süreç devam eder (OR mantığı).
        *   `true`: Tanımlanan olay tanımlarının *hepsinin* gerçekleşmesi gerekir (AND mantığı). Bu durum özellikle Ara Yakalama Olayları (Intermediate Catch Events) için geçerlidir ve nadiren kullanılır.

## Türetilmiş Sınıflar

BPMN'deki belirli Yakalamalı Olay türleri bu sınıftan türetilmiştir:
*   `StartEvent`
*   `IntermediateCatchEvent`
*   `BoundaryEvent`

## Kullanım

Bu soyut sınıf doğrudan kullanılmaz. Süreç motoru, `startEvent`, `intermediateCatchEvent`, `boundaryEvent` gibi etiketlerle karşılaştığında ilgili somut sınıfların örneklerini oluşturur. Motor, bu olaylara ulaştığında, ilişkili `EventDefinition`(lar)a göre bir `EventSubscription` oluşturur ve `Execution`'ı bekletir. İlgili olay (mesaj, zamanlayıcı vb.) gerçekleştiğinde, motor aboneliği bulur, potansiyel veriyi (`DataOutputAssociation` ile) değişkenlere yazar ve bekleyen `Execution`'ı devam ettirir. 