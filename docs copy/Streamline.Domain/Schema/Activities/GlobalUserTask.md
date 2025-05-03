# GlobalUserTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.GlobalTask`

**BPMN Elemanı:** `globalUserTask`

## Açıklama

`GlobalUserTask` (Global Kullanıcı Görevi), BPMN dosyasının kök seviyesinde tanımlanan ve bir insan katılımcının gerçekleştirmesi gereken, yeniden kullanılabilir bir görev şablonudur. Bir `CallActivity` tarafından çağrıldığında, normal bir `UserTask` gibi davranır.

Amacı, farklı süreçlerde tekrarlanan standart kullanıcı görevlerini (örn. "Onay İste", "Bilgi Gir") tek bir yerde tanımlamaktır.

`GlobalTask` sınıfından miras aldığı için yeniden kullanılabilir bir görevin temel özelliklerine (`ResourceRole` vb.) ve `CallableElement`'in özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `globalUserTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Rendering` : `Collection<Rendering>`**
    *   Bu görevin kullanıcı arayüzünde nasıl sunulacağına dair ipuçları veya referanslar içeren bir koleksiyon (normal `UserTask` ile aynı).
*   **`Implementation` : `string` (Öznitelik: `implementation`, Varsayılan: `"##unspecified"`)**
    *   Görev uygulamasının doğasını belirtir. Global Kullanıcı Görevleri için bu genellikle `"##unspecified"` veya `"##HumanTask"` gibi standart bir değer alır (normal `UserTask` ile aynı).

## İlişkili Özellikler (GlobalTask'tan Miras Alınan)

*   **`ResourceRole`**: Görevin varsayılan atamasını veya adaylarını tanımlamak için kullanılır. `CallActivity` bu tanımları geçersiz kılabilir veya genişletebilir.

## Kullanım

`GlobalUserTask` BPMN dosyasının `definitions` elemanı altında tanımlanır. Bir süreç içindeki `CallActivity`, `calledElement` özniteliği ile bu `GlobalUserTask`'ın ID'sine referans vererek onu çağırır.

Süreç motoru, `CallActivity` tarafından çağrıldığında:
1.  `GlobalUserTask` tanımını bulur.
2.  Normal bir `UserTask` için yaptığı gibi bir görev listesi (Tasklist) girdisi (örn. `UserTaskAssignment`) oluşturur.
3.  `GlobalUserTask`'ta tanımlanan `ResourceRole`'ları ve `CallActivity`'deki potansiyel ek atama bilgilerini dikkate alır.
4.  Görev tamamlandığında `CallActivity`'den devam eder. 