# Script

*   **BPMN Tipi:** Yardımcı Eleman
*   **Amaç:** Bir `ScriptTask` tarafından yürütülecek olan betik (script) kodunu içerir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** Yok
*   **Uygulama & Davranış:**
    *   Bir `ScriptTask` elemanının `<script>` alt etiketi olarak XML'de yer alır.
    *   Betiğin metin içeriğini (`Text`) tutar.
    *   Betiğin hangi dilde yazıldığı (`scriptFormat` özniteliği) bu elemanla değil, üst öğesi olan `ScriptTask` ile tanımlanır.
*   **Özellikler:**
    *   `Text` (string[], `XmlText`): Çalıştırılacak betik kodunun metin içeriği.
    *   `Any` (`XmlElement`, `XmlAnyElement`): Alternatif olarak, betiği XML elemanları olarak tanımlamak için kullanılabilir (yaygın değil).
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Activities.ScriptTask` (Bu elemanı içerir)
*   **Önemli Noktalar:**
    *   Süreç içinde otomatik olarak küçük kod parçacıklarının çalıştırılmasını sağlar.
    *   Betiğin içeriği ve desteklenen diller (`scriptFormat`) süreç motoruna bağlıdır.

## XML Örneği

`<script>` elemanı bir `<scriptTask>` içinde kullanılır:

```xml
<process id="Process_ScriptExample" isExecutable="true">
  <!-- ... -->
  <scriptTask id="Task_CalculateTotal" name="Calculate Order Total" scriptFormat="text/groovy">
    <incoming>...</incoming>
    <outgoing>...</outgoing>
    <ioSpecification>
      <dataInput id="Input_Price" name="itemPrice" itemSubjectRef="xsd:double"/>
      <dataInput id="Input_Quantity" name="quantity" itemSubjectRef="xsd:int"/>
      <dataOutput id="Output_Total" name="orderTotal" itemSubjectRef="xsd:double"/>
      <inputSet><dataInputRefs>Input_Price</dataInputRefs><dataInputRefs>Input_Quantity</dataInputRefs></inputSet>
      <outputSet><dataOutputRefs>Output_Total</dataOutputRefs></outputSet>
    </ioSpecification>
    <dataInputAssociation>
      <sourceRef>Prop_Price</sourceRef> <!-- Process property -->
      <targetRef>Input_Price</targetRef>
    </dataInputAssociation>
     <dataInputAssociation>
      <sourceRef>Prop_Qty</sourceRef> <!-- Process property -->
      <targetRef>Input_Quantity</targetRef>
    </dataInputAssociation>
    <dataOutputAssociation>
      <sourceRef>Output_Total</sourceRef>
      <targetRef>Prop_Total</targetRef> <!-- Process property -->
    </dataOutputAssociation>

    <script>
      // Groovy script example
      orderTotal = itemPrice * quantity;
      println("Calculated Total: " + orderTotal);
    </script>

  </scriptTask>
  <!-- ... -->
</process>
``` 