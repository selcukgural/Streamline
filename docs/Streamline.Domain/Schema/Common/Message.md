# Message

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Süreçler arasında (`MessageFlow` aracılığıyla) veya bir süreç ile dış katılımcılar arasında değiş tokuş edilen iletişim içeriğini temsil eder. Bir mesajın adını ve taşıdığı verinin yapısını (`ItemDefinition` referansı ile) tanımlar.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   `MessageFlow` elemanları, hangi `Message`'ın iletilmekte olduğunu `messageRef` özniteliği ile belirtir.
    *   `MessageEventDefinition` (Mesaj Başlangıç/Ara/Bitiş Olayları), `SendTask`, `ReceiveTask` gibi elemanlar, gönderdikleri veya aldıkları mesajı belirtmek için bir `Message`'a `messageRef` özniteliği ile referans verir.
    *   Bir `Interface` içindeki `Operation` tanımları, giriş (`inMessageRef`) ve çıkış (`outMessageRef`) parametreleri olarak `Message`'lara referans verir.
    *   `itemRef` özelliği, mesajın "payload" yani taşıdığı verinin türünü ve yapısını belirtir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Mesajın adı (örn. "Sipariş Bilgisi", "Fatura İsteği").
    *   `ItemRef` (`XmlQualifiedName`, `XmlAttribute`): Mesajın içeriğinin yapısını tanımlayan `ItemDefinition` elemanının ID'sine referans (isteğe bağlı). Eğer belirtilirse, mesajın belirli bir veri yapısına sahip olduğu anlaşılır.
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Events.ItemDefinition` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Flow.MessageFlow` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Events.MessageEventDefinition` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Activities.SendTask` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Activities.ReceiveTask` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Common.Operation` (Bu elemanı kullanır)
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Süreçler arası ve sistemler arası iletişimin temel taşıdır.
    *   Mesaj içeriğinin yapısını tanımlayarak veri entegrasyonunu kolaylaştırır.

## XML Örneği

`<message>` tanımları `<definitions>` altında yer alır ve genellikle `itemDefinition`'lara referans verir:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:tns="http://example.com/shipping"
             xmlns:ord="http://example.com/orderdata"
             targetNamespace="http://example.com/shipping">

  <!-- Dış veri tipi tanımını içe aktarma -->
  <import importType="http://www.w3.org/2001/XMLSchema" 
          location="OrderData.xsd" 
          namespace="http://example.com/orderdata"/> 

  <!-- İçe aktarılan tipe dayalı ItemDefinition -->
  <itemDefinition id="ItemDef_OrderInfo" structureRef="ord:OrderType"/>

  <!-- Basit tip (String) ItemDefinition -->
  <itemDefinition id="ItemDef_TrackingCode" structureRef="xsd:string"/>

  <!-- Mesaj Tanımları -->
  <message id="Msg_ShippingRequest" name="Shipping Request" itemRef="ItemDef_OrderInfo" />

  <message id="Msg_ShippingConfirmation" name="Shipping Confirmation" itemRef="ItemDef_TrackingCode" />

  <message id="Msg_Notification" name="Simple Notification" /> <!-- itemRef olmadan (payload yapısı belirsiz) -->

  <!-- Bu mesajları kullanan bir Collaboration örneği -->
  <collaboration id="Collab_Shipping">
    <participant id="Part_Customer" name="Customer" />
    <participant id="Part_Shipper" name="Shipper" />
    <messageFlow id="MFlow_Req" sourceRef="Part_Customer" targetRef="Part_Shipper" messageRef="Msg_ShippingRequest" />
    <messageFlow id="MFlow_Conf" sourceRef="Part_Shipper" targetRef="Part_Customer" messageRef="Msg_ShippingConfirmation" />
  </collaboration>

</definitions>
``` 