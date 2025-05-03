# SubChoreography

*   **BPMN Tipi:** Somut (Concrete) - `ChoreographyActivity`
*   **Amaç:** Koreografi diyagramları içinde, kendi içinde kapalı ve tekrar kullanılabilir bir etkileşimler dizisini (alt koreografiyi) temsil eder. Katılımcılar arasındaki bir dizi koreografi görevini gruplar.
*   **Konum:** `Streamline.Domain.Schema.Common` (Mantıksal olarak Koreografi ile ilgili olsa da)
*   **Miras:** `ChoreographyActivity` -> `FlowNode` -> `FlowElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   BPMN Koreografi diyagramlarında kullanılır.
    *   Bir veya daha fazla katılımcı arasındaki bir dizi etkileşimi (genellikle `ChoreographyTask`'lar) içerir.
    *   Diyagramlarda karmaşıklığı yönetmek için daraltılmış (genişletme işaretiyle) veya genişletilmiş (içeriğini gösteren) olarak gösterilebilir.
    *   İçerdiği `FlowElement`'ler (görevler, olaylar, ağ geçitleri, akışlar) alt koreografinin detaylı akışını tanımlar.
    *   Bir `CallChoreography` elemanı tarafından çağrılarak farklı koreografi modellerinde yeniden kullanılabilir.
*   **Özellikler:**
    *   `FlowElement` (`Collection<FlowElement>`, `XmlElement`, İsteğe Bağlı): Alt koreografinin iç yapısını oluşturan akış elemanlarının (örneğin, `ChoreographyTask`, `SubChoreography`, `Event`, `Gateway`, `SequenceFlow`) koleksiyonu.
    *   `Artifact` (`Collection<Artifact>`, `XmlElement`, İsteğe Bağlı): Alt koreografi ile ilişkili yapıtların (örneğin, `Association`, `Group`, `TextAnnotation`) koleksiyonu.
    *   (`ChoreographyActivity`, `FlowNode`, `FlowElement`, `BaseElement`'ten ilgili özellikleri miras alır. Özellikle `ChoreographyActivity`'den `ParticipantRef` (katılımcılar), `InitiatingParticipantRef` (başlatan katılımcı), `MessageFlowRef`, `CorrelationKey` önemlidir.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.ChoreographyActivity` (Miras aldığı sınıf)
    *   `Streamline.Domain.Schema.Flow.FlowElement` (İçerdiği elemanların temel tipi)
    *   `Streamline.Domain.Schema.Artifacts.Artifact` (İçerdiği yapıtların temel tipi)
    *   Çeşitli `FlowElement` ve `Artifact` alt sınıfları (örneğin, `ChoreographyTask`, `SequenceFlow`, `Event`, `Gateway`, `Association` vb.)
*   **Önemli Noktalar:**
    *   Orchestration'daki `SubProcess`'in koreografideki karşılığıdır; süreç adımlarını değil, katılımcı etkileşimlerini gruplar.
    *   Karmaşık koreografileri modüler hale getirmek için kullanılır.

## XML Örneği

```xml
<definitions ...>
  <choreography id="MainChoreography">
    <participant id="Participant_A" name="Participant A"/>
    <participant id="Participant_B" name="Participant B"/>
    <messageFlow id="MessageFlow_1" sourceRef="Participant_A" targetRef="Participant_B"/>
    <messageFlow id="MessageFlow_2" sourceRef="Participant_B" targetRef="Participant_A"/>

    <!-- Başlangıç Olayı -->
    <startEvent id="StartChoreo"/>

    <!-- Alt Koreografi -->
    <subChoreography id="SubChoreo_1" name="Detailed Interaction">
      <participantRef>Participant_A</participantRef>
      <participantRef>Participant_B</participantRef>
      <messageFlowRef>MessageFlow_1</messageFlowRef>
      <messageFlowRef>MessageFlow_2</messageFlowRef>
      <!-- Alt koreografi içindeki FlowElement'ler (ChoreographyTask, Gateway vb.) -->
      <choreographyTask id="ChoreoTask_1" name="Task 1" initiatingParticipantRef="Participant_A">
        <participantRef>Participant_A</participantRef>
        <participantRef>Participant_B</participantRef>
        <messageFlowRef>MessageFlow_1</messageFlowRef>
      </choreographyTask>
      <choreographyTask id="ChoreoTask_2" name="Task 2" initiatingParticipantRef="Participant_B">
        <participantRef>Participant_B</participantRef>
        <participantRef>Participant_A</participantRef>
        <messageFlowRef>MessageFlow_2</messageFlowRef>
      </choreographyTask>
      <sequenceFlow id="SeqFlow_Sub1" sourceRef="ChoreoTask_1" targetRef="ChoreoTask_2"/>
    </subChoreography>

    <!-- Bitiş Olayı -->
    <endEvent id="EndChoreo"/>

    <!-- Ana akış -->
    <sequenceFlow id="SeqFlow_Main1" sourceRef="StartChoreo" targetRef="SubChoreo_1"/>
    <sequenceFlow id="SeqFlow_Main2" sourceRef="SubChoreo_1" targetRef="EndChoreo"/>

  </choreography>
</definitions>
``` 