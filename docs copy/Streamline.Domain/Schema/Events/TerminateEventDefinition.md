# TerminateEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Bir süreç örneği içindeki *tüm* paralel akışları (execution token'larını) anında sonlandırmak için kullanılır. Sürecin belirli bir noktada tamamen durdurulması gereken durumlar için tasarlanmıştır.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Sadece `EndEvent` ile kullanılır.
    *   Bu olay tetiklendiğinde, aynı süreç örneği (en üst seviyedeki süreç) içindeki tüm aktiviteler durdurulur, alt süreçler dahil olmak üzere tüm akışlar sonlandırılır.
*   **Özellikler:**
    *   (Standart özellikleri yoktur.)
*   **Önemli Noktalar:**
    *   Normal bir `EndEvent` (None tipi) sadece kendi geldiği akış yolunu sonlandırırken, `TerminateEndEvent` tüm süreci bitirir.
    *   Dikkatli kullanılmalıdır, çünkü bekleyen işleri veya telafi işlemlerini tetiklemeden süreci aniden keser.
    *   Çağrılan süreçlerde (Called Processes) kullanıldığında, sadece çağrılan sürecin içindeki akışları sonlandırır, çağıran süreci etkilemez. 