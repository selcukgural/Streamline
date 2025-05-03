# Resource

*   **BPMN Tipi:** Somut (Concrete) - `RootElement`
*   **Amaç:** Süreç aktivitelerini gerçekleştirmek için gereken varlıkları (insan, ekipman, yazılım, malzeme vb.) temsil eder. Genellikle `Activity` veya `Process` elemanlarına bağlanarak kimin veya neyin bir işi yapacağını belirtir.
*   **Konum:** `Streamline.Domain.Schema.Common`
*   **Miras:** `RootElement` -> `BaseElement`
*   **Uygulama & Davranış:**
    *   Bir kaynağın adını ve isteğe bağlı parametrelerini tanımlar.
    *   `Process` elemanının altında veya `Definitions` altında global olarak tanımlanabilir.
    *   `ResourceRole`, `Performer` gibi elemanlar aracılığıyla aktivitelere bağlanır.
*   **Özellikler:**
    *   `Name` (string, `XmlAttribute`, **Zorunlu**): Kaynağın adı (örn. "Müşteri Temsilcisi", "Onay Sistemi").
    *   `ResourceParameter` (Collection<`ResourceParameter`>, `XmlElement`): Kaynağa özgü parametreleri (örn. yetenek, miktar) tanımlayan bir koleksiyon.
*   **Bağımlılıklar:**
    *   `Streamline.Domain.Schema.Common.ResourceParameter`: Kaynak parametrelerini tutan sınıf.
*   **Önemli Noktalar:**
    *   Kaynak atamaları (resource assignment) ve süreç simülasyonu için kullanılır.
    *   Özellikle `UserTask` elemanlarında, görevin kime atanacağını belirlemek için `PotentialOwner` içinde `Resource` referansları kullanılabilir. 

## XML Örneği

`<resource>` tanımları genellikle `<definitions>` altında yer alır:

```xml
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL" ...>

  <!-- Gerekli ItemDefinition'lar -->
  <itemDefinition id="ItemDef_String" structureRef="xsd:string"/>
  <itemDefinition id="ItemDef_Int" structureRef="xsd:int"/>

  <!-- Kaynak Tanımları -->
  <resource id="Res_Accountant" name="Accountant">
    <documentation>Certified public accountant.</documentation>
    <resourceParameter id="Param_AccountantLevel" name="Level" type="ItemDef_String" isRequired="true"/>
    <resourceParameter id="Param_ExperienceYears" name="ExperienceYears" type="ItemDef_Int"/>
  </resource>

  <resource id="Res_ApprovalSystem" name="Approval System Software">
    <resourceParameter id="Param_SystemVersion" name="Version" type="ItemDef_String"/>
  </resource>

  <resource id="Res_ManagerRole" name="Manager Role"/> <!-- Parametresiz basit rol tanımı -->

  <!-- Bu kaynakları kullanan bir Process -->
  <process id="Proc_ExpenseReport" name="Expense Report Approval">
    <potentialOwner resourceRef="Res_ManagerRole"> <!-- Basit Rol Ataması -->
      <resourceAssignmentExpression>
         <!-- Alternatif olarak ifade ile de atanabilir -->
      </resourceAssignmentExpression>
    </potentialOwner>

    <userTask id="Task_VerifyReport" name="Verify Expense Report">
       <potentialOwner resourceRef="Res_Accountant">
          <resourceAssignmentExpression>
             <!-- İfade ile belirli bir seviyedeki muhasebeci seçilebilir -->
             <formalExpression>${resource.find("Accountant", level="Senior")}</formalExpression>
          </resourceAssignmentExpression>
       </potentialOwner>
    </userTask>
    <!-- ... -->
  </process>

</definitions>
``` 