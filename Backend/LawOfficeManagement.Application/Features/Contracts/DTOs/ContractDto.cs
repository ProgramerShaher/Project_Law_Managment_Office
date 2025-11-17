using LawOfficeManagement.Core.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.Contracts.DTOs
{
    public class ContractDto
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ContractDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int CaseId { get; set; }
        public string CaseTitle { get; set; } = string.Empty;
        public ContractStatus Status { get; set; }
        public FinancialAgreementType FinancialAgreementType { get; set; }
        public decimal? TotalCaseAmount { get; set; }
        public int? Percentage { get; set; }
        public decimal? FinalAgreedAmount { get; set; }
        public string? ContractDocumentUrl { get; set; }
        public decimal? CalculatedAmount { get; set; }
    }
    public class ChangeContractStatusDto
    {
        public ContractStatus Status { get; set; }
    }
    public class CreateContractDto
    {
        [Required]
        [MaxLength(50)]
        public string ContractNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ContractDescription { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int CaseId { get; set; }

        public ContractStatus Status { get; set; } = ContractStatus.Active;
        public FinancialAgreementType FinancialAgreementType { get; set; }
        public decimal? TotalCaseAmount { get; set; }

        [Range(0, 100)]
        public int? Percentage { get; set; }

        public decimal? FinalAgreedAmount { get; set; }
        public string? ContractDocumentUrl { get; set; }
    }

    public class UpdateContractDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ContractDescription { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public ContractStatus Status { get; set; }
        public FinancialAgreementType FinancialAgreementType { get; set; }
        public decimal? TotalCaseAmount { get; set; }

        [Range(0, 100)]
        public int? Percentage { get; set; }

        public decimal? FinalAgreedAmount { get; set; }
        public string? ContractDocumentUrl { get; set; }
    }
    public class ContractsCountDto
    {
        public int Total { get; set; }
        public int Active { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
    }
}
