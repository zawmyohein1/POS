
//using POS.Domain.ViewModels;
using POS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using POS.Common;

namespace POS.UI.MVC.Controllers
{
    public class DbSuppliersController : BaseController
    {
        private const string relativeURI = "DbSuppliers";

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            DateTime t1 = DateTime.Now;
            DbSupplierModelList supplierModelList = new DbSupplierModelList();
            token = HttpContext.Session.GetString("Token");

            HttpResponseMessage response = await _webApiClient.GetAsync(relativeURI, token);

            var responseMessage = string.Empty;
            if (VerifyResponse(response, out responseMessage))
            {
                var requestResult = await response.Content.ReadAsStringAsync();
                supplierModelList = (DbSupplierModelList)JsonConvert.DeserializeObject<DbSupplierModelList>(requestResult);

                ViewData["Controller"] = "Departments";

                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));

                return View("Index", supplierModelList);
            }
            else
            {
                return this.StatusCode((int)response.StatusCode, responseMessage);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            DateTime t1 = DateTime.Now;
            TimeSpan ts = DateTime.Now.Subtract(t1);
            _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
            ViewData["Title"] = "Add DbSupplier";
            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateDbSupplierAsync(DbSupplierModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                DateTime t1 = DateTime.Now;
                var jsonData = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                HttpResponseMessage response = await _webApiClient.PostAsync(relativeURI, jsonData, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<DbSupplierModel>(requestResult);

                    model.Controller = "DbSuppliers";
                    model.Action = "Index";
                    model.ID = model.Supplier_ID;

                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    return await Task.FromResult<ActionResult>(Json(model));
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

        [HttpGet]
        public async Task<ActionResult> UpdateAsync(int id)
        {
            if (id > 0)
            {
                DateTime t1 = DateTime.Now;
                DbSupplierModel supplierModel = new DbSupplierModel();

                HttpResponseMessage response = await _webApiClient.GetAsync(relativeURI + "/" + id, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    supplierModel = JsonConvert.DeserializeObject<DbSupplierModel>(requestResult);
                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    ViewData["Title"] = "Add DbSupplier";
                    return PartialView("_Update", supplierModel);
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> UpdateDbSupplierAsync(DbSupplierModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                DateTime t1 = DateTime.Now;
                var jsonData = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                HttpResponseMessage response = await _webApiClient.PutAsync(relativeURI, jsonData, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<DbSupplierModel>(requestResult);

                    model.Controller = "DbSuppliers";
                    model.Action = "Index";
                    model.ID = model.Supplier_ID;

                    TimeSpan ts = DateTime.Now.Subtract(t1);

                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    return await Task.FromResult<ActionResult>(Json(model));
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
                DbSupplierModel supplierModel = new DbSupplierModel();
                var _username = "Zaw";
                var apiURI = relativeURI + "/" + id + "/" + _username;

                var response = await _webApiClient.DeleteAsync(apiURI, token);
                var responseMessage = string.Empty;

                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = response.Content.ReadAsStringAsync().Result;
                    supplierModel = JsonConvert.DeserializeObject<DbSupplierModel>(requestResult);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, response.ReasonPhrase);
                }
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return await Task.FromResult<ActionResult>(Json(supplierModel));
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
        }
    }
}
