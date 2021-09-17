using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerstaTest.Models.DTOEntities;
using VerstaTest.Models.Entities;

namespace VerstaTest.Models.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrderDTO>();
			CreateMap<OrderDTO, Order>();
		}
	}
}
