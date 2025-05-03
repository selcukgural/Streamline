# Expression

*   **BPMN Tipi:** Soyut (Abstract) - Spesifikasyonda BaseElement'ten türetilir ancak genellikle metin içeriği de taşıdığı için `BaseElementWithMixedContent`'ten türetilmiş.
*   **Amaç:** BPMN modelinde çeşitli yerlerde kullanılan ifadeleri (genellikle koşullar veya hesaplamalar) temsil etmek için soyut bir temel sınıf sağlar. Özellikle `SequenceFlow`'ların koşullarını veya `Gateway` kararlarını belirtmek için kullanılır.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElementWithMixedContent` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bu sınıf, ifadenin kendisini genellikle metin içeriği olarak tutar (`Value` özelliği `BaseElementWithMixedContent`'ten gelir).
    *   En yaygın alt sınıfı `FormalExpression`'dır.
*   **Özellikler:**
    *   (BaseElementWithMixedContent'ten gelen `Value` özelliği ifadenin metnini tutar)
*   **Önemli Noktalar:**
    *   Soyut olduğu için doğrudan kullanılmaz, genellikle `FormalExpression` olarak kullanılır.
    *   İfadenin dili (`expressionLanguage` özelliği) genellikle `FormalExpression` içinde belirtilir. 