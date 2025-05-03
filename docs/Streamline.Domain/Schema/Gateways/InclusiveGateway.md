# Inclusive Gateway

**Namespace:** `Streamline.Domain.Schema.Gateways`
**Base Class:** `Streamline.Domain.Schema.Flow.Gateway`
**BPMN Element:** `inclusiveGateway`

## Açıklama

`InclusiveGateway` (Kapsayıcı Ağ Geçidi), bir süreç akışında hem dallanma (splitting) hem de birleştirme (joining) için kullanılan bir karar noktasıdır. `ExclusiveGateway`'den farklı olarak, `InclusiveGateway` koşulu sağlanan **bir veya daha fazla** giden yolu aynı anda aktif hale getirebilir.

**Dallanma (Splitting) Davranışı:**

*   Ağ geçidine bir token ulaştığında, giden Sıra Akışları üzerindeki koşullar değerlendirilir.
*   Koşulu sağlanan **tüm** yollar için birer token oluşturulur ve bu yollardan eş zamanlı olarak ilerler.
*   Eğer hiçbir koşul sağlanmazsa ve bir `Default` akış tanımlanmışsa, token varsayılan yoldan ilerler.

**Birleştirme (Joining) Davranışı:**

*   `InclusiveGateway` bir birleştirme noktası olarak kullanıldığında (birden fazla gelen Sıra Akışı olduğunda), gelen tüm aktif yollardan tokenların ulaşmasını bekler.
*   Sadece beklenen tüm tokenlar ulaştığında, tek bir token olarak giden yoldan devam eder.
*   Bu, dallanma noktasında ayrılan paralel yolların tekrar senkronize edilmesini sağlar.

## Davranış Özeti

1.  **Dallanma:**
    *   Token varır.
    *   Giden akışların koşulları değerlendirilir.
    *   Koşulu sağlanan *tüm* yollar için tokenlar oluşturulur ve bu yollar aktifleşir.
    *   Hiçbir koşul sağlanmazsa ve varsayılan yol varsa, token varsayılan yoldan ilerler.
2.  **Birleştirme:**
    *   Her gelen aktif akıştan bir token bekler.
    *   Beklenen tüm tokenlar ulaştığında, tek bir token olarak çıkar.

## Özellikler

*   **`Default` (String):** Dallanma davranışı sırasında, ağ geçidinden çıkan Sıra Akışlarından birinin ID'sini içerir. Bu ID'ye sahip Sıra Akışı, diğer giden akışların hiçbirinin koşulu sağlanmadığında kullanılacak varsayılan yol olarak işaretlenir. Bu, hiçbir koşulun sağlanmadığı durumlarda bile sürecin ilerlemesini garantilemeye yardımcı olur.

## Kullanım

`InclusiveGateway`, bazı koşullara bağlı olarak birden fazla işin paralel olarak yapılabileceği veya belirli paralel yolların tamamlanmasının beklenmesi gereken durumlar için kullanılır.

**Örnek Senaryolar (Dallanma):**

*   Bir belge onay sürecinde, hem 'Hukuk Departmanı'nın incelemesi' (koşul: belge hukuki içerik taşıyor) hem de 'Finans Departmanı'nın incelemesi' (koşul: belge finansal veri içeriyor) yolları aynı anda başlatılabilir.
*   Bir ürün siparişinde, 'Stok Kontrolü' (her zaman yapılır) ve 'Müşteri Kredi Limiti Kontrolü' (koşul: yeni müşteri veya limit aşımı riski var) adımları paralel olarak tetiklenebilir.

**Örnek Senaryolar (Birleştirme):**

*   Yukarıdaki belge onay sürecinde, hem Hukuk hem de Finans departmanlarının incelemeleri tamamlandıktan sonra sürecin devam etmesi için birleştirici bir `InclusiveGateway` kullanılır.

`InclusiveGateway`, `ExclusiveGateway`'in katı tek yol kuralı ile `ParallelGateway`'in koşulsuz tüm yolları açması arasında esnek bir denge sunar. 