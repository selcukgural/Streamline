# RootElement

*   **BPMN Tipi:** Soyut (Abstract)
*   **Amaç:** Bir BPMN `Definitions` elemanının doğrudan altında yer alabilen tüm üst seviye elemanlar için temel sınıftır. Bu elemanlar genellikle global olarak tanımlanır ve birden fazla süreç veya işbirliği tarafından referans alınabilir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, `Process`, `Collaboration`, `Choreography`, `Message`, `Error`, `Escalation`, `Signal`, `ItemDefinition`, `Interface`, `Resource`, `DataStore`, `EventDefinition` alt türleri, `GlobalTask` alt türleri, `Category`, `CorrelationProperty`, `EndPoint` gibi `Definitions` elemanının doğrudan alt öğesi olabilen elemanlar tarafından miras alınır.
    *   Bu elemanlar, bir BPMN dosyasının kök `<definitions>` etiketi içinde tanımlanır.
*   **Özellikler:**
    *   `BaseElement`'ten gelen `Id`, `Documentation`, `ExtensionElements` ve `AnyAttribute` özelliklerini miras alır.
    *   Kendine özgü ek standart özellikleri yoktur.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Bir BPMN tanımının ana, global kapsamlı yapı taşlarını temsil eder.
    *   Global kapsamda tanımlandıkları için farklı süreçler arasında paylaşılabilirler (örn. aynı `Message` veya `Error` tanımını farklı süreçlerde kullanmak).
    *   Soyut olduğu için doğrudan örneği oluşturulamaz.

## XML Örneği

`RootElement` soyut olduğundan doğrudan XML'de bulunmaz. Ancak, `<definitions>` elemanının doğrudan alt öğeleri olan somut elemanlar (örn. `<process>`, `<message>`, `<signal>`, `<collaboration>`, `<itemDefinition>`, `<error>` vb.) `RootElement`'ten türemiştir.

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             targetNamespace="http://example.com/globalDefs">

  <!-- RootElement'ten türeyen eleman örnekleri -->

  <message id="Msg_OrderInfo" name="Order Information"/>

  <signal id="Sig_OrderComplete" name="Order Processing Complete"/>

  <error id="Err_PaymentFailed" name="Payment Failed" errorCode="PAY-001"/>

  <itemDefinition id="ItemDef_CustomerData" structureRef="tns:CustomerType"/>

  <interface id="Intf_NotificationService" name="Notification Service">
    <operation id="Op_SendEmail" name="Send Email">
      <inMessageRef>...</inMessageRef>
    </operation>
  </interface>

  <resource id="Res_Manager" name="Department Manager"/>

  <process id="Proc_HandleOrder" name="Handle Customer Order" isExecutable="true">
    <!-- Süreç içeriği -->
  </process>

  <collaboration id="Collab_CustomerSupplier" name="Customer-Supplier Interaction">
    <!-- İşbirliği içeriği -->
  </collaboration>

  <category id="Cat_HighPriority" name="High Priority">
     <categoryValue id="Val_Urgent" value="Urgent"/>
  </category>

  <!-- Diğer global tanımlar (DataStore, Escalation, CorrelationProperty vb.) -->

</definitions>
```

Yukarıdaki tüm elemanlar (`message`, `signal`, `error`, `itemDefinition`, `interface`, `resource`, `process`, `collaboration`, `category`) `RootElement` soyut sınıfından miras alır ve `<definitions>` altında tanımlanabilir. 