# Operation

*   **BPMN Tipi:** Somut (Concrete) - `BaseElement`
*   **Amaç:** Bir `Interface` içinde tanımlanan, genellikle bir girdi mesajı alan, bir çıktı mesajı döndüren ve potansiyel olarak hatalar fırlatabilen tek bir hizmet veya fonksiyon çağrısını temsil eder.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir `Interface` elemanının `Operation` koleksiyonu içinde bulunur.
    *   Giriş parametrelerini temsil eden bir `Message`'a (`inMessageRef`) referans verir.
    *   Çıkış parametrelerini veya dönüş değerini temsil eden bir `Message`'a (`outMessageRef`) referans verebilir.
    *   Operasyon sırasında oluşabilecek ve fırlatılabilecek `Error` tanımlarına (`errorRef`) referans verebilir.
    *   `ServiceTask`, `SendTask`, `ReceiveTask` gibi mesajlaşma aktiviteleri ve `MessageEventDefinition` (mesaj olayları), genellikle belirli bir `Interface`'deki bir `Operation`'ı gerçekleştirmek veya beklemek üzere yapılandırılır (`operationRef` özelliği ile).
    *   `InputOutputBinding`, bir `CallableElement`'in bu operasyonu kendi `DataInput`/`DataOutput`'ları ile nasıl eşleştirdiğini tanımlar.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`, Zorunlu): Operasyonun adı.
    *   `InMessageRef` (`XmlQualifiedName`, `XmlElement`, Zorunlu): Operasyonun giriş mesajını tanımlayan `Message` elemanının ID'sine referans.
    *   `OutMessageRef` (`XmlQualifiedName`, `XmlElement`, İsteğe Bağlı): Operasyonun (varsa) çıkış mesajını tanımlayan `Message` elemanının ID'sine referans.
    *   `ErrorRef` (Collection<`XmlQualifiedName`>, `XmlElement`): Bu operasyonun fırlatabileceği `Error` elemanlarının ID'lerine referanslar.
    *   `ImplementationRef` (`XmlQualifiedName`, `XmlAttribute`, İsteğe Bağlı): Operasyonun somut implementasyonuna (örn. WSDL operasyonu, Java metodu) bir referans.
    *   (`BaseElement`'ten `Id`, `Documentation`, `ExtensionElements`, `AnyAttribute` miras alır.)
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.Interface` (Bu elemanı içerir)
    *   `Streamline.Domain.Schema.Common.Message` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.Error` (dolaylı, referans yoluyla)
    *   `Streamline.Domain.Schema.Common.BaseElement` (Miras aldığı sınıf)
*   **Önemli Noktalar:**
    *   Servis etkileşimlerinin temel birimidir.
    *   Web Servisleri (WSDL) veya diğer RPC (Remote Procedure Call) mekanizmalarıyla güçlü bir benzerlik gösterir.

## XML Örneği

`<operation>` elemanı bir `<interface>` içinde tanımlanır:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL"
             xmlns:tns="http://example.com/service"
             xmlns:data="http://example.com/payloads"
             targetNamespace="http://example.com/service">

  <!-- Gerekli Message ve Error tanımları -->
  <itemDefinition id="ItemDef_RequestData" structureRef="data:RequestPayload"/>
  <itemDefinition id="ItemDef_ResponseData" structureRef="data:ResponsePayload"/>
  <itemDefinition id="ItemDef_FaultData" structureRef="data:FaultDetails"/>

  <message id="Msg_ServiceRequest" name="Service Request Message" itemRef="ItemDef_RequestData"/>
  <message id="Msg_ServiceResponse" name="Service Response Message" itemRef="ItemDef_ResponseData"/>
  <message id="Msg_ServiceFault" name="Service Fault Message" itemRef="ItemDef_FaultData"/>

  <error id="Error_ServiceFault" name="Service Fault" errorCode="SVC-001" structureRef="ItemDef_FaultData"/>

  <!-- Interface Tanımı -->
  <interface id="Intf_MyService" name="My Service Interface">
    <documentation>Interface for accessing My Service.</documentation>

    <!-- Operasyon Tanımı -->
    <operation id="Op_ProcessData" name="Process Data Operation" implementationRef="tns:processDataMethod">
      <inMessageRef>Msg_ServiceRequest</inMessageRef>
      <outMessageRef>Msg_ServiceResponse</outMessageRef>
      <errorRef>Error_ServiceFault</errorRef>
    </operation>

    <!-- Başka bir Operasyon (sadece giriş mesajı) -->
    <operation id="Op_Notify" name="Notify Operation">
      <inMessageRef>Msg_Notification</inMessageRef> <!-- Başka bir mesaj tanımı varsayılıyor -->
    </operation>

  </interface>

  <!-- Bu interface/operation'ı kullanan bir ServiceTask -->
  <process id="Proc_Client" isExecutable="true">
    <serviceTask id="Task_CallService" name="Call My Service" operationRef="Op_ProcessData">
      <!-- ioSpecification, dataInputAssociation, dataOutputAssociation... -->
    </serviceTask>
  </process>

</definitions>
``` 