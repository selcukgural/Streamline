# ScriptTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Task`

**BPMN Elemanı:** `scriptTask`

## Açıklama

`ScriptTask` (Betik Görevi), süreç akışı içinde otomatik olarak bir betik (script) çalıştırmak için kullanılan bir görev türüdür. Süreç değişkenlerini okumak, değiştirmek, basit hesaplamalar yapmak veya harici sistemlerle basit etkileşimler kurmak gibi görevler için kullanılır.

Süreç motoru bir Betik Görevine ulaştığında, belirtilen betiği (`Script` özelliği) belirtilen formatta (`ScriptFormat` özelliği) yorumlar ve çalıştırır.

`Task` sınıfından miras aldığı için bir aktivitenin ve görevin tüm temel özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `scriptTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Script` : `Script`**
    *   Çalıştırılacak olan betik kodunu içeren nesne. `Script` sınıfı genellikle betik metnini tutan bir özelliğe sahiptir.
*   **`ScriptFormat` : `string` (Öznitelik: `scriptFormat`)**
    *   `Script` elemanında yer alan betiğin hangi dilde yazıldığını veya formatını belirten bir MIME türü ifadesi. Örnekler: `"text/javascript"`, `"application/groovy"`, `"text/python"`, `"application/jython"`.
    *   Süreç motorunun bu betiği çalıştırabilmesi için belirtilen script formatını destekleyen bir betik motoruna (scripting engine) sahip olması gerekir.

## İlişkili Özellikler (Activity'den Miras Alınan)

*   **`IoSpecification` / `DataInputAssociation` / `DataOutputAssociation`**: Betiğin ihtiyaç duyduğu girdileri süreç değişkenlerinden almak ve betik çalıştıktan sonra sonuçları süreç değişkenlerine yazmak için kullanılabilir (ancak genellikle betikler değişkenlere doğrudan erişir).

## Kullanım

Süreç motoru, BPMN modelinde bir `scriptTask` elemanıyla karşılaştığında bir `ScriptTask` nesnesi oluşturur. Motor:
1.  `ScriptFormat` özelliğine bakarak uygun betik motorunu belirler.
2.  `Script` özelliğindeki betik kodunu alır.
3.  Betiği, süreç değişkenlerine erişim sağlayarak çalıştırır.
4.  Betik başarıyla tamamlandıktan sonra süreç akışında devam eder.

Eğer betik çalışırken bir hata oluşursa, bu genellikle bir `Incident` oluşturulmasına veya hatanın süreç içinde yakalanıp işlenmesine (Error Boundary Event vb.) neden olur. 