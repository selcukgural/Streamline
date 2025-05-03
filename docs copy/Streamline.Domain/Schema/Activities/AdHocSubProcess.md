# AdHocSubProcess

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.SubProcess`

**BPMN Elemanı:** `adHocSubProcess`

## Açıklama

`AdHocSubProcess` (Duruma Özel Alt Süreç), içindeki Aktivitelerin önceden tanımlanmış bir Sıra Akışı (Sequence Flow) olmadan, isteğe bağlı olarak ve belirli bir sırayla yürütülebildiği özel bir `SubProcess` türüdür. Bu tür alt süreçler, yapılandırılmamış veya esnek iş akışlarını modellemek için kullanılır.

Bir `AdHocSubProcess` içindeki aktiviteler genellikle insanlar tarafından tetiklenir veya belirli koşullara göre aktif hale gelir. Alt sürecin tamamlanması, genellikle içindeki tüm aktivitelerin tamamlanmasına değil, özel olarak tanımlanmış bir `CompletionCondition`'a (Tamamlama Koşulu) bağlıdır.

`SubProcess` sınıfından miras aldığı için bir alt sürecin tüm özelliklerine (iç akış elemanları, vb.) sahiptir.

Bu sınıf, BPMN XML şemasındaki `adHocSubProcess` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`CompletionCondition` : `Expression`**
    *   Alt sürecin ne zaman tamamlanmış kabul edileceğini tanımlayan bir koşul ifadesi. Bu ifade genellikle süreç değişkenlerine veya iç aktivitelerin durumlarına bakar. Bu koşul `true` olarak değerlendirildiğinde alt süreç sona erer.
*   **`CancelRemainingInstances` : `bool` (Öznitelik: `cancelRemainingInstances`, Varsayılan: `true`)**
    *   Eğer `CompletionCondition` sağlandığında alt süreç sona ererken, hala aktif durumda olan iç aktiviteler varsa, bu aktivitelerin otomatik olarak iptal edilip edilmeyeceğini belirtir. `true` ise iptal edilir, `false` ise aktif kalmalarına izin verilir (bu genellikle istenmeyen bir durumdur).
*   **`Ordering` : `AdHocOrdering` (Öznitelik: `ordering`)**
    *   İçindeki aktivitelerin yürütülme sırasını belirler:
        *   `Parallel`: Aktiviteler herhangi bir sırada ve potansiyel olarak aynı anda (paralel) yürütülebilir.
        *   `Sequential`: Aktiviteler herhangi bir sırada yürütülebilir, ancak herhangi bir anda yalnızca bir aktivite aktif olabilir.

## Kullanım

Süreç motoru, BPMN modelinde bir `adHocSubProcess` elemanıyla karşılaştığında bir `AdHocSubProcess` nesnesi oluşturur. Motor:
1.  Alt süreci başlatır ve ilgili kapsamı (`Execution`) oluşturur.
2.  İçindeki aktiviteleri hemen başlatmaz. Aktivitelerin başlatılması genellikle harici tetikleyicilere (örn. kullanıcı arayüzü, gelen olaylar) veya `Ordering` özelliğine bağlı koşullara göre gerçekleşir.
3.  Motor, periyodik olarak veya her aktivite tamamlandığında `CompletionCondition` ifadesini değerlendirir.
4.  `CompletionCondition` `true` olduğunda:
    a.  Eğer `CancelRemainingInstances` `true` ise, hala aktif olan iç aktiviteleri iptal eder.
    b.  Alt süreç kapsamını sonlandırır.
    c.  Ana `Execution`'ı devam ettirir. 