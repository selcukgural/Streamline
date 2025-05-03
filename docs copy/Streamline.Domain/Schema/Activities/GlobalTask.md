# GlobalTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Common.CallableElement`

**BPMN Elemanı:** `globalTask`

## Açıklama

`GlobalTask` (Global Görev), belirli bir Süreç (Process) içinde yer almayan, ancak BPMN dosyasının kök seviyesinde tanımlanan ve farklı Süreçler veya Çağrı Aktiviteleri (`CallActivity`) tarafından yeniden kullanılabilen bir görev tanımıdır. Normal bir `Task`'ın aksine, bir `GlobalTask`'ın gelen veya giden Sıra Akışları (Sequence Flows) yoktur.

Amacı, ortak görev mantığını tek bir yerde tanımlamak ve birden fazla yerde tekrarlamaktan kaçınmaktır.

`CallableElement` sınıfından türediği için bir `name` (isim) özelliğine ve girdi/çıktı tanımları (`IoSpecification`, `IoBinding`) için destek içerir.

Bu sınıf, BPMN XML şemasındaki `globalTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`ResourceRole` : `Collection<ResourceRole>`**
    *   Bu global görevi gerçekleştirmekle ilişkili kaynakları veya rolleri tanımlar. Alt türleri arasında `Performer`, `HumanPerformer`, `PotentialOwner` bulunur.
    *   Bir `CallActivity` bu `GlobalTask`'ı çağırdığında, bu kaynak tanımları çağrılan görevin yürütülmesinde kullanılabilir.

## Türetilmiş Sınıflar

Global görevlerin de belirli türleri vardır ve bunlar bu sınıftan türetilmiştir:
*   `GlobalUserTask`
*   `GlobalBusinessRuleTask`
*   `GlobalScriptTask`
*   `GlobalManualTask`

## Kullanım

`GlobalTask` (veya türevleri) BPMN dosyasının `definitions` elemanı altında tanımlanır. Bir süreç içindeki `CallActivity`, `calledElement` özniteliği ile bu `GlobalTask`'ın ID'sine referans vererek onu çağırır.

Süreç motoru bir `CallActivity` ile karşılaştığında:
1.  `calledElement` ile belirtilen `GlobalTask` tanımını bulur.
2.  Gerekli veri giriş/çıkış eşlemelerini yapar.
3.  `GlobalTask`'ın mantığını (türüne göre - User, Script vb.) yürütür.
4.  Sonuçları `CallActivity`'ye geri döndürür ve ana süreç akışında devam eder. 