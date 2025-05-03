# ProcessType

*   **BPMN Tipi:** Enum (Enumeration)
*   **Amaç:** Bir `Process` elemanının genel türünü veya amacını belirtir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Değerler:**
    *   `None`: Sürecin türü belirtilmemiştir (Varsayılan değer). Bu genellikle yürütülemez veya dahili kullanım için olan süreçler için kullanılır.
    *   `Public`: Sürecin, birden fazla katılımcı (`Participant`) arasındaki etkileşimi (mesaj alışverişi sırası gibi) temsil eden soyut bir süreç olduğunu belirtir. Genellikle iç uygulama detaylarını içermez ve bir `Collaboration` diyagramında diğer katılımcılarla olan arayüzü göstermek için kullanılır.
    *   `Private`: Sürecin, tek bir kuruluş veya katılımcı içindeki belirli bir iş akışını temsil ettiğini belirtir. Bu süreçler yürütülebilir (`isExecutable=true`) veya yürütülemez (analiz veya dokümantasyon amaçlı) olabilir.
*   **Kullanım:**
    *   `Process` elemanının `processType` özniteliğinde kullanılır.
*   **Önemli Noktalar:**
    *   Sürecin kapsamını ve amacını netleştirmeye yardımcı olur.
    *   `Public` süreçler genellikle `Collaboration` diyagramlarında etkileşimi modellemek için, `Private` süreçler ise belirli bir `Participant`'ın iç mantığını modellemek için kullanılır. 