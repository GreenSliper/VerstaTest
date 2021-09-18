using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VerstaTest.Models.DTOEntities;
using VerstaTest.Models.Entities;
using VerstaTest.Models.Validation;
using VerstaTest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace VerstaTest.Services
{
	public interface IOrderService
	{
		Task<List<Order>> GetOrders();
		Task<Order> GetOrder(int id);
		Task<bool> InsertOrder(Order order, ModelStateDictionary modelState, ControllerBase controller);
		Task<bool> UpdateOrder(Order order, ModelStateDictionary modelState, ControllerBase controller);
		Task<bool> DeleteOrder(int id);
	}

	public class OrderService : IOrderService
	{
		private readonly IDbCRUDRepository<OrderDTO> repository;
		private readonly IFieldRevalidator<decimal> decimalRevalidator;
		private readonly IMapper mapper;
		public OrderService(IDbCRUDRepository<OrderDTO> repository, IMapper mapper, IFieldRevalidator<decimal> decimalRevalidator)
		{
			this.decimalRevalidator = decimalRevalidator;
			this.repository = repository;
			this.mapper = mapper;
		}
		public async Task<List<Order>> GetOrders()
		{
			return (await repository.GetAll()).OrderByDescending(x=>x.CreationTime).Select(x=>mapper.Map<Order>(x)).ToList();
		}
		public async Task<Order> GetOrder(int id)
		{
			return mapper.Map<Order>(await repository.Get(id));
		}
		public async Task<bool> InsertOrder(Order order, ModelStateDictionary modelState, ControllerBase controller)
		{
			if (modelState.IsValid || decimalRevalidator.TryReValidateField(modelState, order, "PackageWeight",
					(ord, packW) => ord.PackageWeight = packW, controller))
			{
				await repository.Insert(mapper.Map<OrderDTO>(order));
				return true;
			}
			return false;
		}
		public async Task<bool> UpdateOrder(Order order, ModelStateDictionary modelState, ControllerBase controller)
		{
			if (modelState.IsValid || decimalRevalidator.TryReValidateField(modelState, order, "PackageWeight",
			        (ord, packW) => ord.PackageWeight = packW, controller))
			{
				await repository.Update(mapper.Map<OrderDTO>(order));
				return true;
			}
			return false;
		}
		public async Task<bool> DeleteOrder(int id)
		{
			if (repository.Get(id) != null)
			{
				await repository.Delete(await repository.Get(id));
				return true;
			}
			return false;
		}
	}
}
