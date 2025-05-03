# EndEventHandler

*   **BPMN Elementi:** `End Event` (Bitiş Olayı - None, Error, Escalation, Terminate, Cancel tipleri)
*   **BPMN Amacı:** Bir süreç veya alt süreç içindeki bir akış yolunun sonunu belirtir. Davranışı, içerdiği `EventDefinition`\'a göre değişir:
    *   **None:** Sadece mevcut akış yolunu sonlandırır.
    *   **Error:** Belirli bir hata koduyla akışı sonlandırır ve hatayı yakalayabilecek bir üst seviyeye (boundary event veya event subprocess) yayar.
    *   **Escalation:** Belirli bir yükseltme koduyla sinyal gönderir, ancak varsayılan olarak akışı *sonlandırmaz*. Sinyal üst seviyeler tarafından yakalanabilir.
    *   **Terminate:** Mevcut akış yolunu ve süreç örneği içindeki *diğer tüm aktif akışları* anında sonlandırır.
    *   **Cancel:** Sadece bir "Transaction Subprocess" içinde kullanılır. İşlemin iptal edilmesi gerektiğini belirtir ve ilgili "Cancel Boundary Event"i tetikler.
*   **Uygulama & Davranış:**
    *   Bu handler, bir execution bir `EndEvent`\'e ulaştığında çalışır.
    *   `EndEvent` nesnesinin `EventDefinition` koleksiyonunu kontrol ederek hangi tipte bir bitiş olayı olduğunu belirler (`TerminateEventDefinition`, `ErrorEventDefinition`, `EscalationEventDefinition`, `CancelEventDefinition` veya hiçbiri/null).
    *   Tespit edilen tipe göre ilgili özel `Handle...` yardımcı metodunu çağırır:
        *   **`HandleTerminateEndEventAsync`:** Süreç örneğindeki tüm aktif `Execution`\'ları ve `ActivityInstance`\'ları bulur, `Terminate` ve `Cancel` metotlarını çağırarak sonlandırır. `ProcessInstance`\'ı `Completed` olarak işaretler.
        *   **`HandleErrorEndEventAsync`:** `ErrorEventDefinition`\'dan hata kodunu alır. Mevcut `Execution`\'ı `Fail` metoduyla bu kodla başarısız yapar. Hatayı yaymak için `ExecutionFlowManager.FindAndHandleErrorAsync`\'i çağırır.
        *   **`HandleEscalationEndEventAsync`:** `EscalationEventDefinition`\'dan kodu alır. Mevcut `Execution`\'da `Escalate` metodunu çağırarak kodu ayarlar (execution\'ı sonlandırmaz). Yükseltmeyi yaymak için `ExecutionFlowManager.FindAndHandleEscalationAsync`\'i çağırır. Execution\'ın akışa devam etmesine izin verir (eğer yakalayan handler kesintili değilse).
        *   **`HandleCancelEndEventAsync`:** Olayın bir `Transaction` alt süreci içinde olduğunu doğrular. İlgili `CancelBoundaryEvent`\'i bulur. Mevcut `Execution`\'ı sonlandırır. `ExecutionFlowManager.TerminateScopeContentsAsync` ile transaction kapsamındaki diğer execution\'ları temizler. Son olarak `ExecutionFlowManager.StartExecutionAtNodeAsync` ile `CancelBoundaryEvent`\'ten yeni bir akış başlatır.
        *   **`HandleNoneEndEventAsync`:** Mevcut `Execution`\'ı `Terminate` ile sonlandırır. Eğer bir alt süreç kapsamında ise ve bu sonlanan execution ile alt süreç tamamlanmışsa (`IsScope` parent\'ın başka aktif alt execution\'ı yoksa), alt süreç düğümünde bekleyen ana execution\'ı bulup `ExecutionFlowManager.ContinueExecutionAsync` ile devam ettirir. Eğer root execution ise ve başka aktif execution kalmadıysa, `ProcessInstance`\'ı `Completed` yapar. 