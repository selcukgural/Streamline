# Import

*   **BPMN Tipi:** Yardımcı Eleman
*   **Amaç:** BPMN `Definitions` dosyasına dış XML Şeması (XSD), WSDL veya başka bir BPMN dosyasından tanımları (veri tipleri, mesajlar, arayüzler vb.) dahil etmek için kullanılır. Bu, tanımların yeniden kullanılmasını ve modülerliği sağlar.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** Yok (Doğrudan `Definitions` elemanının bir koleksiyon özelliği içinde kullanılır)
*   **Uygulama & Davranış:**
    *   Bir `Definitions` elemanının `<import>` alt etiketi olarak XML'de yer alır.
    *   `importType` ile hangi tür tanımın (örn. XSD, WSDL) içe aktarıldığını belirtir.
    *   `location` ile tanım dosyasının nerede bulunduğunu belirtir.
    *   `namespace` ile içe aktarılan tanımların BPMN dosyasında hangi isim alanı altında erişilebilir olacağını belirtir.
    *   Motor veya araç, bu bilgileri kullanarak dış tanımları yükler ve BPMN modelindeki referansları (örn. `ItemDefinition`'ın `structureRef`'i, `Message`'ın `itemRef`'i, `Operation`'ın giriş/çıkış mesajları) çözümler.
*   **Özellikler:**
    *   `Namespace` (string, `XmlAttribute`, Zorunlu): İçe aktarılan tanımların hedef isim alanı. Bu isim alanı, BPMN dosyasındaki referanslarda (genellikle `XmlQualifiedName` içinde) kullanılır.
    *   `Location` (string, `XmlAttribute`, Zorunlu): İçe aktarılacak dosyanın URI'si (göreli veya mutlak dosya yolu, URL vb.).
    *   `ImportType` (string, `XmlAttribute`, Zorunlu): İçe aktarılan dosyanın türünü tanımlayan bir URI. Yaygın türler:
        *   `http://www.w3.org/2001/XMLSchema` (XSD için)
        *   `http://schemas.xmlsoap.org/wsdl/` (WSDL 1.1 için)
        *   `http://www.w3.org/ns/wsdl` (WSDL 2.0 için)
        *   `http://www.omg.org/spec/BPMN/20100524/MODEL` (Başka bir BPMN dosyası için)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Definitions` (Bu elemanı içerir)
*   **Önemli Noktalar:**
    *   Karmaşık veri yapılarını veya servis tanımlarını doğrudan BPMN dosyasına gömmek yerine dış referanslarla yönetmeyi sağlar.
    *   Kurumsal veri modelleri veya standart servis arayüzleri ile entegrasyonu kolaylaştırır.

## XML Örneği

`<import>` elemanı `<definitions>` altında kullanılır:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:xsd="http://www.w3.org/2001/XMLSchema"
             xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/"
             xmlns:order="http://example.com/order-schema"
             xmlns:ship="http://example.com/shipping-service"
             targetNamespace="http://example.com/main-process">

  <!-- Bir XML Şeması (XSD) içe aktarma -->
  <import importType="http://www.w3.org/2001/XMLSchema"
          location="OrderSchema.xsd"
          namespace="http://example.com/order-schema" />

  <!-- Bir WSDL dosyası içe aktarma -->
  <import importType="http://schemas.xmlsoap.org/wsdl/"
          location="ShippingService.wsdl"
          namespace="http://example.com/shipping-service" />

  <!-- Başka bir BPMN dosyası içe aktarma (örneğin, global tipler için) -->
  <import importType="http://www.omg.org/spec/BPMN/20100524/MODEL"
          location="GlobalTypes.bpmn"
          namespace="http://example.com/global-types"/>

  <!-- İçe aktarılan tanımları kullanan diğer elemanlar -->
  <itemDefinition id="ItemDef_Order" structureRef="order:OrderType" />
  <message id="Msg_ShippingRequest" itemRef="ship:ShippingRequestMessage"/>
  <interface id="Intf_Shipping" name="Shipping Interface" implementationRef="ship:ShippingPortType"/>

  <!-- ... süreç tanımları ... -->

</definitions>
``` 