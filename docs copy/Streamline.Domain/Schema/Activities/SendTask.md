# SendTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Task`

**BPMN Elemanı:** `sendTask`

## Açıklama

`SendTask` (Gönderme Görevi), bir süreç akışı içinde bir Mesaj (Message) göndermek için kullanılan bir görev türüdür. Bu mesaj genellikle başka bir katılımcıya (Participant), başka bir sürece veya harici bir sisteme yöneliktir.

Süreç motoru bir Gönderme Görevine ulaştığında, belirtilen mesajı (`MessageRef`) yapılandırılmış uygulama (`Implementation`, `OperationRef`) aracılığıyla gönderir.

`Task` sınıfından miras aldığı için bir aktivitenin ve görevin tüm temel özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `sendTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Implementation` : `string` (Öznitelik: `implementation`, Varsayılan: `"##WebService"`)**
    *   Mesajın nasıl gönderileceğini belirtir. Değer, motorun mesajı iletmek için hangi mekanizmayı kullanacağını belirlemesine yardımcı olur. Olası değerler:
        *   `"##WebService"`: Mesajın bir web servisi çağrısı (genellikle `OperationRef` ile belirtilen) aracılığıyla gönderileceğini belirtir.
        *   `"##Unspecified"` veya boş: Uygulama motor tarafından belirlenir (örn. bir mesajlaşma kuyruğu, bir API uç noktası veya özel kod).
        *   Özel bir URI veya tanımlayıcı: Belirli bir mesajlaşma teknolojisine veya hedefe işaret edebilir.
*   **`MessageRef` : `System.Xml.XmlQualifiedName?` (Öznitelik: `messageRef`)**
    *   Gönderilecek olan `Message` elemanına bir referans (`QName`). Bu `Message` elemanı genellikle BPMN dosyasında tanımlanır ve gönderilecek verinin yapısını (`itemRef`) temsil edebilir.
*   **`OperationRef` : `System.Xml.XmlQualifiedName?` (Öznitelik: `operationRef`)**
    *   Eğer `Implementation` `"##WebService"` ise veya başka bir operasyon tabanlı gönderme mekanizması kullanılıyorsa, mesajı göndermek için kullanılacak olan WSDL veya başka bir `Interface` içindeki operasyona bir referans (`QName`) içerir.

## İlişkili Özellikler (Activity'den Miras Alınan)

*   **`IoSpecification` / `DataInputAssociation`**: Gönderilecek mesajın içeriğini oluşturmak için süreç değişkenlerinden veri almak amacıyla kullanılır.

## Kullanım

Süreç motoru, BPMN modelinde bir `sendTask` elemanıyla karşılaştığında bir `SendTask` nesnesi oluşturur. Motor, `Implementation`, `MessageRef`, `OperationRef` ve `DataInputAssociation` bilgilerini kullanarak:
1.  Gönderilecek mesajın içeriğini süreç değişkenlerinden oluşturur.
2.  Belirtilen uygulama mekanizması aracılığıyla mesajı gönderir.
3.  Mesaj gönderildikten sonra (genellikle zamanuyumlu olarak) süreç akışında devam eder.

Mesaj gönderme işlemi sırasında bir hata oluşursa, bu bir `Incident` oluşturabilir veya hata süreç içinde yakalanabilir. 