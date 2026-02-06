using ContractManagment.Api.Data;
using ContractManagment.Api.DTOs.ClientDTOs;
using ContractManagment.Api.DTOs.ContractsDTOs;
using ContractManagment.Api.Exstensions;
using ContractManagment.Api.Models.ClientsModels;
using ContractManagment.Api.Models.ContractsModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContractManagment.Api.Services.ClientServices;

public class ClientsServices : IClientsServices
{
    private readonly ApplicationDbContext _context;

    public ClientsServices(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ServiceResult<int?>> CreateClientAsync(AddClientDto addDto)
    {
        var emailExists = await _context.Clients.AnyAsync(c => c.Email == addDto.Email);

        if (emailExists)
            return ServiceResult<int?>.Failure("Email already in use.");

        var newClient = new Clients
        {
            CompanyRegistrationNumber = addDto.CompanyRegistrationNumber,
            Address = addDto.Address,
            ClientName = addDto.ClientName,
            ContactPerson = addDto.ContactPerson,
            Email = addDto.Email,
            IndustryId = addDto.IndustryId,
            PhoneNumber = addDto.PhoneNumber,
        };
        await _context.AddAsync(newClient);
        await _context.SaveChangesAsync();
        return ServiceResult<int?>.Success(newClient.Id);
    }

    public async Task<ServiceResult<List<GetClientDto>>> GetAllClientsAsync(int skip, int take)
    {
        var clients = await _context.Clients.Select(c => new GetClientDto
        {
            Id = c.Id,
            ClientName = c.ClientName,
            Industry = c.Industry.Name,
            PhoneNumber = c.PhoneNumber,
            NumberOfContracts = c.Contracts.Count(),
        }).ToListAsync();
        return ServiceResult<List<GetClientDto>>.Success(clients);
    }

    public async Task<ServiceResult<GetOneClientDto?>> GetClientByIdAsync(int clientId)
    {
        var client = await _context.Clients.Where(c => c.Id == clientId).Select(c => new GetOneClientDto
        {
            Id = c.Id,
            Address = c.Address,
            StatusIsActive = c.StatusIsActive,
            ClientName = c.ClientName,
            CompanyRegistrationNumber = c.CompanyRegistrationNumber,
            ContactPerson = c.ContactPerson,
            Email = c.Email,
            Industry = c.Industry.Name,
            PhoneNumber = c.PhoneNumber,
            Contracts = c.Contracts.Select(s => new GetClinetContractsForOneClientDto
            {
                Title = s.Title,
                Category = s.Category.Name,
                ContractNumber = s.ContractNumber,
                ContractValue = s.ContractValue,
                EndDate = s.EndDate.ToCustomDateTime(),
                StartDate = s.StartDate.ToCustomDateTime(),
                ContractType = s.ContractType.ToString(),
                Status = s.Status.ToString(),
            }).ToList(),
        }).FirstOrDefaultAsync();

        if (client == null)
            return ServiceResult<GetOneClientDto?>.Failure("Client not found.");

        return ServiceResult<GetOneClientDto?>.Success(client);
    }

    public async Task<ServiceResult<int>> GetClientsCountAsync()
    {
        var count = await _context.Clients.CountAsync();
        return ServiceResult<int>.Success(count);
    }

    public async Task<ServiceResult<GetClientStatisticsDto>> GetClientStatisticsAsync(int clientId)
    {
        var clintContracts = await _context.Contracts.Where(c => c.ClinetId == clientId).Select(c => new
        {
            c.ContractValue,
            c.Status,
        }).ToListAsync();

        return ServiceResult<GetClientStatisticsDto>.Success(new GetClientStatisticsDto
        {
            /// This is wrong here, doing it in a for loop is better. but for now doest matter.
            TotalActiveContractsCount = clintContracts.Count(c => c.Status == ContractStatus.Active),
            TotalActiveContractsVlue = clintContracts.Where(c => c.Status == ContractStatus.Active).Sum(c => c.ContractValue),
            TotalContractsCount = clintContracts.Count,
            TotalContractsValue = clintContracts.Sum(c => c.ContractValue)
        });
    }

    public async Task<ServiceResult<bool>> UpdateClientAsync(
      int clientId,
      UpdateClientDto updateDto)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == clientId);

        if (client == null)
            return ServiceResult<bool>.Failure("Client not found.");

        if (client.IndustryId != updateDto.IndustryId)
        {
            var industryExists = await _context.Industries
           .AnyAsync(i => i.Id == updateDto.IndustryId);

            if (!industryExists)
                return ServiceResult<bool>.Failure("Invalid industry.");
        }


        if (client.Email != updateDto.Email)
        {
            var emailExists = await _context.Clients.AnyAsync(c =>
              c.Email == updateDto.Email &&
              c.Id != clientId
            );

            if (emailExists)
                return ServiceResult<bool>.Failure("Email already in use.");
        }

        client.ClientName = updateDto.ClientName;
        client.ContactPerson = updateDto.ContactPerson;
        client.Email = updateDto.Email;
        client.PhoneNumber = updateDto.PhoneNumber;
        client.Address = updateDto.Address;
        client.CompanyRegistrationNumber = updateDto.CompanyRegistrationNumber;
        client.IndustryId = updateDto.IndustryId;

        await _context.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<bool>> UpdateClientStatusAsync(int clientId)
    {
        var client = await _context.Clients
            .Include(c => c.Contracts)
         .FirstOrDefaultAsync(c => c.Id == clientId);

        if (client == null)
            return ServiceResult<bool>.Failure("Client not found.");
        if (client.StatusIsActive && client.Contracts.Any(c => c.Status == ContractStatus.Active))
        {
            return ServiceResult<bool>.Failure("Can not deactivate a client with active contracts.");
        }
        client.StatusIsActive = !client.StatusIsActive;

        await _context.SaveChangesAsync();
        return ServiceResult<bool>.Success(true);
    }
}
