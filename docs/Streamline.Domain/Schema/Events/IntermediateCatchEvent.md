# IntermediateCatchEvent

**Namespace:** `Streamline.Domain.Schema.Events`

**Temel Sınıf:** `Streamline.Domain.Schema.Events.CatchEvent`

**BPMN Elemanı:** `intermediateCatchEvent`

## Açıklama

`IntermediateCatchEvent` (Ara Yakalamalı Olay), bir süreç akışının *ortasında* belirli bir tetikleyicinin (trigger) gerçekleşmesini bekleyen bir `CatchEvent` türüdür. Süreç akışı bu olaya ulaştığında durur ve ilişkili olay (mesaj, zamanlayıcı, sinyal vb.) gerçekleşene kadar beklemede kalır.

Bu olaylar, harici sistemlerle senkronizasyon sağlamak, belirli bir süre beklemek veya belirli koşulların oluşmasını beklemek için kullanılır.

`CatchEvent` sınıfından miras aldığı için, bir tetikleyici bekleyebilir (`EventDefinition` ile tanımlanır) ve olay gerçekleştiğinde veri üretebilir (`DataOutput`, `DataOutputAssociation`).

Bu sınıf, BPMN XML şemasındaki `intermediateCatchEvent` elemanından otomatik olarak türetilmiştir.

## Özellikler

Bu sınıf, temel sınıfı olan `CatchEvent`'ten tüm özellikleri miras alır. Kendi içinde ek özellik tanımlamaz, ancak davranışını içerdiği `EventDefinition` belirler.

## Yaygın Ara Yakalamalı Olay Türleri (EventDefinition'a göre)

*   **Message Intermediate Catch Event:** Belirli bir mesaj (`MessageEventDefinition`) gelmesini bekler.
*   **Timer Intermediate Catch Event:** Belirli bir süre geçmesini veya belirli bir zamana ulaşılmasını bekler (`TimerEventDefinition`).
*   **Signal Intermediate Catch Event:** Belirli bir sinyalin (`SignalEventDefinition`) yayınlanmasını bekler.
*   **Conditional Intermediate Catch Event:** Belirli bir koşulun (`ConditionalEventDefinition`) `true` olmasını bekler.
*   **Link Intermediate Catch Event:** Sürecin başka bir yerindeki bir `Link Intermediate Throw Event`'ten gelen "sinyali" yakalar (`LinkEventDefinition`). Süreç diyagramını daha okunabilir kılmak için "goto" benzeri bir mekanizma sağlar.

## Kullanım

Süreç motoru, bir `Execution` bir `intermediateCatchEvent`'e ulaştığında:
1.  İlişkili `EventDefinition`(lar)ı kontrol eder.
2.  Bu tanımlara göre bir `EventSubscription` oluşturur (örn. mesaj adı, zamanlayıcı tanımı).
3.  `Execution`'ı bu olayda beklemeye alır.
4.  İlgili olay (mesaj, zamanlayıcı tetiği, sinyal) gerçekleştiğinde:
    a.  Motor ilgili `EventSubscription`'ı bulur ve tüketir.
    b.  Potansiyel olarak olayla gelen veriyi (`DataOutputAssociation` ile) süreç değişkenlerine yazar.
    c.  Bekleyen `Execution`'ı devam ettirir ve olaydan çıkan Sıra Akışını takip eder. 