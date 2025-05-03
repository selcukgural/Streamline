# Signal Sınıfı

`Signal` sınıfı, BPMN 2.0 standardında tanımlanan bir sinyali temsil eder. Sinyaller, süreçler arasında veya bir süreç içindeki farklı noktalar arasında iletişim kurmak için kullanılır. Genellikle bir olay tarafından yayınlanır ve başka bir olay tarafından yakalanır.

`Signal` sınıfı, `RootElement` sınıfından türetilmiştir.

## Özellikler

| Özellik Adı     | Tip                         | Açıklama                                                            | XML Özniteliği |
| --------------- | --------------------------- | ------------------------------------------------------------------- | -------------- |
| `Name`          | `string`                    | Sinyalin adını belirtir.                                            | `name`         |
| `StructureRef`  | `System.Xml.XmlQualifiedName` | Sinyalin taşıdığı verinin yapısını tanımlayan bir öğeye referans. | `structureRef` |

## XML Temsili

`Signal` sınıfı, BPMN XML şemasında `<signal>` öğesi olarak temsil edilir. `name` ve `structureRef` özellikleri XML öznitelikleri olarak eşlenir.

```xml
<signal id="signal_1" name="MySignal" structureRef="ns:MyStructure" />
```

## Kullanım

Sinyaller genellikle `SignalEventDefinition` ile birlikte kullanılır. Bir sinyal olayı (örneğin, `intermediateCatchEvent` veya `startEvent`), belirli bir `Signal`'e referans vererek hangi sinyali yakalayacağını veya fırlatacağını belirtir.

```csharp
// Örnek Signal nesnesi oluşturma
var mySignal = new Signal
{
    Id = "signal_1",
    Name = "OrderReceivedSignal",
    StructureRef = new System.Xml.XmlQualifiedName("payloadStructure", "http://example.com/ns")
};
```

## Öznitelikler

Sınıf, aşağıdaki .NET öznitelikleri ile işaretlenmiştir:

- `[Serializable]`: Sınıfın serileştirilebilir olduğunu gösterir.
- `[XmlTypeAttribute("tSignal", Namespace="...")]`: XML serileştirmesi için tip bilgilerini belirtir.
- `[XmlRootAttribute("signal", Namespace="...")]`: XML serileştirmesi sırasında kök öğe adını ve isim alanını belirtir.
- `[GeneratedCodeAttribute(...)]`: Kodun bir araç tarafından otomatik olarak üretildiğini belirtir.
- `[DebuggerStepThroughAttribute]`: Hata ayıklayıcının bu sınıfa adım atmamasını sağlar.
- `[DesignerCategoryAttribute("code")]`: Tasarımcı araçları için kategori belirtir.

## XML Örneği

Bir sinyal tanımı genellikle `<definitions>` öğesi altında yer alır:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:tns="http://example.com/process"
             xmlns:data="http://example.com/orderdata"
             targetNamespace="http://example.com/process">

  <import importType="http://www.w3.org/2001/XMLSchema" 
          location="orderdata.xsd" 
          namespace="http://example.com/orderdata"/> 

  <itemDefinition id="ItemDef_OrderData" structureRef="data:OrderType"/>

  <signal id="Signal_OrderReceived" name="Order Received" structureRef="ItemDef_OrderData" />

  <!-- Diğer BPMN Elemanları (Process, Events vb.) -->

</definitions>
``` 