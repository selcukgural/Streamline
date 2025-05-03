# Variable

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`Variable`, bir süreç örneği (`ProcessInstance`) veya belirli bir yürütme kapsamı (`Execution`) ile ilişkili bir veri parçasını (değişkeni) temsil eder. Süreç boyunca veri saklamak, kararlar almak (örn. ağ geçitlerinde) ve görevler arasında veri aktarmak için kullanılır.

`EntityBase` sınıfından türediği için bir `Id` ve `DomainEvents` koleksiyonuna sahiptir (ancak genellikle domain event tetiklemez).

## Özellikler

*   **`Name` { get; init; } : `string`**
    *   Değişkenin benzersiz adı. Bu ad, değişkene erişmek için kullanılır.
*   **`Type` { get; init; } : `string`**
    *   Değişkenin veri türünü belirtir. Motorun değeri nasıl yorumlayacağını ve serileştireceğini belirlemesine yardımcı olur. Yaygın türler: `"string"`, `"integer"`, `"long"`, `"double"`, `"boolean"`, `"datetime"`, `"json"`, `"xml"`, `"binary"` vb.
*   **`Value` { get; set; } : `string?`**
    *   Değişkenin değerini içerir. Karmaşık nesneler veya ikili veriler genellikle serileştirilmiş bir string (örn. JSON) olarak saklanır. Değerin gerçek türüne dönüştürülmesi, kullanan kodun sorumluluğundadır (genellikle `Type` özelliğine bakarak).
*   **`ExecutionId` { get; private set; } : `Guid?`**
    *   Bu değişkenin yerel (local) olduğu `Execution`'ın ID'si. Eğer `null` ise, değişken süreç örneği seviyesindedir (global).
    *   Yerel değişkenler, yalnızca tanımlandıkları yürütme kapsamında veya alt yürütme kapsamlarında görünürdür.
*   **`ProcessInstanceId` { get; private set; } : `Guid?`**
    *   Bu değişkenin ait olduğu `ProcessInstance`'ın ID'si. Genellikle `ExecutionId` `null` olduğunda bu alan anlamlıdır ve süreç örneği seviyesindeki (global) değişkenleri belirtir.

## Kullanım

Değişkenler süreç boyunca çeşitli şekillerde oluşturulur ve güncellenir:
*   Süreç başlatılırken başlangıç değişkenleri olarak (`IProcessEngineService.StartProcessInstanceAsync` gibi).
*   Bir görev (örn. Script Task, Service Task) tarafından programatik olarak.
*   Bir Kullanıcı Görevi (User Task) formundan gelen verilerle.
*   Mesaj veya Sinyal olaylarıyla gelen verilerle.

Değişkenler, süreç akışındaki koşullu ifadelerde (örn. Exclusive Gateway'deki Sequence Flow koşulları), görev atamalarında ve görevler arası veri geçişinde kullanılır. `IRepository<Variable>` üzerinden sorgulanabilirler. 