using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SystemConfigurationManager;

public class SystemConfigurationManagerServiceCreateDto
{
    public string Name { get; set; }
    public string Duration { get; set; } //fake because we receive iso8601 format
    public double Price { get; set; }
}

