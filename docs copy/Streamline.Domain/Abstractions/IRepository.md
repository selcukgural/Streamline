# IRepository<T>

**Namespace:** `Streamline.Domain.Abstractions`

Bu jenerik (generic) arayüz, belirli bir varlık (entity) türü (`T`) için temel CRUD (Create, Read, Update, Delete) ve sorgulama işlemlerini tanımlayan Depo (Repository) desenini soyutlar. Veri erişim katmanının detaylarını iş mantığından gizler.

`T`, `EntityBase`\'den türeyen bir sınıf olmalıdır.

## Metotlar

*   **`Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)`:**
    *   Belirtilen ID\'ye sahip varlığı zamanuyumsuz (asynchronously) olarak getirir.
    *   Varlık bulunamazsa `null` döner.

*   **`Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken)`:**
    *   Belirtilen spesifikasyona (`spec`) uyan *ilk* varlığı veya bulunamazsa `null`\'ı zamanuyumsuz olarak getirir.
    *   Spesifikasyon deseni (Specification Pattern), karmaşık sorgu kriterlerini yeniden kullanılabilir nesneler içinde kapsüllemek için kullanılır.

*   **`Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken)`:**
    *   Belirtilen projeksiyon spesifikasyonuna (`spec`) uyan *ilk* varlığın belirtilen sonuca (`TResult`) dönüştürülmüş halini veya bulunamazsa `null`\'ı zamanuyumsuz olarak getirir.
    *   Veritabanından sadece gerekli alanları çekmek (projeksiyon) için kullanılır.

*   **`Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken)`:**
    *   Depodaki *tüm* varlıkları zamanuyumsuz olarak getirir.
    *   Dikkat: Büyük tablolar için kullanmaktan kaçınılmalıdır.

*   **`Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken)`:**
    *   Belirtilen spesifikasyona (`spec`) uyan *tüm* varlıkları zamanuyumsuz olarak getirir.

*   **`Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken)`:**
    *   Belirtilen projeksiyon spesifikasyonuna (`spec`) uyan *tüm* varlıkların belirtilen sonuca (`TResult`) dönüştürülmüş listesini zamanuyumsuz olarak getirir.

*   **`Task AddAsync(T entity, CancellationToken cancellationToken)`:**
    *   Yeni bir varlığı depoya zamanuyumsuz olarak ekler.
    *   Değişiklikler `IUnitOfWork.SaveChangesAsync` çağrılana kadar veritabanına yansıtılmaz.

*   **`Task UpdateAsync(T entity, CancellationToken cancellationToken)`:**
    *   Mevcut bir varlığı depoda zamanuyumsuz olarak günceller.
    *   Değişiklikler `IUnitOfWork.SaveChangesAsync` çağrılana kadar veritabanına yansıtılmaz.

*   **`Task DeleteAsync(T entity, CancellationToken cancellationToken)`:**
    *   Mevcut bir varlığı depodan zamanuyumsuz olarak siler.
    *   Değişiklikler `IUnitOfWork.SaveChangesAsync` çağrılana kadar veritabanına yansıtılmaz.

*   **`Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken)`:**
    *   Belirtilen spesifikasyona (`spec`) uyan varlıkların sayısını zamanuyumsuz olarak döndürür.

*   **`Task<int> CountAsync(CancellationToken cancellationToken)`:**
    *   Depodaki *tüm* varlıkların sayısını zamanuyumsuz olarak döndürür.

*   **`Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken cancellationToken)`:**
    *   Belirtilen spesifikasyona (`spec`) uyan en az bir varlık olup olmadığını zamanuyumsuz olarak kontrol eder.

*   **`Task<bool> AnyAsync(CancellationToken cancellationToken)`:**
    *   Depoda en az bir varlık olup olmadığını zamanuyumsuz olarak kontrol eder. 