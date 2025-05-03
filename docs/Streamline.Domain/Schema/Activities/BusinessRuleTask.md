# BusinessRuleTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Task`

**BPMN Elemanı:** `businessRuleTask`

## Açıklama

`BusinessRuleTask` (İş Kuralı Görevi), bir süreç akışı içinde tanımlanmış iş kurallarını yürütmek için kullanılan bir görev türüdür. Genellikle harici bir İş Kuralı Motoru (Business Rules Engine - BRE) veya bir Karar Yönetimi Sistemi (Decision Management System), örneğin DMN (Decision Model and Notation) kullanan bir sistem ile entegrasyonu temsil eder.

Süreç motoru bir İş Kuralı Görevine ulaştığında, görevin yapılandırmasına göre ilgili iş kuralı setini veya karar tablosunu çalıştırır ve sonuçlarını süreç değişkenlerine yazabilir.

`Task` sınıfından miras aldığı için bir aktivitenin ve görevin tüm temel özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `businessRuleTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Implementation` : `string` (Öznitelik: `implementation`, Varsayılan: `"##unspecified"`)**
    *   İş kuralı görevinin nasıl gerçekleştirileceğini belirtir. Değer, motorun hangi kural setini veya karar mantığını yürüteceğini belirlemesine yardımcı olur. Olası değerler şunları içerebilir:
        *   `"##unspecified"` veya boş: Uygulama motor tarafından belirlenir (örn. bir DMN tablosu referansı, bir kural dosyası adı veya özel bir tanımlayıcıya göre).
        *   Özel bir URI veya tanımlayıcı: Belirli bir kural motoru teknolojisine veya kural setine işaret edebilir.

## İlişkili Özellikler (Activity'den Miras Alınan)

*   **`IoSpecification` / `DataInputAssociation` / `DataOutputAssociation`**: İş kuralı motoruna girdi olarak verilecek verileri süreç değişkenlerinden almak ve kural motorundan dönen sonuçları (kararları) süreç değişkenlerine yazmak için kullanılır.

## Kullanım

Süreç motoru, BPMN modelinde bir `businessRuleTask` elemanıyla karşılaştığında bir `BusinessRuleTask` nesnesi oluşturur. Motor, `Implementation` özelliğine (ve potansiyel olarak ek konfigürasyonlara) bakarak:
1.  Gerekli girdileri (`DataInputAssociation` kullanarak) süreç değişkenlerinden alır.
2.  Belirtilen iş kuralı motorunu veya karar hizmetini çağırır.
3.  Kural motorundan veya karar hizmetinden dönen sonuçları (`DataOutputAssociation` kullanarak) süreç değişkenlerine yazar.
4.  İşlem tamamlandıktan sonra süreç akışında devam eder.

İş kuralı yürütülürken bir hata oluşursa, bu genellikle bir `Incident` oluşturulmasına veya hatanın süreç içinde yakalanıp işlenmesine neden olur. 