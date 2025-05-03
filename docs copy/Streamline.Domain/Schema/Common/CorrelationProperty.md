# CorrelationProperty

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Bir `CorrelationKey`'i oluşturan tekil veri parçalarını tanımlar. Mesajlar arasında eşleştirme yapmak için kullanılan belirli bir iş verisini (örneğin, sipariş numarası, müşteri kimliği, fatura numarası) temsil eder.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   Bir veya daha fazla `CorrelationProperty`, bir `CorrelationKey` tanımında `correlationPropertyRef` özelliği aracılığıyla referans alınarak bir korelasyon anahtarı oluşturur.
    *   Her `CorrelationProperty`, gelen bir mesajdan ilgili verinin nasıl çıkarılacağını tanımlayan en az bir `CorrelationPropertyRetrievalExpression` içermelidir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Korelasyon özelliğinin adı (örn. "orderId", "customerId").
    *   `Type` (`XmlQualifiedName`, `XmlAttribute`): Bu korelasyon özelliğinin veri tipine bir referans (genellikle bir `ItemDefinition` ID'si).
    *   `CorrelationPropertyRetrievalExpression` (Collection<`CorrelationPropertyRetrievalExpression`>, `XmlElement`, Zorunlu): Bu özelliğin değerinin gelen bir mesajdan nasıl (örn. XPath ifadesi ile) çıkarılacağını tanımlayan bir veya daha fazla ifade (`Expression`). Birden fazla ifade, farklı mesaj formatlarından aynı veriyi çıkarmak için kullanılabilir.
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.CorrelationPropertyRetrievalExpression`
    *   `Streamline.Domain.Schema.Common.ItemDefinition` (dolaylı, `Type` referansı yoluyla)
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Mesaj korelasyonunun temel yapı taşıdır.
    *   Bir mesajın içeriğindeki anlamlı bir iş verisini temsil eder.
    *   `CorrelationPropertyRetrievalExpression`, verinin mesajdan nasıl çekileceğinin teknik detayını belirtir. 

## XML Örneği

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:tns="http://example.com/order"
             xmlns:xsd="http://www.w3.org/2001/XMLSchema"
             targetNamespace="http://example.com/order">

  <!-- İlgili Mesaj ve ItemDefinition tanımları -->
  <itemDefinition id="ItemDef_OrderRequest" structureRef="xsd:string"/>
  <message id="Msg_OrderRequest" name="Order Request Message" itemRef="ItemDef_OrderRequest"/>

  <itemDefinition id="ItemDef_CustomerInfo" structureRef="xsd:string"/>
  <message id="Msg_CustomerInfo" name="Customer Info Message" itemRef="ItemDef_CustomerInfo"/>

  <!-- Correlation Property Tanımı -->
  <correlationProperty id="Prop_OrderID" name="Order ID" type="xsd:string">
    <documentation>Unique identifier for the order.</documentation>
    <!-- Sipariş isteği mesajından Order ID'yi alma -->
    <correlationPropertyRetrievalExpression messageRef="Msg_OrderRequest">
      <messagePath language="http://www.w3.org/1999/XPath">/order/orderId</messagePath>
    </correlationPropertyRetrievalExpression>
    <!-- Müşteri bilgisi mesajından Order ID'yi alma (farklı bir yapı varsayımı) -->
    <correlationPropertyRetrievalExpression messageRef="Msg_CustomerInfo">
      <messagePath language="http://www.w3.org/1999/XPath">/customer/relatedOrder/id</messagePath>
    </correlationPropertyRetrievalExpression>
  </correlationProperty>

  <!-- Başka bir Correlation Property -->
  <correlationProperty id="Prop_CustomerID" name="Customer ID"/> <!-- Tip belirtilmemiş -->
    <correlationPropertyRetrievalExpression messageRef="Msg_CustomerInfo">
       <messagePath>/customer/customerId</messagePath> <!-- Dil belirtilmemiş, varsayılan kullanılır -->
    </correlationPropertyRetrievalExpression>

  <!-- Bu property'leri kullanan CorrelationKey tanımı -->
  <collaboration id="Collab_Example">
    <correlationKey id="CKey_OrderCustomer">
      <correlationPropertyRef>Prop_OrderID</correlationPropertyRef>
      <correlationPropertyRef>Prop_CustomerID</correlationPropertyRef>
    </correlationKey>
  </collaboration>

</definitions>
``` 