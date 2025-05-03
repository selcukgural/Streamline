# Task (Soyut/Temel Sınıf)

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Activity`

**BPMN Elemanı:** `tTask`

## Açıklama

`Task`, BPMN 2.0 spesifikasyonunda atomik bir iş birimini temsil eden temel sınıftır. Bir Görev (Task), süreç içinde daha fazla ayrıştırılamayan bir çalışmadır.

Bu sınıf, tüm spesifik görev türleri (`UserTask`, `ServiceTask`, `ScriptTask`, `BusinessRuleTask`, `SendTask`, `ReceiveTask`, `ManualTask`) için ortak bir temel sağlar. `Activity` sınıfından miras aldığı için bir aktivitenin tüm temel özelliklerine (I/O, döngü karakteristikleri, telafi vb.) sahiptir.

Bu sınıfın kendisi genellikle doğrudan kullanılmaz; bunun yerine, süreç modelindeki belirli görev türlerine karşılık gelen türetilmiş sınıflar (`UserTask`, `ServiceTask` vb.) kullanılır.

Bu sınıf, BPMN XML şemasındaki `tTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

Bu sınıf, temel sınıfı olan `Activity`'den tüm özellikleri miras alır. Kendi içinde ek özellik tanımlamaz.

## Türetilmiş Sınıflar

BPMN'deki tüm atomik görev türleri bu sınıftan (veya birbirlerinden) türetilmiştir:
*   `BusinessRuleTask`
*   `ManualTask`
*   `ReceiveTask`
*   `ScriptTask`
*   `SendTask`
*   `ServiceTask`
*   `UserTask`

## Kullanım

Süreç motoru, BPMN modelini ayrıştırırken `userTask`, `serviceTask` gibi etiketlerle karşılaştığında, bu sınıftan türeyen ilgili somut sınıfın (`UserTask`, `ServiceTask`) bir örneğini oluşturur.

## XML Örneği

`Task` soyut bir temel olduğu için doğrudan `<task>` olarak kullanımı nadirdir. Genellikle spesifik alt türleri kullanılır. Ancak teorik bir örnek şöyle olabilir:

```xml
<process id="Process_Example" isExecutable="true">
  <startEvent id="Start"/>
  <sequenceFlow id="Flow1" sourceRef="Start" targetRef="AbstractTask"/>

  <!-- Genellikle <userTask>, <serviceTask> vb. kullanılır -->
  <task id="AbstractTask" name="Perform Abstract Work">
    <incoming>Flow1</incoming>
    <outgoing>Flow2</outgoing>
    <!-- <ioSpecification>, <dataInputAssociation> vb. -->
  </task>

  <sequenceFlow id="Flow2" sourceRef="AbstractTask" targetRef="End"/>
  <endEvent id="End"/>
</process>
```

**Örnek (Spesifik Alt Tür - UserTask):**

```xml
<userTask id="UserTask_ReviewDoc" name="Review Document">
  <incoming>Flow_ToReview</incoming>
  <outgoing>Flow_FromReview</outgoing>
  <potentialOwner>
    <resourceAssignmentExpression>
      <formalExpression>user(john)</formalExpression>
    </resourceAssignmentExpression>
  </potentialOwner>
</userTask>
``` 