# Rendering

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `FlowElement` veya `Artifact`'ın görsel olarak nasıl sunulacağına dair (genellikle araca özgü) bilgileri veya ipuçlarını içerir. Örneğin, özel ikonlar, renkler veya diğer stil bilgileri.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `FlowElement` veya `Artifact`'ın `<rendering>` alt etiketi olarak XML'de yer alır.
    *   Standart BPMN'de belirli alt elemanları veya öznitelikleri yoktur.
    *   Görselleştirme ile ilgili özel bilgiler (ikon URI'si, renk kodu, stil sınıfı vb.) genellikle bu elemanın içindeki `ExtensionElements` aracılığıyla eklenir.
*   **Özellikler:**
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
    *   Kendine özgü standart özellikleri yoktur.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.FlowElement` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Artifacts.Artifact` (Bu elemanı içerebilir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Modelin görsel sunumunu özelleştirmek için kullanılır.
    *   Standart bir yapısı olmadığı için kullanımı ve anlamı tamamen kullanılan modelleme aracına bağlıdır ve genellikle farklı araçlar arasında taşınabilir değildir. 