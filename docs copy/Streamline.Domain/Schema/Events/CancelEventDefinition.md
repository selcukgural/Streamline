# CancelEventDefinition

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Sadece `Transaction` tipi bir alt süreç (`SubProcess`) içinde kullanılır. İşlemin (transaction) başarılı bir şekilde tamamlanmadığını ve iptal edilmesi gerektiğini belirtir.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `EventDefinition` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Sadece `EndEvent` (Transaction alt süreci içinde, iptali tetikleyen) ile kullanılır.
    *   Bu olay tetiklendiğinde, Transaction alt sürecine bağlı olan `Cancel Boundary Event` tetiklenir.
*   **Özellikler:**
    *   (Standart özellikleri yoktur.)
*   **Önemli Noktalar:**
    *   `Transaction` alt süreçleri ve bunlara bağlı `Cancel Boundary Event` ile birlikte çalışır.
    *   İşlemin geri alınması (rollback) veya telafi edilmesi (compensation) gereken durumları modellemek için kullanılır.
    *   `Cancel Boundary Event` tetiklendiğinde, Transaction alt süreci içindeki tüm aktif işler sonlandırılır ve akış `Cancel Boundary Event`'ten devam eder. 