using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Core.Interfaces
{
    public interface ICaseTeamService
    {
        /// <summary>
        /// إضافة عضو جديد لفريق القضية
        /// </summary>
        Task<CaseTeam> AddTeamMemberAsync(int caseId, int lawyerId, string role, bool checkPowerOfAttorney = true);

        /// <summary>
        /// تعيين وكالة مشتقة لعضو فريق القضية
        /// </summary>
        Task<bool> AssignDerivedPowerOfAttorneyAsync(CaseTeam caseTeam, int derivedPowerOfAttorneyId);

        /// <summary>
        /// تعيين وكالة مشتقة باستخدام معرف CaseTeam
        /// </summary>
        Task<bool> AssignDerivedPowerOfAttorneyAsync(int caseTeamId, int derivedPowerOfAttorneyId);

        /// <summary>
        /// التحقق من الوكالة المشتقة وتعيينها تلقائياً
        /// </summary>
        Task<bool> CheckAndAssignDerivedPowerOfAttorney(CaseTeam caseTeam, Case caseEntity);

        /// <summary>
        /// التحقق من الوكالة المشتقة وتعيينها تلقائياً
        /// </summary>
        Task<bool> ValidateAndAssignPowerOfAttorneyAsync(int caseTeamId);

        /// <summary>
        /// جلب الوكالات المشتقة المتاحة للمحامي في القضية
        /// </summary>
        Task<List<DerivedPowerOfAttorney>> GetAvailableDerivedPowerOfAttorneysAsync(int caseId, int lawyerId);

        /// <summary>
        /// إزالة وكالة مشتقة من عضو الفريق
        /// </summary>
        Task<bool> RemoveDerivedPowerOfAttorneyAsync(int caseTeamId);

        /// <summary>
        /// تحديث حالة عضو الفريق (نشط/غير نشط)
        /// </summary>
        Task<bool> UpdateTeamMemberStatusAsync(int caseTeamId, bool isActive, DateTime? endDate = null);

        /// <summary>
        /// التحقق من صلاحية عضو الفريق للقيام بمهمة معينة
        /// </summary>
        Task<bool> ValidateTeamMemberAuthorityAsync(int caseTeamId, string requiredAuthority);

        /// <summary>
        /// جلب فريق القضية مع معلومات الوكالات المشتقة
        /// </summary>
        Task<List<CaseTeam>> GetCaseTeamWithPowerOfAttorneyAsync(int caseId);

        /// <summary>
        /// التحقق من أن المحامي لديه وكالة مشتقة نشطة للقضية
        /// </summary>
        Task<bool> HasActiveDerivedPowerOfAttorneyAsync(int caseId, int lawyerId);
    }
}