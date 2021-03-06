﻿using Newtonsoft.Json.Linq;
using System;

namespace Coldairarrow.Util
{
    public static class JWTHelper
    {
        private static readonly string _headerBase64Url = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}".Base64UrlEncode();
        public static readonly JwtOptions jwtOptions = ConfigHelper.GetValue<JwtOptions>("JwtOptions");

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="jWTPayload"></param>
        /// <returns></returns>
        public static string GetToken(this JWTPayload jWTPayload)
        {
            jWTPayload.Expire = DateTime.Now.AddHours(jwtOptions.AccessExpireHours);
            return GetToken(jWTPayload.ToJson());
        }
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="payloadJsonStr">数据JSON字符串</param>
        /// <returns></returns>
        public static string GetToken(string payloadJsonStr)
        {
            string payloadBase64Url = payloadJsonStr.Base64UrlEncode();
            string sign = $"{_headerBase64Url}.{payloadBase64Url}".ToHMACSHA256String(jwtOptions.Secret);

            return $"{_headerBase64Url}.{payloadBase64Url}.{sign}";
        }

        /// <summary>
        /// 刷新Token
        /// 切换项目--刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static string RefreshToken(this string token, string projectId = "")
        {
            if (CheckToken(token))
            {
                var payload = JWTHelper.GetPayload<JWTPayload>(token);
                payload.ProjectId = projectId.IsNullOrEmpty() ? payload.ProjectId : projectId;

                //验证当前时间是否在有效的刷新时间段内
                if (payload.Expire.AddHours(jwtOptions.RefreshExpireHours) >= DateTime.Now)
                {
                    return payload.GetToken();
                }
                throw new BusException("登陆超时，请重新登陆");
            }
            throw new BusException("token验证失败");
        }

        /// <summary>
        /// 获取Token中的数据
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static JObject GetPayload(string token)
        {
            return token.Split('.')[1].Base64UrlDecode().ToJObject();
        }

        /// <summary>
        /// 获取Token中的数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static T GetPayload<T>(string token)
        {
            if (token.IsNullOrEmpty())
                return default;

            return token.Split('.')[1].Base64UrlDecode().ToObject<T>();
        }

        /// <summary>
        /// 校验Token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static bool CheckToken(string token)
        {
            var items = token.Split('.');
            var oldSign = items[2];
            string newSign = $"{items[0]}.{items[1]}".ToHMACSHA256String(jwtOptions.Secret);

            return oldSign == newSign;
        }
    }

    public class JwtOptions
    {
        public string Secret { get; set; }
        public int AccessExpireHours { get; set; }
        public int RefreshExpireHours { get; set; }
    }
}
