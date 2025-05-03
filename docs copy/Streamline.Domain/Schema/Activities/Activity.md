# Activity (Soyut Sınıf)

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Flow.FlowNode`

**BPMN Elemanı:** `tActivity`

## Açıklama

`Activity`, BPMN 2.0 spesifikasyonunda bir süreç içinde gerçekleştirilen işi temsil eden temel soyut sınıftır. Süreç akışının bir parçasıdır ve hem atomik (Task'lar) hem de atomik olmayan (Sub-Process'ler) iş birimlerini kapsar.

Bu sınıf, tüm aktivite türleri (örn. `Task`, `SubProcess`, `CallActivity`) için ortak özellikleri ve davranışları tanımlar. `FlowNode`'dan türediği için, gelen ve giden sıra akışlarına (`SequenceFlow`) ve mesaj akışlarına (`MessageFlow`) bağlanabilir.

Bu sınıf, BPMN XML şemasındaki `tActivity` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`IoSpecification` : `InputOutputSpecification`**
    *   Aktivitenin veri girişlerini (`DataInput`), veri çıkışlarını (`DataOutput`) ve bunlar arasındaki eşlemeleri (`InputSet`, `OutputSet`) tanımlayan bir yapı.
*   **`Property` : `Collection<Property>`**
    *   Aktiviteye özgü, genellikle çalışma zamanında kullanılan ek özelliklerin (key-value çiftleri) bir koleksiyonu.
*   **`DataInputAssociation` : `Collection<DataInputAssociation>`**
    *   Süreçteki Veri Nesnelerinin (örn. `DataObjectReference`) aktivitenin `DataInput`'larına nasıl bağlandığını tanımlayan ilişkilerin koleksiyonu.
*   **`DataOutputAssociation` : `Collection<DataOutputAssociation>`**
    *   Aktivitenin `DataOutput`'larının süreçteki Veri Nesnelerine nasıl bağlandığını tanımlayan ilişkilerin koleksiyonu.
*   **`ResourceRole` : `Collection<ResourceRole>`**
    *   Aktiviteyi gerçekleştirmekle ilişkili kaynakları veya rolleri tanımlar. Alt türleri arasında `Performer`, `HumanPerformer`, `PotentialOwner` bulunur ve özellikle Kullanıcı Görevleri (User Tasks) için önemlidir.
*   **`LoopCharacteristics` : `LoopCharacteristics`**
    *   Aktivitenin döngüsel davranışını tanımlar. İki türü vardır:
        *   `StandardLoopCharacteristics`: Belirli bir koşul sağlandığı sürece aktivitenin tekrar etmesini sağlar.
        *   `MultiInstanceLoopCharacteristics`: Aktivitenin birden çok örneğinin paralel veya sıralı olarak yürütülmesini sağlar.
*   **`IsForCompensation` : `bool` (Öznitelik: `isForCompensation`, Varsayılan: `false`)**
    *   Bu aktivitenin bir telafi (compensation) aktivitesi olup olmadığını belirtir. Telafi aktiviteleri, bir işlem başarısız olduğunda veya geri alındığında önceden tamamlanmış aktivitelerin etkilerini geri almak için kullanılır.
*   **`StartQuantity` : `string` (Öznitelik: `startQuantity`, Varsayılan: `"1"`)**
    *   Özellikle Çoklu Örnek (Multi-Instance) aktiviteler için, aktivitenin *başlaması* için gereken örnek sayısını belirtir. Varsayılan olarak 1'dir.
*   **`CompletionQuantity` : `string` (Öznitelik: `completionQuantity`, Varsayılan: `"1"`)**
    *   Özellikle Çoklu Örnek aktiviteler için, aktivitenin *tamamlanmış* sayılması için gereken örnek sayısını belirtir. Varsayılan olarak 1'dir.
*   **`Default` : `string?` (Öznitelik: `default`)**
    *   Aktiviteden çıkan koşullu Sıra Akışları (Sequence Flows) arasında, hiçbir koşul sağlanmadığında izlenecek varsayılan akışın ID'sini belirtir. Genellikle ayrılan (diverging) ağ geçitlerinde kullanılır, ancak aktivitelerde de tanımlanabilir.

## Türetilmiş Sınıflar

BPMN'deki tüm somut iş elemanları bu sınıftan türetilmiştir:
*   `Task` (ve onun alt türleri: `UserTask`, `ServiceTask`, `ScriptTask` vb.)
*   `SubProcess` (ve onun alt türleri: `AdHocSubProcess`, `Transaction`)
*   `CallActivity`

## Kullanım

Bu soyut sınıf doğrudan kullanılmaz. Süreç motoru, BPMN modelini ayrıştırırken `Task`, `SubProcess` gibi somut aktivite türlerine ait nesneler oluşturur. Motor, bu nesnelerin özelliklerini (örn. `LoopCharacteristics`, `IsForCompensation`) kullanarak aktivitenin yürütme davranışını belirler. 