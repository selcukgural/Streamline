# GlobalManualTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.GlobalTask`

**BPMN Elemanı:** `globalManualTask`

## Açıklama

`GlobalManualTask` (Global Manuel Görev), BPMN dosyasının kök seviyesinde tanımlanan ve süreç motoru dışında gerçekleştirilen, yeniden kullanılabilir bir görev şablonudur. Bir `CallActivity` tarafından çağrıldığında, normal bir `ManualTask` gibi davranır (yani motor genellikle bunu bir geçiş noktası olarak kabul eder).

Amacı, farklı süreçlerde tekrarlanan standart manuel adımları (örn. "Belgeyi Arşivle") belgelemek ve tek bir yerde tanımlamaktır.

`GlobalTask` sınıfından miras aldığı için yeniden kullanılabilir bir görevin temel özelliklerine ve `CallableElement`'in özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `globalManualTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

Bu sınıf, temel sınıfı olan `GlobalTask`'tan tüm özellikleri miras alır. Kendi içinde ek özellik tanımlamaz.

## Kullanım

`GlobalManualTask` BPMN dosyasının `definitions` elemanı altında tanımlanır. Bir süreç içindeki `CallActivity`, `calledElement` özniteliği ile bu `GlobalManualTask`'ın ID'sine referans vererek onu çağırır.

Süreç motoru, `CallActivity` tarafından çağrıldığında:
1.  `GlobalManualTask` tanımını bulur.
2.  Normal bir `ManualTask` gibi, genellikle herhangi bir işlem yapmadan veya beklemeden hemen `CallActivity`'den devam eder.

Bu eleman, sürecin belirli bir noktasında harici bir manuel işin yapıldığını göstermek için kullanılır. 