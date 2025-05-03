# ServiceTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Task`

**BPMN Elemanı:** `serviceTask`

## Açıklama

`ServiceTask` (Servis Görevi), bir süreç akışı içinde harici bir uygulamayı, bir web servisini veya herhangi bir otomatik hizmeti çağırmak için kullanılan bir görev türüdür. İnsan müdahalesi gerektirmeyen otomatik adımları modellemek için kullanılır.

Süreç motoru bir Servis Görevine ulaştığında, genellikle yapılandırılmış olan uygulamayı veya servisi çağırır, yanıtını bekler (veya zamanuyumsuz olarak devam eder) ve sonuçları süreç değişkenlerine yazabilir.

`Task` sınıfından miras aldığı için bir aktivitenin ve görevin tüm temel özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `serviceTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Implementation` : `string` (Öznitelik: `implementation`, Varsayılan: `"##WebService"`)**
    *   Servis görevinin nasıl gerçekleştirileceğini belirtir. Değer, motorun görevi nasıl yürüteceğini belirlemesine yardımcı olur. Olası değerler şunları içerebilir:
        *   `"##WebService"`: Görevin bir web servisi çağrısı olduğunu belirtir (genellikle `OperationRef` ile birlikte kullanılır).
        *   `"##Unspecified"` veya boş: Uygulama motor tarafından belirlenir (örn. bir sınıf adı veya ifadeye göre).
        *   Özel bir URI veya tanımlayıcı: Belirli bir teknolojiye veya uygulamaya işaret edebilir.
*   **`OperationRef` : `System.Xml.XmlQualifiedName?` (Öznitelik: `operationRef`)**
    *   Eğer `Implementation` `"##WebService"` ise, çağrılacak olan WSDL (Web Services Description Language) operasyonuna bir referans (`QName`) içerir. Bu operasyon genellikle BPMN dosyasında tanımlanmış bir `Interface` içinde bulunur.

## İlişkili Özellikler (Activity'den Miras Alınan)

Servis Görevleri için özellikle önemli olan `Activity` sınıfından miras alınan özellikler şunlardır:

*   **`IoSpecification` / `DataInputAssociation` / `DataOutputAssociation`**: Servise gönderilecek girdileri süreç değişkenlerinden almak ve servisten dönen sonuçları süreç değişkenlerine yazmak için kullanılır.

## Kullanım

Süreç motoru, BPMN modelinde bir `serviceTask` elemanıyla karşılaştığında bir `ServiceTask` nesnesi oluşturur. Motor, `Implementation` ve `OperationRef` özelliklerine (ve potansiyel olarak ek konfigürasyonlara) bakarak:
1.  Belirtilen servisi veya uygulamayı çağırır.
2.  Gerekli girdileri (`DataInputAssociation` kullanarak) süreç değişkenlerinden alır ve servise gönderir.
3.  Servisten dönen yanıtı (`DataOutputAssociation` kullanarak) süreç değişkenlerine yazar.
4.  İşlem tamamlandıktan sonra süreç akışında devam eder.

Servis çağrılarının zamanuyumsuz (asynchronous) olarak yapılması ve motorun yanıtı beklemesi veya bir geri çağırma (callback) mekanizması kullanması yaygın bir yaklaşımdır (genellikle `asyncBefore`/`asyncAfter` veya Job Executor kullanılarak). 