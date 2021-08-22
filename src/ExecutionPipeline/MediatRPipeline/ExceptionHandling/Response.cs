using System;
using System.Net;
using Newtonsoft.Json;

namespace ExecutionPipeline.MediatRPipeline.ExceptionHandling
{
    /// <summary>
    /// Used for BLL and low layer results.
    /// </summary>
    public class Response
    {
        public bool IsSuccess { get; }
        
        public bool IsFailure => !IsSuccess;
        
        [JsonProperty] // Workaround the serializer.
        public string StackTrace { get; private set; }
        
        /// <summary>
        /// Set to string if there will be a need to define custom domain / interaction / data transfer specific response codes.
        /// Example would be gRPC and REST response codes.
        /// Augumentation can be done using some domain defined codes.
        /// </summary>
        [JsonProperty] // Workaround the serializer.
        public string ResponseCode { get; private set; }
        
        [Obsolete("Used only for serializers and mappers")]
        public Response()
        {
            //
        }
        protected Response(bool isSuccess, string stackTrace, string code)
        {
            this.IsSuccess = isSuccess;
            this.StackTrace = stackTrace;
            this.ResponseCode = code;
        }
        
        protected Response(bool isSuccess, string stackTrace, HttpStatusCode code)
        {
            this.IsSuccess = isSuccess;
            this.StackTrace = stackTrace;
            this.ResponseCode = code.ToString();
        }

        public static Response Fail(string message)
        {
            return new Response(false,message, HttpStatusCode.BadRequest);
        }
        
        public static Response Fail(string message, string code)
        {
            return new Response(false,message, code);
        }
        
        public static Response Fail(string message, HttpStatusCode code)
        {
            return new Response(false,message, code);
        }

        public static Response<T> Fail<T>(string message, string code)
        {
            return new Response<T>(default(T),false,message, code);
        }


        public static Response Ok()
        {
            return new Response(true,string.Empty, HttpStatusCode.OK);
        }

        public static Response Ok(string code)
        {
            return new Response(true, string.Empty, code);
        }
        
        public static Response<T> Ok<T>(T value)
        {
            return new Response<T>(value, true, string.Empty, HttpStatusCode.OK);
        }

        public static Response<T> Ok<T>(T value, string code)
        {
            return new Response<T>(value, true, string.Empty, code);
        }
        
        public static Response<T> Ok<T>(T value, HttpStatusCode code)
        {
            return new Response<T>(value, true, string.Empty, code);
        }

        public static Response Combine(params Response[] results)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                    return result;
            }

            return Ok();
        }
    }
    public class Response<T> : Response
    {
        private readonly T _value;


        public T Value
        {
            get
            {

                return _value;
            }
        }

        protected internal Response(T value, bool isSuccess, string stackTrace, string code) 
            : base(isSuccess, stackTrace, code)
        {
            _value = value;
        }
        
        protected internal Response(T value, bool isSuccess, string stackTrace, HttpStatusCode code) 
            : base(isSuccess, stackTrace, code)
        {
            _value = value;
        }
    }
}