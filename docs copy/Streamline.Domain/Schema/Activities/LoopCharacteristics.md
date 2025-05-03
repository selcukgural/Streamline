# LoopCharacteristics (Soyut Sınıf)

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Common.BaseElement`

**BPMN Elemanı:** `loopCharacteristics` (Soyut)

## Açıklama

`LoopCharacteristics` (Döngü Karakteristikleri), BPMN 2.0'da bir Aktivitenin (Task veya SubProcess) tekrarlı yürütülme davranışını tanımlayan mekanizmalar için soyut bir temel sınıftır.

Bir Aktivite, ilişkili bir `LoopCharacteristics` tanımına sahip olabilir. Bu tanım, aktivitenin kaç kez, nasıl (sıralı mı paralel mi) ve hangi koşullar altında tekrarlanacağını belirtir.

`BaseElement` sınıfından miras alır.

Bu sınıf, BPMN XML şemasındaki soyut `tLoopCharacteristics` elemanından otomatik olarak türetilmiştir.

## Özellikler

Bu soyut sınıf, doğrudan özellik tanımlamaz. Özellikler, türetilmiş somut sınıflarda tanımlanır.

## Türetilmiş Sınıflar

BPMN 2.0'da iki tür döngü karakteristiği tanımlanmıştır ve bunlar bu sınıftan türetilmiştir:

*   **`StandardLoopCharacteristics`**: Belirli bir koşul (`loopCondition`) sağlandığı sürece aktivitenin tekrar etmesini sağlar (geleneksel `while` veya `do-while` döngülerine benzer).
*   **`MultiInstanceLoopCharacteristics`**: Aktivitenin birden çok örneğinin (instance) paralel veya sıralı olarak yürütülmesini sağlar (bir koleksiyon üzerinde döngü yapmaya benzer).

## Kullanım

Bir aktivite (Task veya SubProcess) tanımlanırken, BPMN modelleyicisi bu aktiviteye bir `standardLoopCharacteristics` veya `multiInstanceLoopCharacteristics` elemanı ekleyebilir. Süreç motoru, bu aktiviteye ulaştığında, ilişkili `LoopCharacteristics` tanımını okur ve aktivitenin yürütülmesini bu tanıma göre yönetir. 