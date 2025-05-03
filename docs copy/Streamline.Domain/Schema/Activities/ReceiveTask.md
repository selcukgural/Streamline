# ReceiveTask

**Namespace:** `Streamline.Domain.Schema.Activities`

**Temel Sınıf:** `Streamline.Domain.Schema.Activities.Task`

**BPMN Elemanı:** `receiveTask`

## Açıklama

`ReceiveTask` (Alma Görevi), süreç akışı içinde belirli bir Mesajın (Message) başka bir katılımcıdan veya sistemden gelmesini bekleyen bir görev türüdür. Süreç motoru bir Alma Görevine ulaştığında, ilgili mesaj gelene kadar `Execution`'ı bu görevde bekletir.

Bu görev, süreçler arası iletişimde veya harici sistemlerden gelen yanıtları beklemek için kullanılır.

`Task` sınıfından miras aldığı için bir aktivitenin ve görevin tüm temel özelliklerine sahiptir.

Bu sınıf, BPMN XML şemasındaki `receiveTask` elemanından otomatik olarak türetilmiştir.

## Özellikler

*   **`Implementation` : `string` (Öznitelik: `implementation`, Varsayılan: `"##WebService"`)**
    *   Mesajın nasıl beklendiğini veya alınacağını belirtir. Değer, motorun mesajı almak için hangi mekanizmayı kullanacağını belirlemesine yardımcı olur. Olası değerler:
        *   `"##WebService"`: Mesajın bir web servisi çağrısı (genellikle `OperationRef` ile belirtilen) aracılığıyla alınacağını belirtir.
        *   `"##Unspecified"` veya boş: Uygulama motor tarafından belirlenir (örn. bir mesajlaşma kuyruğu, bir API uç noktası veya özel kod).
        *   Özel bir URI veya tanımlayıcı: Belirli bir mesajlaşma teknolojisine veya kaynağa işaret edebilir.
*   **`Instantiate` : `bool` (Öznitelik: `instantiate`, Varsayılan: `false`)**
    *   Eğer `true` olarak ayarlanırsa, bu göreve karşılık gelen mesaj geldiğinde yeni bir süreç örneği başlatılır. Bu özellik `ReceiveTask` için nadiren kullanılır; genellikle Mesaj Başlangıç Olayları (Message Start Events) için geçerlidir.
*   **`MessageRef` : `System.Xml.XmlQualifiedName?` (Öznitelik: `messageRef`)**
    *   Beklenen `Message` elemanına bir referans (`QName`). Bu `Message` elemanı genellikle BPMN dosyasında tanımlanır ve alınacak verinin yapısını (`itemRef`) temsil edebilir.
*   **`OperationRef` : `System.Xml.XmlQualifiedName?` (Öznitelik: `operationRef`)**
    *   Eğer `Implementation` `"##WebService"` ise veya başka bir operasyon tabanlı alma mekanizması kullanılıyorsa, mesajı almak için kullanılacak olan WSDL veya başka bir `Interface` içindeki operasyona bir referans (`QName`) içerir.

## İlişkili Özellikler (Activity'den Miras Alınan)

*   **`IoSpecification` / `DataOutputAssociation`**: Alınan mesajın içeriğini veya verisini süreç değişkenlerine yazmak için kullanılır.

## Kullanım

Süreç motoru, BPMN modelinde bir `receiveTask` elemanıyla karşılaştığında bir `ReceiveTask` nesnesi oluşturur. Motor:
1.  `MessageRef` ve potansiyel olarak `OperationRef` veya `Implementation`'a göre bir `EventSubscription` (türü `"message"`) oluşturur.
2.  İlgili `Execution`'ı bu görevde beklemeye alır.
3.  Beklenen mesaj (`EventSubscription` ile eşleşen) sisteme ulaştığında:
    a.  İlgili `EventSubscription` bulunur ve tüketilir.
    b.  Mesajın içeriği (`DataOutputAssociation` kullanarak) süreç değişkenlerine yazılır.
    c.  Bekleyen `Execution` devam ettirilir. 