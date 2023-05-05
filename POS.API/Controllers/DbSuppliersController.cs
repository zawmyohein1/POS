using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using POS.API.Helper;
using POS.Domain.IServices;
using POS.Domain.ViewModels;
using POS.Infrastructure.Exceptions.DataAccess;

namespace POS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DbSuppliersController : BaseController
    {
        private readonly IDbSupplierservice _supplierService;
        public DbSuppliersController(IDbSupplierservice SupplierService)
        {
            _supplierService = SupplierService;
        }
       

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<DbSupplierModelList>> Get()
        {
            try
            {
                DateTime t1 = DateTime.Now;
                var SupplierModelList = await _supplierService.GetAllDbSuppliers();
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(SupplierModelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<DbSupplierModel>> Get(int id)
        {
            if (id <= 0)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid Input");
            }

            try
            {
                DateTime t1 = DateTime.Now;
                var SupplierModel = await _supplierService.GetDbSupplierById(id);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(SupplierModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DbSupplierModel>> Post(DbSupplierModel model)
        {
            if (model == null)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid Input");
            }

            try
            {
                DateTime t1 = DateTime.Now;
                var SupplierModel = await _supplierService.CreateDbSupplier(model);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return CreatedAtAction(nameof(Get), new { id = SupplierModel.Supplier_ID }, SupplierModel);

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
        public async Task<ActionResult<DbSupplierModel>> Put(DbSupplierModel model)
        {
            if (model == null)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
            try
            {
                var SupplierModel = await _supplierService.UpdateDbSupplier(model);
                return CreatedAtAction(nameof(Get), new { id = SupplierModel.Supplier_ID }, SupplierModel);
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
        [HttpDelete("{id:int:min(1)}/{auditSupplierName}")]
        public async Task<ActionResult<DbSupplierModel>> Delete(int id, string auditSupplierName)
        {
            if (id <= 0)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
            try
            {
                DateTime t1 = DateTime.Now;
                var SupplierModel = await _supplierService.DeleteDbSupplier(id, auditSupplierName);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(SupplierModel);
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
