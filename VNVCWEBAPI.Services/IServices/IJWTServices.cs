using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IJWTServices
    {
        Task<ResponseAPI> getTokenAsync(LoginRequest loginRequest, string? deviceId, string IPAdress);
        Task<ResponseAPI> renewRefreshTokenAsync(int UserId, string IPAdress, string? deviceId, JWTRequest jWTRequest);
        Task<ResponseAPI> checkValidate(JWTRequest refreshTokenRequest);
        Task<ResponseAPI> RevokeRefreshToken(JWTRequest refreshToken);
        Task<bool> isTokenLive(string accessToken);
    }
}
