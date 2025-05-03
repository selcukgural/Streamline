# CallActivity

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Activity`

**BPMN Elemanı:** `callActivity`

## Açıklama

`CallActivity` (Çağrı Aktivitesi), bir süreç akışı içinde tanımlanmış başka bir global (yeniden kullanılabilir) Süreci (Process) çağırmak için kullanılan bir aktivite türüdür. Bu, süreç mantığını modülerleştirmek ve farklı süreçlerde ortak adımları yeniden kullanmak için güçlü bir mekanizmadır.

Süreç motoru bir Çağrı Aktivitesine ulaştığında, `CalledElement` özelliği ile belirtilen süreci bulur, yeni bir süreç örneği başlatır (alt süreç), üst süreçten alt sürece veri aktarır ve alt süreç tamamlanana kadar ana süreçteki `Execution`'ı bekletir. Alt süreç tamamlandığında, sonuçlar üst sürece geri aktarılabilir ve ana süreç akışı devam eder.

`Activity` sınıfından miras aldığı için bir aktivitenin tüm temel özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `callActivity` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`CalledElement` : `System.Xml.XmlQualifiedName` (Öznitelik: `calledElement`)**
    *   Çağrılacak olan global `Process` elemanına bir referans (`QName`). Bu genellikle çağrılacak sürecin ID'sini içerir. Bu süreç aynı BPMN dosyasında veya farklı bir dosyada tanımlanmış olabilir (motorun konfigürasyonuna bağlı olarak).

## İlişkili Özellikler (Activity'den Miras Alınan)

*   **`IoSpecification` / `DataInputAssociation` / `DataOutputAssociation`**: Üst süreçteki değişkenlerden alınan verileri çağrılan alt sürece başlangıç değişkenleri olarak aktarmak (Input) ve alt süreç tamamlandığında sonuçları üst süreçteki değişkenlere geri yazmak (Output) için kullanılır.

## Farkı: SubProcess vs CallActivity

*   **SubProcess:** Kapsayıcı sürecin bir parçasıdır, kendi içinde tanımlanır ve sadece o süreç içinde kullanılır. Kapsayıcı sürecin değişkenlerine doğrudan erişebilir.
*   **CallActivity:** Ayrı, global bir süreci çağırır. Bu çağrılan süreç farklı BPMN dosyalarında tanımlanabilir ve birden fazla süreç tarafından yeniden kullanılabilir. Veri aktarımı açıkça Input/Output eşlemeleri ile yapılır.

## Kullanım

Süreç motoru, BPMN modelinde bir `callActivity` elemanıyla karşılaştığında bir `CallActivity` nesnesi oluşturur. Motor:
1.  `CalledElement` özelliğini kullanarak çağrılacak süreç tanımını bulur.
2.  `DataInputAssociation` kullanarak üst süreçten alt sürece aktarılacak başlangıç değişkenlerini hazırlar.
3.  Çağrılan süreç için yeni bir `ProcessInstance` başlatır.
4.  Ana süreçteki ilgili `Execution`'ı, çağrılan süreç tamamlanana kadar beklemeye alır.
5.  Çağrılan süreç tamamlandığında:
    a.  `DataOutputAssociation` kullanarak alt süreçten üst sürece veri aktarır.
    b.  Bekleyen `Execution`'ı devam ettirir. 