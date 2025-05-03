# ItemDefinition

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Süreç içinde kullanılan veya iletilen verilerin yapısını ve türünü tanımlar. `DataObject`, `DataInput`, `DataOutput`, `Message`, `Property` gibi elemanlar tarafından referans alınarak, bu elemanların taşıdığı verinin ne tür bir veri olduğunu (örn. string, integer, veya `Import` ile tanımlanmış karmaşık bir XML şeması/WSDL tipi) belirtir.
*   **Konum:** `Streamline.Domain.Schema.Events` (Not: Standartta `Common` veya `Data` olması daha beklendik olsa da, bu projede `Events` altında bulunuyor)
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Global olarak `<definitions>` altında tanımlanır.
    *   `structureRef` özelliği ile dışarıdan `Import` edilen veya önceden tanımlanmış (örn. XSD, WSDL, JSON Schema) bir veri yapısına referans verebilir. Eğer `structureRef` yoksa, genellikle basit veya platforma özgü bir tip (örn. `System.String`, `System.Object`) varsayılır.
    *   `isCollection` özelliği, tanımlanan yapının tek bir örnek mi yoksa bir koleksiyon/liste mi olduğunu belirtir.
    *   `itemKind` özelliği, verinin fiziksel mi yoksa bilgi tabanlı mı olduğunu belirtir (genellikle `Information`).
    *   Diğer BPMN elemanları (`DataObject`, `Message` vb.) `itemSubjectRef` veya `itemRef` gibi özelliklerle bu `ItemDefinition`'a referans verir.
*   **Özellikler:**
    *   `StructureRef` (`XmlQualifiedName`, `XmlAttribute`): Verinin yapısını tanımlayan dış bir tipe (genellikle `Import` ile içeri aktarılmış XSD veya WSDL tipi ya da temel XML şema tipleri) referans. İsteğe bağlıdır.
    *   `IsCollection` (bool, `XmlAttribute`, Varsayılan: `false`): Bu tanımın tek bir öğe yerine bir öğe koleksiyonunu mu temsil ettiğini belirtir.
    *   `ItemKind` (`ItemKind` enum, `XmlAttribute`, Varsayılan: `Information`): Öğenin türünü belirtir (`Information` veya `Physical`).
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Yapı referansını tutmak için.
    *   `Streamline.Domain.Schema.Common.ItemKind`: Öğe türünü belirten enum (Bu enum'un `Common` altında olması beklenir).
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
    *   `Streamline.Domain.Schema.Common.Import` (Dış yapıları tanımlamak için kullanılabilir)
*   **Önemli Noktalar:**
    *   Süreç verilerinin tip güvenliğini sağlamak ve veri eşleştirmelerini (data mapping) kolaylaştırmak için önemlidir.
    *   Veri odaklı süreç modellemede merkezi bir rol oynar.
    *   Referans verdiği yapı (`structureRef`) genellikle BPMN motoru veya araç tarafından yorumlanabilmelidir.

## XML Örneği

`<itemDefinition>` tanımları `<definitions>` altında yer alır:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:xsd="http://www.w3.org/2001/XMLSchema"
             xmlns:order="http://example.com/orderSchema"
             targetNamespace="http://example.com/process">

  <!-- Dış XSD içe aktarma -->
  <import importType="http://www.w3.org/2001/XMLSchema" 
          location="order.xsd" 
          namespace="http://example.com/orderSchema"/>

  <!-- Basit tip ItemDefinition (XML Schema tipi referansı) -->
  <itemDefinition id="ItemDef_CustomerID" name="Customer ID" 
                  structureRef="xsd:string" 
                  itemKind="Information" 
                  isCollection="false"/>

  <!-- İçe aktarılan karmaşık tipe referans veren ItemDefinition -->
  <itemDefinition id="ItemDef_OrderDetails" name="Order Details" 
                  structureRef="order:OrderType" 
                  itemKind="Information"/> <!-- isCollection varsayılan (false) -->

  <!-- Koleksiyon ItemDefinition -->
  <itemDefinition id="ItemDef_OrderItemsList" name="List of Order Items" 
                  structureRef="order:OrderItemType" 
                  isCollection="true"/>

  <!-- Belirli bir yapıya referans vermeyen ItemDefinition (tip belirsiz) -->
  <itemDefinition id="ItemDef_Status" name="Processing Status"/>

  <!-- Bu tanımları kullanan bir Process -->
  <process id="Proc_OrderHandling" name="Handle Order">
    <property id="Prop_CustID" name="customerIdentifier" itemSubjectRef="ItemDef_CustomerID"/>
    <dataObject id="DataObj_CurrentOrder" name="Current Order Data" itemSubjectRef="ItemDef_OrderDetails"/>
    <dataObject id="DataObj_Items" name="Order Items" itemSubjectRef="ItemDef_OrderItemsList"/>
    <!-- ... -->
  </process>

  <!-- Bu tanımları kullanan bir Message -->
  <message id="Msg_OrderConfirmation" name="Order Confirmation Message" itemRef="ItemDef_OrderDetails"/>

</definitions>
``` 