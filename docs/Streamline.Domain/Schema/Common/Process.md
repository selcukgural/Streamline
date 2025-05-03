# Process

*   **BPMN Tipi:** Somut (Concrete) - `CallableElement`, `RootElement`
*   **Amaç:** Belirli bir başlangıç olayından (`StartEvent`) başlayıp bir veya daha fazla bitiş olayında (`EndEvent`) sona eren, sıralı veya paralel adımlardan (Aktiviteler, Geçitler, Olaylar) oluşan bir iş akışını tanımlayan ana kapsayıcıdır. Bir iş sürecinin tüm mantığını, akışını, verilerini ve kaynaklarını içerir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `CallableElement` -> `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   `Definitions` elemanının altında bir `RootElement` olarak tanımlanır.
    *   Bir `Process` genellikle şunları içerir:
        *   **Akış Elemanları (`FlowElement`):** Sürecin adımlarını ve akışını tanımlayan Olaylar, Aktiviteler, Geçitler ve `SequenceFlow`'lar.
        *   **Veri Elemanları:** Süreç içinde kullanılan `DataObject`, `DataObjectReference`, `DataStoreReference` ve süreç değişkenlerini tanımlayan `Property` elemanları.
        *   **Kulvarlar (`LaneSet`, `Lane`):** Akış elemanlarını rol veya sorumluluk bazında gruplandıran yapılar.
        *   **Kaynak Rolleri (`ResourceRole`):** Süreçteki aktiviteleri kimin (`Performer`, `HumanPerformer`, `PotentialOwner`) yapacağını tanımlar.
        *   **Artifact'lar:** Açıklamalar (`TextAnnotation`), gruplamalar (`Group`) ve veri ilişkileri (`Association`).
        *   **Korelasyon (`CorrelationSubscription`):** Sürecin dış mesajları belirli iş bağlamlarına göre nasıl alacağını tanımlar.
    *   `isExecutable="true"` olarak işaretlenmişse, bir BPMN motoru tarafından yürütülebilir.
    *   `CallableElement` olduğu için, başka bir süreçteki `CallActivity` tarafından çağrılabilir.
    *   Bir `Participant` (Havuz), `processRef` özniteliği ile bu `Process` tanımını referans alabilir.
*   **Özellikler:**
    *   `ProcessType` (`ProcessType` enum, `XmlAttribute`, Varsayılan: `None`): Sürecin türünü belirtir (`None`, `Public`, `Private`).
    *   `IsClosed` (bool, `XmlAttribute`, Varsayılan: `false`): Sürecin (genellikle Public ise) bir işbirliği içinde gösterildiğinde iç detaylarının gizlenip gizlenmeyeceğini belirtir.
    *   `IsExecutable` (bool, `XmlAttribute`): Sürecin bir motor tarafından yürütülmek üzere tasarlanıp tasarlanmadığını belirtir.
    *   `DefinitionalCollaborationRef` (`XmlQualifiedName`, `XmlAttribute`): Bu sürecin tanımının yapıldığı (definitional) `Collaboration`'a referans (isteğe bağlı).
    *   `Auditing` (`Auditing`, `XmlElement`): Süreç geneli için denetim yapılandırması.
    *   `Monitoring` (`Monitoring`, `XmlElement`): Süreç geneli için izleme yapılandırması.
    *   `Property` (Collection<`Property`>, `XmlElement`): Süreç kapsamındaki veri değişkenleri.
    *   `LaneSet` (Collection<`LaneSet`>, `XmlElement`): Sürecin kulvar setleri.
    *   `FlowElement` (Collection<`FlowElement`>, `XmlElement`): Sürecin akış elemanları (Olaylar, Aktiviteler, Geçitler, SequenceFlow, Veri Nesneleri vb.).
    *   `Artifact` (Collection<`Artifact`>, `XmlElement`): Süreçle ilişkili Artifact'ler (Association, Group, TextAnnotation).
    *   `ResourceRole` (Collection<`ResourceRole`>, `XmlElement`): Süreçle ilişkili kaynak rolleri (Performer, PotentialOwner vb.).
    *   `CorrelationSubscription` (Collection<`CorrelationSubscription`>, `XmlElement`): Sürecin mesaj abonelikleri.
    *   `Supports` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu sürecin desteklediği diğer süreçlere referanslar.
    *   (`CallableElement`, `RootElement`, `BaseElement`'ten ilgili özellikleri miras alır: `Name`, `SupportedInterfaceRef`, `IoSpecification`, `IoBinding`, `Id`, `Documentation`, `ExtensionElements` vb.)
*   **Bağımlılıklar:**
    *   Neredeyse tüm diğer BPMN şema elemanları (`FlowElement` alt türleri, `Data` alt türleri, `Common` alt türleri vb.)
    *   `Streamline.Domain.Schema.Common.CallableElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   BPMN modellemesinin merkezindedir.
    *   Bir işin veya görevin başlangıçtan bitişe nasıl aktığını tüm detaylarıyla tanımlar.

    *   **BPMN Tipi:** Somut (Concrete) - `CallableElement`, `RootElement`
    *   **Amaç:** Belirli bir başlangıç olayından (`StartEvent`) başlayıp bir veya daha fazla bitiş olayında (`EndEvent`) sona eren bir dizi aktivite ve akışın tanımlandığı ana yapıdır. Bir iş sürecinin mantığını ve akışını içerir.
    *   **Konum:** `Streamline.Domain.Schema.Common` (Spesifikasyonda RootElement altında, bu projede Common altına konmuş olabilir)
    *   **Miras:** `CallableElement` -> `RootElement` -> `BaseElement`
    *   **Uygulama & Davranış:**
        *   `Definitions` elemanının altında bir `RootElement` olarak tanımlanır.
        *   Bir süreç, `FlowElement`'leri (Olaylar, Aktiviteler, Gateway'ler, SequenceFlow'lar, Veri Nesneleri), `Artifact`'ları (Açıklamalar, Gruplar) ve `LaneSet`'leri (Kulvarlar) içerir.
        *   Sürecin yürütülebilir olup olmadığını (`IsExecutable`), türünü (`ProcessType`) ve diğer süreçlerle olan ilişkilerini (`Supports`, `DefinitionalCollaborationRef`) belirten özelliklere sahiptir.
        *   Kaynak rollerini (`ResourceRole`) ve korelasyon aboneliklerini (`CorrelationSubscription`) tanımlayabilir.
    *   **Özellikler:**
        *   `Name` (string, `XmlAttribute`, Miras): Sürecin adı.
        *   `IsExecutable` (bool, `XmlAttribute`, İsteğe Bağlı): Sürecin bir iş akışı motoru tarafından yürütülüp yürütülemeyeceğini belirtir.
        *   `ProcessType` (`ProcessType` enum, `XmlAttribute`, Varsayılan: `None`): Sürecin türünü belirtir (`None`, `Public`, `Private`). `Public` süreçler genellikle farklı katılımcılar arasındaki etkileşimi soyutlar, `Private` süreçler ise bir katılımcının iç iş akışını detaylandırır.
        *   `IsClosed` (bool, `XmlAttribute`, Varsayılan: `false`): Sürecin dış katılımcılar tarafından görülebilir olup olmadığını belirtir (genellikle `Collaboration` diyagramlarında kullanılır).
        *   `DefinitionalCollaborationRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Bu sürecin detaylandırdığı `Collaboration` içindeki `Participant`'a referans.
        *   `Supports` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu sürecin desteklediği veya kullandığı diğer süreçlere referanslar.
        *   `Property` (Collection<`Property`>, `XmlElement`): Süreç düzeyinde tanımlanmış veri alanları (süreç değişkenleri).
        *   `LaneSet` (Collection<`LaneSet`>, `XmlElement`): Süreç içindeki aktivitelerin sorumluluklarını veya rollerini görsel olarak gruplayan kulvar setleri.
        *   `FlowElement` (Collection<`FlowElement`>, `XmlElement`): Sürecin akışını oluşturan tüm Olay, Aktivite, Gateway, SequenceFlow, Veri Nesnesi gibi elemanların koleksiyonu.
        *   `Artifact` (Collection<`Artifact`>, `XmlElement`): Sürece ait Açıklama, Grup gibi artifact'ların koleksiyonu.
        *   `ResourceRole` (Collection<`ResourceRole`>, `XmlElement`): Süreçte yer alan kaynak rollerinin (Performer, PotentialOwner vb.) koleksiyonu.
        *   `CorrelationSubscription` (Collection<`CorrelationSubscription`>, `XmlElement`): Sürecin belirli mesajları veya sinyalleri nasıl ilişkilendireceğini tanımlayan abonelikler.
        *   `Auditing` (`Auditing`, `XmlElement`, Miras): Süreç düzeyinde denetim ayarları.
        *   `Monitoring` (`Monitoring`, `XmlElement`, Miras): Süreç düzeyinde izleme ayarları.
    *   **Bağımlılıklar:**
        *   Neredeyse tüm diğer BPMN Şema elemanları (`FlowElement` alt tipleri, `Artifact` alt tipleri, `LaneSet`, `Property`, `ResourceRole`, `CorrelationSubscription` vb.).
    *   **Önemli Noktalar:**
        *   Bir BPMN dosyasındaki asıl iş mantığını içeren ana yapıdır.
        *   Bir `Definitions` içinde birden fazla `Process` olabilir.
        *   `IsExecutable=true` olan süreçler motor tarafından çalıştırılabilir.

## XML Örneği

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL" ... >

  <!-- Süreç Tanımı -->
  <process id="Proc_SimpleApproval" name="Simple Approval Process" isExecutable="true" processType="Private">
    <documentation>A basic process for handling approval requests.</documentation>

    <!-- Süreç Değişkeni -->
    <property id="Prop_RequestAmount" name="requestAmount" itemSubjectRef="xsd:decimal"/>

    <!-- Akış Elemanları -->
    <startEvent id="StartRequest" name="Request Received"/>

    <userTask id="Task_ReviewRequest" name="Review Request">
      <potentialOwner>
        <resourceAssignmentExpression>
          <formalExpression>group(approvers)</formalExpression>
        </resourceAssignmentExpression>
      </potentialOwner>
    </userTask>

    <exclusiveGateway id="Gateway_Decision" name="Amount Check" default="Flow_Reject"/>

    <scriptTask id="Task_NotifyApproved" name="Notify Approved" scriptFormat="text/javascript">
      <script>notifier.send("approved");</script>
    </scriptTask>

    <scriptTask id="Task_NotifyRejected" name="Notify Rejected" scriptFormat="text/javascript">
      <script>notifier.send("rejected");</script>
    </scriptTask>

    <endEvent id="EndApproved" name="Request Approved"/>
    <endEvent id="EndRejected" name="Request Rejected"/>

    <!-- Sıra Akışları -->
    <sequenceFlow id="Flow1" sourceRef="StartRequest" targetRef="Task_ReviewRequest"/>
    <sequenceFlow id="Flow2" sourceRef="Task_ReviewRequest" targetRef="Gateway_Decision"/>
    <sequenceFlow id="Flow_Approve" name="Amount <= 100" sourceRef="Gateway_Decision" targetRef="Task_NotifyApproved">
      <conditionExpression xsi:type="tFormalExpression">${requestAmount <= 100}</conditionExpression>
    </sequenceFlow>
    <sequenceFlow id="Flow_Reject" name="Amount > 100" sourceRef="Gateway_Decision" targetRef="Task_NotifyRejected"/>
    <sequenceFlow id="Flow4" sourceRef="Task_NotifyApproved" targetRef="EndApproved"/>
    <sequenceFlow id="Flow5" sourceRef="Task_NotifyRejected" targetRef="EndRejected"/>

    <!-- (İsteğe bağlı) Kulvarlar -->
    <laneSet>
       <lane name="Requester"/>
       <lane name="Approver">
           <flowNodeRef>Task_ReviewRequest</flowNodeRef>
           <flowNodeRef>Gateway_Decision</flowNodeRef>
           <flowNodeRef>Task_NotifyApproved</flowNodeRef>
           <flowNodeRef>Task_NotifyRejected</flowNodeRef>
           <flowNodeRef>EndApproved</flowNodeRef>
           <flowNodeRef>EndRejected</flowNodeRef>
       </lane>
    </laneSet>

  </process>

</definitions>
``` 