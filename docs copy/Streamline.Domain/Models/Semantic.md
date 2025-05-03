# Semantic.cs Sınıfları

**Namespace:** `Streamline.Domain.Models.BPMN`

## Açıklama

`Semantic.cs` dosyası, BPMN 2.0 (Business Process Model and Notation) standardının XML şemasından (XSD) otomatik olarak türetilmiş C# sınıflarını içerir. Bu sınıflar, `.bpmn` uzantılı XML dosyalarını ayrıştırmak (parsing/deserialization) ve BPMN modelinin anlamsal yapısını uygulama içinde temsil etmek için kullanılır. Dosya, `MonoXSD` gibi bir araç kullanılarak oluşturulmuştur.

Bu sınıflar, BPMN elemanlarını (örneğin, Aktiviteler, Olaylar, Ağ Geçitleri, Akışlar) ve bunların özelliklerini C# nesneleri olarak modellemektedir. `System.Xml.Serialization` namespace'indeki öznitelikler (attributes) kullanılarak XML elemanları ve öznitelikleri ile C# sınıfları ve özellikleri arasındaki eşleme sağlanır.

## Temel Sınıflar ve Yapılar

Dosya, BPMN spesifikasyonundaki hiyerarşiyi yansıtan birçok sınıf içerir. Bazı temel ve sık kullanılan üst sınıflar şunlardır:

*   `tBaseElement`: Tüm BPMN elemanları için temel soyut sınıf. ID ve dokümantasyon gibi ortak özellikleri içerir.
*   `tRootElement`: Süreç, İşbirliği, Veri Deposu gibi kök seviyesindeki elemanlar için temel sınıf.
*   `tFlowElement`: Bir süreç içindeki akışın parçası olabilen elemanlar (Aktiviteler, Ağ Geçitleri, Olaylar, Akışlar) için temel sınıf.
*   `tActivity`: Süreçte gerçekleştirilen işi temsil eden soyut sınıf (örn. `tTask`, `tSubProcess`).
*   `tEvent`: Süreçte meydana gelen bir olayı temsil eden soyut sınıf (örn. `tStartEvent`, `tEndEvent`, `tIntermediateCatchEvent`).
*   `tGateway`: Süreç akışının ayrıldığı veya birleştiği noktaları temsil eden soyut sınıf (örn. `tExclusiveGateway`, `tParallelGateway`).
*   `tSequenceFlow`: Süreç içindeki iki akış elemanı arasındaki sıralı akışı temsil eder.
*   `tDefinitions`: Bir BPMN XML dosyasının kök elemanını temsil eder ve tüm tanımları içerir.

## Kullanım

Bu sınıflar genellikle `Streamline.Engine` katmanında, bir BPMN süreci yüklendiğinde kullanılır. `System.Xml.Serialization.XmlSerializer` gibi bir araçla `.bpmn` dosyası okunarak `tDefinitions` nesnesine dönüştürülür. Motor daha sonra bu nesne ağacını kullanarak süreç örneğini (process instance) yürütür.

## Notlar

*   Bu dosya otomatik olarak oluşturulduğu için, manuel olarak değiştirilmemelidir. BPMN şemasında bir güncelleme olursa, dosyanın yeniden oluşturulması gerekebilir.
*   Sınıf ve özellik adları doğrudan BPMN 2.0 XML şemasından geldiği için, daha fazla detay için resmi [BPMN 2.0 Spesifikasyonu](https://www.omg.org/spec/BPMN/2.0/) referans alınmalıdır. 