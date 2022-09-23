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
    public class UsersController : BaseController
    {
        private const string relativeURI = "Users";      

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            DateTime t1 = DateTime.Now;
            UserModelList Usermodellist = new UserModelList();
            token = HttpContext.Session.GetString("Token");

            HttpResponseMessage response = await _webApiClient.GetAsync(relativeURI, token);

            var responseMessage = string.Empty;
            if (VerifyResponse(response, out responseMessage))
            {
                var requestResult = await response.Content.ReadAsStringAsync();
                Usermodellist = (UserModelList)JsonConvert.DeserializeObject<UserModelList>(requestResult);

                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return View("Index", Usermodellist);
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

        public async Task<ActionResult> CreateUserAsync(UserModel UserModel)
        {
            if (ModelState.IsValid && UserModel != null)
            {
                DateTime t1 = DateTime.Now;              
                var jsonData = JsonConvert.SerializeObject(UserModel, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                HttpResponseMessage response = await _webApiClient.PostAsync(relativeURI, jsonData, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    UserModel = JsonConvert.DeserializeObject<UserModel>(requestResult);
                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    return await Task.FromResult<ActionResult>(Json(UserModel));
                }
                else
                {
                    return this.StatusCode((int)response.StatusCode, responseMessage);
                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest,ErrorKeys.InvalidInput);
            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdateAsync(int id)
        {
            if (id > 0)
            {
                DateTime t1 = DateTime.Now;
                UserModel UserModel = new UserModel();
             
                HttpResponseMessage response = await _webApiClient.GetAsync(relativeURI + "/" + id, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    UserModel = JsonConvert.DeserializeObject<UserModel>(requestResult);
                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    return PartialView("_Update", UserModel);
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
        public async Task<ActionResult> UpdateUserAsync(UserModel UserModel)
        {
            if (ModelState.IsValid && UserModel != null)
            {
                DateTime t1 = DateTime.Now;
                var jsonData = JsonConvert.SerializeObject(UserModel, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                
                HttpResponseMessage response = await _webApiClient.PutAsync(relativeURI, jsonData, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    UserModel = JsonConvert.DeserializeObject<UserModel>(requestResult);
                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    return await Task.FromResult<ActionResult>(Json(UserModel));
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
                UserModel UserModel = new UserModel();
                var _username = "Zaw";
                var apiURI = relativeURI + "/" + id + "/" + _username;
            
                var response = await _webApiClient.DeleteAsync(apiURI, token);
                var responseMessage = string.Empty;

                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = response.Content.ReadAsStringAsync().Result;
                    UserModel = (UserModel)JsonConvert.DeserializeObject<UserModel>(requestResult);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, response.ReasonPhrase);
                }
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return await Task.FromResult<ActionResult>(Json(UserModel));
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ErrorKeys.InvalidInput);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(UserModel userModel)
        {
            if (ModelState.IsValid && userModel != null)
            {
                DateTime t1 = DateTime.Now;
                var jsonData = JsonConvert.SerializeObject(userModel, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                HttpResponseMessage response = await _webApiClient.PostAsync(relativeURI + "/Login", jsonData, null);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    userModel = JsonConvert.DeserializeObject<UserModel>(requestResult);
                    if (userModel.ResultCode == 0)
                    {
                        HttpContext.Session.SetInt32("UserId", userModel.User_ID);
                        HttpContext.Session.SetString("Email", userModel.Email);

                        var menu = "Dashboard";
                        var actionURL = "Home/Dashboard/";

                        TimeSpan ts = DateTime.Now.Subtract(t1);
                        _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));

                        HttpContext.Session.SetString("Token", userModel.Token);

                        return RedirectToAction("Index", "Home", new { menu = menu, actionURL = actionURL });
                    }
                    else
                    {
                        return View("Login");
                    }
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
        public ActionResult Logout()
        {
            DateTime t1 = DateTime.Now;
            TimeSpan ts = DateTime.Now.Subtract(t1);
            _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }
    }
}
