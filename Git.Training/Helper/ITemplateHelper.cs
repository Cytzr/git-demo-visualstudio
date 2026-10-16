
using Git.Training.Model.ExampleDB;
using Git.Training.Model.Request;
using Git.Training.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Git.Training.Helper
{
    public interface ITemplateHelper
    {
        Task<List<TemplateResponseDTO>> GetTemplate(Expression<Func<Template, bool>> predicate = null, int page = 1, int take = 10, Func<IQueryable<Template>, IOrderedQueryable<Template>> orderBy = null);
        Task AddTemplate(AddTemplateRequestDTO req);
        Task UpdateTemplate(UpdateTemplateRequestDTO req);
        Task DeleteTemplate(DeleteTemplateRequestDTO req);
        Task ExecUpdateTemplate(ExecUpdateTemplateRequestDTO req);
        Task ExecDeleteTemplate(ExecDeleteTemplateRequestDTO req);
    }
}
