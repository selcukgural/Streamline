# Event-Based Gateway

**Namespace:** `Streamline.Domain.Schema.Gateways`
**Base Class:** `Streamline.Domain.Schema.Flow.Gateway`
**BPMN Element:** `eventBasedGateway`

## Açıklama

`EventBasedGateway` (Olay Tabanlı Ağ Geçidi), bir süreç akışında bir dallanma noktasıdır ve bu noktadan sonraki yol, takip eden bir dizi olaydan hangisinin *ilk* gerçekleştiğine bağlı olarak belirlenir. Bu ağ geçidi, belirli bir noktada birden fazla farklı olayın meydana gelme olasılığı olduğunda ve süreç akışının bu olaylardan yalnızca birine yanıt vermesi gerektiğinde kullanılır.

Bir token (yürütme) `EventBasedGateway`'e ulaştığında, ağ geçidinden çıkan her bir Sıra Akışı (Sequence Flow), bir Yakalama Olayı'na (Catch Event) veya bir Mesaj Alma Görevi'ne (Receive Task) bağlanmalıdır. Token, bu takip eden olaylardan birinin tetiklenmesini bekler. İlk tetiklenen olay, token'ın ilerleyeceği yolu belirler ve diğer potansiyel yollar devre dışı bırakılır.

## Davranış

1.  **Token Varışı:** Bir token `EventBasedGateway`'e ulaştığında, token ağ geçidinde bekler.
2.  **Olay Bekleme:** Ağ geçidi, kendisinden çıkan Sıra Akışlarına bağlı olan Yakalama Olayları'nın (örneğin, Message Intermediate Catch Event, Timer Intermediate Catch Event) veya Mesaj Alma Görevleri'nin tetiklenmesini bekler.
3.  **İlk Olay Tetiklenir:** Takip eden olaylardan biri tetiklendiğinde (örneğin, beklenen mesaj alındığında veya zamanlayıcı dolduğunda), token o olayın yolunu takip eder.
4.  **Diğer Yollar Devre Dışı:** İlk olay tetiklendikten sonra, `EventBasedGateway`'den çıkan diğer tüm yollar etkinliğini yitirir ve bu yollara bağlı olaylar artık o token için tetiklenemez (Exclusive davranış).

**Önemli Not:** `EventBasedGateway`'den çıkan Sıra Akışlarının hedefi *doğrudan* bir Yakalama Olayı veya Mesaj Alma Görevi olmalıdır. Bir Script Task veya Service Task gibi başka bir aktiviteye bağlanamazlar.

## Özellikler

*   **`Instantiate` (Boolean):** Varsayılan değeri `false`'tur. Eğer `true` olarak ayarlanırsa, bu ağ geçidi yeni bir süreç örneği başlatabilir. Bu genellikle bir süreç başlangıcında veya bir Olay Alt Süreci (Event Sub-Process) içinde kullanılır. Süreç örneği, takip eden olaylardan biri tetiklendiğinde başlar.
*   **`EventGatewayType` (Enum - `EventBasedGatewayType`):** Ağ geçidinin davranışını belirler.
    *   `Exclusive` (Varsayılan): İlk tetiklenen olay kazanır ve diğer yollar iptal edilir.
    *   `Parallel`: (Daha az yaygın) Takip eden tüm olayların tetiklenmesini gerektirir. BPMN standardında bu tür genellikle `Inclusive Gateway` ile modellenir, ancak şema bu seçeneği sunabilir.

## Kullanım

`EventBasedGateway`, bir süreç akışında belirsizliğin olduğu ve akışın hangi yönde devam edeceğinin dışsal bir olaya veya zamana bağlı olduğu durumları modellemek için kullanılır.

**Örnek Senaryolar:**

*   Bir siparişin onaylanmasını beklerken, müşteriden bir iptal mesajı gelmesi veya belirli bir süre içinde onay gelmemesi (zamanlayıcı olayı) durumlarından hangisinin önce gerçekleşeceğine göre akışı yönlendirmek.
*   Bir destek talebi oluşturulduktan sonra, müşteriden ek bilgi gelmesini veya belirli bir süre içinde yanıt gelmezse talebin otomatik olarak kapatılmasını beklemek.

`EventBasedGateway`, süreci daha dinamik ve olaylara duyarlı hale getirmek için güçlü bir araçtır. 