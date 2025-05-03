# Runtime Enums

**Namespace:** `Streamline.Domain.Runtime`

Bu dosya, Streamline çalışma zamanı varlıklarının durumlarını temsil etmek için kullanılan numaralandırma (enum) türlerini içerir.

## ProcessInstanceState

Bir `ProcessInstance`'ın genel durumunu belirtir.

```csharp
public enum ProcessInstanceState
{
    /// <summary>Süreç örneği şu anda aktif olarak çalışıyor.</summary>
    Running,
    /// <summary>Süreç örneği geçici olarak durdurulmuş (askıya alınmış).</summary>
    Suspended,
    /// <summary>Süreç örneği başarıyla tamamlandı.</summary>
    Completed,
    /// <summary>Süreç örneği bir hata veya beklenmedik durum nedeniyle iptal edildi.</summary>
    Aborted,
    /// <summary>Süreç örneği dışarıdan bir müdahale ile (örn. yönetici tarafından) sonlandırıldı.</summary>
    Terminated
}
```

## ActivityInstanceState

Bir `ActivityInstance`'ın (bir akış elemanının çalışma zamanı örneği) durumunu belirtir.

```csharp
public enum ActivityInstanceState
{
    /// <summary>Akış elemanı şu anda yürütülüyor veya bir sonraki adıma geçmeyi bekliyor.</summary>
    Active,
    /// <summary>Akış elemanının yürütülmesi başarıyla tamamlandı.</summary>
    Completed,
    /// <summary>Akış elemanının yürütülmesi sırasında bir hata oluştu.</summary>
    Faulted,
    /// <summary>Akış elemanının yürütülmesi zorla sonlandırıldı (örn. kesintili bir sınır olayı veya sonlandırma uç olayı tarafından).</summary>
    Terminated,
    /// <summary>Akış elemanının yürütülmesi iptal edildi (örn. telafi (compensation) mekanizması veya kesintili olmayan bir olay alt süreci tarafından).</summary>
    Cancelled
}
```

## Kullanım

Bu enum'lar, ilgili varlıkların (`ProcessInstance`, `ActivityInstance`) `State` özelliklerinde kullanılır ve süreçlerin ve aktivitelerin mevcut durumunu anlamak ve yönetmek için temel oluşturur. 