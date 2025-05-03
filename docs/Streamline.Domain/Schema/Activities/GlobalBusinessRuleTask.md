# GlobalBusinessRuleTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.GlobalTask`

**BPMN Elemanı:** `globalBusinessRuleTask`

## Açıklama

`GlobalBusinessRuleTask` (Global İş Kuralı Görevi), BPMN dosyasının kök seviyesinde tanımlanan ve belirli iş kurallarını yürüten, yeniden kullanılabilir bir görev şablonudur. Bir `CallActivity` tarafından çağrıldığında, normal bir `BusinessRuleTask` gibi davranır.

Amacı, farklı süreçlerde tekrarlanan standart iş kuralı veya karar mantığı yürütme adımlarını tek bir yerde tanımlamaktır.

`GlobalTask` sınıfından miras aldığı için yeniden kullanılabilir bir görevin temel özelliklerine ve `CallableElement`'in özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `globalBusinessRuleTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Implementation` : `string` (Öznitelik: `implementation`, Varsayılan: `"##unspecified"`)**
    *   İş kuralı görevinin nasıl gerçekleştirileceğini belirtir. Normal `BusinessRuleTask` ile aynı anlama gelir ve genellikle çağrılacak kural setini, DMN tablosunu veya kullanılacak kural motoru entegrasyonunu tanımlar.

## İlişkili Özellikler (GlobalTask'tan Miras Alınan)

*   **`IoSpecification`**: Çağrıldığında iş kuralı motoruna hangi verilerin girdi olarak verileceğini ve motorun çıktılarının nasıl alınacağını tanımlar.

## Kullanım

`GlobalBusinessRuleTask` BPMN dosyasının `definitions` elemanı altında tanımlanır. Bir süreç içindeki `CallActivity`, `calledElement` özniteliği ile bu `GlobalBusinessRuleTask`'ın ID'sine referans vererek onu çağırır.

Süreç motoru, `CallActivity` tarafından çağrıldığında:
1.  `GlobalBusinessRuleTask` tanımını bulur.
2.  `CallActivity`'deki veri eşlemelerini kullanarak girdileri hazırlar.
3.  `Implementation` özelliğine göre ilgili iş kuralı motorunu veya karar hizmetini çağırır.
4.  Sonuçları `CallActivity`'deki veri eşlemeleri aracılığıyla ana sürece aktarır.
5.  `CallActivity`'den devam eder. 