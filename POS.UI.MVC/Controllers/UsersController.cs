﻿
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
    public class UsersController : BaseController
    {
        private const string relativeURI = "Users";

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            DateTime t1 = DateTime.Now;
            UserModelList userModelList = new UserModelList();
            token = HttpContext.Session.GetString("Token");

            HttpResponseMessage response = await _webApiClient.GetAsync(relativeURI, token);

            var responseMessage = string.Empty;
            if (VerifyResponse(response, out responseMessage))
            {
                var requestResult = await response.Content.ReadAsStringAsync();
                userModelList = (UserModelList)JsonConvert.DeserializeObject<UserModelList>(requestResult);

                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return View("Index", userModelList);
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
            ViewData["Title"] = "Add User";
            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(UserModel model)
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
                    model = JsonConvert.DeserializeObject<UserModel>(requestResult);

                    model.Controller = "Users";
                    model.Action = "Index";
                    model.ID = model.User_ID;

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
                UserModel userModel = new UserModel();

                HttpResponseMessage response = await _webApiClient.GetAsync(relativeURI + "/" + id, token);

                var responseMessage = string.Empty;
                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = await response.Content.ReadAsStringAsync();
                    userModel = JsonConvert.DeserializeObject<UserModel>(requestResult);
                    TimeSpan ts = DateTime.Now.Subtract(t1);
                    _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                    ViewData["Title"] = "Add User";
                    return PartialView("_Update", userModel);
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
        public async Task<ActionResult> UpdateUserAsync(UserModel model)
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
                    model = JsonConvert.DeserializeObject<UserModel>(requestResult);

                    model.Controller = "Users";
                    model.Action = "Index";
                    model.ID = model.User_ID;

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
                UserModel userModel = new UserModel();
                var _username = "Zaw";
                var apiURI = relativeURI + "/" + id + "/" + _username;

                var response = await _webApiClient.DeleteAsync(apiURI, token);
                var responseMessage = string.Empty;

                if (VerifyResponse(response, out responseMessage))
                {
                    var requestResult = response.Content.ReadAsStringAsync().Result;
                    userModel = JsonConvert.DeserializeObject<UserModel>(requestResult);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, response.ReasonPhrase);
                }
                TimeSpan ts = DateTime.Now.Subtract(t1);
                _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));
                return await Task.FromResult<ActionResult>(Json(userModel));
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
            userModel.User_Name = "default";
            userModel.Role = "Admin";
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
