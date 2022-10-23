using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API.Controllers
{
    [Route(ApiVer1UrlDefinition.Auth.Token)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IFirebaseService _firebaseService;
        private readonly IUserRepository _userRepository;

        public AuthController(ITokenService tokenService,
                              IFirebaseService firebaseService,
                              IUserRepository userRepository)
        {
            this._tokenService = tokenService;
            this._firebaseService = firebaseService;
            this._userRepository = userRepository;
        }

        [HttpPost("token")]
        public ActionResult<JWTToken> GenerateToken([FromBody] AccessToken accessToken)
        {
            // get uid by access token
            string uid = _firebaseService.VerifyIdToken(accessToken);

            if (uid == null)
            {
                return BadRequest(new { Message = "accessToken invalid!" });
            }

            // get user record by uid
            var userRecord = _firebaseService.GetUserInfo(uid);

            // check user already exist
            var user = _userRepository.GetUser(userRecord.Email);

            if (user.IsDelete)
            {
                return NotFound(new { Message = "This account has been locked" });
            }

            if (user == null)
            {
                user = new User()
                {
                    Firstname = userRecord.DisplayName.Substring(0, userRecord.DisplayName.IndexOf(" ")).Trim(),
                    Lastname = userRecord.DisplayName.Substring(userRecord.DisplayName.IndexOf(" ") + 1).Trim(),
                    PhoneNumber = userRecord.PhoneNumber,
                    Email = userRecord.Email,
                    AvatarUrl = userRecord.PhotoUrl,
                    RoleID = RoleBase.VISITOR,
                    IsDelete = false,
                    TokenDevice = accessToken.RegisToken,
                };

                _userRepository.AddUser(user);
            }
            else
            {
                user.TokenDevice = accessToken.RegisToken;
                _userRepository.Update(user);
            }

            // get jwt token
            string jwtToken = _tokenService.GenerateJwtToken(user);

            // get refresh token
            //string refreshToken = _tokenService.GenerateRefreshToken();

            JWTToken token = new()
            {
                ID = user.ID,
                Email = userRecord.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                AvatarUrl = user.AvatarUrl,
                JwtToken = jwtToken,
                //RefreshToken = refreshToken
            };

            //_tokenService.AddToken(user.ID, jwtToken, refreshToken);

            return Ok(token);
        }

        //[HttpPost("refreshtoken")]
        //public ActionResult<RefreshToken> RefreshToken([FromBody] RefreshToken refreshToken)
        //{
        //    var token = _tokenService.CheckRefreshToken(refreshToken);

        //    if (token == null) return BadRequest(new { Message = "JwtToken or RefreshToken invalid!" });

        //    _tokenService.ExpiredToken(token);

        //    var user = _userRepository.GetUserByID(token.UserID);

        //    string jwtToken = _tokenService.GenerateJwtToken(user);
        //    var newRefreshToken = _tokenService.GenerateRefreshToken();

        //    _tokenService.AddToken(user.ID, jwtToken, newRefreshToken);

        //    return Ok(new RefreshToken() { Token = jwtToken, Refresh = newRefreshToken });
        //}
    }
}
