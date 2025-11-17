using System.Linq.Expressions;

namespace LawOfficeManagement.Core.Interfaces
{
    /// <summary>
    /// واجهة عامة لإدارة عمليات قاعدة البيانات (CRUD) والاستعلامات المتقدمة.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        // ==============================
        // 🟢 عمليات CRUD الأساسية
        // ==============================
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetAll();


        // ==============================
        // 🔍 عمليات الاستعلام المتقدمة
        // ==============================

        /// <summary>
        /// استعلام مرن مع خيارات التصفية والترتيب والتضمين والتصفح.
        /// </summary>
        Task<IReadOnlyList<T>> GetFilteredAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null,
            int? skip = null,
            int? take = null
        );

        /// <summary>
        /// يعيد الكيان الأول الذي يطابق الشرط أو القيمة الافتراضية.
        /// </summary>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
                                     string? includeProperties = null);

        /// <summary>
        /// يعيد قيمة من نوع مخصص (Projection).
        /// </summary>
        Task<IReadOnlyList<TResult>> SelectAsync<TResult>(
            Expression<Func<T, bool>>? predicate,
            Expression<Func<T, TResult>> selector,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? skip = null,
            int? take = null
        );

        /// <summary>
        /// يتحقق ما إذا كان أي سجل يطابق الشرط المحدد.
        /// </summary>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// يعيد عدد السجلات مع إمكانية تمرير شرط.
        /// </summary>
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    }
}
