# SubProcessHandler

*   **BPMN Elementi:** `Sub-Process` (Alt Süreç - Gömülü)
*   **BPMN Amacı:** Bir süreç içindeki adımları gruplamak için kullanılır. Akışı basitleştirir ve yeniden kullanılabilir mantık blokları oluşturmaya yardımcı olur. Gömülü alt süreç, ana sürecin bir parçası olarak çalışır ve kendi başlangıç ve bitiş olaylarına sahiptir.
*   **Uygulama & Davranış:**
    *   Bu handler bir `SubProcess` elemanına ulaşıldığında çalışır.
    *   Gelen `Execution`\'ı bu `SubProcess` düğümünde beklemeye alır.
    *   Yeni bir `Execution` oluşturur. Bu yeni execution, alt sürecin **kapsamını (scope)** temsil eder (`IsScope=true`). Ana execution\'ı `ParentExecution` olarak referans alır ve `ScopeFlowNodeId` olarak `SubProcess`\'in ID\'sini kaydeder.
    *   Alt süreç tanımı içindeki **tüm** `StartEvent`\'leri bulur.
    *   Her bir `StartEvent` için *yeni bir alt execution* oluşturur (bu alt execution\'lar, kapsam execution\'ını parent olarak alır).
    *   Her yeni alt execution için `ExecutionFlowManager.StartExecutionAtNodeAsync` (veya benzeri) çağırarak alt sürecin iç akışlarını başlatır.
    *   Ana execution\'ı (alt sürece gelen) **devam ettirmez**. Bu execution, alt süreç içindeki tüm akışlar tamamlanana kadar (`EndEventHandler` tarafından tetiklenene kadar) `SubProcess` düğümünde bekler. 