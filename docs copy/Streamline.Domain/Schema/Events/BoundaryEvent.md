# BoundaryEvent

**Namespace:** `Streamline.Domain.Schema.Events`

**Temel Sınıf:** `Streamline.Domain.Schema.Events.CatchEvent`

**BPMN Elemanı:** `boundaryEvent`

## Açıklama

`BoundaryEvent` (Sınır Olayı), bir Aktivite'nin (örneğin, bir Görev veya Alt Süreç) sınırına eklenen bir `CatchEvent` türüdür. Aktivite çalışırken, sınır olayı belirli bir tetikleyiciyi (örneğin, bir Hata, Zamanlayıcı, Mesaj, Sinyal, Yükseltme) dinler. Olay tetiklendiğinde, sınır olayı aktiviteyi *kesintiye uğratabilir* (Interrupting Boundary Event) ve aktivite devam ederken *paralel bir akış başlatabilir* (Non-interrupting Boundary Event).

Bu sınıf, BPMN XML şemasındaki `boundaryEvent` elemanından otomatik olarak türetilmiştir.

## Özellikler

`CatchEvent`'ten miras aldığı özelliklere ek olarak aşağıdaki özelliklere sahiptir:

*   **`CancelActivity`**: `bool` (Varsayılan: `true`)
    *   Bu olay tetiklendiğinde, bağlı olduğu Aktivite'nin iptal edilip edilmeyeceğini belirler.
    *   `true` (Interrupting): Aktivite iptal edilir ve akış sınır olayından çıkan Sıra Akışı üzerinden devam eder.
    *   `false` (Non-interrupting): Aktivite çalışmaya devam ederken, sınır olayından çıkan Sıra Akışı üzerinden *paralel* yeni bir akış başlar.
*   **`AttachedToRef`**: `System.Xml.XmlQualifiedName`
    *   Bu sınır olayının hangi Aktivite'nin sınırına bağlı olduğunu belirten referans (Aktivite'nin `id`'si).

## Davranış

Sınır olayının davranışı büyük ölçüde içerdiği `EventDefinition`'a ve `CancelActivity` özelliğinin değerine bağlıdır:

*   **Interrupting (`CancelActivity = true`)**: Aktivite çalışırken olay tetiklenirse, aktivite hemen durdurulur (iptal edilir) ve süreç akışı sınır olayından çıkan yola yönlendirilir.
*   **Non-interrupting (`CancelActivity = false`)**: Aktivite çalışırken olay tetiklenirse, aktivite normal şekilde çalışmaya devam eder. *Aynı anda*, sınır olayından çıkan yol üzerinden yeni bir paralel süreç akışı başlatılır.

## Yaygın Sınır Olayı Türleri (EventDefinition'a göre)

*   **Error Boundary Event:** Aktivite içinde bir Hata (`ErrorEventDefinition`) meydana geldiğinde tetiklenir. Genellikle kesintiye uğratıcıdır (`CancelActivity=true`).
*   **Timer Boundary Event:** Belirli bir süre (`TimerEventDefinition`) geçtikten sonra veya belirli bir zamanda tetiklenir. Hem kesintiye uğratıcı hem de uğratmayan olabilir.
*   **Message Boundary Event:** Belirli bir Mesaj (`MessageEventDefinition`) alındığında tetiklenir. Hem kesintiye uğratıcı hem de uğratmayan olabilir.
*   **Signal Boundary Event:** Belirli bir Sinyal (`SignalEventDefinition`) yayınlandığında tetiklenir. Hem kesintiye uğratıcı hem de uğratmayan olabilir.
*   **Escalation Boundary Event:** Belirli bir Yükseltme (`EscalationEventDefinition`) fırlatıldığında tetiklenir. Genellikle kesintiye uğratmayan (`CancelActivity=false`) olarak kullanılır, ancak kesintiye uğratıcı da olabilir.
*   **Conditional Boundary Event:** Belirli bir Koşul (`ConditionalEventDefinition`) sağlandığında tetiklenir. Hem kesintiye uğratıcı hem de uğratmayan olabilir.
*   **Compensation Boundary Event:** Telafi (Compensation) işlemini tetiklemek için kullanılır (`CompensateEventDefinition`). Sadece bir Aktivite *başarıyla tamamlandıktan sonra* tetiklenebilir ve telafi akışını başlatır. Tanımı gereği kesintiye uğratmaz (çünkü aktivite zaten bitmiştir).

## Kullanım

Süreç motoru, bir Aktivite'ye bağlı `BoundaryEvent`'leri şu şekilde yönetir:
1.  Bir `Execution` bir Aktivite'ye girdiğinde, motor bu aktiviteye bağlı tüm `BoundaryEvent`'leri (ve içerdikleri `EventDefinition`'ları) kaydeder ve dinlemeye başlar.
2.  Eğer Aktivite çalışırken dinlenen olaylardan biri tetiklenirse:
    *   `CancelActivity` `true` ise, Aktivite'yi çalıştıran `Execution`(lar) sonlandırılır ve sınır olayından yeni bir `Execution` başlatılır.
    *   `CancelActivity` `false` ise, Aktivite'yi çalıştıran `Execution`(lar) devam ederken, sınır olayından *paralel* yeni bir `Execution` başlatılır.
3.  Eğer Aktivite normal şekilde tamamlanırsa, bağlı olan `BoundaryEvent`'lerin (Compensation hariç) dinlenmesi durdurulur. 