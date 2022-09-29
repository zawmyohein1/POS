using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helper;
using POS.Domain.IServices;
using POS.Domain.ViewModels;
using POS.Infrastructure.Exceptions.DataAccess;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserservice _userService;

        public UsersController(IUserservice userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserModel>> Login(UserModel model)
        {
            if (model == null)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
            try
            {
                DateTime t1 = DateTime.Now;
                var userModel = await _userService.LoginUser(model);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(userModel);

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
        [HttpGet]
        public async Task<ActionResult<UserModelList>> Get()
        {
            try
            {
                DateTime t1 = DateTime.Now;
                var userModelList = await _userService.GetAllUsers();
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(userModelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            if (id <= 0)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid Input");
            }

            try
            {
                DateTime t1 = DateTime.Now;
                var userModel = await _userService.GetUserById(id);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(userModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel model)
        {
            if (model == null)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid Input");
            }

            try
            {
                DateTime t1 = DateTime.Now;
                var userModel = await _userService.CreateUser(model);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return CreatedAtAction(nameof(Get), new { id = userModel.User_ID }, userModel);

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
        public async Task<ActionResult<UserModel>> Put(UserModel model)
        {
            if (model == null)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
            try
            {
                var userModel = await _userService.UpdateUser(model);
                return CreatedAtAction(nameof(Get), new { id = userModel.User_ID }, userModel);
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
        [HttpDelete("{id:int:min(1)}/{auditUserName}")]
        public async Task<ActionResult<UserModel>> Delete(int id, string auditUserName)
        {
            if (id <= 0)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
            try
            {
                DateTime t1 = DateTime.Now;
                var userModel = await _userService.DeleteUser(id, auditUserName);
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return Ok(userModel);
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
