# EventDefinition

*   **BPMN Tipi:** Soyut (Abstract) - `RootElement`
*   **Amaç:** BPMN Olaylarının (`Event`) altında yatan spesifik nedeni veya tetikleyiciyi tanımlayan elemanlar için soyut temel sınıftır. Bir olayın ne tür bir olay olduğunu (Mesaj, Zamanlayıcı, Hata, Sinyal vb.) belirtir.
*   **Konum:** `Streamline.Domain.Schema.Events`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, belirli olay türlerini tanımlayan somut sınıflar (`MessageEventDefinition`, `TimerEventDefinition`, `ErrorEventDefinition`, `SignalEventDefinition`, `ConditionalEventDefinition`, `LinkEventDefinition`, `CompensateEventDefinition`, `CancelEventDefinition`, `TerminateEventDefinition`, `EscalationEventDefinition`) tarafından miras alınır.
    *   Bir `Event` elemanı (örn. `StartEvent`, `IntermediateCatchEvent`, `EndEvent`) genellikle bir veya daha fazla (bazı durumlarda) `EventDefinition` alt elemanı içerir.
*   **Özellikler:**
    *   (Genel özellikleri yoktur, belirli alt sınıflar olaya özgü referansları veya yapılandırmaları içerir. Örneğin `MessageEventDefinition` bir `MessageRef` içerir, `TimerEventDefinition` zamanlama ifadesini içerir.)
*   **Önemli Noktalar:**
    *   Bir olayın davranışını belirleyen ana unsurdur.
    *   "None" tipi olaylarda (örn. basit `StartEvent` veya `EndEvent`) `EventDefinition` bulunmaz.
    *   `CatchEvent` ve `ThrowEvent` elemanları, içerdikleri `EventDefinition`'a göre farklı davranırlar.
    *   Soyut olduğu için doğrudan örneği oluşturulamaz. 