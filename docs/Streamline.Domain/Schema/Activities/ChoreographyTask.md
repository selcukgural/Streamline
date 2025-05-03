# ChoreographyTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Choreography.ChoreographyActivity` (Varsayılan)

**BPMN Elemanı:** `choreographyTask`

## Açıklama

`ChoreographyTask` (Koreografi Görevi), bir Koreografi Diyagramı (Choreography Diagram) içinde iki veya daha fazla Katılımcı (Participant) arasındaki bir etkileşimi temsil eder. Bu etkileşim genellikle bir veya daha fazla Mesaj Akışı (`MessageFlow`) ile ifade edilen bir mesaj alışverişidir.

Bir `ChoreographyTask`, hangi katılımcıların etkileşimde bulunduğunu (`participantRef`) ve hangi mesajların değiş tokuş edildiğini (`messageFlowRef`) belirtir. Başlatan katılımcı genellikle beyaz renkle gösterilir.

**Not:** Koreografi modelleri (Choreography), genellikle süreçlerin (Process) nasıl yürütüldüğünden ziyade, farklı taraflar arasındaki etkileşim protokollerini tanımlamak için kullanılır. Streamline gibi bir süreç motoru, genellikle Koreografi modellerini doğrudan yürütmez, ancak bu modeller süreç tasarımı ve entegrasyonu için değerli olabilir.

Bu sınıf, BPMN XML şemasındaki `choreographyTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`MessageFlowRef` : `Collection<System.Xml.XmlQualifiedName>`**
    *   Bu Koreografi Görevini oluşturan Mesaj Akışlarına (`MessageFlow`) yapılan referansların (`QName`) koleksiyonu. Her referans, katılımcılar arasında gidip gelen bir mesaja karşılık gelir.

## İlişkili Özellikler (ChoreographyActivity'den Miras Alınan)

*   **`ParticipantRef` : `Collection<System.Xml.XmlQualifiedName>`**: Bu etkileşime katılan Katılımcılara (`Participant`) yapılan referansların koleksiyonu (genellikle iki tane).
*   **`InitiatingParticipantRef` : `System.Xml.XmlQualifiedName`**: Etkileşimi başlatan (ilk mesajı gönderen) katılımcıya referans.
*   **`LoopType` : `ChoreographyLoopType`**: Koreografi aktivitesinin döngü türünü belirtir (None, Standard, MultiInstanceSequential, MultiInstanceParallel).

## Kullanım

`ChoreographyTask` elemanları, BPMN Koreografi Diyagramlarında katılımcılar arasındaki etkileşim adımlarını modellemek için kullanılır. Örneğin, "Sipariş Gönder" ve "Sipariş Onayı Al" gibi iki mesaj akışını içeren bir "Sipariş Verme" etkileşimini temsil edebilir.

Süreç motorları genellikle bu elemanları doğrudan yürütmez, ancak model analizi veya süreç belgeleri için önemlidir. 