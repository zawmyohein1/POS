//using POS.Domain.ViewModels;
using POS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using EvaSign.Common;

namespace POS.UI.MVC.Controllers
{
    public class DepartmentsController : BaseController
    {
        private const string relativeURI = "Departments";

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            DateTime t1 = DateTime.Now;
            DepartmentModelList departmentmodellist = new DepartmentModelList();
            token = HttpContext.Session.GetString("Token");

            HttpResponseMessage response = await _webApiClient.GetAsync(relativeURI, token);

            var responseMessage = string.Empty;
            if (VerifyResponse(response, out responseMessage))
            {
                var requestResult = await response.Content.ReadAsStringAsync();
                departmentmodellist = (DepartmentModelList)JsonConvert.DeserializeObject<DepartmentModelList>(requestResult);

                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return View("Index", departmentmodellist);
            }
            else
            {
                //return new HttpStatusCodeResult(response.StatusCode, responseMessage);
                return this.StatusCode((int)response.StatusCode, responseMessage);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            DateTime t1 = DateTime.Now;
            TimeSpan ts = DateTime.Now.Subtract(t1);
            _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateDepartmentAsync(DepartmentModel DepartmentModel)
        {
            if (ModelState.IsValid && DepartmentModel != null)
            {
                DateTime t1 = DateTime.Now;
                var jsonData = JsonConvert.SerializeObject(DepartmentModel, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                HttpResponseMessage response = await _webApiClient.PostAsync(relativeURI, jsonData, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    DepartmentModel = JsonConvert.DeserializeObject<DepartmentModel>(requestResult);
                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    
                    return await Task.FromResult<ActionResult>(Json(DepartmentModel));
                }
                else
                {
                    return this.StatusCode((int)response.StatusCode, responseMessage);
                }
            }
            else
            {
                return PartialView("_Create", DepartmentModel);
            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdateAsync(int id)
        {
            if (id > 0)
            {
                DateTime t1 = DateTime.Now;
                DepartmentModel DepartmentModel = new DepartmentModel();

                HttpResponseMessage response = await _webApiClient.GetAsync(relativeURI + "/" + id, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    DepartmentModel = JsonConvert.DeserializeObject<DepartmentModel>(requestResult);
                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    return PartialView("_Update", DepartmentModel);
                }
                else
                {
                    return this.StatusCode((int)response.StatusCode, responseMessage);
                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateDepartmentAsync(DepartmentModel DepartmentModel)
        {
            if (ModelState.IsValid && DepartmentModel != null)
            {
                DateTime t1 = DateTime.Now;
                var jsonData = JsonConvert.SerializeObject(DepartmentModel, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                HttpResponseMessage response = await _webApiClient.PutAsync(relativeURI, jsonData, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    DepartmentModel = JsonConvert.DeserializeObject<DepartmentModel>(requestResult);
                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    
                    return await Task.FromResult<ActionResult>(Json(DepartmentModel));
                }
                else
                {
                    return this.StatusCode((int)response.StatusCode, responseMessage);
                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (id > 0)
            {
                DateTime t1 = DateTime.Now;
                DepartmentModel DepartmentModel = new DepartmentModel();
                var _username = "Zaw";
                var apiURI = relativeURI + "/" + id + "/" + _username;

                var response = await _webApiClient.DeleteAsync(apiURI, token);
                var responseMessage = string.Empty;

                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = response.Content.ReadAsStringAsync().Result;
                    DepartmentModel = (DepartmentModel)JsonConvert.DeserializeObject<DepartmentModel>(requestResult);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, response.ReasonPhrase);
                }
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return await Task.FromResult<ActionResult>(Json(DepartmentModel));
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
        }

    }
}
