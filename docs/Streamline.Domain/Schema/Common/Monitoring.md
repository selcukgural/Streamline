# Monitoring

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** `FlowElement`'lerin (Aktiviteler, Olaylar, Geçitler vb.) çalışma zamanı performansını veya durumunu izlemek için kullanılan meta verileri veya yapılandırmaları içindir. İş Aktivitesi İzleme (BAM - Business Activity Monitoring) araçları tarafından kullanılabilir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `FlowElement`'in `<monitoring>` alt etiketi olarak XML'de yer alır.
    *   Standart BPMN'de belirli alt elemanları veya öznitelikleri yoktur.
    *   İzleme ile ilgili özel bilgiler (örn. KPI tanımları, izleme olay seviyeleri, BAM sistemiyle entegrasyon detayları) genellikle bu elemanın içindeki `ExtensionElements` aracılığıyla eklenir.
*   **Özellikler:**
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
    *   Kendine özgü standart özellikleri yoktur.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.FlowElement` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Süreç performansını ve sağlığını izlemek için kullanılan araçlarla entegrasyonu kolaylaştırır.
    *   Standart bir yapısı olmadığı için kullanımı ve anlamı tamamen kullanılan BAM aracına veya özel implementasyona bağlıdır. 