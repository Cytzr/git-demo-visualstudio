
using Binus.WS.Pattern.Helper;
using Binus.WS.Pattern.Entities.Interfaces;
using Binus.WS.Pattern.Redis.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Git.Training.Model.ExampleDB;
using Git.Training.Model.Request;
using Git.Training.Model.ExampleDB;
using Git.Training.Model.Response;

namespace Git.Training.Helper
{
	public class TemplateHelper : ITemplateHelper
	{
		private readonly IEntityHelper<Template> _entityHelper;
		private readonly IRedisHelper _redisHelper;

        public TemplateHelper(IEntityHelper<Template> entityHelper, IRedisHelper redisHelper)
        {
            _entityHelper = entityHelper;
			_redisHelper = redisHelper;
        }

		public async Task<List<TemplateResponseDTO>> GetTemplate(Expression<Func<Template, bool>> predicate = null, int page = 1, int take = 10, Func<IQueryable<Template>, IOrderedQueryable<Template>> orderBy = null)
		{
			try
			{
				var data = await _entityHelper.GetPages(page, take).Select(t => new TemplateResponseDTO
				{
					Id = t.Id,
					Name = t.Name
				}).ToListAndCacheAsync<TemplateResponseDTO>(new TimeSpan(hours:0,minutes:10,seconds:0), "cacheKey");

				return data;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}		
		
		public async Task AddTemplate(AddTemplateRequestDTO req)
		{
			try
			{
				await _entityHelper.AddAsync(req.Data);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}		

		public async Task UpdateTemplate(UpdateTemplateRequestDTO req)
		{
			try
			{
				var target = (await _entityHelper.GetAsync()).Where(t => t.Id == req.Id).FirstOrDefault();
				target.Name = req.Data.Name;
                await _entityHelper.UpdateAsync(target);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task DeleteTemplate(DeleteTemplateRequestDTO req)
		{
			try
			{
				var target = (await _entityHelper.GetAsync()).Where(t => t.Id == req.Id).FirstOrDefault();
                await _entityHelper.DeleteAsync(target);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		
		// New Feature in EF Core 7.0+ Exec Update
		// Read the documentation at https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whats-new-in-ef-core-7.0#bulk-update-and-delete

		public async Task ExecUpdateTemplate(ExecUpdateTemplateRequestDTO req)
		{
			try
			{
				var query = await _entityHelper.GetAsync();
                var affectedRows = await query
                    .Where(t => t.Id == req.Id)
					.ExecuteUpdateAsync(builder => builder
					.SetProperty(t => t.Name,    // property selector
							 t => req.Data.Name)  // new value selector
													// .SetProperty(t => t.OtherProp, t => req.Data.OtherProp) // if you have more
					);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// New Feature in EF Core 7.0+ Exec Delete
		// Read the documentation at https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whats-new-in-ef-core-7.0#bulk-update-and-delete

		public async Task ExecDeleteTemplate(ExecDeleteTemplateRequestDTO req)
		{
			try
			{
				var query = await _entityHelper.GetAsync();
				await query
					.Where(t => t.Id == req.Id)
					.ExecuteDeleteAsync();
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
