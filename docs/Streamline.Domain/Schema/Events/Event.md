# Event (Soyut Sınıf)

**Namespace:** `Streamline.Domain.Schema.Events`

**Temel Sınıf:** `Streamline.Domain.Schema.Flow.FlowNode`

**BPMN Elemanı:** `tEvent`

## Açıklama

`Event` (Olay), BPMN 2.0 spesifikasyonunda süreç yaşam döngüsü boyunca meydana gelen bir şeyi temsil eden temel soyut sınıftır. Olaylar, süreçleri başlatır, süreç akışını etkiler (yönlendirir, geciktirir, kesintiye uğratır) ve süreçleri sonlandırır.

BPMN'de farklı olay türleri vardır (Başlangıç, Bitiş, Ara, Sınır) ve bu olayların da farklı tetikleyicileri (Mesaj, Zamanlayıcı, Hata, Sinyal vb.) olabilir. Bu sınıf, tüm bu olay türleri için ortak bir temel sağlar.

`FlowNode`'dan türediği için, Olaylar da süreç akışının bir parçasıdır ve gelen/giden Sıra Akışlarına (`SequenceFlow`) bağlanabilir (bazı kısıtlamalarla, örn. Başlangıç Olaylarının gelen akışı olmaz, Bitiş Olaylarının giden akışı olmaz).

Bu sınıf, BPMN XML şemasındaki `tEvent` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Property` : `Collection<Property>`**
    *   Olaya özgü, genellikle çalışma zamanında kullanılan ek özelliklerin (key-value çiftleri) bir koleksiyonu.

## Türetilmiş Sınıflar

BPMN'deki temel olay kategorileri bu sınıftan türetilmiştir:
*   **`CatchEvent` (Yakalamalı Olay - Soyut):** Bir tetikleyiciyi bekleyen olaylar (örn. `IntermediateCatchEvent`, `StartEvent`, `BoundaryEvent`).
*   **`ThrowEvent` (Fırlatmalı Olay - Soyut):** Bir tetikleyiciyi (sonucu) fırlatan olaylar (örn. `IntermediateThrowEvent`, `EndEvent`, `ImplicitThrowEvent`).

Bu kategorilerin altında da spesifik olay türleri bulunur:
*   `StartEvent`
*   `EndEvent`
*   `IntermediateCatchEvent`
*   `IntermediateThrowEvent`
*   `BoundaryEvent`
*   `ImplicitThrowEvent` (Genellikle modellemede doğrudan kullanılmaz)

## İlişkili Elemanlar

Olayların davranışını belirleyen en önemli ilişkili elemanlar **Olay Tanımlarıdır (`EventDefinition`)**. Bir olayın ne tür bir tetikleyiciye sahip olduğunu (Mesaj, Zamanlayıcı, Hata vb.) `EventDefinition` alt elemanı belirtir. Bir olay (özellikle Yakalamalı ve Fırlatmalı Ara Olaylar) birden fazla `EventDefinition` içerebilir (paralel olaylar için), ancak genellikle tek bir tane içerir.

## Kullanım

Bu soyut sınıf doğrudan kullanılmaz. Süreç motoru, BPMN modelini ayrıştırırken `startEvent`, `endEvent`, `intermediateCatchEvent` gibi etiketlerle karşılaştığında, bu sınıftan türeyen ilgili somut sınıfın (`StartEvent`, `EndEvent` vb.) bir örneğini oluşturur. Motor, olayın türüne ve ilişkili `EventDefinition`(larına) göre davranışını belirler (örn. süreç başlatma, bekleme, sonuç fırlatma, akışı kesintiye uğratma). 