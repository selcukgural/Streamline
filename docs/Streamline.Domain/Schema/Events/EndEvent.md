# End Event

**Namespace:** `Streamline.Domain.Schema.Events`
**Base Class:** `Streamline.Domain.Schema.Events.ThrowEvent`
**BPMN Element:** `endEvent`

## Açıklama

`EndEvent`, bir süreç akışının veya bir akış yolunun sonlandığı noktayı temsil eden bir BPMN olay türüdür. Akış bu olaya ulaştığında, ilgili akış yolu tamamlanır. Bir süreçte birden fazla `EndEvent` olabilir ve her biri farklı bir son durumu veya sonucu temsil edebilir.

`EndEvent` sınıfı, `ThrowEvent` sınıfından türetilmiştir. Bu, son olayın aynı zamanda bir olay *fırlatma* yeteneğine sahip olabileceği anlamına gelir (örneğin, bir hata veya mesaj fırlatabilir).

## Davranış

Bir token (yürütme) bir `EndEvent`'e ulaştığında, o token tüketilir ve akış o yolda sonlanır.

*   **Varsayılan Son Olay:** Eğer `EndEvent` belirli bir türle (örneğin, Hata, İptal) ilişkilendirilmemişse, sadece akışı sonlandırır.
*   **Belirli Son Olay Türleri:** `EndEvent` farklı türlerde olabilir ve her biri özel bir davranış sergiler:
    *   **Error End Event:** Belirtilen bir hatayı fırlatır ve bu hata üst kapsamdaki bir Error Boundary Event veya Error Start Event tarafından yakalanabilir.
    *   **Terminate End Event:** Süreç örneği içindeki tüm paralel akışları derhal sonlandırır. Sadece en üst seviye süreçte kullanılmalıdır.
    *   **Message End Event:** Belirtilen bir mesajı gönderir.
    *   **Signal End Event:** Belirtilen bir sinyali yayınlar.
    *   **Compensation End Event:** Bir telafi işlemini tetikler.
    *   **Cancel End Event:** Bir Transaction alt süreci içindeki iptal işlemini tetikler.

Bu özel son olay türleri genellikle `EndEvent`'ten türetilen veya `eventDefinition` özelliği aracılığıyla yapılandırılan ayrı sınıflarla temsil edilir. `EndEvent` sınıfı, bu özel davranışlar için temel yapıyı sağlar.

## Özellikler

`EndEvent` sınıfı, temel sınıfı `ThrowEvent`'ten özellikler miras alır. Kendine özgü önemli ek özellikleri yoktur, ancak davranışını belirleyen `eventDefinitions` koleksiyonunu içerebilir.

## Kullanım

`EndEvent`, bir süreç modelinde akışların mantıksal sonunu belirtmek için kullanılır. Farklı son olay türleri, sürecin farklı sonuçlarını veya tamamlanma şekillerini modellemek için kullanılır. Örneğin, başarılı bir tamamlama varsayılan bir son olayla gösterilirken, bir hata durumu bir Error End Event ile gösterilebilir. 