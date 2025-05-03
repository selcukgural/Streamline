# ComplexGateway

*   **BPMN Tipi:** Somut (Concrete)
*   **Amaç:** Diğer standart geçit türlerinin (Exclusive, Inclusive, Parallel) davranışlarının yeterli olmadığı karmaşık birleştirme ve ayırma senaryolarını modellemek için kullanılır. Davranışı genellikle bir `activationCondition` ile tanımlanır.
*   **Konum:** `Streamline.Domain.Schema.Gateways`
*   **Miras:** `Gateway` -> `FlowNode` -> `FlowElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Hem birleştirme (converging) hem de ayırma (diverging) için kullanılabilir.
    *   **Ayırma:** Genellikle `activationCondition` ifadesi, hangi giden akışların aktif edileceğini belirler.
    *   **Birleştirme:** Gelen akışlardan belirli bir kombinasyon geldiğinde (genellikle `activationCondition` ile tanımlanır) giden akışı tetikler.
*   **Özellikler:**
    *   `ActivationCondition` (`Expression`, `XmlElement`, İsteğe Bağlı): Geçidin ne zaman aktif olacağını (birleştirme için) veya hangi giden yolların seçileceğini (ayırma için) tanımlayan ifade. Bu ifadenin yorumlanması BPMN motoruna bağlıdır.
    *   `Default` (string, `XmlAttribute`, İsteğe Bağlı): Ayırma davranışında, eğer `ActivationCondition` sonucu hiçbir yolun seçilmemesine neden olursa, varsayılan olarak takip edilecek giden `SequenceFlow`'un ID'si.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Expression`: Aktivasyon koşulunu tutmak için.
*   **Önemli Noktalar:**
    *   Kullanımı nadirdir ve genellikle kaçınılması önerilir, çünkü süreç mantığını karmaşıklaştırır ve anlaşılması zor hale getirebilir.
    *   Standart geçitlerin yetersiz kaldığı çok özel durumlar için bir kaçış mekanizmasıdır.
    *   Motor tarafından desteklenmesi ve `activationCondition`'ın nasıl yorumlanacağı garanti değildir. 