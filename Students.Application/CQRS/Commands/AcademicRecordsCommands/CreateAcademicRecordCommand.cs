using MediatR;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Application.CQRS.Commands.AcademicRecordsCommands
{
	public class CreateAcademicRecordCommand : IRequest<BaseCommandResponse>
	{
		public AcademicRecordUpddateDto? AcademicRecord { get; set; }
	}
}
