using System;	
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Git.Training.Output;
	
using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.Service;
using Binus.WS.Pattern.RouteGuardV3;
using Git.Training.Model.Request;
using System.Threading.Tasks;
using Git.Training.Helper;
	
namespace Git.Training.Services
{
	[ApiController]
	[Route("template")]
	public class TemplateService : BaseService
	{
		private readonly ITemplateHelper _templateHelper;
		public TemplateService(ILogger<BaseService> logger, ITemplateHelper TemplateHelper) : base(logger)
		{
			_templateHelper = TemplateHelper;
		}

		/// <summary>
		/// Get All Template List
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     GET /template
		///
		/// </remarks>
		/// <returns>A list of Template</returns>
		/// <response code="200">Returns a list of Template</response>
		[HttpGet]
		[Produces("application/json")]
		[RouteGuardV3(Authentication = "Bearer")]
		[ProducesResponseType(typeof(TemplateOutput), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetTemplate()
		{
			try
			{
				var objJSON = new TemplateOutput();
				objJSON.ListTemplate = await _templateHelper.GetTemplate();
				return new OkObjectResult(objJSON);
			}
			catch(Exception ex)
			{
				return StatusCode(500, new OutputBase(ex));
			}
		}

		[HttpGet]
		[Route("testing-git")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> TestingKah()
		{
			try
			{
				return new OkObjectResult("Hello conflict");
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Add Template
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /template
		///
		/// </remarks>
		/// <response code="200">Successfully added a new record!</response>
		[HttpPost]
		[Produces("application/json")]
		[RouteGuardV3(Authentication = "Bearer")]
		[ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
		public async Task<IActionResult> AddTemplate([FromBody] AddTemplateRequestDTO data)
		{
			try
			{
				await _templateHelper.AddTemplate(data);
				return new OkObjectResult(new OutputBase());
			}
			catch(Exception ex)
			{
				return StatusCode(500, new OutputBase(ex));
			}
		}

		/// <summary>
		/// Delete Template
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     DELETE /template
		///     
		/// </remarks>
		/// <response code="200">Successfully deleted a record!</response>
		[HttpDelete]
		[Produces("application/json")]
		[RouteGuardV3(Authentication = "Bearer")]
		[ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteTemplate([FromBody] DeleteTemplateRequestDTO data)
		{
			try
			{
				await _templateHelper.DeleteTemplate(data);
				return new OkObjectResult(new OutputBase());
			}
			catch(Exception ex)
			{
				return StatusCode(500, new OutputBase(ex));
			}
		}

		/// <summary>
		/// Update Template
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     PATCH /template
		///     
		/// </remarks>
		/// <response code="200">Successfully update record!</response>
		[HttpPatch]
		[Produces("application/json")]
		[RouteGuardV3(Authentication = "Bearer")]
		[ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateTemplate([FromBody] UpdateTemplateRequestDTO data)
		{
			try
			{
				await _templateHelper.UpdateTemplate(data);
				return new OkObjectResult(new OutputBase());
			}
			catch(Exception ex)
			{
				return StatusCode(500, new OutputBase(ex));
			}
		}
		/// <summary>
        /// Update Template
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /Template/exec
        ///     
        /// </remarks>
        /// <response code="200">Successfully update record!</response>
        [HttpPatch]
		[Route("exec")]
        [Produces("application/json")]
        [RouteGuardV3(Authentication = "Bearer")]
		[ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExecUpdateTemplate([FromBody] ExecUpdateTemplateRequestDTO data)
        {
            try
            {
                await _templateHelper.ExecUpdateTemplate(data);
                return new OkObjectResult(new OutputBase());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }
		/// <summary>
        /// Update Template
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /Template/exec
        ///     
        /// </remarks>
        /// <response code="200">Successfully update record!</response>
        [HttpDelete]
        [Route("exec")]
        [Produces("application/json")]
        [RouteGuardV3(Authentication = "Bearer")]
		[ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExecDeleteTemplate([FromBody] ExecDeleteTemplateRequestDTO data)
        {
            try
            {
                await _templateHelper.ExecDeleteTemplate(data);
                return new OkObjectResult(new OutputBase());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }
	}
}