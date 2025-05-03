# StandardLoopCharacteristics

*   **BPMN Tipi:** Somut (Concrete) - `LoopCharacteristics`
*   **Amaç:** Bir `Activity`'nin (genellikle `Task` veya `SubProcess`) basit bir döngü içinde tekrarlanacağını belirtir. Döngü, belirli bir sayıda veya bir koşul sağlanana kadar devam edebilir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `LoopCharacteristics` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Activity`'nin `<standardLoopCharacteristics>` alt etiketi olarak bulunur.
    *   Diyagramlarda genellikle aktivitenin altında bir döngü oku simgesi ile gösterilir.
    *   Döngünün ne zaman biteceği `loopMaximum` (maksimum tekrar sayısı) veya `loopCondition` (devam etme/bitme koşulu) ile belirlenir.
    *   `testBefore` özniteliği, `loopCondition`'ın döngünün başında mı (`true`, while döngüsü gibi) yoksa sonunda mı (`false`, do-while döngüsü gibi) kontrol edileceğini belirler.
*   **Özellikler:**
    *   `LoopCondition` (`Expression`, `XmlElement`, İsteğe Bağlı): Döngünün devam etme koşulunu tanımlayan ifade. Koşulun nasıl yorumlanacağı (`testBefore`'a bağlıdır).
    *   `TestBefore` (bool, `XmlAttribute`, Varsayılan: `false`): Döngü koşulunun (`LoopCondition`) aktivite yürütülmeden *önce* mi (`true`) yoksa *sonra* mı (`false`) test edileceğini belirtir.
        *   `TestBefore = true`: Koşul doğru olduğu sürece döngü devam eder (while).
        *   `TestBefore = false`: Aktivite en az bir kez çalışır, sonra koşul doğru olduğu sürece döngü devam eder (do-while).
    *   `LoopMaximum` (string, `XmlAttribute`, İsteğe Bağlı): Döngünün maksimum kaç kez yürütüleceğini belirten bir sayı veya ifade. (Not: Bu alanın tipi `Expression` olması beklenirken `string` olarak tanımlanmış.)
    *   (`LoopCharacteristics` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.LoopCharacteristics` (Miras aldığı sınıf)
    *   `Streamline.Domain.Schema.Common.Expression`
    *   `Streamline.Domain.Schema.Activities.Activity` (Bu elemanı içerir)
*   **Önemli Noktalar:**
    *   `MultiInstanceLoopCharacteristics`'ten farklı olarak, aktivite her seferinde aynı bağlamda tekrarlanır, paralel örnekler oluşturulmaz.
    *   Döngü sayacını veya koşulun durumunu yönetmek genellikle süreç değişkenleri ve ifadelerle yapılır.

## XML Örneği

`<standardLoopCharacteristics>` bir aktivitenin içinde tanımlanır:

### Koşul ile Döngü (While Döngüsü - TestBefore=true)
```xml
<process ...>
  <property id="Prop_Counter" name="loopCounter" itemSubjectRef="xsd:int"/>
  <scriptTask id="Task_InitializeCounter" name="Init Counter">
    <script>loopCounter = 0;</script>
  </scriptTask>
  <userTask id="Task_ProcessItem" name="Process Item">
    <standardLoopCharacteristics testBefore="true">
       <loopCondition xsi:type="tFormalExpression">${loopCounter < 5}</loopCondition>
    </standardLoopCharacteristics>
     <!-- Görev içinde sayaç artırılabilir (örn. bir form ile veya DataOutputAssociation ile) -->
     <!-- Örnek: <dataOutputAssociation> ... loopCounter = loopCounter + 1 ... </dataOutputAssociation> -->
  </userTask>
  <!-- ... -->
</process>
```

### Maksimum Tekrar Sayısı ile Döngü
```xml
<process ...>
  <serviceTask id="Task_RetrySend" name="Retry Sending Data">
    <standardLoopCharacteristics loopMaximum="3" /> <!-- En fazla 3 kez dener -->
  </serviceTask>
  <!-- ... -->
</process>
```

### Koşul ile Döngü (Do-While Döngüsü - TestBefore=false)
```xml
<process ...>
   <property id="Prop_Status" name="itemStatus" itemSubjectRef="xsd:string"/>
   <userTask id="Task_ReviewStatus" name="Review Item Status">
     <standardLoopCharacteristics testBefore="false">
       <loopCondition xsi:type="tFormalExpression">${itemStatus != "Completed"}</loopCondition>
     </standardLoopCharacteristics>
     <!-- Görev, itemStatus'u güncelleyebilir -->
   </userTask>
  <!-- ... -->
</process>
``` 