# DomainEvent (Soyut Sınıf)

**Namespace:** `Streamline.Domain.Events` (Varsayılan)

Bu soyut (abstract) sınıf, projedeki tüm Domain Event sınıfları için temel bir yapı sağlar. Domain Event\'ler, iş alanında (domain) gerçekleşen ve diğer sistem bileşenlerinin ilgilenebileceği önemli olayları temsil eder.

Bu base sınıf genellikle ortak özellikler (örn. olay zaman damgası) veya davranışlar içerebilir, ancak mevcut tanımda (muhtemelen) sadece bir işaretleyici (marker) görevi görür veya MediatR `INotification` gibi arayüzleri implemente edebilir.

Diğer event sınıfları (`ActivityFailedEvent` vb.) bu sınıftan türetilmelidir. 