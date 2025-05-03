# GlobalScriptTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.GlobalTask`

**BPMN Elemanı:** `globalScriptTask`

## Açıklama

`GlobalScriptTask` (Global Betik Görevi), BPMN dosyasının kök seviyesinde tanımlanan ve belirli bir betiği (script) çalıştıran, yeniden kullanılabilir bir görev şablonudur. Bir `CallActivity` tarafından çağrıldığında, normal bir `ScriptTask` gibi davranır.

Amacı, farklı süreçlerde tekrarlanan standart betik mantığını (örn. veri dönüştürme, basit hesaplama) tek bir yerde tanımlamaktır.

`GlobalTask` sınıfından miras aldığı için yeniden kullanılabilir bir görevin temel özelliklerine ve `CallableElement`'in özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `globalScriptTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Script` : `Script`**
    *   Çalıştırılacak olan betik kodunu içeren nesne (normal `ScriptTask` ile aynı).
*   **`ScriptLanguage` : `string` (Öznitelik: `scriptLanguage`)**
    *   `Script` elemanında yer alan betiğin hangi dilde yazıldığını belirtir. Normal `ScriptTask`'taki `scriptFormat` özniteliği ile benzer amaca hizmet eder, ancak URI/MIME türü yerine genellikle doğrudan dilin adını (örn. `JavaScript`, `Groovy`) içerir.
    *   Süreç motorunun bu betiği çalıştırabilmesi için belirtilen dili destekleyen bir betik motoruna sahip olması gerekir.

## İlişkili Özellikler (GlobalTask'tan Miras Alınan)

*   **`IoSpecification`**: Çağrıldığında betiğe girdi olarak verilecek verileri ve betikten alınacak çıktıları tanımlar.

## Kullanım

`GlobalScriptTask` BPMN dosyasının `definitions` elemanı altında tanımlanır. Bir süreç içindeki `CallActivity`, `calledElement` özniteliği ile bu `GlobalScriptTask`'ın ID'sine referans vererek onu çağırır.

Süreç motoru, `CallActivity` tarafından çağrıldığında:
1.  `GlobalScriptTask` tanımını bulur.
2.  `CallActivity`'deki veri eşlemelerini kullanarak girdileri hazırlar.
3.  `ScriptLanguage` özelliğine göre uygun betik motorunu belirler.
4.  `Script` özelliğindeki betik kodunu çalıştırır.
5.  Sonuçları `CallActivity`'deki veri eşlemeleri aracılığıyla ana sürece aktarır.
6.  `CallActivity`'den devam eder. 