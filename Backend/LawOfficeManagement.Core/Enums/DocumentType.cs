namespace LawOfficeManagement.Core.Enums
{
    public enum DocumentType
    {
        Image = 1,
        Pdf = 2,
        Word = 3,
        Excel = 4,
        Other = 5
    }

    public enum EntityOwnerType
    {
        Case = 1,
        Office = 3,
        CaseSession =2 ,
        /// <summary>
        /// الشهود
        /// </summary>
        CaseWitness =5,
        /// <summary>
        /// الأدلة
        /// </summary>
        CaseEvidence =6,
        Other = 7,

    }
}
