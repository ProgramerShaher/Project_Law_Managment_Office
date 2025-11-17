namespace LawOfficeManagement.Core.Entities
{
    /// <summary>
    /// (نوع العميل (فردي - شخصي - محامي الخصم -شاهد - الموكل
    /// </summary>
    public class ClientRole : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// كل ClientRole يمكن أن يكون له مجموعة من العملاء المرتبطين به.
        /// </summary>

        // علاقة عكسية مع العملاء
        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}
