using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.Services
{
    public class JWTServices : IJWTServices
    {
        private readonly ILoginServices loginServices;
        private readonly ILoginSessionServices loginSessionServices;
        private readonly IPermissionServices permissionServices;
        private readonly IStaffServices staffServices;
        private readonly IPermissionDetailsServices permissionDetailsServices;
        public JWTServices(ILoginServices loginServices,
            ILoginSessionServices loginSessionServices,
            IPermissionServices permissionServices,
            IStaffServices staffServices,
            IPermissionDetailsServices permissionDetailsServices)
        {
            this.loginServices = loginServices;
            this.loginSessionServices = loginSessionServices;
            this.permissionServices = permissionServices;
            this.staffServices = staffServices;
            this.permissionDetailsServices = permissionDetailsServices;
        }
        //Check Token Validate
        public async Task<ResponseAPI> checkValidate(JWTRequest jWTRequest)
        {
            var loginSession = await loginSessionServices.GetLoginSession(jWTRequest);
            //Check had login session
            if (loginSession == null)
                return new ResponseAPI
                {
                    isSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                };

            //Check expired
            if (loginSession.isExpired)
            {
                return new ResponseAPI
                {
                    isSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Messages = new string[] { "Token is Expired" }
                };
            }

            return new ResponseAPI
            {
                isSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        public async Task<ResponseAPI> renewRefreshTokenAsync(int UserId, string IPAdress, string? deviceId, JWTRequest jWTRequest)
        {
            //Get loginsession
            var loginSession = loginSessionServices.GetLoginSessionByJWT(jWTRequest);

            //Check login Session null
            if (loginSession == null)
                return new ResponseAPI
                {
                    isSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound
                };

            //Delete Login session
            await loginSessionServices.DeleteLoginSession(jWTRequest);

            //Get UserLogin
            var userLogin = await loginServices.GetLogin(UserId);

            //Get Staff Permission 
            var staffPermission = await staffServices.GetStaff(loginSession.StaffId ?? 0);

            int permissionId = userLogin.StaffId == null ? 3 : staffPermission.PermissionId;
            var accessToken = await GenerateAccessToken(userLogin.Id, IPAdress, permissionId);
            var refreshToken = GenerateRefreshToken();
            //Save to Database
            return await saveTokenLogin(accessToken, refreshToken, IPAdress, userLogin, permissionId, deviceId);
        }


        //Genarate JWT TOKEN
        public async Task<ResponseAPI> getTokenAsync(LoginRequest loginRequest, string? deviceId, string IPAdress)
        {
            var userLogin = await loginServices.GetLogin(loginRequest);
            if (userLogin == null)
                return new ResponseAPI
                {
                    isSuccess = false,
                    Messages = new string[] { "Tài khoản hoặc mật khẩu không chính xác." },
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            if (userLogin.isLock)
            {
                return new ResponseAPI
                {
                    isSuccess = false,
                    Messages = new string[] { "Tài khoản tạm khoá. Vui lòng liên hệ admin để biết thêm chi tiết." },
                    StatusCode = (int)HttpStatusCode.Locked
                };
            }
            if (!userLogin.isValidate)
            {
                return new ResponseAPI
                {
                    isSuccess = false,
                    Messages = new string[] { "Tài khoản chưa kích hoạt. Vui lòng liên hệ admin để kích hoạt." },
                    StatusCode = (int)HttpStatusCode.Locked
                };
            }
            int permissionId = 3;
            if (userLogin.CustomerId == null)
            {
                var staffPermission = await staffServices.GetStaff(userLogin.StaffId ?? 3);

                permissionId = userLogin.StaffId == null ? 3 : staffPermission.PermissionId;
            }

            var accessToken = await GenerateAccessToken(userLogin.Id, IPAdress, permissionId);
            var refreshToken = GenerateRefreshToken();

            return await saveTokenLogin(accessToken, refreshToken, IPAdress, userLogin, permissionId, deviceId);
        }
        //Check AccessToken is live

        public async Task<bool> isTokenLive(string accessToken)
        {
            var loginSession = await loginSessionServices
                .Where(x => x.AccessToken.Equals(accessToken))
                .FirstOrDefaultAsync();

            if (loginSession == null || loginSession.isRevoked || loginSession.isExpired)
                return false;
            return true;
        }

        //Revoke Token
        public async Task<ResponseAPI> RevokeRefreshToken(JWTRequest jWTRequest)
        {
            if (await loginSessionServices.DeleteLoginSession(jWTRequest))
            {
                return new ResponseAPI
                {
                    isSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Messages = new string[] { "Revoked" }
                };
            }
            return new ResponseAPI
            {
                isSuccess = false,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Messages = new string[] { "Revoke Fail" }
            };
        }

        //Save Token TO database
        private async Task<ResponseAPI> saveTokenLogin(string accessToken, string refreshToken, string iPAdress, LoginModel userLogin, int permissionId, string? deviceId)
        {
            var tokenInfomation = GetJwtSecurity(accessToken);

            var loginSessionModel = new LoginSessionModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expired = DateTime.UtcNow.AddDays(JWTConfig.RefreshTokenTime),
                IPAddress = iPAdress,
                isRevoked = false,
                TokenId = tokenInfomation.Id,
                CustomerId = permissionId != 3 ? null : userLogin.CustomerId,
                StaffId = permissionId == 3 ? null : userLogin.StaffId,
                DeviceId = deviceId
            };

            if (await loginSessionServices.InsertLoginSession(loginSessionModel))
            {
                var permission = await permissionServices.GetPermission(permissionId);
                return new ResponseAPI
                {
                    isSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = new JWTResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        AccessToken_Type = "Bearer",
                        User = userLogin,
                        ExpiredTime = JWTConfig.RefreshTokenTime,
                        PermissionId = permissionId,
                        PermissionName = permission?.Name
                    }
                };
            }
            else
            {
                return new ResponseAPI
                {
                    isSuccess = false,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        //Get infomation JWT Token
        private JwtSecurityToken GetJwtSecurity(string accessToken)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            return tokenHandle.ReadJwtToken(accessToken);
        }
        //Genarate Refresh Token
        private string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        //Genar
        private async Task<string> GenerateAccessToken(int Userid, string iPAdress, int permissionId)
        {
            //get permission Name
            var permission = await permissionServices.GetPermission(permissionId);
            var permissionDetails = new List<PermissionDetailsModel>();
            if (permission != null)
                permissionDetails = await permissionDetailsServices
                    .GetPermissionDetailsByPermissionId(permissionId).ToListAsync();

            var secretBytes = Encoding.UTF8.GetBytes(JWTConfig.SecretKey);
            //Init TokenSecurity Handle
            var tokenSecurityHandle = new JwtSecurityTokenHandler();

            //Access Claim
            var claim = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, Userid.ToString()),
                new Claim(ClaimTypes.Role, permission.Name),
            });
            foreach (var perD in permissionDetails)
            {
                claim.AddClaim(
                    new Claim(ClaimTypes.Role, perD.PermissionValue)
                    );
            }
            //JWT Descriptor
            var jwtDescription = new SecurityTokenDescriptor
            {
                Subject = claim,
                Expires = DateTime.UtcNow.AddDays(JWTConfig.AccessTokenTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha256)
            };
            //Write token
            var token = tokenSecurityHandle.CreateToken(jwtDescription);
            return tokenSecurityHandle.WriteToken(token);
        }
    }
}
