# IUnitOfWork

**Namespace:** `Streamline.Domain.Abstractions`

Bu arayüz, İş Birimi (Unit of Work) desenini temsil eder. Amacı, bir iş işlemi (business transaction) sırasında yapılan tüm veri değişikliklerini (ekleme, güncelleme, silme) tek bir atomik işlem olarak yönetmektir. Değişiklikler toplu halde kaydedilir veya geri alınır.

## Metotlar

*   **`IRepository<T> GetRepository<T>() where T : EntityBase`:**
    *   Belirtilen varlık türü (`T`) için bir Depo (Repository) örneği döndürür.
    *   Bu metot, iş mantığının belirli varlıklar üzerinde işlem yapmak için ihtiyaç duyduğu depolara erişmesini sağlar.

*   **`Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)`:**
    *   Bu iş birimi kapsamında takip edilen tüm değişiklikleri (eklenen, güncellenen, silinen varlıklar) veritabanına zamanuyumsuz olarak kaydeder.
    *   Başarıyla kaydedilen değişiklik sayısını (etkilenen satır sayısı) döndürür.
    *   İşlemin atomik olmasını sağlar; ya tüm değişiklikler kaydedilir ya da hiçbiri kaydedilmez (veritabanı transaction\'ı kullanarak).

## Kullanım

Genellikle bir Application Service veya Command Handler içinde `IUnitOfWork` enjekte edilir. Handler, gerekli depoları `GetRepository` ile alır, varlıklar üzerinde değişiklikler yapar ve işlemin sonunda `SaveChangesAsync`\'i çağırarak tüm değişiklikleri tek seferde veritabanına yazar. 