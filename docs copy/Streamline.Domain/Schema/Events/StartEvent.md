# StartEvent

**Namespace:** `Streamline.Domain.Schema.Events`

**Temel Sınıf:** `Streamline.Domain.Schema.Events.CatchEvent`

**BPMN Elemanı:** `startEvent`

## Açıklama

`StartEvent` (Başlangıç Olayı), bir Sürecin (Process) veya bir Olay Alt Sürecinin (Event Sub-Process) nasıl başlayacağını gösteren bir `CatchEvent` türüdür. Her sürecin (veya Olay Alt Sürecinin) en az bir Başlangıç Olayı olmalıdır.

Başlangıç Olaylarının gelen Sıra Akışı (Sequence Flow) olamaz, yalnızca giden Sıra Akışları olabilir.

`CatchEvent` sınıfından miras aldığı için, bir tetikleyici bekleyebilir (`EventDefinition` ile tanımlanır) ve olay gerçekleştiğinde veri üretebilir (`DataOutput`, `DataOutputAssociation`).

Bu sınıf, BPMN XML şemasındaki `startEvent` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`IsInterrupting` : `bool` (Öznitelik: `isInterrupting`, Varsayılan: `true`)**
    *   Bu özellik **yalnızca Olay Alt Süreçleri (Event Sub-Processes)** içindeki Başlangıç Olayları için anlamlıdır.
    *   `true` (Varsayılan): Başlangıç Olayı tetiklendiğinde, içinde bulunduğu kapsamdaki (Olay Alt Sürecinin eklendiği Süreç veya Alt Süreç) diğer tüm aktif yürütmeler (`Execution`'lar) sonlandırılır. Akış yalnızca Olay Alt Süreci içinde devam eder.
    *   `false` (Kesintisiz - Non-Interrupting): Başlangıç Olayı tetiklendiğinde, içinde bulunduğu kapsamdaki diğer aktif yürütmeler devam ederken, Olay Alt Süreci için yeni, paralel bir yürütme başlatılır.
    *   Normal (üst seviye) Süreçlerin veya gömülü Alt Süreçlerin Başlangıç Olayları için bu öznitelik her zaman `true` olarak kabul edilir.

## Yaygın Başlangıç Olayı Türleri (EventDefinition'a göre)

*   **None Start Event:** Belirli bir tetikleyici yoktur. Süreç genellikle manuel olarak veya bir API çağrısıyla başlatılır.
*   **Message Start Event:** Belirli bir mesaj (`MessageEventDefinition`) geldiğinde süreç başlar.
*   **Timer Start Event:** Belirli bir zamanda veya periyodik olarak (`TimerEventDefinition`) süreç başlar.
*   **Signal Start Event:** Belirli bir sinyal (`SignalEventDefinition`) yayınlandığında süreç başlar.
*   **Conditional Start Event:** Belirli bir koşul (`ConditionalEventDefinition`) sağlandığında süreç başlar.
*   **Error Start Event:** Yalnızca Olay Alt Süreçlerinde kullanılır. Belirtilen bir hata (`ErrorEventDefinition`) yakalandığında Olay Alt Süreci başlar.
*   **Escalation Start Event:** Yalnızca Olay Alt Süreçlerinde kullanılır. Belirtilen bir yükseltme (`EscalationEventDefinition`) yakalandığında Olay Alt Süreci başlar.
*   **Compensation Start Event:** Yalnızca Olay Alt Süreçlerinde kullanılır. Bir telafi işlemi (`CompensateEventDefinition`) tetiklendiğinde Olay Alt Süreci başlar.

## Kullanım

Süreç motoru, bir süreci başlatma isteği aldığında (örn. API çağrısı, mesaj gelişi, zamanlayıcı tetiği):
1.  İlgili süreç tanımını bulur.
2.  Başlatma isteğiyle eşleşen `StartEvent`(leri) belirler (örn. mesaj adı, sinyal adı).
3.  Eşleşen her `StartEvent` için yeni bir `ProcessInstance` ve birincil `Execution` oluşturur.
4.  Eğer olay veri içeriyorsa (`DataOutputAssociation` ile), veriyi süreç değişkenlerine yazar.
5.  `Execution`'ı `StartEvent`'ten çıkan Sıra Akışı üzerinden devam ettirir. 