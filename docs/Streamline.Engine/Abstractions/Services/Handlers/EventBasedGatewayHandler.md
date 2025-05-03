# EventBasedGatewayHandler

*   **BPMN Elementi:** `Event-Based Gateway` (Olay Tabanlı Ağ Geçidi)
*   **BPMN Amacı:** Akış yolunu, takip eden olaylardan *hangisinin ilk gerçekleştiğine* bağlı olarak yönlendirir. Ağ geçidini takip eden elemanlar genellikle `IntermediateCatchEvent` (Timer, Message, Signal vb.) veya `ReceiveTask` olmalıdır.
*   **Uygulama & Davranış:**
    *   Bu handler bir `EventBasedGateway`\'e ulaşıldığında çalışır.
    *   Ağ geçidine doğrudan bağlı olan *tüm* giden `SequenceFlow`\'ları ve onların hedefindeki olayları (örn. `IntermediateCatchEvent`) bulur.
    *   Bulunan *her bir* olay için (Timer, Message, Signal vb.) ilgili `EventSubscription`\'ı oluşturur (örn. `IntermediateCatchEventHandler`\'daki gibi). Eğer olay Timer ise, zamanlayıcıyı kurar.
    *   Mevcut `Execution`\'ı bu ağ geçidinde **beklemeye alır**.
    *   Bu olaylardan *hangisi ilk tetiklenirse* (örneğin timer dolar, mesaj gelir), o olayın bağlı olduğu `SequenceFlow` takip edilir.
    *   İlk olay tetiklendiğinde, bu ağ geçidi tarafından oluşturulan *diğer tüm* `EventSubscription`\'lar (ve ilgili timer job\'ları) iptal edilir.
    *   Tetiklenen olayın yolundan `ExecutionFlowManager.ContinueExecutionAsync` ile devam edilir. 