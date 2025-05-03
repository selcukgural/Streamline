# ManualTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Task`

**BPMN Elemanı:** `manualTask`

## Açıklama

`ManualTask` (Manuel Görev), süreç motorunun dışında, bir insan tarafından herhangi bir yazılım veya sistem yardımı olmadan gerçekleştirilen bir görevi temsil eder. Örneğin, bir dokümanı fiziksel olarak postalamak veya bir ekipmanı manuel olarak ayarlamak gibi.

Süreç motoru genellikle Manuel Görevler üzerinde herhangi bir işlem yapmaz veya beklemez. Bu görevler, sürecin belirli bir aşamasında harici bir işin yapıldığını belgelemek amacıyla modelde yer alır. Motor, Manuel Göreve ulaştığında genellikle doğrudan bir sonraki adıma geçer.

`Task` sınıfından miras aldığı için bir aktivitenin ve görevin tüm temel özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `manualTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

Bu sınıf, temel sınıfı olan `Task`'tan tüm özellikleri miras alır. Kendi içinde ek özellik tanımlamaz.

## Kullanım

Süreç motoru, BPMN modelinde bir `manualTask` elemanıyla karşılaştığında bir `ManualTask` nesnesi oluşturur. Motor, bu görevi genellikle bir "geçiş" (pass-through) aktivitesi olarak ele alır:
1.  Görevin başladığına dair bir olay (audit log) kaydedebilir.
2.  Herhangi bir bekleme yapmadan veya harici bir sistemi tetiklemeden hemen görevin tamamlandığını varsayar.
3.  Süreç akışında bir sonraki elemana devam eder.

Manuel görevin gerçekte ne zaman ve nasıl yapıldığı süreç motorunun kontrolü dışındadır ve modelleme/belgeleme amacıyla kullanılır. 