# Transaction

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.SubProcess`

**BPMN Elemanı:** `transaction`

## Açıklama

`Transaction` (İşlem), işlemsel davranışları (transactional behavior) modellemek için kullanılan özel bir `SubProcess` (Alt Süreç) türüdür. Amacı, bir dizi aktivitenin "ya hep ya hiç" (all-or-nothing) mantığıyla yürütülmesini sağlamaktır. Genellikle uzun süren iş işlemlerinde (long-running business transactions), özellikle dağıtık sistemlerde tutarlılığı sağlamak için kullanılır.

Bir `Transaction` alt süreci üç olası sonuçla bitebilir:
1.  **Başarılı Tamamlanma:** İçindeki tüm aktiviteler başarılı olursa ve bir Son Olayına (End Event) ulaşılırsa, işlem başarılı kabul edilir.
2.  **İptal (Cancel):** İşlem devam ederken bir İptal Olayı (Cancel End Event veya Cancel Boundary Event) tetiklenirse, işlem iptal edilir ve ilişkili telafi (compensation) aktiviteleri *tetiklenmez*. Akış, İptal Sınır Olayına (Cancel Boundary Event) bağlı olan Sıra Akışı üzerinden devam eder.
3.  **Hata/Telafi (Error/Compensation):** İşlem içinde bir hata oluşursa (veya bir Hata Olayı tetiklenirse) veya dışarıdan bir Telafi Olayı (Compensate End Event veya Message Boundary Event for compensation) tetiklenirse, işlem *başarıyla tamamlanmış* olan iç aktiviteler için telafi mekanizması çalıştırılır. Ardından akış, Hata Sınır Olayına (Error Boundary Event) veya Telafi Sınır Olayına (Compensation Boundary Event - genellikle mesajla tetiklenir) bağlı olan Sıra Akışı üzerinden devam eder.

`SubProcess` sınıfından miras aldığı için bir alt sürecin tüm özelliklerine (iç akış elemanları, vb.) sahiptir.

Bu sınıf, BPMN XML şemasındaki `transaction` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Method` : `string` (Öznitelik: `method`, Varsayılan: `"##Compensate"`)**
    *   Kullanılacak olan işlem protokolünü belirtir. BPMN spesifikasyonu şu anda yalnızca `"##Compensate"` değerini standart olarak tanımlamaktadır. Bu, işlemin telafi (compensation) mekanizmasını kullandığını gösterir.

## İlişkili Elemanlar

`Transaction` alt süreçleri genellikle aşağıdaki olaylarla birlikte kullanılır:
*   **Cancel End Event:** İşlemi iptal etmek için `Transaction` içinde kullanılır.
*   **Cancel Boundary Event:** Ana süreç akışından `Transaction`'ı iptal etmek için `Transaction` aktivitesinin sınırına eklenir.
*   **Error End Event / Error Boundary Event:** `Transaction` içinde veya sınırında hata durumlarını yönetmek için kullanılır.
*   **Compensate End Event / Compensation Boundary Event / Compensation Intermediate Event / Compensation Start Event:** Telafi mantığını tetiklemek ve yönetmek için kullanılır.
*   **Compensation Association:** Bir aktiviteyi, onu telafi eden aktiviteye bağlar.

## Kullanım

Süreç motoru, BPMN modelinde bir `transaction` elemanıyla karşılaştığında bir `Transaction` nesnesi oluşturur. Motor, bunu bir `SubProcess` gibi yürütür ancak ek olarak:
1.  İşlem sırasında başarıyla tamamlanan aktiviteleri takip eder.
2.  Bir İptal (Cancel) olayı gerçekleşirse, işlemi sonlandırır ve ilgili Cancel Boundary Event'ten devam eder.
3.  Bir Hata (Error) olayı veya Telafi (Compensation) tetiği gerçekleşirse:
    a.  Önce, işlem içinde başarıyla tamamlanmış ve telafi aktivitesi olan adımların telafi mantığını (Compensation Handler'ları) çalıştırır.
    b.  Telafi tamamlandıktan sonra, işlemi sonlandırır ve ilgili Error Boundary Event veya Compensation Boundary Event'ten devam eder.