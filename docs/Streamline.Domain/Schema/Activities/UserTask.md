# UserTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Task`

**BPMN Elemanı:** `userTask`

## Açıklama

`UserTask` (Kullanıcı Görevi), bir süreç akışı içinde bir insan katılımcının gerçekleştirmesi gereken bir işi temsil eder. Süreç motoru bir Kullanıcı Görevine ulaştığında, genellikle bir görev listesi (tasklist) uygulamasına bu görevi ekler ve görevin tamamlanmasını bekler.

Kullanıcılar, görev listesi üzerinden kendilerine atanan veya aday oldukları görevleri görür, talep eder (claim) ve tamamlarlar. Görevin tamamlanmasıyla süreç akışı devam eder.

`Task` sınıfından miras aldığı için bir aktivitenin ve görevin tüm temel özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `userTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Rendering` : `Collection<Rendering>`**
    *   Bu görevin kullanıcı arayüzünde nasıl sunulacağına dair ipuçları veya referanslar içeren bir koleksiyon. Örneğin, kullanılacak formun ID'sini veya bir UI bileşenini işaret edebilir.
*   **`Implementation` : `string` (Öznitelik: `implementation`, Varsayılan: `"##unspecified"`)**
    *   Görev uygulamasının doğasını belirtir. Kullanıcı Görevleri için bu genellikle `"##unspecified"` veya `"##HumanTask"` gibi standart bir değer alır veya boş bırakılır. Diğer görev türlerinde (örn. ServiceTask) daha spesifik anlamlar taşır (örn. bir web servis URL'si).

## İlişkili Özellikler (Activity'den Miras Alınan)

Kullanıcı Görevleri için özellikle önemli olan `Activity` sınıfından miras alınan bazı özellikler şunlardır:

*   **`ResourceRole`**: Görevin kime atanacağını (`Assignee`), kimlerin aday olduğunu (`CandidateUsers`, `CandidateGroups`) veya kimin sorumlu olduğunu (`PotentialOwner`) tanımlamak için kullanılır. Bu roller genellikle `HumanPerformer` veya `PotentialOwner` alt türleri olarak tanımlanır.
*   **`IoSpecification` / `DataInputAssociation` / `DataOutputAssociation`**: Görev formunda gösterilecek veya görev tamamlandığında süreç değişkenlerine yazılacak verileri tanımlamak için kullanılır.

## Kullanım

Süreç motoru, BPMN modelinde bir `userTask` elemanıyla karşılaştığında bir `UserTask` nesnesi oluşturur. Motor, `ResourceRole` tanımlarına bakarak:
1.  Eğer bir `Assignee` tanımlanmışsa, görevi doğrudan o kullanıcıya atar.
2.  Eğer `CandidateUsers` veya `CandidateGroups` tanımlanmışsa, görevi aday durumunda oluşturur.

Ardından motor, genellikle bir `UserTaskAssignment` kaydı oluşturarak görevi bir görev listesine bildirir ve `Execution`'ı bu görevde bekletir. Kullanıcı görevi tamamladığında (genellikle görev listesi uygulaması aracılığıyla), motor ilgili `Execution`'ı devam ettirir. 