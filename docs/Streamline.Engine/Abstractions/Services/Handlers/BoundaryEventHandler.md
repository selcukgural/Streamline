# BoundaryEventHandler

*   **BPMN Elementi:** `Boundary Event` (Sınır Olayı - Timer, Error, Escalation, Message, Signal, Cancel, Compensate, Conditional)
*   **BPMN Amacı:** Bir aktiviteye (genellikle Task veya SubProcess) eklenir. Aktivite çalışırken belirli bir olayın gerçekleşmesi durumunda (veya aktivite tamamlandıktan sonra - compensate için) normal akışı kesintiye uğratan (interrupting) veya paralel bir akış başlatan (non-interrupting) bir yol sağlar.
*   **Uygulama & Davranış:**
    *   Bu handler sınıfının varlığı DI kaydında görünüyor ancak tipik akışta doğrudan `FlowNodeHandlerFactory` tarafından çağrılması beklenmez.
    *   Sınır olaylarının kaydı ve tetiklenmesi genellikle `ExecutionFlowManager` içinde yönetilir:
        *   **Kayıt:** Bir aktiviteye girildiğinde (`ExecutionFlowManager.ContinueExecutionAsync` içinde), `RegisterBoundaryEventSubscriptionsAsync` metodu çağrılarak aktiviteye bağlı sınır olayları için `EventSubscription`\'lar oluşturulur (ve Timer ise job kurulur).
        *   **Tetikleme:** İlgili olay gerçekleştiğinde (örn. Timer patladığında `TriggerBoundaryTimerEventAsync` çağrılır, hata oluştuğunda `FindAndHandleErrorAsync` ilgili boundary event\'i bulur, mesaj/sinyal geldiğinde ilgili subscription bulunur ve handler tetiklenir), `ExecutionFlowManager` genellikle `StartExecutionAtNodeAsync` çağırarak sınır olayından yeni bir execution başlatır. Eğer olay kesintili (interrupting) ise, `TerminateScopeContentsAsync` (veya benzeri) ile ana aktivitenin execution\'ını/kapsamını sonlandırır.
    *   Eğer bu `BoundaryEventHandler` sınıfı bir şekilde çağrılırsa, muhtemelen `ExecuteAsync` metodu, gelen `BoundaryEvent`\'ten doğrudan `StartExecutionAtNodeAsync` çağırarak akışı başlatacaktır. Ancak ana mantık `ExecutionFlowManager`\'dadır. 