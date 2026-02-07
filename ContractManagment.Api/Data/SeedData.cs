using ContractManagment.Api.Models;
using ContractManagment.Api.Models.ClientsModels;
using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;

namespace ContractManagment.Api.Data;

public static class SeedData
{
    ////////////////////////
//    public static List<Industry> industries =
//[
//    new() { Name = "Technology" },
//    new() { Name = "Finance" },
//    new() { Name = "Healthcare" },
//    new() { Name = "Education" },
//    new() { Name = "Manufacturing" }
//];
    ////////////////////////
//    public static List<Categories> categories =
//[
//    new() { Name = "Software", Description = "IT & software contracts",     },
//    new() { Name = "Legal", Description = "Legal agreements",     },
//    new() { Name = "HR", Description = "Human resources",     },
//    new() { Name = "Finance", Description = "Financial contracts",     },
//    new() { Name = "Operations", Description = "Operational contracts",     }
//];
    ////////////////////////
//    public static List<ContractDocumentType> documentTypes =
//[
//    new() { Name = "Agreement" },
//    new() { Name = "Invoice" },
//    new() { Name = "Proposal" },
//];
//    ////////////////////////
//    public static List<Clients> clients =
//[
//    new()
//    {
//        ClientName = "TechNova",
//        ContactPerson = "Ali Hassan",
//        Email = "ali@technova.com",
//        PhoneNumber = "770001",
//        Address = "Baghdad",
//        CompanyRegistrationNumber = "REG-001",
//        IndustryId = industries[0].Id,


//    },
//    new()
//    {
//        ClientName = "FinCorp",
//        ContactPerson = "Sara Ahmed",
//        Email = "sara@fincorp.com",
//        PhoneNumber = "770002",
//        Address = "Erbil",
//        CompanyRegistrationNumber = "REG-002",
//        IndustryId = industries[1].Id,


//    },
//    new()
//    {
//        ClientName = "HealthPlus",
//        ContactPerson = "Omar Khalid",
//        Email = "omar@healthplus.com",
//        PhoneNumber = "770003",
//        Address = "Basra",
//        CompanyRegistrationNumber = "REG-003",
//        IndustryId = industries[2].Id,


//    },
//    new()
//    {
//        ClientName = "EduSmart",
//        ContactPerson = "Noor Saleh",
//        Email = "noor@edusmart.com",
//        PhoneNumber = "770004",
//        Address = "Sulaymaniyah",
//        CompanyRegistrationNumber = "REG-004",
//        IndustryId = industries[3].Id,
        
//    },
//    new()
//    {
//        ClientName = "BuildPro",
//        ContactPerson = "Zaid Ali",
//        Email = "zaid@buildpro.com",
//        PhoneNumber = "770005",
//        Address = "Kirkuk",
//        CompanyRegistrationNumber = "REG-005",
//        IndustryId = industries[4].Id,


//    }
//];

    ////////////////////////
//    public static List<Contracts> contracts = new List<Contracts>
//{
//    new() { Title = "ERP System", ClinetId = clients[0].Id, CategoryId = categories[0].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1), ContractValue = 50000, ContractType = ContractType.FixedPrice,     },
//    new() { Title = "Legal Advisory", ClinetId = clients[1].Id, CategoryId = categories[1].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(6), ContractValue = 20000, ContractType = ContractType.Consulting,     },
//    new() { Title = "HR Outsourcing", ClinetId = clients[2].Id, CategoryId = categories[2].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1), ContractValue = 30000, ContractType = ContractType.Retainer,     },
//    new() { Title = "Audit Services", ClinetId = clients[3].Id, CategoryId = categories[3].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(9), ContractValue = 25000, ContractType = ContractType.TimeAndMaterial,     },
//    new() { Title = "Supply Management", ClinetId = clients[4].Id, CategoryId = categories[4].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(2), ContractValue = 60000, ContractType = ContractType.Support,     },

//    new() { Title = "Mobile App", ClinetId = clients[0].Id, CategoryId = categories[0].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(10), ContractValue = 40000, ContractType = ContractType.FixedPrice,     },
//    new() { Title = "Compliance Review", ClinetId = clients[1].Id, CategoryId = categories[1].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(8), ContractValue = 22000, ContractType = ContractType.Consulting,     },
//    new() { Title = "Recruitment Support", ClinetId = clients[2].Id, CategoryId = categories[2].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(7), ContractValue = 18000, ContractType = ContractType.Retainer,     },
//    new() { Title = "Financial Reporting", ClinetId = clients[3].Id, CategoryId = categories[3].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(12), ContractValue = 35000, ContractType = ContractType.TimeAndMaterial,     },
//    new() { Title = "Operations Consulting", ClinetId = clients[4].Id, CategoryId = categories[4].Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(15), ContractValue = 45000, ContractType = ContractType.Consulting,     }
//};

    ////////////////////////
    //public static  List<ContractDocuments> documents =
    //[
    //    new() { DocumentName = "Main Contract", ContractId = contracts[0].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c1.pdf", FileSizeInBytes = 120000, UploadedBy = "system11",     },
    //new() { DocumentName = "Appendix A", ContractId = contracts[0].Id, DocumentTypeId = documentTypes[1].Id, FilePath = "/docs/c1-a.docx", FileSizeInBytes = 80000, UploadedBy = "system4",     },
    //new() { DocumentName = "Signed Copy", ContractId = contracts[1].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c2.pdf", FileSizeInBytes = 110000, UploadedBy = "system",     },

    //new() { DocumentName = "Invoice 1", ContractId = contracts[2].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c3.pdf", FileSizeInBytes = 90000, UploadedBy = "system4123",     },
    //new() { DocumentName = "Invoice 2", ContractId = contracts[2].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c3-2.pdf", FileSizeInBytes = 95000, UploadedBy = "system51",     },

    //new() { DocumentName = "Scope", ContractId = contracts[3].Id, DocumentTypeId = documentTypes[1].Id, FilePath = "/docs/c4.docx", FileSizeInBytes = 70000, UploadedBy = "system1235",     },
    //new() { DocumentName = "Final Report", ContractId = contracts[3].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c4.pdf", FileSizeInBytes = 150000, UploadedBy = "system1232",     },

    //new() { DocumentName = "Contract File", ContractId = contracts[4].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c5.pdf", FileSizeInBytes = 130000, UploadedBy = "system61234",     },
    //new() { DocumentName = "Extension", ContractId = contracts[5].Id, DocumentTypeId = documentTypes[1].Id, FilePath = "/docs/c6.docx", FileSizeInBytes = 88000, UploadedBy = "system3123",     },

    //new() { DocumentName = "Checklist", ContractId = contracts[6].Id, DocumentTypeId = documentTypes[2].Id, FilePath = "/docs/c7.png", FileSizeInBytes = 45000, UploadedBy = "system1235",     },
    //new() { DocumentName = "Summary", ContractId = contracts[7].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c8.pdf", FileSizeInBytes = 99000, UploadedBy = "system6321",     },

    //new() { DocumentName = "Agreement", ContractId = contracts[8].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c9.pdf", FileSizeInBytes = 125000, UploadedBy = "system1236312",     },
    //new() { DocumentName = "Annex", ContractId = contracts[8].Id, DocumentTypeId = documentTypes[1].Id, FilePath = "/docs/c9-annex.docx", FileSizeInBytes = 77000, UploadedBy = "system5234",     },

    //new() { DocumentName = "Closing Doc", ContractId = contracts[9].Id, DocumentTypeId = documentTypes[0].Id, FilePath = "/docs/c10.pdf", FileSizeInBytes = 160000, UploadedBy = "system52345234",     }
    //];

}
