# MultiInstanceLoopCharacteristics

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.LoopCharacteristics`

**BPMN Elemanı:** `multiInstanceLoopCharacteristics`

## Açıklama

`MultiInstanceLoopCharacteristics` (Çoklu Örnek Döngü Karakteristikleri), bir Aktivitenin (Task veya SubProcess) birden çok örneğinin paralel veya sıralı olarak yürütülmesini tanımlamak için kullanılır. Bu, genellikle bir koleksiyondaki her öğe için aynı aktivitenin tekrarlanması gereken durumlarda kullanılır.

Bir aktiviteye `multiInstanceLoopCharacteristics` eklendiğinde, süreç motoru bu aktivite için birden fazla örnek oluşturur ve bunların yürütülmesini yönetir.

`LoopCharacteristics` soyut sınıfından türemiştir.

Bu sınıf, BPMN XML şemasındaki `multiInstanceLoopCharacteristics` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`LoopCardinality` : `Expression?`**
    *   Oluşturulacak toplam örnek sayısını dinamik olarak belirleyen bir ifade. Örneğin, `"${myCollection.size()}"` gibi bir ifade, `myCollection` adlı süreç değişkeninin boyutunu kullanabilir.
*   **`LoopDataInputRef` : `System.Xml.XmlQualifiedName?`**
    *   Örneklerin oluşturulması için kullanılacak veri koleksiyonunu içeren bir `ItemAwareElement`'e (genellikle bir `DataObjectReference` veya süreç değişkeni) referans. Motor, bu koleksiyondaki her öğe için bir aktivite örneği oluşturur.
*   **`LoopDataOutputRef` : `System.Xml.XmlQualifiedName?`**
    *   Tamamlanan her aktivite örneğinden gelen çıktıların toplanacağı bir `ItemAwareElement`'e referans. Tüm örnekler tamamlandığında bu koleksiyon kullanılabilir.
*   **`InputDataItem` : `DataInput?`**
    *   `LoopDataInputRef` koleksiyonundaki *tek bir öğeyi* temsil eden `DataInput` tanımı. Her aktivite örneği başlatıldığında, bu `DataInput` o örneğe özgü koleksiyon öğesiyle doldurulur ve aktivitenin içindeki `DataInputAssociation`'lar aracılığıyla kullanılabilir.
*   **`OutputDataItem` : `DataOutput?`**
    *   Tek bir aktivite örneğinin ürettiği çıktıyı temsil eden `DataOutput` tanımı. Her örnek tamamlandığında, bu `DataOutput`'un değeri `LoopDataOutputRef` ile belirtilen koleksiyona eklenir.
*   **`ComplexBehaviorDefinition` : `Collection<ComplexBehaviorDefinition>`**
    *   `Behavior` özniteliği `Complex` olarak ayarlandığında, örneklerin etkinleştirilmesi veya atlanması için karmaşık koşullar tanımlamak amacıyla kullanılır (nadiren kullanılır).
*   **`CompletionCondition` : `Expression?`**
    *   Çoklu örnek döngüsünün ne zaman tamamlanmış kabul edileceğini tanımlayan bir ifade. Bu ifade genellikle:
        *   Tamamlanan örnek sayısını (`nrOfCompletedInstances`), 
        *   Toplam örnek sayısını (`nrOfInstances`), 
        *   Aktif örnek sayısını (`nrOfActiveInstances`) 
        *   ve süreç değişkenlerini kullanabilir.
    *   Eğer `LoopCardinality` belirtilmişse ve `CompletionCondition` belirtilmemişse, genellikle tüm örnekler tamamlandığında döngü biter.
*   **`IsSequential` : `bool` (Öznitelik: `isSequential`, Varsayılan: `false`)**
    *   Aktivite örneklerinin nasıl yürütüleceğini belirtir:
        *   `false` (Varsayılan): Örnekler paralel olarak, aynı anda başlatılabilir.
        *   `true`: Örnekler sıralı olarak, bir önceki tamamlandıktan sonra bir sonraki başlatılır.
*   **`Behavior` : `MultiInstanceFlowCondition` (Öznitelik: `behavior`, Varsayılan: `All`)**
    *   Döngünün genel akış davranışını belirler:
        *   `All`: Döngü, ancak `CompletionCondition` sağlanırsa (veya tüm örnekler tamamlanırsa) sona erer.
        *   `One`: Döngü, ilk aktivite örneği tamamlandığında veya `oneBehaviorEventRef` ile belirtilen olay tetiklendiğinde sona erer. Diğer aktif örnekler genellikle iptal edilir.
        *   `None`: Döngü, `noneBehaviorEventRef` ile belirtilen olay tetiklendiğinde başlar ve `oneBehaviorEventRef` ile belirtilen olay tetiklendiğinde (veya bir örnek tamamlandığında, davranışa bağlı olarak) sona erer.
        *   `Complex`: Davranış `ComplexBehaviorDefinition` ile belirlenir.
*   **`OneBehaviorEventRef` : `System.Xml.XmlQualifiedName?` (Öznitelik: `oneBehaviorEventRef`)**
    *   `Behavior` `One` veya `None` olduğunda, döngüyü sonlandıracak olan `EventDefinition`'a bir referans.
*   **`NoneBehaviorEventRef` : `System.Xml.XmlQualifiedName?` (Öznitelik: `noneBehaviorEventRef`)**
    *   `Behavior` `None` olduğunda, örneklerin oluşturulmasını tetikleyecek olan `EventDefinition`'a bir referans.

## Kullanım

Bir aktiviteye (Task veya SubProcess) `multiInstanceLoopCharacteristics` eklendiğinde, süreç motoru:
1.  `LoopCardinality` veya `LoopDataInputRef` kullanarak kaç tane örnek oluşturulacağını belirler.
2.  `IsSequential` değerine göre örnekleri paralel veya sıralı olarak başlatır.
3.  Her örnek için `InputDataItem`'ı ilgili veriyle doldurur.
4.  Örneklerin tamamlanmasını ve `CompletionCondition`'ı izler.
5.  Döngü tamamlandığında, `OutputDataItem`'ları `LoopDataOutputRef` koleksiyonunda toplar (varsa) ve ana aktiviteden çıkan akışı takip eder.

## MultiInstanceFlowCondition Enum

```csharp
public enum MultiInstanceFlowCondition
{
    None,
    One,
 