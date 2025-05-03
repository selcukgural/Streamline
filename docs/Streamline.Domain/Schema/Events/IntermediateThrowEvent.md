# IntermediateThrowEvent

**Namespace:** `Streamline.Domain.Schema.Events`

**Temel Sınıf:** `Streamline.Domain.Schema.Events.ThrowEvent`

**BPMN Elemanı:** `intermediateThrowEvent`

## Açıklama

`IntermediateThrowEvent` (Ara Fırlatmalı Olay), bir süreç akışının *ortasında* belirli bir sonucu üreten veya bir tetikleyiciyi (trigger) fırlatan bir `ThrowEvent` türüdür. Süreç akışı bu olaya ulaştığında, ilişkili eylemi (mesaj gönderme, sinyal yayınlama vb.) gerçekleştirir ve ardından genellikle olaydan çıkan Sıra Akışını (Sequence Flow) takip ederek devam eder.

`ThrowEvent` sınıfından miras aldığı için, bir sonuç üretebilir (`EventDefinition` ile tanımlanır) ve bu eylem için veri kullanabilir (`DataInput`, `DataInputAssociation`).

Bu sınıf, BPMN XML şemasındaki `intermediateThrowEvent` elemanından otomatik olarak türetilmiştir.

## Özellikler

Bu sınıf, temel sınıfı olan `ThrowEvent`'ten tüm özellikleri miras alır. Kendi içinde ek özellik tanımlamaz, ancak davranışını içerdiği `EventDefinition` belirler.

## Yaygın Ara Fırlatmalı Olay Türleri (EventDefinition'a göre)

*   **None Intermediate Throw Event:** Herhangi bir özel eylem gerçekleştirmez, yalnızca akışın devam ettiğini gösterir (nadiren kullanılır, genellikle akış doğrudan bir sonraki elemana bağlanır).
*   **Message Intermediate Throw Event:** Belirli bir mesajı (`MessageEventDefinition`) gönderir.
*   **Signal Intermediate Throw Event:** Belirli bir sinyali (`SignalEventDefinition`) yayınlar (broadcast eder).
*   **Escalation Intermediate Throw Event:** Belirli bir yükseltmeyi (`EscalationEventDefinition`) fırlatır.
*   **Compensate Intermediate Throw Event:** Belirli bir aktivite veya kapsam için telafi (compensation) işlemini tetikler (`CompensateEventDefinition`).
*   **Link Intermediate Throw Event:** Sürecin başka bir yerindeki bir `Link Intermediate Catch Event`'e "sinyal" gönderir (`LinkEventDefinition`). Süreç diyagramını daha okunabilir kılmak için "goto" benzeri bir mekanizma sağlar.

## Kullanım

Süreç motoru, bir `Execution` bir `intermediateThrowEvent`'e ulaştığında:
1.  İlişkili `EventDefinition`(lar)ı kontrol eder.
2.  Gerekli veriyi (`DataInputAssociation` kullanarak) süreç değişkenlerinden alır.
3.  Tanımlanan eylemi gerçekleştirir (mesaj gönderme, sinyal yayınlama vb.).
4.  Eylemi gerçekleştirdikten sonra, `Execution`'ı olaydan çıkan Sıra Akışı üzerinden devam ettirir. 