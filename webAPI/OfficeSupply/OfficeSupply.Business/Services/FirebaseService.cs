using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using OfficeSupply.Business.IServices;
using OfficeSupply.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Services
{
    public class FirebaseService : IFirebaseService
    {
        public FirebaseService()
        {
            // create firebase app using service account
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("service-account-file.json")
            });
        }

        public string VerifyIdToken(AccessToken accessToken)
        {
            string uid = null;

            try
            {
                // verify idtoken, receive firebase token if verified
                var token = FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(accessToken.IdToken).Result;

                if (token != null)
                {
                    uid = token.Uid;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return uid;
        }

        public UserRecord GetUserInfo(string uid)
        {
            UserRecord userRecord = null;
            try
            {
                // get userinfo by uid
                userRecord = FirebaseAuth.DefaultInstance.GetUserAsync(uid).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return userRecord;
        }

        public void SendMessage(string regisToken, string title, string body)
        {
            var message = new Message()
            {
                Token = regisToken,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body,
                },
            };

            var result = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
        }
    }
}
