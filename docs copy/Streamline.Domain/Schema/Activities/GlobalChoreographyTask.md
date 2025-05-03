# GlobalChoreographyTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Choreography.Choreography` (Varsayılan)

**BPMN Elemanı:** `globalChoreographyTask`

## Açıklama

`GlobalChoreographyTask` (Global Koreografi Görevi), BPMN dosyasının kök seviyesinde tanımlanan ve iki veya daha fazla Katılımcı arasındaki tipik bir etkileşim kalıbını temsil eden, yeniden kullanılabilir bir koreografi tanımıdır. Normal bir `ChoreographyTask` gibi, genellikle bir mesaj alışverişini içerir.

Amacı, farklı Koreografi Diyagramlarında veya İşbirliği Diyagramlarında (`Collaboration`) tekrarlanan standart etkileşimleri (örn. "Teklif İste/Gönder") tek bir yerde tanımlamaktır.

`Choreography` sınıfından miras aldığı için Akış Elemanları (`FlowElement`) ve Katılımcılar (`Participant`) içerebilir (ancak GlobalChoreographyTask genellikle sadece katılımcı referansları ve mesaj akışları içerir).

Bu sınıf, BPMN XML şemasındaki `globalChoreographyTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`InitiatingParticipantRef` : `System.Xml.XmlQualifiedName` (Öznitelik: `initiatingParticipantRef`)**
    *   Bu global koreografi görevindeki etkileşimi başlatan (ilk mesajı gönderen) Katılımcıya (`Participant`) referans (`QName`).

## İlişkili Özellikler (Choreography'den Miras Alınan)

*   **`Participant` : `Collection<Participant>`**: Bu global etkileşimde yer alan varsayılan katılımcıları tanımlayabilir.
*   **`MessageFlow` : `Collection<MessageFlow>`**: Katılımcılar arasındaki mesaj alışverişlerini tanımlar.

## Kullanım

`GlobalChoreographyTask` BPMN dosyasının `definitions` elemanı altında tanımlanır. Bir İşbirliği Diyagramı (`Collaboration`) içindeki bir `CallChoreography` aktivitesi, `calledChoreographyRef` özniteliği ile bu `GlobalChoreographyTask`'ın ID'sine referans vererek onu çağırır.

`CallChoreography` aktivitesi, `GlobalChoreographyTask`'ta tanımlanan etkileşim kalıbını kendi bağlamında (belirli katılımcılarla eşleştirerek) yeniden kullanır.

Süreç motorları genellikle Koreografi elemanlarını doğrudan yürütmediği için, bu elemanlar daha çok modelleme ve tasarım amacıyla kullanılır. 