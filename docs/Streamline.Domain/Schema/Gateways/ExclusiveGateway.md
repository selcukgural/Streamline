# Exclusive Gateway

**Namespace:** `Streamline.Domain.Schema.Gateways`
**Base Class:** `Streamline.Domain.Schema.Flow.Gateway`
**BPMN Element:** `exclusiveGateway`

## Açıklama

`ExclusiveGateway` (Özel Ağ Geçidi veya XOR Ağ Geçidi), bir süreç akışında bir karar noktasını temsil eder. Bu ağ geçidine bir token (yürütme) ulaştığında, ağ geçidinden çıkan Sıra Akışları (Sequence Flows) üzerinde tanımlanan koşullar değerlendirilir. Koşulu sağlanan *yalnızca bir* yol seçilir ve token o yoldan ilerler.

`ExclusiveGateway`, iş kurallarına veya verilere dayalı olarak sürecin farklı yollara dallanması gereken durumlar için kullanılır. İsmi, çıkan yollardan sadece bir tanesinin (exclusive) seçilebilmesinden gelir.

## Davranış

1.  **Token Varışı:** Bir token `ExclusiveGateway`'e ulaştığında, ağ geçidinde durmaz, hemen çıkan yolları değerlendirmeye başlar.
2.  **Koşul Değerlendirme:** Ağ geçidinden çıkan her bir Sıra Akışı'na genellikle bir koşul (Condition Expression) atanır. Sistem bu koşulları (genellikle tanımlandıkları sırayla) değerlendirir.
3.  **İlk Uygun Yol Seçilir:** Koşulu `true` olarak dönen *ilk* Sıra Akışı seçilir ve token bu yoldan ilerler.
4.  **Diğer Yollar Değerlendirilmez:** Bir yol seçildikten sonra, diğer Sıra Akışlarının koşulları (eğer varsa) değerlendirilmez.
5.  **Varsayılan Yol (Default Flow):** Eğer çıkan Sıra Akışlarından hiçbirinin koşulu sağlanmazsa ve bir "varsayılan" (default) akış tanımlanmışsa, token bu varsayılan yoldan ilerler. Eğer varsayılan yol tanımlı değilse ve hiçbir koşul sağlanmazsa, bu durum genellikle bir modelleme hatası olarak kabul edilir ve süreç örneği bir hataya düşebilir veya beklenmedik bir durumda kalabilir.

## Özellikler

*   **`Default` (String):** Ağ geçidinden çıkan Sıra Akışlarından birinin ID'sini içerir. Bu ID'ye sahip Sıra Akışı, diğer giden akışların hiçbirinin koşulu sağlanmadığında kullanılacak varsayılan yol olarak işaretlenir. Bu, her durumda sürecin ilerleyebilmesini garantilemek için önemlidir.

## Kullanım

`ExclusiveGateway`, süreç akışındaki koşullu dallanmalar için standart BPMN elemanıdır.

**Örnek Senaryolar:**

*   Bir kredi başvurusunun onaylanıp reddedileceğine karar vermek (örneğin, kredi skoru > 700 ise onay yolu, değilse ret yolu).
*   Bir sipariş tutarına göre farklı indirim seviyeleri uygulamak (örneğin, tutar < 100 TL ise %5 indirim yolu, 100 TL <= tutar < 500 TL ise %10 indirim yolu, tutar >= 500 TL ise %15 indirim yolu).
*   Bir müşteri tipine göre farklı hizmet süreçlerini başlatmak (örneğin, 'VIP Müşteri' ise özel destek yolu, 'Standart Müşteri' ise standart destek yolu).

`ExclusiveGateway`, iş mantığını süreç modellerine açık ve net bir şekilde yansıtmak için temel bir araçtır. 