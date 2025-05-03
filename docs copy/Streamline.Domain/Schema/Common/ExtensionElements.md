# ExtensionElements

*   **BPMN Tipi:** Yardımcı Eleman / Kapsayıcı
*   **Amaç:** Herhangi bir `BaseElement`'e (yani hemen hemen tüm BPMN elemanlarına) standart dışı, özel XML elemanları veya verileri eklemek için kullanılan bir kapsayıcıdır. BPMN standardının genişletilebilirliğini sağlar.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** Yok
*   **Uygulama & Davranış:**
    *   Herhangi bir `BaseElement`'in `<extensionElements>` alt etiketi olarak XML'de yer alır.
    *   İçinde, BPMN standardında tanımlanmamış herhangi bir sayıda ve türde XML elemanı barındırabilir.
    *   Bu elemanlar genellikle belirli bir modelleme aracı, yürütme platformu veya özel bir uygulama alanı tarafından tanımlanır ve kullanılır.
*   **Özellikler:**
    *   `Any` (Collection<`XmlElement`>, `XmlAnyElement`): Standart dışı tüm XML elemanlarını içeren bir koleksiyon. Serileştirme/deserileştirme sırasında, BPMN şemasında tanımlı olmayan tüm alt elemanlar bu koleksiyona eklenir veya bu koleksiyondan okunur.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.BaseElement` (Bu elemanı içerir)
*   **Önemli Noktalar:**
    *   BPMN'in en önemli genişletilebilirlik mekanizmasıdır.
    *   Modelleme araçlarının özel nitelikler eklemesine (örn. görsel düzen bilgileri, simülasyon parametreleri) veya yürütme motorlarının ek yapılandırmalar tanımlamasına olanak tanır.
    *   Buraya eklenen elemanların anlamı ve işlenmesi, ilgili özel araca veya platforma bağlıdır. Standart BPMN uyumlu bir motor, bu elemanları genellikle göz ardı eder (eğer ilgili `Extension` için `mustUnderstand` `true` değilse). 