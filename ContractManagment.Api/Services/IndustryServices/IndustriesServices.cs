using ContractManagment.Api.Data;
using ContractManagment.Api.DTOs.ClientDTOs;
using ContractManagment.Api.Models.ClientsModels;
using Microsoft.EntityFrameworkCore;

namespace ContractManagment.Api.Services.IndustryServices;

public class IndustriesServices: IIndustriesServices
{
    private readonly ApplicationDbContext _context;

    public IndustriesServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<List<GetIndustryDto>>> GetAllAsync()
    {
        var industries = await _context.Industries
            .AsNoTracking()
            .Select(i => new GetIndustryDto
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToListAsync();

        return ServiceResult<List<GetIndustryDto>>.Success(industries);
    }

    public async Task<ServiceResult<GetIndustryDto>> GetByIdAsync(int id)
    {
        var industry = await _context.Industries
            .AsNoTracking()
            .Where(i => i.Id == id)
            .Select(i => new GetIndustryDto
            {
                Id = i.Id,
                Name = i.Name
            })
            .FirstOrDefaultAsync();

        if (industry == null)
            return ServiceResult<GetIndustryDto>.Failure("Industry not found");

        return ServiceResult<GetIndustryDto>.Success(industry);
    }

    public async Task<ServiceResult<int>> CreateAsync(AddIndustryDto dto)
    {
        var exists = await _context.Industries
            .AnyAsync(i => i.Name == dto.Name);

        if (exists)
            return ServiceResult<int>.Failure("Industry with the same name already exists");

        var industry = new Industry
        {
            Name = dto.Name
        };

        _context.Industries.Add(industry);
        await _context.SaveChangesAsync();

        return ServiceResult<int>.Success(industry.Id);
    }

    public async Task<ServiceResult<bool>> UpdateAsync(UpdateIndustryDto dto)
    {
        var industry = await _context.Industries.FindAsync(dto.Id);
        if (industry == null)
            return ServiceResult<bool>.Failure("Industry not found");

        var nameExists = await _context.Industries
            .AnyAsync(i => i.Name == dto.Name && i.Id != dto.Id);

        if (nameExists)
            return ServiceResult<bool>.Failure("Another industry with the same name already exists");

        industry.Name = dto.Name;
        await _context.SaveChangesAsync();

        return ServiceResult<bool>.Success (true);
    }

}
