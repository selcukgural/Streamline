# Parallel Gateway

**Namespace:** `Streamline.Domain.Schema.Gateways`
**Base Class:** `Streamline.Domain.Schema.Flow.Gateway`
**BPMN Element:** `parallelGateway`

## Açıklama

`ParallelGateway` (Paralel Ağ Geçidi), bir süreç akışında paralel yollar oluşturmak (dallanma) ve bu yolları senkronize etmek (birleştirme) için kullanılır. Diğer ağ geçitlerinden farklı olarak, `ParallelGateway` **koşul değerlendirmez**.

**Dallanma (Splitting) Davranışı:**

*   Ağ geçidine bir token ulaştığında, herhangi bir koşul kontrolü yapılmaksızın, giden **tüm** Sıra Akışları aynı anda aktif hale getirilir.
*   Her giden yol için birer token oluşturulur ve bu yollardan eş zamanlı olarak ilerler.

**Birleştirme (Joining) Davranışı:**

*   `ParallelGateway` bir birleştirme noktası olarak kullanıldığında (birden fazla gelen Sıra Akışı olduğunda), gelen **tüm** aktif yollardan birer token ulaşmasını bekler.
*   Sadece beklenen tüm tokenlar (her aktif gelen yol için bir tane) ulaştığında, tek bir token olarak giden yoldan devam eder.
*   Bu davranış, daha önceki bir paralel dallanma noktasında ayrılan tüm işlerin tamamlandığından emin olmak için kritik bir senkronizasyon mekanizması sağlar.

## Davranış Özeti

1.  **Dallanma:**
    *   Token varır.
    *   Koşul değerlendirmesi yapılmaz.
    *   Giden *tüm* yollar için tokenlar oluşturulur ve bu yollar aktifleşir.
2.  **Birleştirme:**
    *   Her gelen aktif akıştan bir token bekler.
    *   Beklenen *tüm* tokenlar ulaştığında, tek bir token olarak çıkar.

## Özellikler

`ParallelGateway`'in `ExclusiveGateway` (`Default`) veya `InclusiveGateway` (`Default`) gibi kendine özgü ek özellikleri **yoktur**. Davranışı tamamen gelen ve giden akışların sayısına ve yapısına bağlıdır.

## Kullanım

`ParallelGateway`, birbirinden bağımsız olarak aynı anda yürütülebilecek görevleri başlatmak veya bu tür paralel görevlerin tümünün tamamlanmasını beklemek için kullanılır.

**Örnek Senaryolar (Dallanma):**

*   Bir müşteri siparişi alındığında, 'Faturayı Hazırla', 'Ürünü Paketle' ve 'Kargo Bilgilerini Oluştur' görevleri aynı anda başlatılabilir.
*   Bir yazılım geliştirme sürecinde, 'Kod Derleme', 'Birim Testlerini Çalıştırma' ve 'Kod Analizi' adımları paralel olarak tetiklenebilir.

**Örnek Senaryolar (Birleştirme):**

*   Yukarıdaki müşteri siparişi örneğinde, fatura hazırlandıktan, ürün paketlendikten ve kargo bilgileri oluşturulduktan sonra 'Siparişi Gönder' adımına geçmek için birleştirici bir `ParallelGateway` kullanılır.
*   Yazılım geliştirme örneğinde, derleme, testler ve analiz tamamlandıktan sonra 'Dağıtım Paketini Oluştur' adımına geçmeden önce tüm paralel işlemlerin bitmesini beklemek için kullanılır.

`ParallelGateway`, süreçlerde gerçek paralellik ve senkronizasyon noktaları oluşturmak için temel bir yapıdır. 