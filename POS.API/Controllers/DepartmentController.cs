using POS.API.Helper;
using POS.Domain.IServices;
using POS.Domain.ViewModels;
using POS.Infrastructure.Exceptions.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController
    {
        private readonly IDepartmentservice _departmentService;

        public DepartmentsController(IDepartmentservice departmentService)
        {
            _departmentService = departmentService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<DepartmentModelList>> Get()
        {
            try
            {
                DateTime t1 = DateTime.Now;
                var departmentModelList = await _departmentService.GetAllDepartments();
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(departmentModelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentModel>> Get(int id)
        {
            if (id <= 0)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid Input");
            }
            try
            {
                DateTime t1 = DateTime.Now;
                var departmentModel = await _departmentService.GetDepartmentById(id);

                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(departmentModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DepartmentModel>> Post(DepartmentModel model)
        {
            if (model == null)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid Input");
            }

            try
            {
                DateTime t1 = DateTime.Now;
                var departmentModel = await _departmentService.CreateDepartment(model);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return CreatedAtAction(nameof(Get), new { id = departmentModel.Department_ID }, departmentModel);

            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ErrorKeys.EntityNotFound);
            }
            catch (DependencyNotFoundException ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ErrorKeys.DependecyNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }

        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<DepartmentModel>> Put(DepartmentModel model)
        {
            if (model == null)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
            try
            {
                var departmentModel = await _departmentService.UpdateDepartment(model);
                return CreatedAtAction(nameof(Get), new { id = departmentModel.Department_ID }, departmentModel);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ErrorKeys.EntityNotFound);
            }
            catch (DependencyNotFoundException ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ErrorKeys.DependecyNotFound);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        [Authorize]
        [HttpDelete("{id:int:min(1)}/{auditDepartmentName}")]
        public async Task<ActionResult<DepartmentModel>> Delete(int id, string auditDepartmentName)
        {
            if (id <= 0)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
            try
            {
                DateTime t1 = DateTime.Now;
                var departmentModel = await _departmentService.DeleteDepartment(id, auditDepartmentName);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(departmentModel);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ErrorKeys.EntityNotFound);
            }
            catch (DependencyNotFoundException ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ErrorKeys.DependecyNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }
    }
}
