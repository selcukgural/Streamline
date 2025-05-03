# EntityBase (Soyut Sınıf)

**Namespace:** `Streamline.Domain.Runtime`

## Açıklama

`EntityBase`, Streamline projesindeki tüm çalışma zamanı varlıkları (Runtime Entities - örn. `ProcessInstance`, `Execution`, `ActivityInstance`) için temel (base) soyut sınıftır. Tüm varlıklara ortak bir kimlik (`Id`) ve Domain Event'leri yönetme yeteneği sağlar.

## Özellikler

*   **`Id` { get; protected set; } : `Guid`**
    *   Varlığın benzersiz kimliğidir. Dağıtık sistemlerle uyumluluk için `Guid` türündedir.
    *   Varlık oluşturulduğunda otomatik olarak bir Guid atanır.
    *   `protected set` erişimcisi, ID'nin yalnızca varlığın kendisi veya ondan türeyen sınıflar tarafından (genellikle constructor veya ORM tarafından) ayarlanabilmesini sağlar.

*   **`DomainEvents` { get; } : `IReadOnlyCollection<DomainEvent>`**
    *   Bu varlık tarafından tetiklenen Domain Event'lerinin salt okunur (read-only) bir koleksiyonunu döndürür.
    *   Bu olaylar, genellikle bir iş kuralı ihlal edildiğinde veya önemli bir durum değişikliği olduğunda varlığın kendisi tarafından (`AddDomainEvent` metoduyla) eklenir.
    *   Koleksiyon dışarıdan değiştirilemez.
    *   API üzerinden serileştirilmemesi için `[JsonIgnore]` özniteliği ile işaretlenmiştir.

## Metotlar

*   **`protected void AddDomainEvent(DomainEvent domainEvent)`**
    *   Varlığın `DomainEvents` koleksiyonuna yeni bir domain event ekler.
    *   Bu metot `protected` olduğu için yalnızca türetilmiş sınıfların içinden çağrılabilir.

*   **`public void ClearDomainEvents()`**
    *   Varlığın `DomainEvents` koleksiyonundaki tüm olayları temizler.
    *   Bu metot genellikle olaylar `IUnitOfWork` tarafından işlendikten ve dağıtıldıktan (dispatched) sonra çağrılır, böylece aynı olaylar tekrar işlenmez.

## Kullanım

`ProcessInstance`, `Execution`, `ActivityInstance`, `Incident`, `Variable`, `EventSubscription`, `Job` gibi tüm ana çalışma zamanı varlıkları bu sınıftan türetilmelidir. Bu sayede ortak bir ID yapısına ve domain event yayınlama yeteneğine sahip olurlar. 