# FormalExpression

*   **BPMN Tipi:** Somut (Concrete) - `Expression`
*   **Amaç:** Belirli bir ifade dilinde (örn. XPath, C#, Java, Groovy, JUEL) yazılmış, genellikle bir süreç motoru tarafından değerlendirilebilen bir ifadeyi temsil eder. Koşulları, veri atamalarını, betikleri, zamanlayıcıları vb. tanımlamak için yaygın olarak kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `Expression` -> `BaseElementWithMixedContent` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `SequenceFlow`'ların `conditionExpression`'ı, `ScriptTask`'ın `script`'i, `TimerEventDefinition`'ın zaman ifadesi (`timeDate`, `timeDuration`, `timeCycle`), `Assignment`'ın `from` veya `to`'su, `CorrelationPropertyBinding`'in `dataPath`'i, `CorrelationPropertyRetrievalExpression`'ın `messagePath`'i gibi birçok yerde kullanılır.
    *   İçeriği (`Text`) ifadenin kendisini içerir.
    *   `language` özniteliği, ifadenin hangi dilde yazıldığını belirtir, bu da motorun ifadeyi doğru şekilde yorumlamasını sağlar.
    *   `evaluatesToTypeRef` özniteliği, ifadenin sonucunun hangi veri tipinde olacağını belirtir.
*   **Özellikler:**
    *   `Language` (string, `XmlAttribute`): İfadenin yazıldığı dilin URI'si (örn. `http://www.w3.org/1999/XPath`, `http://www.omg.org/spec/FEEL/20140401`, `C#`, `Java`). Eğer belirtilmezse, varsayılan ifade dili (genellikle süreç motoru tarafından belirlenir) kullanılır.
    *   `EvaluatesToTypeRef` (`XmlQualifiedName`, `XmlAttribute`): İfadenin değerlendirilmesi sonucunda dönecek olan değerin veri tipine (`ItemDefinition` ID'sine) bir referans (isteğe bağlı).
    *   İçerik (`Text` veya `Any`, `Expression`'dan miras): Değerlendirilecek olan ifadenin metni.
    *   (`Expression`, `BaseElementWithMixedContent`, `BaseElement`'ten ilgili özellikleri miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.ItemDefinition` (dolaylı, `evaluatesToTypeRef` yoluyla)
    *   `Streamline.Domain.Schema.Common.Expression` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   BPMN modellerindeki dinamik davranışların ve veri manipülasyonlarının çoğunu tanımlamak için kullanılan temel mekanizmadır.
    *   Desteklenen diller ve ifadelerin nasıl değerlendirildiği, kullanılan süreç motoruna bağlıdır.

## XML Örneği

`FormalExpression` genellikle diğer BPMN elemanlarının içinde kullanılır:

### Sıra Akışı Koşulu (SequenceFlow Condition)
```xml
<sequenceFlow id="Flow_ApprovePath" sourceRef="Gateway_Decision" targetRef="Task_NotifyApproved">
  <conditionExpression xsi:type="tFormalExpression" language="http://www.omg.org/spec/FEEL/20140401">
    amount &lt;= 1000 and customerStatus = "Gold"
  </conditionExpression>
</sequenceFlow>
```

### Zamanlayıcı Tanımı (Timer Event Definition - Cycle)
```xml
<timerEventDefinition id="TimerDef_NightlyBatch">
  <timeCycle xsi:type="tFormalExpression" language="http://www.cron.com/notation">0 0 * * *</timeCycle> <!-- Her gece yarısı -->
</timerEventDefinition>
```

### Veri Atama (Data Assignment - ResourceAssignmentExpression)
```xml
<potentialOwner>
  <resourceAssignmentExpression>
    <formalExpression language="C#">"user(" + managerUsername + ")"</formalExpression>
  </resourceAssignmentExpression>
</potentialOwner>
```

### Korelasyon Veri Yolu (CorrelationPropertyBinding)
```xml
<correlationPropertyBinding correlationPropertyRef="Prop_InvoiceNumber">
  <dataPath xsi:type="tFormalExpression" language="http://www.w3.org/1999/XPath">/invoice/header/number</dataPath>
</correlationPropertyBinding>
``` 