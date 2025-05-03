# CallableElement

*   **BPMN Tipi:** Soyut (Abstract) - `RootElement`'ten türemiştir.
*   **Amaç:** Başka bir süreç tanımından (`CallActivity` kullanarak) çağrılabilen, tekrar kullanılabilir iş mantığı birimleri için temel sınıftır. Bu elemanlar genellikle kendi giriş ve çıkış veri tanımlarına sahiptir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu soyut sınıf, `Process` ve `GlobalTask` (ve onun alt türleri: `GlobalUserTask`, `GlobalScriptTask`, `GlobalBusinessRuleTask`, `GlobalManualTask`) tarafından miras alınır.
    *   Bir `CallActivity` tarafından `calledElement` özelliği ile referans verildiğinde, bu `CallableElement` tarafından tanımlanan iş mantığı yürütülür.
    *   Çağıran ve çağrılan eleman arasındaki veri akışını yönetmek için `InputOutputSpecification` ve `InputOutputBinding` elemanlarını içerebilir.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`): Çağrılabilir elemanın adı.
    *   `SupportedInterfaceRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu elemanın sunduğu veya gerektirdiği `Interface` tanımlarına (genellikle web servisleri gibi dış etkileşimler için) referanslar.
    *   `IoSpecification` (`InputOutputSpecification`, `XmlElement`): Bu elemanın çalışması için gereken giriş verilerini (`DataInput`) ve çalışması sonucunda üreteceği çıkış verilerini (`DataOutput`) tanımlayan spesifikasyon.
    *   `IoBinding` (Collection<`InputOutputBinding`>, `XmlElement`): Çağıran `CallActivity`'nin veri nesneleri ile bu `CallableElement`'in `IoSpecification`'ında tanımlanan giriş/çıkışlar arasındaki eşleştirmeyi (veri aktarımını) tanımlayan bağlamalar.
    *   (`RootElement` ve `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `System.Xml.XmlQualifiedName`: Arayüz ve diğer referanslar için.
    *   `Streamline.Domain.Schema.Common.InputOutputSpecification`: Girdi/Çıktı spesifikasyonları için.
    *   `Streamline.Domain.Schema.Common.InputOutputBinding`: Girdi/Çıktı bağlamaları için.
    *   `Streamline.Domain.Schema.Common.Interface` (dolaylı): `SupportedInterfaceRef` ile ilişkilidir.
    *   `Streamline.Domain.Schema.Common.RootElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Tekrar kullanılabilir süreç parçalarını veya görevleri modellemek için temel oluşturur.
    *   `CallActivity`, `calledElement` özelliği ile bir `CallableElement`'in ID'sine referans vererek onu çağırır.
    *   Veri giriş/çıkış yönetimi için `IoSpecification` ve `IoBinding` önemlidir.
    *   Soyut olduğu için doğrudan örneği oluşturulamaz.

## XML Örneği

`CallableElement` soyut olduğundan doğrudan XML'de bulunmaz. Ancak, ondan türeyen somut elemanlar (`Process`, `GlobalTask` alt türleri) bu özellikleri kullanır ve `CallActivity` tarafından çağrılabilir.

```xml
<definitions ...>

  <!-- CallableElement'ten türeyen Process -->
  <process id="Proc_ReusableSub" name="Reusable Sub-Process">
    <ioSpecification>
      <dataInput id="SubInput_ID" name="InputID" itemSubjectRef="xsd:string"/>
      <dataOutput id="SubOutput_Result" name="SubProcessResult" itemSubjectRef="xsd:string"/>
      <inputSet><dataInputRefs>SubInput_ID</dataInputRefs></inputSet>
      <outputSet><dataOutputRefs>SubOutput_Result</dataOutputRefs></outputSet>
    </ioSpecification>
    <!-- Sürecin iç akışı ... -->
     <startEvent/>
     <scriptTask id="Task_SubProc" name="Do work in sub-process">
       <script>SubProcessResult = "Processed: " + InputID;</script>
     </scriptTask>
     <endEvent/>
     <sequenceFlow sourceRef="StartSub" targetRef="Task_SubProc"/>
     <sequenceFlow sourceRef="Task_SubProc" targetRef="EndSub"/>
  </process>

  <!-- CallableElement'ten türeyen GlobalUserTask -->
  <globalUserTask id="GlobalTask_Approval" name="Standard Approval Task">
    <ioSpecification>
       <dataInput id="GTaskInput_Request" name="ApprovalRequest"/>
       <dataOutput id="GTaskOutput_Decision" name="ApprovalDecision"/>
       <inputSet><dataInputRefs>GTaskInput_Request</dataInputRefs></inputSet>
       <outputSet><dataOutputRefs>GTaskOutput_Decision</dataOutputRefs></outputSet>
    </ioSpecification>
    <potentialOwner>...</potentialOwner>
    <!-- Rendering vb. -->
  </globalUserTask>

  <!-- Ana Süreç -->
  <process id="Proc_Main" name="Main Process" isExecutable="true">
     <property id="Prop_MainID" name="mainID" itemSubjectRef="xsd:string"/>
     <property id="Prop_SubResult" name="subResult" itemSubjectRef="xsd:string"/>
     <property id="Prop_ApprovalReq" name="approvalReq"/>
     <property id="Prop_ApprovalDecision" name="approvalDecision"/>

    <!-- Proc_ReusableSub'ı çağıran CallActivity -->
    <callActivity id="CallSubProcess" name="Call Reusable Sub" calledElement="Proc_ReusableSub">
      <ioSpecification>
         <dataInput id="MainInput_ID" name="InputID" itemSubjectRef="xsd:string"/>
         <dataOutput id="MainOutput_Result" name="SubProcessResult" itemSubjectRef="xsd:string"/>
         <inputSet><dataInputRefs>MainInput_ID</dataInputRefs></inputSet>
         <outputSet><dataOutputRefs>MainOutput_Result</dataOutputRefs></outputSet>
      </ioSpecification>
      <dataInputAssociation>
        <sourceRef>Prop_MainID</sourceRef>
        <targetRef>MainInput_ID</targetRef>
      </dataInputAssociation>
      <dataOutputAssociation>
        <sourceRef>MainOutput_Result</sourceRef>
        <targetRef>Prop_SubResult</targetRef>
      </dataOutputAssociation>
    </callActivity>

     <!-- GlobalTask_Approval'ı çağıran CallActivity -->
    <callActivity id="CallGlobalTask" name="Call Approval Task" calledElement="GlobalTask_Approval">
       <ioSpecification>
          <dataInput id="MainInput_ApprovalReq" name="ApprovalRequest"/>
          <dataOutput id="MainOutput_ApprovalDecision" name="ApprovalDecision"/>
          <inputSet><dataInputRefs>MainInput_ApprovalReq</dataInputRefs></inputSet>
          <outputSet><dataOutputRefs>MainOutput_ApprovalDecision</dataOutputRefs></outputSet>
       </ioSpecification>
      <dataInputAssociation>
        <sourceRef>Prop_ApprovalReq</sourceRef>
        <targetRef>MainInput_ApprovalReq</targetRef>
      </dataInputAssociation>
       <dataOutputAssociation>
        <sourceRef>MainOutput_ApprovalDecision</sourceRef>
        <targetRef>Prop_ApprovalDecision</targetRef>
      </dataOutputAssociation>
    </callActivity>

    <!-- Diğer elemanlar -->
  </process>

</definitions>
```

Bu örnekte hem `Proc_ReusableSub` (`Process` olarak) hem de `GlobalTask_Approval` (`GlobalUserTask` olarak) `CallableElement`'ten türemiştir ve `ioSpecification` içerirler. `Proc_Main` içindeki `callActivity` elemanları, `calledElement` özniteliği ile bu çağrılabilir elemanlara referans verir ve `dataInputAssociation`/`dataOutputAssociation` ile veri aktarımını sağlar. 