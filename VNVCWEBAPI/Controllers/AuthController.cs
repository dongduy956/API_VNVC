using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Library;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ActivatorUtilitiesConstructor]
    public class AuthController : ControllerBase
    {
        private readonly IJWTServices jWTServices;
        private readonly ICustomerServices customerServices;
        private readonly ILoginServices loginServices;
        private readonly ITwilioRestClient twilio;

        public AuthController(IJWTServices jWTServices,
            ICustomerServices customerServices,
            ILoginServices loginServices,
            ITwilioRestClient twilio)
        {
            this.customerServices = customerServices;
            this.loginServices = loginServices;
            this.jWTServices = jWTServices;
            this.twilio = twilio;
        }
        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<IActionResult> Login(string? deviceId, LoginRequest loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();
            var response = await jWTServices.getTokenAsync(loginRequest, deviceId, getIP());
            return Ok(response);
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetChatToken()
        {
            return Ok(new ResponseAPI
            {
                Data = LoginConfig.ChatAPI,
                StatusCode=Ok().StatusCode,
                isSuccess=true,
               
            });
        }
        [HttpPut("[Action]")]


        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            int.TryParse(stringId, out var id);
            var result = await loginServices.ChangePassword(id, model);
            if (result == -1)
                return NotFound(new ResponseAPI
                {
                    Data = result,
                    isSuccess = false,
                    Messages = new string[] { "Sai mật khẩu" },
                    StatusCode = NotFound().StatusCode
                });
            else if (result == 0)
                return BadRequest(new ResponseAPI
                {
                    isSuccess = false,
                    Messages = new string[] { "Vui lòng thử lại sau" },
                });
            return Ok(new ResponseAPI
            {
                isSuccess = true,
                Data = result,
                Messages = new string[] { "Thay đổi mật khẩu thành công" },
                StatusCode = Ok().StatusCode
            }); ;
        }
        [HttpPost("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(string? deviceId, JWTRequest jWTRequest)
        {
            JwtSecurityToken jwt = new JwtSecurityToken(jWTRequest.AccessToken);
            if (jWTRequest == null)
                return BadRequest();
            var userId = jwt.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.NameId))?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }
            var response = await jWTServices.renewRefreshTokenAsync(Convert.ToInt32(userId), getIP(), deviceId, jWTRequest);
            return Ok(response);
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Logout(JWTRequest jWTRequest)
        {
            //var AccessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer", "");
            var response = await jWTServices.RevokeRefreshToken(jWTRequest);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (model.CustomerTypeId == null)
                model.CustomerTypeId = 1;
            var userExits = await loginServices.CheckExitsUserbyUsername(model.PhoneNumber);
            if (userExits)
            {
                return Ok(new ResponseAPI
                {
                    StatusCode = (int)HttpStatusCode.Found,
                    isSuccess = false,
                    Messages = new string[] { "user exits" }
                });
            }

            var insert = await customerServices.InsertCustomerAsync(model);
            if (insert)
            {
                string passwordHash = StringLibrary.PasswordHash(model.Password);
                string code = StringLibrary.GenerateCode();
                var insertLogin = await loginServices.InsertLogin(new Services.ViewModels.LoginModel
                {
                    isLock = false,
                    Code = code,
                    Created = model.Created,
                    CustomerId = model.Id,
                    PasswordHash = model.Password.PasswordHash(),
                    Username = model.PhoneNumber,
                    isValidate = false,
                });
                if (insertLogin)
                {
                    //Send SMS
                    //try
                    //{
                    //    var message = await MessageResource.CreateAsync(
                    //        to: new PhoneNumber(model.PhoneNumber),
                    //        from: new PhoneNumber(TwilioConfig.PhoneFrom),
                    //        body: $"Cảm ơn bạn đã đăng ký và sử dụng dịch vụ của VNVC, mã xác thực của bạn là: {code}",
                    //        client: twilio
                    //    );
                    //}
                    //catch (Exception ex) { }
                    return Ok(new ResponseAPI
                    {
                        isSuccess = true,
                        Messages = new string[] { "Register Success" },
                        Data = model,
                        StatusCode = Ok().StatusCode
                    });
                }
            }
            return BadRequest(new ResponseAPI
            {
                isSuccess = false,
                Messages = new string[] { "Register Failed, please try again" },
                StatusCode = BadRequest().StatusCode
            });
        }
        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<IActionResult> CheckUserExits(string phoneNumber, bool? isRecovery)
        {
            var isExits = await loginServices.CheckExitsUserbyUsername(phoneNumber);
            if (isExits)
            {
                //Send SMS
                //if (isRecovery.HasValue)
                //{
                //    if (isRecovery.Value)
                //    {
                //        string code = await loginServices.RenewVerifyCode(phoneNumber);
                //        string myHostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                //        try
                //        {
                //            string ph = phoneNumber.Substring(1);
                //            var message = await MessageResource.CreateAsync(
                //                to: new PhoneNumber($"+84{phoneNumber.Substring(1)}"),
                //                from: new PhoneNumber(TwilioConfig.PhoneFrom),
                //                body: $"Bạn đang khôi phục mật khẩu của VNVC, vui lòng không chia sẻ liên kết này cho bất kì ai: {myHostUrl}/api/recoveryPassword?phonenumber={phoneNumber}&code={code}",
                //                client: twilio
                //            );
                //        }
                //        catch (Exception ex) { }
                //    }
                //}
                return Ok(new ResponseAPI
                {
                    StatusCode = (int)HttpStatusCode.Found,
                    Data = isExits,
                    isSuccess = true,
                    Messages = new string[] { "user exits" }
                });
            }
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                Data = isExits,
                isSuccess = true,
                Messages = new string[] { "user not found" }
            });
        }

        [AllowAnonymous,HttpGet("[Action]/{phoneNumber}/{code}")]
        public async Task<IActionResult> RecoveryPassword(string phoneNumber, string code)
        {
            var user = await loginServices.GetLogins().FirstOrDefaultAsync(x => x.Username.Equals(phoneNumber) && x.Code == code);
            if (user != null)
            {
                string password = StringLibrary.GenerateCode(true);
                user.PasswordHash = password.PasswordHash();
                user.Code = null;
                try
                {
                    var isSuccess = await loginServices.UpdateLogin(user);
                    if (isSuccess)
                    {
                        //Send SMS
                        //var message = await MessageResource.CreateAsync(
                        //    to: new PhoneNumber(phoneNumber),
                        //    from: new PhoneNumber(TwilioConfig.PhoneFrom),
                        //    body: $"Khôi phục thành công, mật khẩu của bạn là: {password}",
                        //    client: twilio
                        //);
                    }
                }
                catch (Exception ex) { }
            }
            return Ok("Khôi phục mật khẩu thành công");
        }

        [AllowAnonymous, HttpPost("[Action]")]
        public async Task<IActionResult> VerifyCode(string username, string code)
        {
            var user = await loginServices.GetLogin(username);
            if (user == null)
                return NotFound(new ResponseAPI
                {
                    isSuccess = false,
                    Messages = new string[] { "Tài khoản không tồn tại" }
                });
            if (user.Code == code)
            {
                var result = await loginServices.UpdateLoginValidate(user.Id);
                if (result)
                {
                    return Ok(new ResponseAPI
                    {
                        Data = result,
                        isSuccess = true,
                        Messages = new string[] { "Kích hoạt tài khoản thành công" },
                        StatusCode = Ok().StatusCode,
                    });
                }
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Lỗi hệ thống, vui lòng thử lại sau" },
                    StatusCode = Ok().StatusCode,
                });
            }
            return BadRequest(new ResponseAPI
            {
                isSuccess = true,
                Messages = new string[] { "Mã code không đúng, vui lòng thử lại" },
                StatusCode = Ok().StatusCode,
            });
        }

        [AllowAnonymous, HttpPost("[Action]/{id}")]
        public async Task<IActionResult> RenewVerifyCode(string username)
        {
            var user = await loginServices.GetLogin(username);
            if (user == null)
                return NotFound(new ResponseAPI
                {
                    isSuccess = false,
                    Messages = new string[] { "Tài khoản không tồn tại" },
                    StatusCode = NotFound().StatusCode
                });
            if (string.IsNullOrEmpty(user.Code))
            {
                return Ok(new ResponseAPI
                {
                    isSuccess = false,
                    Data = false,
                    StatusCode = (int)HttpStatusCode.Forbidden,
                    Messages = new string[] { "Không có quyền" }
                });
            }
            var result = await loginServices.RenewVerifyCode(username);
            if (result.Length > 4)
            {
                try
                {
                    var message = await MessageResource.CreateAsync(
                        to: new PhoneNumber(user.Username),
                        from: new PhoneNumber(TwilioConfig.PhoneFrom),
                        body: $"Cảm ơn bạn đã đăng ký và sử dụng dịch vụ của VNVC, mã xác thực của bạn là: {result}",
                        client: twilio
                    );
                }
                catch (Exception ex) { }
                return Ok(new ResponseAPI
                {
                    isSuccess = true,
                    Messages = new string[] { "Renew thành công" },
                    Data = result,
                    StatusCode = Ok().StatusCode,
                });
            }
            return Ok(new ResponseAPI
            {
                isSuccess = true,
                Messages = new string[] { "Lỗi hệ thống, vui lòng thử lại sau" },
                Data = result,
                StatusCode = (int)HttpStatusCode.InternalServerError,
            });
        }

        private string? getIP()
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Headers["X-Forwarded-For"]))
            {
                return HttpContext.Request.Headers["X-Forwarded-For"];
            }
            return Request.HttpContext.Connection.RemoteIpAddress?.ToString();
        }
    }
}
